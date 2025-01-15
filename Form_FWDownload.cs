using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace VEXI
{


    public partial class Form_FWDownload : Form
    {
        #region variable
        //다운로드 구조체
        private struct TDownloadRec
        {
            public ConstClass.TDownloadMode DownloadMode;
            public uint totalSize;
            public Byte PgType;
            public Byte DwnType;
            public UInt32 DwPosition;
            public ushort DwIndex;
            public ushort OnepacketSize;
            public ushort ListOnepacketSize;
            public ushort FileCRC;
            public string dwFileName;
            public DateTime LastTxTime;
            public byte TxRepeatCount;

            public DateTime StartResTime;
        }

        public Form_Main form_Main;
        TDownloadRec dwnRec;
        //파일데이터를 넣는 리스트
        List<byte[]> DownloadList = new List<byte[]>();
        TPacketClass DwTxPacket = new TPacketClass();
        //다운로드 데이터를 넣는 임시버퍼로 Array.resize 를 통해 크기를 변경하며 사용한다
        byte[] DwnTxDatabuffer = new byte[6];
        #endregion


        public Form_FWDownload()
        {
            InitializeComponent();
        }

        private void Form_FWDownload_Load(object sender, EventArgs e)
        {
            if (this.IsMdiChild)
            {
                form_Main = (Form_Main)this.MdiParent;
            }
            else
            {
                form_Main = (Form_Main)this.Owner;
            }

            rbBoot.Visible = form_Main.IsAdmin;

        }

        #region 기능함수
        //다운로드 수신 데이터 처리 이벤트
        //현재의 다운로드 모드 기준으로 해당하는 수신 데이터 타입에 대해서만 처리하도록 한다
        //ACK 응답시 다음 처리, Nack 응답시 재전송
        //재전송 횟수를 초과하면 실패처리
        public void RxDownloadres(byte TmpCMD2, byte TmpSEQ, byte[] TmpRxDatas)
        {
            switch (dwnRec.DownloadMode)
            {
                case ConstClass.TDownloadMode.DWMode_Start:
                    //Result 1
                    //Reason 1
                    //TotalSize 4
                    //OnePacketLen 2
                    if (TmpCMD2 == ConstClass.CMD2_20)
                    {
                        if (TmpRxDatas[0] == ConstClass.CODE_ACK)
                        {
                            dwnRec.StartResTime = DateTime.Now;
                            addDownLoadDebug(String.Format("{0,-10} {1}", "[RX_ACK]", "Download Start"));

                            dwnRec.OnepacketSize = BitConverter.ToUInt16(TmpRxDatas, 6);
                            MakeDownloadList(false, dwnRec.dwFileName);
                            dwnRec.TxRepeatCount = 0;
                            dwnRec.DwPosition = 0;
                            //시작 응답 후 3초 있다가 데이터 보내는 거 시작하기. (이 동작은 삭제함) 
                            dwnRec.DownloadMode = ConstClass.TDownloadMode.DWMode_Data;
                            DownloadData(false, 0);
                        }
                        else
                        {
                            addDownLoadDebug(String.Format("{0,-10} {1}", "[RX_NACK]", "Download Start"));
                            if (dwnRec.TxRepeatCount > 3)
                            {
                                DownloadStop(ConstClass.TDownloadStopReason.reason_NackReason, TmpRxDatas[1]);
                            }
                            else
                            {
                                DownloadStart();
                            }
                        }
                    }
                    break;
                case ConstClass.TDownloadMode.DWMode_Data:
                    //Result 1
                    //Reason 1
                    //downloaded 4
                    if (TmpCMD2 == ConstClass.CMD2_21)
                    {
                        if (TmpRxDatas[0] == ConstClass.CODE_ACK)
                        {
                            addDownLoadDebug(" ");
                            addDownLoadDebug(String.Format("{0,-10} {1}", "[RX_ACK]", "Download Data"));

                            if (dwnRec.TxRepeatCount > 3)
                            {
                                DownloadStop(ConstClass.TDownloadStopReason.reason_ManyRepeat,0);
                            }
                            else
                            {
                                addDownLoadDebug(string.Format("ACK 응답수신 후 전송 : {0} {1}", TmpSEQ, BitConverter.ToUInt32(TmpRxDatas, 2)));
                                DownloadData(true, BitConverter.ToUInt32(TmpRxDatas, 2));
                            }
                        }
                        else
                        {
                            addDownLoadDebug(String.Format("{0,-10} {1}", "[RX_NACK]", "Download Data"));
                            if (dwnRec.TxRepeatCount > 4)
                            {
                                DownloadStop(ConstClass.TDownloadStopReason.reason_NackReason, TmpRxDatas[1]);
                            }
                            else
                            {
                                addDownLoadDebug(" ");
                                addDownLoadDebug(string.Format("NACK 응답수신 후 전송 : {0}", BitConverter.ToUInt32(TmpRxDatas, 2)));
                                DownloadData(false, BitConverter.ToUInt32(TmpRxDatas, 2));
                            }
                        }
                    }
                    break;
                case ConstClass.TDownloadMode.DWMode_End:
                    //Result 1
                    //Reason 1
                    //downloaded 4
                    //calc CRC 2
                    if (TmpCMD2 == ConstClass.CMD2_22)
                    {

                        if (TmpRxDatas[0] == ConstClass.CODE_ACK)
                        {
                            addDownLoadDebug(String.Format("{0,-10} {1}", "[RX_ACK]", "Download End"));
                            DownloadStop(ConstClass.TDownloadStopReason.reason_Success, 0);
                        }
                        else
                        {
                            addDownLoadDebug(String.Format("{0,-10} {1}", "[RX_NACK]", "Download End"));
                            if (dwnRec.TxRepeatCount > 2)
                            {
                                DownloadStop(ConstClass.TDownloadStopReason.reason_NackReason, TmpRxDatas[1]);
                            }
                            else
                            {
                                DownloadEnd();
                            }
                        }
                    }
                    break;
            }
        }

        //다운로드 시작 명령 송신 함수
        private void DownloadStart()
        {
            //PGType 1
            //DGType 1
            //FileSize 4
            byte[] intarray = new byte[4];

            Array.Resize(ref DwnTxDatabuffer, 6);
            DwnTxDatabuffer[0] = dwnRec.PgType;
            DwnTxDatabuffer[1] = dwnRec.DwnType;
            intarray = BitConverter.GetBytes(dwnRec.totalSize);
            Array.Copy(intarray, 0, DwnTxDatabuffer, 2, 4);

            dwnRec.TxRepeatCount++;
            dwnRec.LastTxTime = DateTime.Now;
            dwnRec.DownloadMode = ConstClass.TDownloadMode.DWMode_Start;
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_20, DwnTxDatabuffer);
            addDownLoadDebug(String.Format("{0,-10} {1}", "[TX]", "Download Start"));
            //Debug
            //string TmpStr = "";
            //for (int i = 0; i < DwnTxDatabuffer.Length; i++)
            //{
            //    TmpStr = TmpStr + string.Format(" {0:X2}", DwnTxDatabuffer[i]);
            //}
            //addDownLoadDebug("TX " + TmpStr);
        }

        //다운로드 데이터 송신 함수
        //장치로부터 이미 수신받은 데이터 크기를 넘긴다. 맨처음 시작시는 0 을 넘긴다. 
        private void DownloadData(bool AckReason, UInt32 downedcount)
        {
            //다운로드데이터 위치 4
            //다운로드하는 데이터 크기 2
            //데이터
            if (dwnRec.DwPosition == downedcount)
            {
                dwnRec.TxRepeatCount++;
            }
            else
            {
                dwnRec.TxRepeatCount = 1;
            }

            if (downedcount <= dwnRec.DwPosition)
            {
                if (dwnRec.DwPosition != 0)
                {
                    addDownLoadDebug("!!!!!!!!!!!!! " + String.Format("Done:{0} DwPosition:{1}", downedcount, dwnRec.DwPosition));
                }
            }

            if (AckReason)
            {
                if (downedcount == dwnRec.DwPosition)
                {
                    return;
                }
            }
            

            dwnRec.DwPosition = downedcount;
            

            if (dwnRec.DwPosition >= dwnRec.totalSize)
            {
                dwnRec.TxRepeatCount = 0;
                DownloadEnd();
            }
            else
            {
                //dwnRec.DwIndex = (ushort)((downedcount - 1) / (dwnRec.OnepacketSize) + 1);

                dwnRec.DwIndex = (ushort)((downedcount) / (dwnRec.OnepacketSize));

                ushort TmpCount = (ushort)DownloadList[dwnRec.DwIndex].Length;

                Array.Resize(ref DwnTxDatabuffer, 6 + TmpCount);

                byte[] intarray = new byte[4];
                intarray = BitConverter.GetBytes(dwnRec.DwPosition);
                Array.Copy(intarray, 0, DwnTxDatabuffer, 0, 4);
                intarray = BitConverter.GetBytes(TmpCount);
                Array.Copy(intarray, 0, DwnTxDatabuffer, 4, 2);
                Array.Copy(DownloadList[dwnRec.DwIndex], 0, DwnTxDatabuffer, 6, TmpCount);

                dwnRec.LastTxTime = DateTime.Now;
                dwnRec.DownloadMode = ConstClass.TDownloadMode.DWMode_Data;
                byte TmpTxSEQ = form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_21, DwnTxDatabuffer);

                addDownLoadDebug(String.Format("TXBuffer:{5} SEQ:{4} Done:{0} DwPosition:{1} DwIndex:{2} / {3}", downedcount, dwnRec.DwPosition, dwnRec.DwIndex + 1, DownloadList.Count, TmpTxSEQ, form_Main.COMMDataManager.UserDataCount));
                //addDownLoadDebug(String.Format("{0,-10} {1}     Addr : {2,-12} , Count : {3}", "[TX]", "Download Data", dwnRec.DwPosition, TmpCount));
                progressBar1.Value = dwnRec.DwIndex + 1;
                lblDownloadProgress.Text = string.Format("{0} / {1}", dwnRec.DwPosition, dwnRec.totalSize);
            }
        }

        //다운로드 완료 데이터 송신
        private void DownloadEnd()
        {
            //파일 CRC 2

            dwnRec.FileCRC = CalcFileCRC();

            Array.Resize(ref DwnTxDatabuffer, 2);
            DwnTxDatabuffer = BitConverter.GetBytes(dwnRec.FileCRC);
            Array.Reverse(DwnTxDatabuffer);

            dwnRec.TxRepeatCount++;
            dwnRec.LastTxTime = DateTime.Now;
            dwnRec.DownloadMode = ConstClass.TDownloadMode.DWMode_End;

            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_22, DwnTxDatabuffer);
            addDownLoadDebug(String.Format("{0,-10} {1}", "[TX]", "Download End"));

            progressBar1.Value = progressBar1.Maximum;
            lblDownloadProgress.Text = string.Format("{0} / {0}", dwnRec.totalSize);
        }

        //다운로드 중지, 실패
        private void DownloadStop(ConstClass.TDownloadStopReason TmpStopReason, byte NackReasonData)
        {
            switch (TmpStopReason)
            {
                case ConstClass.TDownloadStopReason.reason_Success:
                    addDownLoadDebug(String.Format("{0,-10} {1}", "[RESULT]", "Download Success"));
                    break;
                case ConstClass.TDownloadStopReason.reason_NoResponse:
                    addDownLoadDebug(String.Format("{0,-10} {1}", "[RESULT]", "Download Fail : NoResponse"));
                    break;
                case ConstClass.TDownloadStopReason.reason_NackReason:
                    switch (NackReasonData)
                    {
                        case 0x11: addDownLoadDebug(String.Format("{0,-10} {1}{2}", "[RESULT]", "Download Fail : NackReason : ", ConstClass.Nakc_0x11)); break;
                        case 0x12: addDownLoadDebug(String.Format("{0,-10} {1}{2}", "[RESULT]", "Download Fail : NackReason : ", ConstClass.Nakc_0x12)); break;
                        case 0x13: addDownLoadDebug(String.Format("{0,-10} {1}{2}", "[RESULT]", "Download Fail : NackReason : ", ConstClass.Nakc_0x13)); break;
                        case 0x14: addDownLoadDebug(String.Format("{0,-10} {1}{2}", "[RESULT]", "Download Fail : NackReason : ", ConstClass.Nakc_0x14)); break;
                        case 0x15: addDownLoadDebug(String.Format("{0,-10} {1}{2}", "[RESULT]", "Download Fail : NackReason : ", ConstClass.Nakc_0x15)); break;
                        case 0x17: addDownLoadDebug(String.Format("{0,-10} {1}{2}", "[RESULT]", "Download Fail : NackReason : ", ConstClass.Nakc_0x17)); break;
                        case 0x18: addDownLoadDebug(String.Format("{0,-10} {1}{2}", "[RESULT]", "Download Fail : NackReason : ", ConstClass.Nakc_0x18)); break;
                        case 0x19: addDownLoadDebug(String.Format("{0,-10} {1}{2}", "[RESULT]", "Download Fail : NackReason : ", ConstClass.Nakc_0x19)); break;
                        case 0x21: addDownLoadDebug(String.Format("{0,-10} {1}{2}", "[RESULT]", "Download Fail : NackReason : ", ConstClass.Nakc_0x21)); break;
                        case 0x22: addDownLoadDebug(String.Format("{0,-10} {1}{2}", "[RESULT]", "Download Fail : NackReason : ", ConstClass.Nakc_0x22)); break;
                        case 0x23: addDownLoadDebug(String.Format("{0,-10} {1}{2}", "[RESULT]", "Download Fail : NackReason : ", ConstClass.Nakc_0x23)); break;
                        case 0x24: addDownLoadDebug(String.Format("{0,-10} {1}{2}", "[RESULT]", "Download Fail : NackReason : ", ConstClass.Nakc_0x24)); break;
                        case 0x25: addDownLoadDebug(String.Format("{0,-10} {1}{2}", "[RESULT]", "Download Fail : NackReason : ", ConstClass.Nakc_0x25)); break;
                        case 0x31: addDownLoadDebug(String.Format("{0,-10} {1}{2}", "[RESULT]", "Download Fail : NackReason : ", ConstClass.Nakc_0x31)); break;
                        case 0x32: addDownLoadDebug(String.Format("{0,-10} {1}{2}", "[RESULT]", "Download Fail : NackReason : ", ConstClass.Nakc_0x32)); break;
                        case 0x33: addDownLoadDebug(String.Format("{0,-10} {1}{2}", "[RESULT]", "Download Fail : NackReason : ", ConstClass.Nakc_0x33)); break;
                        case 0x34: addDownLoadDebug(String.Format("{0,-10} {1}{2}", "[RESULT]", "Download Fail : NackReason : ", ConstClass.Nakc_0x34)); break;
                        case 0x35: addDownLoadDebug(String.Format("{0,-10} {1}{2}", "[RESULT]", "Download Fail : NackReason : ", ConstClass.Nakc_0x35)); break;
                        default: addDownLoadDebug(String.Format("{0,-10} {1}{2:X2}", "[RESULT]", "Download Fail : NackReason : ", NackReasonData)); break;
                    }
                    
                    break;
                case ConstClass.TDownloadStopReason.reason_ManyRepeat:
                    addDownLoadDebug(String.Format("{0,-10} {1}", "[RESULT]", "Download Fail : Many Retry"));
                    break;
                case ConstClass.TDownloadStopReason.reasn_UserStop:
                    addDownLoadDebug(String.Format("{0,-10} {1}", "[RESULT]", "Download Fail : UserStop"));
                    break;
            }
            dwnRec.DownloadMode = ConstClass.TDownloadMode.DWMode_None;
            form_Main.COMMDataManager.ISPollingEnable = true;
        }

        //다운로드파일 CRC 반환
        private ushort CalcFileCRC()
        {
            ushort Tmp = 0;
            ushort i;

            for (i = 0; i < DownloadList.Count; i++)
            {
                Tmp = Global_Class.UTIL_CheckSum(Tmp, DownloadList[i], 0, (ushort)DownloadList[i].Length);
            }

            return Tmp;
        }

        //다운로드 초기화
        private void InitDownRec(string tmpfileName)
        {
            InitDownRec();
            MakeDownloadList(true, tmpfileName);
        }

        //다운로드 구조체 초기화
        private void InitDownRec()
        {
            lb_Download.Items.Clear();
            dwnRec.DownloadMode = ConstClass.TDownloadMode.DWMode_None;
            dwnRec.LastTxTime = DateTime.Now;
            dwnRec.TxRepeatCount = 0;
            dwnRec.DwPosition = 0;
            dwnRec.DwIndex = 0;
            dwnRec.OnepacketSize = 1024;
            dwnRec.FileCRC = 0x000;
        }

        //다운로드 파일의 데이터로 다운로드리스트 구성
        //OnepacketSize 단위로 리스트에 넣는다
        private bool MakeDownloadList(bool Init, string tmpfileName)
        {

            dwnRec.dwFileName = tmpfileName;
            lblFileName.Text = System.IO.Path.GetFileName(dwnRec.dwFileName);

            if (Global_Class.UTIL_File_exists(dwnRec.dwFileName))
            {

                if (!Init)
                {
                    if (dwnRec.ListOnepacketSize == dwnRec.OnepacketSize)
                    {
                        return true;
                    }
                }

                byte[] readData;
                DownloadList.Clear();

                using (BinaryReader br = new BinaryReader(File.Open(dwnRec.dwFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        dwnRec.ListOnepacketSize = dwnRec.OnepacketSize;
                        dwnRec.totalSize = (uint)br.BaseStream.Length;
                        if (rbBoot.Checked)
                        {
                            dwnRec.PgType = 0x01;
                            dwnRec.DwnType = 0x02;
                        }
                        else if (rbApp.Checked)
                        {
                            dwnRec.PgType = 0x00;
                            dwnRec.DwnType = 0x00;
                        }
                        else
                        {
                            dwnRec.PgType = 0x00;
                            dwnRec.DwnType = 0x01;
                        }


                        while (br.BaseStream.Position < dwnRec.totalSize)
                        {
                            readData = br.ReadBytes(dwnRec.OnepacketSize);
                            DownloadList.Add(readData);

                        }
                    }
                    finally
                    {
                        br.Close();
                    }
                }

                progressBar1.Maximum = DownloadList.Count;
                progressBar1.Value = 0;
                lblDownloadProgress.Text = string.Format("0 / {0}", dwnRec.totalSize);

                return true;
            }
            else
            {

                lblFileName.Text = lblFileName.Text + " IS NOT EXIST";
                return false;
            }
        }

        
        //다운로드 진행 상황, 알림 등의 디버깅내용을 화면에 표출
        private void addDownLoadDebug(string TmpStr)
        {
            lb_Download.Items.Add(TmpStr);
            lb_Download.SelectedIndex = lb_Download.Items.Count - 1;
        }


        # endregion

        #region 컴포넌트 이벤트

        //다운로드 타이머
        //다운로드 중인지에 따라 버튼 활성화/비활성화
        //무응답 처리 - 다운로드 재전송은 Nack 수신시와 무응답시 2가지 경우 모두 이루어지는데 무응답을 타이머를 통해 송신 후 일정시간안에 응답이 없는지를 체크하여야 한다
        private void DWTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan ts;

            btnFileOpen.Enabled = (dwnRec.DownloadMode == ConstClass.TDownloadMode.DWMode_None);
            btnStratDownload.Enabled = (dwnRec.DownloadMode == ConstClass.TDownloadMode.DWMode_None);
            btnStopDownload.Enabled = !(dwnRec.DownloadMode == ConstClass.TDownloadMode.DWMode_None);
            DWTimer.Enabled = !(dwnRec.DownloadMode == ConstClass.TDownloadMode.DWMode_None);
            if (dwnRec.DownloadMode == ConstClass.TDownloadMode.DWMode_None)
            {
                form_Main.COMMDataManager.CommInterval = 50;
            } else
            {
                if (form_Main.COMMDataManager.COMM_Mode == ConstClass.COMM_UDP)
                {
                    form_Main.COMMDataManager.CommInterval = 30;
                } else
                {
                    form_Main.COMMDataManager.CommInterval = 50;
                }
            }


            //무응답 처리, 재전송 처리
            switch (dwnRec.DownloadMode)
            {
                case ConstClass.TDownloadMode.DWMode_Start:
                    if (DateTime.Compare(dwnRec.LastTxTime, DateTime.Now) > 0)
                    {
                        dwnRec.LastTxTime = DateTime.Now;
                    }
                    ts = DateTime.Now - dwnRec.LastTxTime;
                    if (ts.TotalMilliseconds > 5000)
                    {
                        if (dwnRec.TxRepeatCount > 3)
                        {
                            DownloadStop(ConstClass.TDownloadStopReason.reason_NoResponse, 0);
                        }
                        else
                        {
                            DownloadStart();
                        }
                    }

                    break;
                case ConstClass.TDownloadMode.DWMode_Data:
                    if (DateTime.Compare(dwnRec.LastTxTime, DateTime.Now) > 0)
                    {
                        dwnRec.LastTxTime = DateTime.Now;
                    }
                    ts = DateTime.Now - dwnRec.LastTxTime;
                    if (ts.TotalMilliseconds > 2000)
                    {
                        //ts = DateTime.Now - dwnRec.StartResTime;
                        //if (ts.TotalMilliseconds > 3000)
                        {
                            if (dwnRec.TxRepeatCount > 4)
                            {
                                DownloadStop(ConstClass.TDownloadStopReason.reason_NoResponse, 0);
                            }
                            else
                            {
                                addDownLoadDebug("Timer 전송");
                                DownloadData(false, dwnRec.DwPosition);
                            }
                        }
                    }

                    break;
                case ConstClass.TDownloadMode.DWMode_End:
                    if (DateTime.Compare(dwnRec.LastTxTime, DateTime.Now) > 0)
                    {
                        dwnRec.LastTxTime = DateTime.Now;
                    }
                    ts = DateTime.Now - dwnRec.LastTxTime;
                    if (ts.TotalMilliseconds > 2000)
                    {
                        if (dwnRec.TxRepeatCount > 2)
                        {
                            DownloadStop(ConstClass.TDownloadStopReason.reason_NoResponse, 0);
                        }
                        else
                        {
                            DownloadEnd();
                        }
                    }

                    break;
            }
        }

        private void btnStratDownload_Click(object sender, EventArgs e)
        {
            DWTimer.Enabled = true;
            if (MakeDownloadList(true, dwnRec.dwFileName))
            {
                form_Main.COMMDataManager.ISPollingEnable = false;
                InitDownRec();
                DownloadStart();

            }
            else
            {
                //
            }
        }

        private void btnFileOpen_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.bin|*.BIN";

            if (openFileDialog1.InitialDirectory == "")
            { 
                openFileDialog1.InitialDirectory = Application.StartupPath;
            } else
            {
                if (dwnRec.dwFileName == "")
                {
                    openFileDialog1.InitialDirectory = Application.StartupPath;
                } else
                {
                    openFileDialog1.InitialDirectory = dwnRec.dwFileName;
                }
            }
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                InitDownRec(openFileDialog1.FileName);
            }
        }

        private void btnStopDownload_Click(object sender, EventArgs e)
        {
            DownloadStop(ConstClass.TDownloadStopReason.reasn_UserStop, 0);
        }

        private void Form_FWDownload_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dwnRec.DownloadMode != ConstClass.TDownloadMode.DWMode_None)
            {
                form_Main.GlobalObj.MsgBox_Confirm_OK("F/W 다운로드중입니다");
                e.Cancel = true;
            }
        }

        #endregion

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
