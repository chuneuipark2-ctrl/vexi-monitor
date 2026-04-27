using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;   //DllImport
using System.Windows.Forms;

namespace VEXI
{
    public partial class Form_EMSTotal : Form
    {

        public Form_Main form_Main;
        public bool Want_Data = false;
        private int ProcessStep;
        private int ProcessNextStep;
        private DateTime ProcessStepTime;
        private byte RetryCount;
        private byte DEVMode_Prev;

        public static VEXI_DEFS.TDEV_CtrlRes_2Byte CTRLRES_2byte;

        private static VEXI_DEFS.TEMS_DevConfigRes ems_REC_EMSConfig_RES;
        private static VEXI_DEFS.TEMS_DevConfigCtrl ems_REC_EMSConfigCtrl;
        private static VEXI_DEFS.EMS_IOConfig ems_REC_DEV_IOConfig_RES;
        private static VEXI_DEFS.EMS_IOConfig ems_REC_DEV_IOConfigCtrl;
        private static VEXI_DEFS.TEMS_CTRLParamRes ems_CtrlParam_RES;
        private static VEXI_DEFS.TEMS_CTRLParamCTRL ems_CtrlParam_CTRL;
        private static VEXI_DEFS.TEMS_DriveParamRes ems_DriveParam_RES;
        private static VEXI_DEFS.TEMS_DriveParamCTRL ems_DriveParam_CTRL;
        private static VEXI_DEFS.TEMS_LiftParamRes ems_LiftParam_RES;
        private static VEXI_DEFS.TEMS_LiftParamCTRL ems_LiftParam_CTRL;

        private static VEXI_DEFS.TEMS_StationParam ems_StationParam;
        private static VEXI_DEFS.TEMS_PositionSetParam ems_PositionParam;
        private static VEXI_DEFS.TEMS_SpeedAreaGroupParam ems_SpeedAreaGroupParam;

        private static VEXI_DEFS.TEMS_PositionSetCTRLRes ems_PositionSet_CtrlRes;
        private static VEXI_DEFS.TEMS_SpeedAreaGroupCTRLRes ems_SpeedAreaGroupSet_CtrlRes;
        private static VEXI_DEFS.TEMS_StationParamCTRLRes ems_StationParam_CTRLRes;


        private static Label[] lbl_DownProgress;
        private static Label[] lbl_DateTime;
        private static CheckBox[] cb_Set;

        private static Label[] lbl_UploadProgress;
        private static Label[] lbl_SaveProgress;
        private static CheckBox[] cb_Load;

        public Form_EMSTotal()
        {
            InitializeComponent();

            lbl_DateTime = new Label[] { null, lblSaveTime1, lblSaveTime2, lblSaveTime3, lblSaveTime4, lblSaveTime5, null, lblSaveTime7, lblSaveTime8, lblSaveTime9};
            lbl_DownProgress = new Label[] { null, lblDownResult1, lblDownResult2, lblDownResult3, lblDownResult4, lblDownResult5, null, lblDownResult7, lblDownResult8, lblDownResult9};
            lbl_UploadProgress = new Label[] { null, lblUploadResult1, lblUploadResult2, lblUploadResult3, lblUploadResult4, lblUploadResult5, null, lblUploadResult7, lblUploadResult8, lblUploadResult9 };
            lbl_SaveProgress = new Label[] { null, lblSaveResult1, lblSaveResult2, lblSaveResult3, lblSaveResult4, lblSaveResult5, null, lblSaveResult7, lblSaveResult8, lblSaveResult9 };
            cb_Set = new CheckBox[] { null, cbSet1, cbSet2, cbSet3, cbSet4, cbSet5, null, cbSet7, cbSet8, cbSet9 };
            cb_Load = new CheckBox[] { null, cbLoad1, cbLoad2, cbLoad3, cbLoad4, cbLoad5, null, cbLoad7, cbLoad8, cbLoad9 };
        }

        #region 컴포넌트 이벤트
        private void Form_EMSTotal_Load(object sender, EventArgs e)
        {
            if (this.IsMdiChild)
            {
                form_Main = (Form_Main)this.MdiParent;
            }
            else
            {
                form_Main = (Form_Main)this.Owner;
            }
        }
        #endregion

        #region 기능함수
        private void Display_DownHeader(bool Isinit)
        {
            if (Isinit)
            {
                for (byte i = 1; i < 10; i++)
                {
                    if (lbl_DateTime[i] != null) lbl_DateTime[i].Text = "----";
                    if (lbl_DownProgress[i] != null) lbl_DownProgress[i].Text = "----";
                }
            }

            if (cb_Set[1] != null) cb_Set[1].Enabled = (form_Main.EMS_ToTalFile.myHeader.MCU_CFG_Flag.Save_Flag == 1);
            if (cb_Set[2] != null) cb_Set[2].Enabled = (form_Main.EMS_ToTalFile.myHeader.IO_CFG_Flag.Save_Flag == 1);
            if (cb_Set[3] != null) cb_Set[3].Enabled = (form_Main.EMS_ToTalFile.myHeader.CTRL_PARAM_Flag.Save_Flag == 1);
            if (cb_Set[4] != null) cb_Set[4].Enabled = (form_Main.EMS_ToTalFile.myHeader.DRIVE_PARAM_Flag.Save_Flag == 1);
            if (cb_Set[5] != null) cb_Set[5].Enabled = (form_Main.EMS_ToTalFile.myHeader.LIFT_PARAM_Flag.Save_Flag == 1);
            //if (cb_Set[6] != null) cb_Set[6].Enabled = (form_Main.EMS_ToTalFile.myHeader.RACK_CFG_Flag_Reserved.Save_Flag == 1);
            if (cb_Set[7] != null) cb_Set[7].Enabled = (form_Main.EMS_ToTalFile.myHeader.STATION_CFG_Flag.Save_Flag == 1);
            if (cb_Set[8] != null) cb_Set[8].Enabled = (form_Main.EMS_ToTalFile.myHeader.POSITION_CFG_Flag.Save_Flag == 1);
            if (cb_Set[9] != null) cb_Set[9].Enabled = (form_Main.EMS_ToTalFile.myHeader.AREASPEED_CFG_Flag.Save_Flag == 1);
            if (Isinit)
            {
                for (byte i = 1; i < 10; i++)
                {
                    if (cb_Set[i] != null)
                    {
                        if (!cb_Set[i].Enabled)
                        {
                            cb_Set[i].Checked = false;
                        }

                    }
                }
            }

            if (Isinit)
            {
                if (form_Main.EMS_ToTalFile.myHeader.MCU_CFG_Flag.Save_Flag == 1)
                {
                    DateTime TmpDateTime = new DateTime(form_Main.EMS_ToTalFile.myHeader.MCU_CFG_Flag.Save_Update);
                    if (lbl_DateTime[1] != null) lbl_DateTime[1].Text = TmpDateTime.ToString();
                    
                }
                if (form_Main.EMS_ToTalFile.myHeader.IO_CFG_Flag.Save_Flag == 1)
                {
                    DateTime TmpDateTime = new DateTime(form_Main.EMS_ToTalFile.myHeader.IO_CFG_Flag.Save_Update);
                    if (lbl_DateTime[2] != null) lbl_DateTime[2].Text = TmpDateTime.ToString();
                    
                }
                if (form_Main.EMS_ToTalFile.myHeader.CTRL_PARAM_Flag.Save_Flag == 1)
                {
                    DateTime TmpDateTime = new DateTime(form_Main.EMS_ToTalFile.myHeader.CTRL_PARAM_Flag.Save_Update);
                    if (lbl_DateTime[3] != null) lbl_DateTime[3].Text = TmpDateTime.ToString();
                    
                }
                if (form_Main.EMS_ToTalFile.myHeader.DRIVE_PARAM_Flag.Save_Flag == 1)
                {
                    DateTime TmpDateTime = new DateTime(form_Main.EMS_ToTalFile.myHeader.DRIVE_PARAM_Flag.Save_Update);
                    if (lbl_DateTime[4] != null) lbl_DateTime[4].Text = TmpDateTime.ToString();
                    
                }
                if (form_Main.EMS_ToTalFile.myHeader.LIFT_PARAM_Flag.Save_Flag == 1)
                {
                    DateTime TmpDateTime = new DateTime(form_Main.EMS_ToTalFile.myHeader.LIFT_PARAM_Flag.Save_Update);
                    if (lbl_DateTime[5] != null) lbl_DateTime[5].Text = TmpDateTime.ToString();
                    
                }
                //if (form_Main.EMS_ToTalFile.myHeader.RACK_CFG_Flag_Reserved.Save_Flag == 1)
                //{
                //    DateTime TmpDateTime = new DateTime(form_Main.EMS_ToTalFile.myHeader.RACK_CFG_Flag_Reserved.Save_Update);
                //    if (lbl_DateTime[6] != null) lbl_DateTime[6].Text = TmpDateTime.ToString();
                    
                //}
                if (form_Main.EMS_ToTalFile.myHeader.STATION_CFG_Flag.Save_Flag == 1)
                {
                    DateTime TmpDateTime = new DateTime(form_Main.EMS_ToTalFile.myHeader.STATION_CFG_Flag.Save_Update);
                    if (lbl_DateTime[7] != null) lbl_DateTime[7].Text = TmpDateTime.ToString();
                }
                if (form_Main.EMS_ToTalFile.myHeader.POSITION_CFG_Flag.Save_Flag == 1)
                {
                    DateTime TmpDateTime = new DateTime(form_Main.EMS_ToTalFile.myHeader.POSITION_CFG_Flag.Save_Update);
                    if (lbl_DateTime[8] != null) lbl_DateTime[8].Text = TmpDateTime.ToString();
                }
                if (form_Main.EMS_ToTalFile.myHeader.AREASPEED_CFG_Flag.Save_Flag == 1)
                {
                    DateTime TmpDateTime = new DateTime(form_Main.EMS_ToTalFile.myHeader.AREASPEED_CFG_Flag.Save_Update);
                    if (lbl_DateTime[9] != null) lbl_DateTime[9].Text = TmpDateTime.ToString();
                }
            }

        }

        private void Display_UploadEnable(bool TmpEnable)
        {
            for (byte i = 1; i < 10; i++)
            {
                if (cb_Load[i] != null)
                {
                    cb_Load[i].Enabled = TmpEnable;
                }

                if (!TmpEnable)
                {
                    if (lbl_UploadProgress[i] != null) lbl_UploadProgress[i].Text = "---";
                    if (lbl_SaveProgress[i] != null) lbl_SaveProgress[i].Text = "---";
                }
            }

            btn_UploadSave.Enabled = TmpEnable;
            cbLoadAll.Enabled = TmpEnable;
            pnDownload.Enabled = TmpEnable;
        }

        private void Display_DownloadEnable(bool TmpEnable)
        {
            if (!TmpEnable)
            {
                for (byte i = 1; i < 10; i++)
                {
                    if ((cb_Set[i] != null) && (lbl_DownProgress[i] != null))
                    {
                        if (cb_Set[i].Enabled && cb_Set[i].Checked)
                        {
                            lbl_DownProgress[i].Text = "진행대기";
                        }
                        else
                        {
                            lbl_DownProgress[i].Text = "----";
                        }

                        cb_Set[i].Enabled = TmpEnable;
                    }
                }
            }
            else
            {

            }

            btn_Download.Enabled = TmpEnable;
            btn_LoadTotalFile.Enabled = TmpEnable;
            cbSetAll.Enabled = TmpEnable;
            pnUpload.Enabled = TmpEnable;

        }

        private unsafe void Ctrl_EMS_CFG()
        {
            form_Main.EMS_ToTalFile.FileName = openFileDialog1.FileName;
            if (form_Main.EMS_ToTalFile.Read_MCU_CFG(ref ems_REC_EMSConfigCtrl))
            {
                //장치구조 => CtrlFlag[0] 의 7bit 가 서면 장치쪽에서 데이터 영역 통짜로 Copy 함 (Reserved에 쓰레기값 들어 있어서 Verify 통과 못하는 문제 해결)
                ems_REC_EMSConfigCtrl.CtrlFlag[0] = (byte)(ems_REC_EMSConfigCtrl.CtrlFlag[0] | 0x80);
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_26, ems_REC_EMSConfigCtrl);
                Want_Data = true;
            }
        }

        private void Ctrl_IO_CFG()
        {
            form_Main.EMS_ToTalFile.FileName = openFileDialog1.FileName;
            if (form_Main.EMS_ToTalFile.Read_IO_CFG(ref ems_REC_DEV_IOConfigCtrl))
            {
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_24, ems_REC_DEV_IOConfigCtrl);
                Want_Data = true;
            }
        }

        private unsafe void Ctrl_CTRL_PARAM_CFG()
        {
            form_Main.EMS_ToTalFile.FileName = openFileDialog1.FileName;
            if (form_Main.EMS_ToTalFile.Read_EMS_CTRL_PARAM(ref ems_CtrlParam_CTRL))
            {
                //제어파라미터 => CtrlFlag[0] 의 7bit 가 서면 장치쪽에서 데이터 영역 통짜로 Copy 함 (Reserved에 쓰레기값 들어 있어서 Verify 통과 못하는 문제 해결)
                ems_CtrlParam_CTRL.CtrlFlag[0] = (byte)(ems_CtrlParam_CTRL.CtrlFlag[0] | 0x80);
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A2, ems_CtrlParam_CTRL);
                Want_Data = true;
            }
        }


        private unsafe void Ctrl_DRIVE_PARAM_CFG()
        {
            form_Main.EMS_ToTalFile.FileName = openFileDialog1.FileName;
            if (form_Main.EMS_ToTalFile.Read_EMS_DRIVE_PARAM(ref ems_DriveParam_CTRL))
            {
                //주행드라이브 => CtrlFlag[0] 의 7bit 가 서면 장치쪽에서 데이터 영역 통짜로 Copy 함 (Reserved에 쓰레기값 들어 있어서 Verify 통과 못하는 문제 해결)
                ems_DriveParam_CTRL.CtrlFlag[0] = (byte)(ems_DriveParam_CTRL.CtrlFlag[0] | 0x80);
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A4, ems_DriveParam_CTRL);
                Want_Data = true;
            }
        }
        private unsafe void Ctrl_LIFT_PARAM_CFG()
        {
            form_Main.EMS_ToTalFile.FileName = openFileDialog1.FileName;
            if (form_Main.EMS_ToTalFile.Read_EMS_LIFT_PARAM(ref ems_LiftParam_CTRL))
            {
                //리프트드라이브 => CtrlFlag[0] 의 7bit 가 서면 장치쪽에서 데이터 영역 통짜로 Copy 함 (Reserved에 쓰레기값 들어 있어서 Verify 통과 못하는 문제 해결)
                ems_LiftParam_CTRL.CtrlFlag[0] = (byte)(ems_LiftParam_CTRL.CtrlFlag[0] | 0x80);
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A6, ems_LiftParam_CTRL);
                Want_Data = true;
            }
        }

        private unsafe void Ctrl_Position_CFG()
        {
            form_Main.EMS_ToTalFile.FileName = openFileDialog1.FileName;
            if (form_Main.EMS_ToTalFile.Read_EMS_PositionParam(ref ems_PositionParam))
            {
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_95, ems_PositionParam);
                form_Main.COMMDataManager.ISPolingDelayStop = true;
                form_Main.COMMDataManager.PolingDelayTxTime = DateTime.Now;
                Want_Data = true;
            }
        }

        private void Ctrl_Station_CFG()
        {
            form_Main.EMS_ToTalFile.FileName = openFileDialog1.FileName;
            if (form_Main.EMS_ToTalFile.Read_EMS_StationParam(ref ems_StationParam))
            {
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_99, ems_StationParam);
                form_Main.COMMDataManager.ISPolingDelayStop = true;
                form_Main.COMMDataManager.PolingDelayTxTime = DateTime.Now;
                Want_Data = true;
            }
        }

        private unsafe void Ctrl_AreaSpeed_CFG()
        {
            form_Main.EMS_ToTalFile.FileName = openFileDialog1.FileName;
            if (form_Main.EMS_ToTalFile.Read_EMS_SpeedAreaParam(ref ems_SpeedAreaGroupParam))
            {

                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_9B, ems_SpeedAreaGroupParam);
                form_Main.COMMDataManager.ISPolingDelayStop = true;
                form_Main.COMMDataManager.PolingDelayTxTime = DateTime.Now;
                Want_Data = true;
            }
        }

        public void Response_MCU_CFGCtrl(byte[] Data)
        {
            if (Data[0] == 0x00)
            {
                ProcessStep = 15;
            }
            else
            {
                ProcessStep = 14;
            }
        }

        public void Response_IO_CFG(byte[] Data)
        {
            if (Data[0] == 0x00)
            {
                ProcessStep = 25;
            }
            else
            {
                ProcessStep = 24;
            }
        }

        public void Response_CTRL_PARAM_CFG(byte[] Data)
        {
            if (Data[0] == 0x00)
            {
                ProcessStep = 35;
            }
            else
            {
                ProcessStep = 34;
            }
        }


        public void Response_DRIVE_PARAM_CFG(byte[] Data)
        {
            if (Data[0] == 0x00)
            {
                ProcessStep = 45;
            }
            else
            {
                ProcessStep = 44;
            }
        }

        public void Response_LIFT_PARAM_CFG(byte[] Data)
        {
            if (Data[0] == 0x00)
            {
                ProcessStep = 55;
            }
            else
            {
                ProcessStep = 54;
            }
        }


        public void Response_Position_CFG(byte[] Data)
        {
            ems_PositionSet_CtrlRes = (VEXI_DEFS.TEMS_PositionSetCTRLRes)Global_Class.UTIL_BytesToStructure(Data, typeof(VEXI_DEFS.TEMS_PositionSetCTRLRes));

            if (ems_PositionSet_CtrlRes.CtrlResult == ConstClass.CODE_ACK)
            
            {
                RetryCount = 0;
                ProcessStep = 75;
            }
            else
            {
                ProcessStep = 74;
            }
        }

        public void Response_Station_CFG(byte[] Data)
        {
            ems_StationParam_CTRLRes = (VEXI_DEFS.TEMS_StationParamCTRLRes)Global_Class.UTIL_BytesToStructure(Data, typeof(VEXI_DEFS.TEMS_StationParamCTRLRes));

            if (ems_StationParam_CTRLRes.CtrlResult == ConstClass.CODE_ACK)
            {
                ProcessStep = 85;
            }
            else
            {
                ProcessStep = 84;
            }
        }

        public void Response_SpeedArea_CFG(byte[] Data)
        {
            ems_SpeedAreaGroupSet_CtrlRes = (VEXI_DEFS.TEMS_SpeedAreaGroupCTRLRes)Global_Class.UTIL_BytesToStructure(Data, typeof(VEXI_DEFS.TEMS_SpeedAreaGroupCTRLRes));

            if (ems_SpeedAreaGroupSet_CtrlRes.CtrlResult == ConstClass.CODE_ACK)
            {
                RetryCount = 0;
                ProcessStep = 95;
            }
            else
            {
                ProcessStep = 94;
            }
        }

        #endregion

        private void btn_LoadTotalFile4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.EMScfg|*.EMSCFG";

            if (openFileDialog1.FileName == "")
            {
                openFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                form_Main.EMS_ToTalFile.FileName = openFileDialog1.FileName;
                form_Main.EMS_ToTalFile.Read_Header(ConstClass.TYPE_EMS);
                Display_DownHeader(true);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool TmpBool = false;


            if (GetNextDownloadIndex(0) != -1) TmpBool = true;

            if (TmpBool)
            {
                if (!form_Main.COMMDataManager.ISCOMM_ResponsGood)
                {
                    MessageBox.Show("통신 연결을 확인하세요");
                    return;
                }

                DEVMode_Prev = form_Main.COMMDataManager.DevRec.ems_REC_EMSSt.DevMode;

                RetryCount = 0;
                ProcessStep = 1;
                ProcessNextStep = GetNextDownloadIndex(0);
                ProcessStepTime = DateTime.Now;
                Display_DownloadEnable(false);
                DownloadTimer.Enabled = true;

            }
            else
            {
                MessageBox.Show("다운로드에 포함 시킬 항목을 체크 해주세요");
            }
        }
        private void Do_Ctrl_DevMode(byte TmpCMD2, byte CtrlData)
        {
            switch (TmpCMD2)
            {
                case ConstClass.CMD2_58:
                    //현재의 장치의 모드값을 제어 구조체에 반영하고 나서 제어값을 만들어야 한다.
                    byte[] ctrlValue = { 0, 0 };

                    if (Global_Class.BitStatus(form_Main.COMMDataManager.DevRec.ems_REC_EMSSt.DevMode, 0)) ctrlValue[0] = 2;
                    else if (Global_Class.BitStatus(form_Main.COMMDataManager.DevRec.ems_REC_EMSSt.DevMode, 1)) ctrlValue[0] = 0;
                    else if (Global_Class.BitStatus(form_Main.COMMDataManager.DevRec.ems_REC_EMSSt.DevMode, 3)) ctrlValue[0] = 1;
                    if (Global_Class.BitStatus(form_Main.COMMDataManager.DevRec.ems_REC_EMSSt.DevMode, 2)) ctrlValue[1] |= 0x01;


                    switch (CtrlData)
                    {
                        case 0: //수동모드
                            ctrlValue[0] = 0;
                            break;
                        case 1: //셋업모드
                            ctrlValue[0] = 1;
                            break;
                        case 2: //자동모드
                            ctrlValue[0] = 2;
                            break;
                        case 10: //강제모드 OFF
                            ctrlValue[1] &= 0x00;
                            break;
                        case 11: //강제모드 ON
                            ctrlValue[1] |= 0x01;
                            break;
                    }
                    form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, TmpCMD2, ctrlValue);
                    break;
            }
        }

        private int GetNextDownloadIndex(int TmpNowIndex)
        {
            for (int i = TmpNowIndex + 1; i < 10; i++)
            {
                if (cb_Set[i] != null)
                {
                    if (cb_Set[i].Checked)
                    {
                        return (i * 10);
                    }
                }
            }

            return -1;
        }

        private int GetNextUploadIndex(int TmpNowIndex)
        {
            for (int i = TmpNowIndex + 1; i < 10; i++)
            {
                if (cb_Load[i] != null)
                {
                    if (cb_Load[i].Checked)
                    {
                        return (i * 10);
                    }
                }
            }

            return -1;
        }

        private void DownloadTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan ts;
            int ProcessStep_Simple = ProcessStep / 10;

            if (DateTime.Compare(ProcessStepTime, DateTime.Now) > 0)
            {
                ProcessStepTime = DateTime.Now;
            }

            switch (ProcessStep)
            {
                case 1:
                    if (ProcessNextStep == -1)
                    {
                        ProcessStep = 100;
                    }
                    else
                    {
                        if (RetryCount == 0)
                        {
                            ProcessStepTime = DateTime.Now;
                            form_Main.COMMDataManager.DevRec.Flag_In_DevStatus = false;
                        }
                        RetryCount = 1;
                        if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus)
                        {
                            Do_Ctrl_DevMode(ConstClass.CMD2_58, 1);
                            ProcessStepTime = DateTime.Now;
                            form_Main.COMMDataManager.DevRec.Flag_In_DevStatus = false;
                            ProcessStep = 2;
                        }
                        else
                        {
                            
                            ts = DateTime.Now - ProcessStepTime;
                            if (ts.TotalMilliseconds > 20000)
                            {
                                ProcessStep = 1;
                                ProcessNextStep = -1;
                            }
                        }
                    }
                    break;
                case 2:
                    if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus)
                    {
                        if ((form_Main.COMMDataManager.DevRec.ems_REC_EMSSt.DevMode & 0x08) == 0)
                        {
                            if (RetryCount <= 3)
                            {
                                ts = DateTime.Now - ProcessStepTime;
                                if (ts.TotalMilliseconds > 1000)
                                {
                                    RetryCount++;
                                    Do_Ctrl_DevMode(ConstClass.CMD2_58, 1);
                                    ProcessStepTime = DateTime.Now;
                                }
                            }
                            else
                            {
                                ts = DateTime.Now - ProcessStepTime;
                                if (ts.TotalMilliseconds > 3000)
                                {
                                    ProcessStep = 1;
                                    ProcessNextStep = -1;
                                }
                            }
                        }
                        else
                        {
                            RetryCount = 0;
                            ProcessStep = ProcessNextStep;
                        }
                    }
                    else
                    {
                        ts = DateTime.Now - ProcessStepTime;
                        if (ts.TotalMilliseconds > 20000)
                        {
                            ProcessStep = 1;
                            ProcessNextStep = -1;
                        }
                    }
                    break;
                case 10:
                    if (cb_Set[ProcessStep_Simple] != null)
                    {
                        if (cb_Set[ProcessStep_Simple].Checked)
                        {
                            RetryCount++;
                            Ctrl_EMS_CFG();
                            if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 진행";
                            ProcessStepTime = DateTime.Now;
                            ProcessStep = ProcessStep_Simple * 10 + 1;
                        }
                        else
                        {
                            RetryCount = 0;
                            ProcessStep = 1;
                            ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                        }
                    } else
                    {
                        RetryCount = 0;
                        ProcessStep = 1;
                        ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    }
                    break;
                case 11:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 2000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 3;
                    }
                    break;
                case 13:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "무응답";
                    if (RetryCount >= 2)
                    {
                        RetryCount = 0;
                        ProcessStep = 1;
                        ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    }
                    else
                    {
                        ProcessStep = ProcessStep_Simple * 10;
                    }
                    break;
                case 14:

                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 NACK";
                    RetryCount = 0;
                    ProcessStep = 1;
                    ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    break;
                case 15:
                    RetryCount = 0;
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 ACK";
                    RetryCount = 0;
                    ProcessStep = 1;
                    ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    break;
                case 20:
                    if (cb_Set[ProcessStep_Simple] != null)
                    {

                        if (cb_Set[ProcessStep_Simple].Checked)
                        {
                            RetryCount++;
                            Ctrl_IO_CFG();
                            if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 진행";
                            ProcessStepTime = DateTime.Now;
                            ProcessStep = ProcessStep_Simple * 10 + 1;
                        }
                        else
                        {
                            RetryCount = 0;
                            ProcessStep = 1;
                            ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                        }
                    } else
                    {
                        RetryCount = 0;
                        ProcessStep = 1;
                        ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    }
                    break;
                case 21:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 1000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 3;
                    }
                    break;
                case 23:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "무응답";
                    if (RetryCount >= 2)
                    {
                        RetryCount = 0;
                        ProcessStep = 1;
                        ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    }
                    else
                    {
                        ProcessStep = ProcessStep_Simple * 10;
                    }
                    break;
                case 24:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 NACK";
                    RetryCount = 0;
                    ProcessStep = 1;
                    ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    break;
                case 25:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 ACK";
                    RetryCount = 0;
                    ProcessStep = 1;
                    ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    break;
                case 30:
                    if (cb_Set[ProcessStep_Simple] != null)
                    {

                        if (cb_Set[ProcessStep_Simple].Checked)
                        {
                            RetryCount++;
                            Ctrl_CTRL_PARAM_CFG();
                            if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 진행";
                            ProcessStepTime = DateTime.Now;
                            ProcessStep = ProcessStep_Simple * 10 + 1;

                        }
                        else
                        {
                            RetryCount = 0;
                            ProcessStep = 1;
                            ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                        }
                    } else
                    {
                        RetryCount = 0;
                        ProcessStep = 1;
                        ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    }
                    break;
                case 31:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 1000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 3;
                    }
                    break;
                case 33:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "무응답";
                    if (RetryCount >= 2)
                    {
                        RetryCount = 0;
                        ProcessStep = 1;
                        ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    }
                    else
                    {
                        ProcessStep = ProcessStep_Simple * 10;
                    }
                    break;
                case 34:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 NACK";
                    RetryCount = 0;
                    ProcessStep = 1;
                    ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    break;
                case 35:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 ACK";
                    RetryCount = 0;
                    ProcessStep = 1;
                    ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    break;
                case 40:
                    if (cb_Set[ProcessStep_Simple] != null)
                    {

                        if (cb_Set[ProcessStep_Simple].Checked)
                        {
                            RetryCount++;
                            Ctrl_DRIVE_PARAM_CFG();
                            if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 진행";
                            ProcessStepTime = DateTime.Now;
                            ProcessStep = ProcessStep_Simple * 10 + 1;

                        }
                        else
                        {
                            RetryCount = 0;
                            ProcessStep = 1;
                            ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                        }
                    }
                    else
                    {
                        RetryCount = 0;
                        ProcessStep = 1;
                        ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    }
                    break;
                case 41:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 1000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 3;
                    }
                    break;
                case 43:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "무응답";
                    if (RetryCount >= 2)
                    {
                        RetryCount = 0;
                        ProcessStep = 1;
                        ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    }
                    else
                    {
                        ProcessStep = ProcessStep_Simple * 10;
                    }
                    break;
                case 44:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 NACK";
                    RetryCount = 0;
                    ProcessStep = 1;
                    ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    break;
                case 45:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 ACK";
                    RetryCount = 0;
                    ProcessStep = 1;
                    ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    break;
                case 50:
                    if (cb_Set[ProcessStep_Simple] != null)
                    {

                        if (cb_Set[ProcessStep_Simple].Checked)
                        {
                            RetryCount++;
                            Ctrl_LIFT_PARAM_CFG();
                            if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 진행";
                            ProcessStepTime = DateTime.Now;
                            ProcessStep = ProcessStep_Simple * 10 + 1;

                        }
                        else
                        {
                            RetryCount = 0;
                            ProcessStep = 1;
                            ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                        }
                    } else
                    {
                        RetryCount = 0;
                        ProcessStep = 1;
                        ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    }
                    break;
                case 51:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 1000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 3;
                    }
                    break;
                case 53:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "무응답";
                    if (RetryCount >= 2)
                    {
                        RetryCount = 0;
                        ProcessStep = 1;
                        ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    }
                    else
                    {
                        ProcessStep = ProcessStep_Simple * 10;
                    }
                    break;
                case 54:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 NACK";
                    RetryCount = 0;
                    ProcessStep = 1;
                    ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    break;
                case 55:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 ACK";
                    RetryCount = 0;
                    ProcessStep = 1;
                    ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    break;
                case 60:
                    RetryCount = 0;
                    ProcessStep = 1;
                    ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    break;
                case 70:
                    if (cb_Set[ProcessStep_Simple] != null)
                    {

                        if (cb_Set[ProcessStep_Simple].Checked)
                        {
                            RetryCount++;
                            Ctrl_Position_CFG();
                            if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 진행";
                            ProcessStepTime = DateTime.Now;
                            ProcessStep = ProcessStep_Simple * 10 + 1;

                        }
                        else
                        {
                            RetryCount = 0;
                            ProcessStep = 1;
                            ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                        }
                    }
                    else
                    {
                        RetryCount = 0;
                        ProcessStep = 1;
                        ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    }
                    break;
                case 71:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 1000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 3;
                    }
                    break;
                case 73:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "무응답";
                    if (RetryCount >= 2)
                    {
                        RetryCount = 0;
                        ProcessStep = 1;
                        ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    }
                    else
                    {
                        ProcessStep = ProcessStep_Simple * 10;
                    }
                    break;
                case 74:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 NACK";
                    RetryCount = 0;
                    ProcessStep = 1;
                    ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    break;
                case 75:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 ACK";
                    RetryCount = 0;
                    ProcessStep = 1;
                    ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    break;

                case 80:
                    if (cb_Set[ProcessStep_Simple] != null)
                    {

                        if (cb_Set[ProcessStep_Simple].Checked)
                        {
                            RetryCount++;
                            Ctrl_Station_CFG();
                            if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 진행";
                            ProcessStepTime = DateTime.Now;
                            ProcessStep = ProcessStep_Simple * 10 + 1;

                        }
                        else
                        {
                            RetryCount = 0;
                            ProcessStep = 1;
                            ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                        }
                    }
                    else
                    {
                        RetryCount = 0;
                        ProcessStep = 1;
                        ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    }
                    break;
                case 81:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 1000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 3;
                    }
                    break;
                case 83:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "무응답";
                    if (RetryCount >= 2)
                    {
                        RetryCount = 0;
                        ProcessStep = 1;
                        ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    }
                    else
                    {
                        ProcessStep = ProcessStep_Simple * 10;
                    }
                    break;
                case 84:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 NACK";
                    RetryCount = 0;
                    ProcessStep = 1;
                    ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    break;
                case 85:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 ACK";
                    RetryCount = 0;
                    ProcessStep = 1;
                    ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    break;
                case 90:
                    if (cb_Set[ProcessStep_Simple] != null)
                    {
                        if (cb_Set[ProcessStep_Simple].Checked)
                        {
                            RetryCount++;
                            Ctrl_AreaSpeed_CFG();
                            if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 진행";
                            ProcessStepTime = DateTime.Now;
                            ProcessStep = ProcessStep_Simple * 10 + 1;

                        }
                        else
                        {
                            RetryCount = 0;
                            ProcessStep = 1;
                            ProcessNextStep = -1;
                        }
                    }
                    else
                    {
                        RetryCount = 0;
                        ProcessStep = 1;
                        ProcessNextStep = -1;
                    }
                    break;
                case 91:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 1000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 3;
                    }
                    break;
                case 93:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "무응답";
                    if (RetryCount >= 2)
                    {
                        RetryCount = 0;
                        ProcessStep = 1;
                        ProcessNextStep = -1;
                    }
                    else
                    {
                        ProcessStep = ProcessStep_Simple * 10;
                    }
                    break;
                case 94:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 NACK";
                    RetryCount = 0;
                    ProcessStep = 1;
                    ProcessNextStep = -1;
                    break;
                case 95:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 ACK";
                    RetryCount = 0;
                    ProcessStep = 1;
                    ProcessNextStep = -1;
                    break;
                case 100:
                    Want_Data = false;
                    if ((DEVMode_Prev & 0x08) == 0)
                    {
                        Do_Ctrl_DevMode(ConstClass.CMD2_58, 0);
                    }
                    else
                    {
                        Do_Ctrl_DevMode(ConstClass.CMD2_58, 1);
                    }
                    ProcessStepTime = DateTime.Now;
                    ProcessStep = 101;
                    break;
                case 101:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 1000)
                    {
                        if ((DEVMode_Prev & 0x08) == 0)
                        {
                            Do_Ctrl_DevMode(ConstClass.CMD2_58, 0);
                        }
                        else
                        {
                            Do_Ctrl_DevMode(ConstClass.CMD2_58, 1);
                        }

                        Display_DownHeader(false);
                        Display_DownloadEnable(true);
                        DownloadTimer.Enabled = false;
                    }
                    break;
            }
        }

        private void UploadTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan ts;
            int ProcessStep_Simple = ProcessStep / 10;
            if (DateTime.Compare(ProcessStepTime, DateTime.Now) > 0)
            {
                ProcessStepTime = DateTime.Now;
            }
            switch (ProcessStep)
            {
                case -1:
                    Want_Data = false;
                    Display_UploadEnable(true);
                    UploadTimer.Enabled = false;
                    break;
                case 10:
                    RetryCount++;
                    Request_MCU_CFG();
                    ProcessStep = ProcessStep_Simple * 10 + 1;
                    ProcessStepTime = DateTime.Now;
                    break;
                case 11:
                    if (DateTime.Compare(ProcessStepTime, DateTime.Now) > 0)
                    {
                        ProcessStepTime = DateTime.Now;
                    }
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 2000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 3;
                    }
                    break;
                case 13:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "무응답";
                    if (RetryCount >= 2)
                    {
                        RetryCount = 0;
                        ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    }
                    else
                    {
                        ProcessStep = ProcessStep_Simple * 10;
                    }
                    break;
                case 15:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "응답 ACK";
                    if (lbl_SaveProgress[ProcessStep_Simple] != null) lbl_SaveProgress[ProcessStep_Simple].Text = "저장 완료";
                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    break;
                case 16:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "응답 ACK";
                    if (lbl_SaveProgress[ProcessStep_Simple] != null) lbl_SaveProgress[ProcessStep_Simple].Text = "저장 실패";
                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    break;

                case 20:
                    RetryCount++;
                    Request_IO_CFG();
                    ProcessStep = ProcessStep_Simple * 10 + 1;
                    ProcessStepTime = DateTime.Now;
                    break;
                case 21:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 2000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 3;
                    }
                    break;
                case 23:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "무응답";
                    if (RetryCount >= 2)
                    {
                        RetryCount = 0;
                        ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    }
                    else
                    {
                        ProcessStep = ProcessStep_Simple * 10;
                    }
                    break;
                case 25:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "응답 ACK";
                    if (lbl_SaveProgress[ProcessStep_Simple] != null) lbl_SaveProgress[ProcessStep_Simple].Text = "저장 완료";
                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    break;
                case 26:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "응답 ACK";
                    if (lbl_SaveProgress[ProcessStep_Simple] != null) lbl_SaveProgress[ProcessStep_Simple].Text = "저장 실패";
                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    break;

                case 30:
                    RetryCount++;
                    Request_CTRL_PARAM_CFG();
                    ProcessStep = ProcessStep_Simple * 10 + 1;
                    ProcessStepTime = DateTime.Now;
                    break;
                case 31:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 2000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 3;
                    }
                    break;
                case 33:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "무응답";
                    if (RetryCount >= 2)
                    {
                        RetryCount = 0;
                        ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    }
                    else
                    {
                        ProcessStep = ProcessStep_Simple * 10;
                    }
                    break;
                case 35:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "응답 ACK";
                    if (lbl_SaveProgress[ProcessStep_Simple] != null) lbl_SaveProgress[ProcessStep_Simple].Text = "저장 완료";
                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    break;
                case 36:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "응답 ACK";
                    if (lbl_SaveProgress[ProcessStep_Simple] != null) lbl_SaveProgress[ProcessStep_Simple].Text = "저장 실패";
                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    break;

                case 40:
                    RetryCount++;
                    Request_DRIVE_PARAM_CFG();
                    ProcessStep = ProcessStep_Simple * 10 + 1;
                    ProcessStepTime = DateTime.Now;
                    break;
                case 41:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 2000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 3;
                    }
                    break;
                case 43:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "무응답";
                    if (RetryCount >= 2)
                    {
                        RetryCount = 0;
                        ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    }
                    else
                    {
                        ProcessStep = ProcessStep_Simple * 10;
                    }
                    break;
                case 45:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "응답 ACK";
                    if (lbl_SaveProgress[ProcessStep_Simple] != null) lbl_SaveProgress[ProcessStep_Simple].Text = "저장 완료";
                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    break;
                case 46:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "응답 ACK";
                    if (lbl_SaveProgress[ProcessStep_Simple] != null) lbl_SaveProgress[ProcessStep_Simple].Text = "저장 실패";
                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    break;
                case 50:
                    RetryCount++;
                    Request_LIFT_PARAM_CFG();
                    ProcessStep = ProcessStep_Simple * 10 + 1;
                    ProcessStepTime = DateTime.Now;
                    break;
                case 51:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 2000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 3;
                    }
                    break;
                case 53:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "무응답";
                    if (RetryCount >= 2)
                    {
                        RetryCount = 0;
                        ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    }
                    else
                    {
                        ProcessStep = ProcessStep_Simple * 10;
                    }
                    break;
                case 55:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "응답 ACK";
                    if (lbl_SaveProgress[ProcessStep_Simple] != null) lbl_SaveProgress[ProcessStep_Simple].Text = "저장 완료";
                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    break;
                case 56:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "응답 ACK";
                    if (lbl_SaveProgress[ProcessStep_Simple] != null) lbl_SaveProgress[ProcessStep_Simple].Text = "저장 실패";
                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    break;

                case 60:
                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    break;

                case 70:
                    RetryCount++;
                    Request_EMSPosition_CFG(0, 0, 0);
                    ProcessStep = ProcessStep_Simple * 10 + 1;
                    ProcessStepTime = DateTime.Now;
                    break;
                case 71:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 2000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 3;
                    }
                    break;
                case 73:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "무응답";
                    if (RetryCount >= 2)
                    {
                        RetryCount = 0;
                        ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    }
                    else
                    {
                        ProcessStep = ProcessStep_Simple * 10;
                    }
                    break;
                case 75:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "응답 ACK";
                    if (lbl_SaveProgress[ProcessStep_Simple] != null) lbl_SaveProgress[ProcessStep_Simple].Text = "저장 완료";
                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    break;
                case 76:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "응답 ACK";
                    if (lbl_SaveProgress[ProcessStep_Simple] != null) lbl_SaveProgress[ProcessStep_Simple].Text = "저장 실패";
                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    break;
                case 80:
                    RetryCount++;
                    Request_Station_CFG();
                    ProcessStep = ProcessStep_Simple * 10 + 1;
                    ProcessStepTime = DateTime.Now;
                    break;
                case 81:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 2000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 3;
                    }
                    break;
                case 83:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "무응답";
                    if (RetryCount >= 2)
                    {
                        RetryCount = 0;
                        ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    }
                    else
                    {
                        ProcessStep = ProcessStep_Simple * 10;
                    }
                    break;
                case 85:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "응답 ACK";
                    if (lbl_SaveProgress[ProcessStep_Simple] != null) lbl_SaveProgress[ProcessStep_Simple].Text = "저장 완료";
                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    break;
                case 86:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "응답 ACK";
                    if (lbl_SaveProgress[ProcessStep_Simple] != null) lbl_SaveProgress[ProcessStep_Simple].Text = "저장 실패";
                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    break;
                case 90:
                    RetryCount++;
                    Request_SpeedArea_CFG();
                    ProcessStep = ProcessStep_Simple * 10 + 1;
                    ProcessStepTime = DateTime.Now;
                    break;
                case 91:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 2000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 3;
                    }
                    break;
                case 93:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "무응답";
                    if (RetryCount >= 2)
                    {
                        RetryCount = 0;
                        ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    }
                    else
                    {
                        ProcessStep = ProcessStep_Simple * 10;
                    }
                    break;
                case 95:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "응답 ACK";
                    if (lbl_SaveProgress[ProcessStep_Simple] != null) lbl_SaveProgress[ProcessStep_Simple].Text = "저장 완료";
                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    break;
                case 96:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "응답 ACK";
                    if (lbl_SaveProgress[ProcessStep_Simple] != null) lbl_SaveProgress[ProcessStep_Simple].Text = "저장 실패";
                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    break;
            }
        }

        private void cbSetAll_CheckedChanged(object sender, EventArgs e)
        {
            for (byte i = 1; i < 10; i++)
            {
                if (cb_Set[i] != null)
                {
                    if (cb_Set[i].Enabled)
                    {
                        cb_Set[i].Checked = cbSetAll.Checked;
                    }
                    else
                    {
                        if (cb_Set[i].Checked) cb_Set[i].Checked = false;
                    }
                }
            }
        }

        private void cbLoadAll_CheckedChanged(object sender, EventArgs e)
        {
            for (byte i = 1; i < 10; i++)
            {
                if (cb_Load[i] != null)
                {
                    cb_Load[i].Checked = cbLoadAll.Checked;
                }
            }
        }

        private void btn_UploadSave_Click(object sender, EventArgs e)
        {
            bool TmpBool = false;

            if (GetNextUploadIndex(0) != -1) TmpBool = true;

            if (TmpBool)
            {
                if (!form_Main.COMMDataManager.ISCOMM_ResponsGood)
                {
                    MessageBox.Show("통신 연결을 확인하세요");
                    return;
                }

                saveFileDialog1.Filter = "*.EMScfg|*.EMSCFG";

                if (saveFileDialog1.FileName == "")
                {
                    saveFileDialog1.InitialDirectory = Application.StartupPath;
                }
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    form_Main.EMS_ToTalFile.FileName = saveFileDialog1.FileName;

                    Display_UploadEnable(false);

                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(0);
                    ProcessStepTime = DateTime.Now;
                    UploadTimer.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("업로드에 포함 시킬 항목을 체크 해주세요");
            }
        }


        private void Request_MCU_CFG()
        {
            form_Main.COMMDataManager.ADD_TxUserZeroData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_25, (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_DevConfigReq)));
            Want_Data = true;
        }

        public unsafe void Process_MCU_CFG_Load(byte[] Data)
        {
            ems_REC_EMSConfig_RES = (VEXI_DEFS.TEMS_DevConfigRes)Global_Class.UTIL_BytesToStructure(Data, typeof(VEXI_DEFS.TEMS_DevConfigRes));

            ems_REC_EMSConfigCtrl.CtrlFlag[0] = 0x80;
            ems_REC_EMSConfigCtrl.Data = ems_REC_EMSConfig_RES;


            form_Main.EMS_ToTalFile.FileName = saveFileDialog1.FileName;
            if (form_Main.EMS_ToTalFile.Write_MCU_CFG(ems_REC_EMSConfigCtrl))
            {
                ProcessStep = 15;
            } else
            {
                ProcessStep = 16;
            }
        }

        private void Request_IO_CFG()
        {
            form_Main.COMMDataManager.ADD_TxUserZeroData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_23, (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_IOConfigReq)));
            Want_Data = true;
        }

        public unsafe void Process_IO_CFG_Load(byte[] Data)
        {
            ems_REC_DEV_IOConfig_RES = (VEXI_DEFS.EMS_IOConfig)Global_Class.UTIL_BytesToStructure(Data, Data.Length, typeof(VEXI_DEFS.EMS_IOConfig));

            form_Main.EMS_ToTalFile.FileName = saveFileDialog1.FileName;
            if (form_Main.EMS_ToTalFile.Write_IO_CFG(ems_REC_DEV_IOConfig_RES))
            {
                ProcessStep = 25;
            }
            else
            {
                ProcessStep = 26;
            }
        }

        private void Request_CTRL_PARAM_CFG()
        {
            form_Main.COMMDataManager.ADD_TxUserZeroData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A1, (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_REC_PARAMReq)));
            Want_Data = true;
        }

        public unsafe void Process_CTRL_PARAM_CFG_Load(byte[] Data)
        {
            ems_CtrlParam_RES = (VEXI_DEFS.TEMS_CTRLParamRes)Global_Class.UTIL_BytesToStructure(Data, typeof(VEXI_DEFS.TEMS_CTRLParamRes));

            ems_CtrlParam_CTRL.CtrlFlag[0] = 0x80;
            ems_CtrlParam_CTRL.CtrlFlag[1] = 0x00;
            ems_CtrlParam_CTRL.CtrlFlag[2] = 0x00;
            ems_CtrlParam_CTRL.CtrlFlag[3] = 0x00;
            ems_CtrlParam_CTRL.ParamItemsRec = ems_CtrlParam_RES;

            form_Main.EMS_ToTalFile.FileName = saveFileDialog1.FileName;
            if (form_Main.EMS_ToTalFile.Write_EMS_CTRL_PARAM(ems_CtrlParam_CTRL))
            {
                ProcessStep = 35;
            }
            else
            {
                ProcessStep = 36;
            }

        }

        private void Request_DRIVE_PARAM_CFG()
        {
            form_Main.COMMDataManager.ADD_TxUserZeroData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A3, (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_REC_PARAMReq)));
            Want_Data = true;
        }

        public unsafe void Process_DRIVE_PARAM_CFG_Load(byte[] Data)
        {
            ems_DriveParam_RES = (VEXI_DEFS.TEMS_DriveParamRes)Global_Class.UTIL_BytesToStructure(Data, typeof(VEXI_DEFS.TEMS_DriveParamRes));

            ems_DriveParam_CTRL.CtrlFlag[0] = 0x80;
            ems_DriveParam_CTRL.CtrlFlag[1] = 0x00;
            ems_DriveParam_CTRL.CtrlFlag[2] = 0x00;
            ems_DriveParam_CTRL.CtrlFlag[3] = 0x00;
            
            ems_DriveParam_CTRL.ParamItemsRec = ems_DriveParam_RES;

            form_Main.EMS_ToTalFile.FileName = saveFileDialog1.FileName;
            if (form_Main.EMS_ToTalFile.Write_EMS_DRIVE_PARAM(ems_DriveParam_CTRL))
            {
                ProcessStep = 45;
            }
            else
            {
                ProcessStep = 46;
            }
        }

        private void Request_LIFT_PARAM_CFG()
        {
            form_Main.COMMDataManager.ADD_TxUserZeroData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A5, (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_REC_PARAMReq)));
            Want_Data = true;
        }

        public unsafe void Process_LIFT_PARAM_CFG_Load(byte[] Data)
        {
            ems_LiftParam_RES = (VEXI_DEFS.TEMS_LiftParamRes)Global_Class.UTIL_BytesToStructure(Data, typeof(VEXI_DEFS.TEMS_LiftParamRes));

            ems_LiftParam_CTRL.CtrlFlag[0] = 0x80;
            ems_LiftParam_CTRL.CtrlFlag[1] = 0x00;
            ems_LiftParam_CTRL.CtrlFlag[2] = 0x00;
            ems_LiftParam_CTRL.CtrlFlag[3] = 0x00;
            
            ems_LiftParam_CTRL.ParamItemsRec = ems_LiftParam_RES;

            form_Main.EMS_ToTalFile.FileName = saveFileDialog1.FileName;
            if (form_Main.EMS_ToTalFile.Write_EMS_LIFT_PARAM(ems_LiftParam_CTRL))
            {
                ProcessStep = 55;
            }
            else
            {
                ProcessStep = 56;
            }
        }

        private unsafe void Request_Station_CFG()
        {
            VEXI_DEFS.TEMS_StationConfigReq req;
            Global_Class.UTIL_Byteptr_clear((byte*)req.Reserved, Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_StationConfigReq)));
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_98, req);
            Want_Data = true;
        }


        private unsafe void Request_EMSPosition_CFG(byte DataType, byte Startindex, byte Endindex)
        {
            VEXI_DEFS.TEMS_PositionSetParamReq req;
            Global_Class.UTIL_Byteptr_clear((byte*)req.Reserved, Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_PositionSetParamReq)));
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_94, req);

            Want_Data = true;
        }


        private unsafe void Request_SpeedArea_CFG()
        {
            VEXI_DEFS.TEMS_SpeedAreaGroupConfigReq req;
            Global_Class.UTIL_Byteptr_clear((byte*)req.Reserved, Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_SpeedAreaGroupConfigReq)));
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_9A, req);
            Want_Data = true;
        }

        public unsafe void Process_EMSPosition_Load(byte[] Data)
        {
            ems_PositionParam = (VEXI_DEFS.TEMS_PositionSetParam)Global_Class.UTIL_BytesToStructure(Data, Data.Length, typeof(VEXI_DEFS.TEMS_PositionSetParam));


            form_Main.EMS_ToTalFile.FileName = saveFileDialog1.FileName;
            if (form_Main.EMS_ToTalFile.Write_EMS_PositionParam(ems_PositionParam))
            {
                ProcessStep = 75;
            }
            else
            {
                ProcessStep = 76;
            }
        }



        public void Process_Station_CFG_Load(byte[] Data)
        {
            ems_StationParam = (VEXI_DEFS.TEMS_StationParam)Global_Class.UTIL_BytesToStructure(Data, typeof(VEXI_DEFS.TEMS_StationParam));

            form_Main.EMS_ToTalFile.FileName = saveFileDialog1.FileName;
            if (form_Main.EMS_ToTalFile.Write_EMS_StationParam(ems_StationParam))
            {
                ProcessStep = 85;
            }
            else
            {
                ProcessStep = 86;
            }
        }

        public unsafe void Process_EMS_SpeedArea_Load(byte[] Data)
        {
            ems_SpeedAreaGroupParam = (VEXI_DEFS.TEMS_SpeedAreaGroupParam)Global_Class.UTIL_BytesToStructure(Data, typeof(VEXI_DEFS.TEMS_SpeedAreaGroupParam));

            form_Main.EMS_ToTalFile.FileName = saveFileDialog1.FileName;
            if (form_Main.EMS_ToTalFile.Write_EMS_SpeedAreaParam(ems_SpeedAreaGroupParam))
            {
                ProcessStep = 95;
            }
            else
            {
                ProcessStep = 96;
            }
        }


    }

}
