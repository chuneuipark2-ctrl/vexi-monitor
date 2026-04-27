using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;   //DllImport
using System.Windows.Forms;

namespace VEXI
{
    public partial class Form_SRMTotal : Form
    {

        public Form_Main form_Main;
        public bool Want_Data = false;
        private int ProcessStep;
        private int ProcessNextStep;
        private DateTime ProcessStepTime;
        private byte RetryCount;
        private byte DEVMode_Prev;
        private bool IsSRM_New = true;

        public static VEXI_DEFS.TDEV_CtrlRes_2Byte CTRLRES_2byte;

        private static VEXI_DEFS.TSRM_DevConfigRes srm_REC_SRMConfig_RES;
        private static VEXI_DEFS.TSRM_DevConfigCtrl srm_REC_SRMConfigCtrl;
        private static VEXI_DEFS.SRM_IOConfig srm_REC_SRM_IOConfig_RES;
        private static VEXI_DEFS.SRM_IOConfig srm_REC_SRM_IOConfigCtrl;
        private static VEXI_DEFS.TSRM_CTRLParamRes srm_CtrlParam_RES;
        private static VEXI_DEFS.TSRM_CTRLParamCTRL srm_CtrlParam_CTRL;
        private static VEXI_DEFS.TSRM_LiftParamRes srm_LiftParam_RES;
        private static VEXI_DEFS.TSRM_LiftParamCTRL srm_LiftParam_Ctrl;
        private static VEXI_DEFS.TSRM_DriveParamRes srm_DriveParam_RES;
        private static VEXI_DEFS.TSRM_DriveParamCTRL srm_DriveParam_CTRL;
        private static VEXI_DEFS.TSRM_ForkParamRes srm_ForkParam_RES;
        private static VEXI_DEFS.TSRM_ForkParamCTRL srm_ForkParam_CTRL;
        
        private static VEXI_DEFS.TSRM_NoUseRack srm_NoUseRack_RES;
        private static VEXI_DEFS.TSRM_NoUseRack srm_NoUseRack_CTRL;
        private static VEXI_DEFS.TSRM_SpecialRack srm_SpecialRack_RES;
        private static VEXI_DEFS.TSRM_SpecialRack srm_SpecialRackCTRL;
        private static VEXI_DEFS.TSRM_StationParam srm_StationParam_RES;
        private static VEXI_DEFS.TSRM_StationParam srm_StationParam_CTRL;

        
        private static VEXI_DEFS.TSRM_CellPositionRES srm_CellPosition_RES;
        private static VEXI_DEFS.TSRM_CellPositionRES srm_BayLPosition_RES;
        private static VEXI_DEFS.TSRM_CellPositionRES srm_LevelLPosition_RES;
        private static VEXI_DEFS.TSRM_CellPositionRES srm_BayRPosition_RES;
        private static VEXI_DEFS.TSRM_CellPositionRES srm_LevelRPosition_RES;
        private static VEXI_DEFS.TSRM_CellPositionCTRL srm_CellPosition_CTRL;

        private static VEXI_DEFS.TSRM_CellOffsetREQ srm_CellOffset_Req;
        private static VEXI_DEFS.TSRM_CellOffset srm_CellOffset_RES;
        private static VEXI_DEFS.TSRM_CellOffset srm_CellOffset_CTRL;
        private VEXI_DEFS.SRM_CellOffset_Total CellOffset_Total = new VEXI_DEFS.SRM_CellOffset_Total();


        private static Label[] lbl_DownProgress;
        private static Label[] lbl_DateTime;
        private static CheckBox[] cb_Set;

        private static Label[] lbl_UploadProgress;
        private static Label[] lbl_SaveProgress;
        private static CheckBox[] cb_Load;

        public Form_SRMTotal()
        {
            InitializeComponent();

            lbl_DateTime = new Label[] { null, lblSaveTime1, lblSaveTime2, lblSaveTime3, lblSaveTime4, lblSaveTime5, lblSaveTime6, lblSaveTime7, lblSaveTime8, null, lblSaveTime10, lblSaveTime11, lblSaveTime12, lblSaveTime13 };
            lbl_DownProgress = new Label[] { null, lblDownResult1, lblDownResult2, lblDownResult3, lblDownResult4, lblDownResult5, lblDownResult6, lblDownResult7, lblDownResult8, null, lblDownResult10, lblDownResult11, lblDownResult12, lblDownResult13 };
            lbl_UploadProgress = new Label[] { null, lblUploadResult1, lblUploadResult2, lblUploadResult3, lblUploadResult4, lblUploadResult5, lblUploadResult6, lblUploadResult7, lblUploadResult8, null, lblUploadResult10, lblUploadResult11, lblUploadResult12, lblUploadResult13 };
            lbl_SaveProgress = new Label[] { null, lblSaveResult1, lblSaveResult2, lblSaveResult3, lblSaveResult4, lblSaveResult5, lblSaveResult6, lblSaveResult7, lblSaveResult8, null, lblSaveResult10, lblSaveResult11, lblSaveResult12, lblSaveResult13 };
            cb_Set = new CheckBox[] { null, cbSet1, cbSet2, cbSet3, cbSet4, cbSet5, cbSet6, cbSet7, cbSet8, null, cbSet10, cbSet11, cbSet12, cbSet13 };
            cb_Load = new CheckBox[] { null, cbLoad1, cbLoad2, cbLoad3, cbLoad4, cbLoad5, cbLoad6, cbLoad7, cbLoad8, null, cbLoad10, cbLoad11, cbLoad12, cbLoad13 };
        }

        #region 컴포넌트 이벤트
        private void Form_SRMTotal_Load(object sender, EventArgs e)
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
                for (byte i = 1; i < 14; i++)
                {
                    if (lbl_DateTime[i] != null) lbl_DateTime[i].Text = "----";
                    if (lbl_DownProgress[i] != null) lbl_DownProgress[i].Text = "----";
                }
            }

            if(cb_Set[1] != null) cb_Set[1].Enabled = (form_Main.SRM_ToTalFile.myHeader.SRM_CFG_Flag.Save_Flag == 1);
            if (cb_Set[2] != null) cb_Set[2].Enabled = (form_Main.SRM_ToTalFile.myHeader.IO_CFG_Flag.Save_Flag == 1);
            if (cb_Set[3] != null) cb_Set[3].Enabled = (form_Main.SRM_ToTalFile.myHeader.CTRL_PARAM_Flag.Save_Flag == 1);
            if (cb_Set[4] != null) cb_Set[4].Enabled = (form_Main.SRM_ToTalFile.myHeader.LIFT_PARAM_Flag.Save_Flag == 1);
            if (cb_Set[5] != null) cb_Set[5].Enabled = (form_Main.SRM_ToTalFile.myHeader.DRIVE_PARAM_Flag.Save_Flag == 1);
            if (cb_Set[6] != null) cb_Set[6].Enabled = (form_Main.SRM_ToTalFile.myHeader.FORK_PARAM_Flag.Save_Flag == 1);
            if (cb_Set[7] != null) cb_Set[7].Enabled = (form_Main.SRM_ToTalFile.myHeader.NRACK_Flag.Save_Flag == 1);
            if (cb_Set[8] != null) cb_Set[8].Enabled = (form_Main.SRM_ToTalFile.myHeader.SRACK_Flag.Save_Flag == 1);
            //if (cb_Set[9] != null) cb_Set[9].Enabled = (form_Main.SRM_ToTalFile.myHeader.RACK_CFG_Flag_Reserved.Save_Flag == 1);
            if (cb_Set[10] != null) cb_Set[10].Enabled = (form_Main.SRM_ToTalFile.myHeader.STATION_CFG_Flag.Save_Flag == 1);
            if (cb_Set[11] != null) cb_Set[11].Enabled = (form_Main.SRM_ToTalFile.myHeader.POSITION_LEFT_CFG_Flag.Save_Flag == 1);
            if (cb_Set[12] != null) cb_Set[12].Enabled = (form_Main.SRM_ToTalFile.myHeader.POSITION_RIGHT_CFG_Flag.Save_Flag == 1);
            if (cb_Set[13] != null) cb_Set[13].Enabled = (form_Main.SRM_ToTalFile.myHeader.OFFSET_CFG_Flag.Save_Flag == 1);
            if (Isinit)
            {
                for (byte i = 1; i < 14; i++)
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
                if (form_Main.SRM_ToTalFile.myHeader.SRM_CFG_Flag.Save_Flag == 1)
                {
                    DateTime TmpDateTime = new DateTime(form_Main.SRM_ToTalFile.myHeader.SRM_CFG_Flag.Save_Update);
                    if (lbl_DateTime[1] != null) lbl_DateTime[1].Text = TmpDateTime.ToString();
                    
                }
                if (form_Main.SRM_ToTalFile.myHeader.IO_CFG_Flag.Save_Flag == 1)
                {
                    DateTime TmpDateTime = new DateTime(form_Main.SRM_ToTalFile.myHeader.IO_CFG_Flag.Save_Update);
                    if (lbl_DateTime[2] != null) lbl_DateTime[2].Text = TmpDateTime.ToString();
                    
                }
                if (form_Main.SRM_ToTalFile.myHeader.CTRL_PARAM_Flag.Save_Flag == 1)
                {
                    DateTime TmpDateTime = new DateTime(form_Main.SRM_ToTalFile.myHeader.CTRL_PARAM_Flag.Save_Update);
                    if (lbl_DateTime[3] != null) lbl_DateTime[3].Text = TmpDateTime.ToString();
                    
                }
                if (form_Main.SRM_ToTalFile.myHeader.LIFT_PARAM_Flag.Save_Flag == 1)
                {
                    DateTime TmpDateTime = new DateTime(form_Main.SRM_ToTalFile.myHeader.LIFT_PARAM_Flag.Save_Update);
                    if (lbl_DateTime[4] != null) lbl_DateTime[4].Text = TmpDateTime.ToString();
                    
                }
                if (form_Main.SRM_ToTalFile.myHeader.DRIVE_PARAM_Flag.Save_Flag == 1)
                {
                    DateTime TmpDateTime = new DateTime(form_Main.SRM_ToTalFile.myHeader.DRIVE_PARAM_Flag.Save_Update);
                    if (lbl_DateTime[5] != null) lbl_DateTime[5].Text = TmpDateTime.ToString();
                    
                }
                if (form_Main.SRM_ToTalFile.myHeader.FORK_PARAM_Flag.Save_Flag == 1)
                {
                    DateTime TmpDateTime = new DateTime(form_Main.SRM_ToTalFile.myHeader.FORK_PARAM_Flag.Save_Update);
                    if (lbl_DateTime[6] != null) lbl_DateTime[6].Text = TmpDateTime.ToString();
                    
                }
                if (form_Main.SRM_ToTalFile.myHeader.NRACK_Flag.Save_Flag == 1)
                {
                    DateTime TmpDateTime = new DateTime(form_Main.SRM_ToTalFile.myHeader.NRACK_Flag.Save_Update);
                    if (lbl_DateTime[7] != null) lbl_DateTime[7].Text = TmpDateTime.ToString();
                }
                if (form_Main.SRM_ToTalFile.myHeader.SRACK_Flag.Save_Flag == 1)
                {
                    DateTime TmpDateTime = new DateTime(form_Main.SRM_ToTalFile.myHeader.SRACK_Flag.Save_Update);
                    if (lbl_DateTime[8] != null) lbl_DateTime[8].Text = TmpDateTime.ToString();
                }
                //if (form_Main.SRM_ToTalFile.myHeader.RACK_CFG_Flag_Reserved.Save_Flag == 1)
                //{
                //DateTime TmpDateTime = new DateTime(form_Main.SRM_ToTalFile.myHeader.RACK_CFG_Flag_Reserved.Save_Update);
                //if (lbl_DateTime[9] != null) lbl_DateTime[9].Text = TmpDateTime.ToString();
                //}
                if (form_Main.SRM_ToTalFile.myHeader.STATION_CFG_Flag.Save_Flag == 1)
                {
                    DateTime TmpDateTime = new DateTime(form_Main.SRM_ToTalFile.myHeader.STATION_CFG_Flag.Save_Update);
                    if (lbl_DateTime[10] != null) lbl_DateTime[10].Text = TmpDateTime.ToString();
                }
                if (form_Main.SRM_ToTalFile.myHeader.POSITION_LEFT_CFG_Flag.Save_Flag == 1)
                {
                    DateTime TmpDateTime = new DateTime(form_Main.SRM_ToTalFile.myHeader.POSITION_LEFT_CFG_Flag.Save_Update);
                    if (lbl_DateTime[11] != null) lbl_DateTime[11].Text = TmpDateTime.ToString();
                }
                if (form_Main.SRM_ToTalFile.myHeader.POSITION_RIGHT_CFG_Flag.Save_Flag == 1)
                {
                    DateTime TmpDateTime = new DateTime(form_Main.SRM_ToTalFile.myHeader.POSITION_RIGHT_CFG_Flag.Save_Update);
                    if (lbl_DateTime[12] != null) lbl_DateTime[12].Text = TmpDateTime.ToString();
                }
                if (form_Main.SRM_ToTalFile.myHeader.OFFSET_CFG_Flag.Save_Flag == 1)
                {
                    DateTime TmpDateTime = new DateTime(form_Main.SRM_ToTalFile.myHeader.OFFSET_CFG_Flag.Save_Update);
                    if (lbl_DateTime[13] != null) lbl_DateTime[13].Text = TmpDateTime.ToString();
                }
            }

        }

        private void Display_UploadEnable(bool TmpEnable)
        {
            for (byte i = 1; i < 14; i++)
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
                for (byte i = 1; i < 14; i++)
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

        private unsafe void Ctrl_SRM_CFG()
        {
            form_Main.SRM_ToTalFile.FileName = openFileDialog1.FileName;
            if (form_Main.SRM_ToTalFile.Read_MCU_CFG(ref srm_REC_SRMConfigCtrl))
            {
                //장치구조 => CtrlFlag[0] 의 7bit 가 서면 장치쪽에서 데이터 영역 통짜로 Copy 함 (Reserved에 쓰레기값 들어 있어서 Verify 통과 못하는 문제 해결)
                srm_REC_SRMConfigCtrl.CtrlFlag[0] = (byte)(srm_REC_SRMConfigCtrl.CtrlFlag[0] | 0x80);
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_26, srm_REC_SRMConfigCtrl);
                Want_Data = true;
            }
        }

        private void Ctrl_IO_CFG()
        {
            form_Main.SRM_ToTalFile.FileName = openFileDialog1.FileName;
            if (form_Main.SRM_ToTalFile.Read_IO_CFG(ref srm_REC_SRM_IOConfigCtrl))
            {
                if (!IsSRM_New)
                {
                    Process_SRMCtrl_OLDIO();
                }

                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_24, srm_REC_SRM_IOConfigCtrl);
                Want_Data = true;
            }
        }

        private unsafe void Ctrl_CTRL_PARAM_CFG()
        {
            form_Main.SRM_ToTalFile.FileName = openFileDialog1.FileName;
            if (form_Main.SRM_ToTalFile.Read_SRM_CTRL_PARAM(ref srm_CtrlParam_CTRL))
            {
                //제어파라미터 => CtrlFlag[0] 의 7bit 가 서면 장치쪽에서 데이터 영역 통짜로 Copy 함 (Reserved에 쓰레기값 들어 있어서 Verify 통과 못하는 문제 해결)
                srm_CtrlParam_CTRL.CtrlFlag[0] = (byte)(srm_CtrlParam_CTRL.CtrlFlag[0] | 0x80);
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A2, srm_CtrlParam_CTRL);
                Want_Data = true;
            }
        }
        private unsafe void Ctrl_LIFT_PARAM_CFG()
        {
            form_Main.SRM_ToTalFile.FileName = openFileDialog1.FileName;
            if (form_Main.SRM_ToTalFile.Read_SRM_LIFT_PARAM(ref srm_LiftParam_Ctrl))
            {
                //리프트드라이브 => CtrlFlag[0] 의 7bit 가 서면 장치쪽에서 데이터 영역 통짜로 Copy 함 (Reserved에 쓰레기값 들어 있어서 Verify 통과 못하는 문제 해결)
                srm_LiftParam_Ctrl.CtrlFlag[0] = (byte)(srm_LiftParam_Ctrl.CtrlFlag[0] | 0x80);
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A6, srm_LiftParam_Ctrl);
                Want_Data = true;
            }
        }

        private unsafe void Ctrl_DRIVE_PARAM_CFG()
        {
            form_Main.SRM_ToTalFile.FileName = openFileDialog1.FileName;
            if (form_Main.SRM_ToTalFile.Read_SRM_DRIVE_PARAM(ref srm_DriveParam_CTRL))
            {
                //주행드라이브 => CtrlFlag[0] 의 7bit 가 서면 장치쪽에서 데이터 영역 통짜로 Copy 함 (Reserved에 쓰레기값 들어 있어서 Verify 통과 못하는 문제 해결)
                srm_DriveParam_CTRL.CtrlFlag[0] = (byte)(srm_DriveParam_CTRL.CtrlFlag[0] | 0x80);
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A4, srm_DriveParam_CTRL);
                Want_Data = true;
            }
        }
        private unsafe void Ctrl_FORK_PARAM_CFG()
        {
            form_Main.SRM_ToTalFile.FileName = openFileDialog1.FileName;
            if (form_Main.SRM_ToTalFile.Read_SRM_ForkParamCTRL(ref srm_ForkParam_CTRL))
            {
                //리프트드라이브 => CtrlFlag[0] 의 7bit 가 서면 장치쪽에서 데이터 영역 통짜로 Copy 함 (Reserved에 쓰레기값 들어 있어서 Verify 통과 못하는 문제 해결)
                srm_ForkParam_CTRL.CtrlFlag[0] = (byte)(srm_ForkParam_CTRL.CtrlFlag[0] | 0x80);
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A8, srm_ForkParam_CTRL);
                Want_Data = true;
            }
        }

        private void Ctrl_NoUseRack_CFG()
        {
            form_Main.SRM_ToTalFile.FileName = openFileDialog1.FileName;
            if (form_Main.SRM_ToTalFile.Read_SRM_NoUseRack(ref srm_NoUseRack_CTRL))
            {
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_9D, srm_NoUseRack_CTRL);
                form_Main.COMMDataManager.ISPolingDelayStop = true;
                form_Main.COMMDataManager.PolingDelayTxTime = DateTime.Now;
                Want_Data = true;
            }
        }

        private void Ctrl_SpecalRack_CFG()
        {
            form_Main.SRM_ToTalFile.FileName = openFileDialog1.FileName;
            if (form_Main.SRM_ToTalFile.Read_SRM_SpecialRack(ref srm_SpecialRackCTRL))
            {
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_9F, srm_SpecialRackCTRL);
                form_Main.COMMDataManager.ISPolingDelayStop = true;
                form_Main.COMMDataManager.PolingDelayTxTime = DateTime.Now;
                Want_Data = true;
            }
        }

        private void Ctrl_LevelLPosition_CFG()
        {
            form_Main.SRM_ToTalFile.FileName = openFileDialog1.FileName;
            if (form_Main.SRM_ToTalFile.Read_SRM_LevelLPositionCTRL(ref srm_CellPosition_CTRL))
            {
                UInt16 TmpTxLen;
                TmpTxLen = (UInt16)(Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CellPosition_Header)) + (srm_CellPosition_CTRL.Header.EndNo - srm_CellPosition_CTRL.Header.StartNo + 1) * Marshal.SizeOf(typeof(int)));
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_95, TmpTxLen, srm_CellPosition_CTRL);
                form_Main.COMMDataManager.ISPolingDelayStop = true;
                form_Main.COMMDataManager.PolingDelayTxTime = DateTime.Now;
                Want_Data = true;
            }
        }

        private void Ctrl_BayLPosition_CFG()
        {
            form_Main.SRM_ToTalFile.FileName = openFileDialog1.FileName;
            if (form_Main.SRM_ToTalFile.Read_SRM_BayLPositionCTRL(ref srm_CellPosition_CTRL))
            {
                UInt16 TmpTxLen;
                TmpTxLen = (UInt16)(Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CellPosition_Header)) + (srm_CellPosition_CTRL.Header.EndNo - srm_CellPosition_CTRL.Header.StartNo + 1) * Marshal.SizeOf(typeof(int)));
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_95, TmpTxLen, srm_CellPosition_CTRL);
                form_Main.COMMDataManager.ISPolingDelayStop = true;
                form_Main.COMMDataManager.PolingDelayTxTime = DateTime.Now;
                Want_Data = true;
            }
        }

        private void Ctrl_LevelRPosition_CFG()
        {
            form_Main.SRM_ToTalFile.FileName = openFileDialog1.FileName;
            if (form_Main.SRM_ToTalFile.Read_SRM_LevelRPositionCTRL(ref srm_CellPosition_CTRL))
            {
                UInt16 TmpTxLen;
                TmpTxLen = (UInt16)(Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CellPosition_Header)) + (srm_CellPosition_CTRL.Header.EndNo - srm_CellPosition_CTRL.Header.StartNo + 1) * Marshal.SizeOf(typeof(int)));
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_95, TmpTxLen, srm_CellPosition_CTRL);
                form_Main.COMMDataManager.ISPolingDelayStop = true;
                form_Main.COMMDataManager.PolingDelayTxTime = DateTime.Now;
                Want_Data = true;
            }
        }

        private void Ctrl_BayRPosition_CFG()
        {
            form_Main.SRM_ToTalFile.FileName = openFileDialog1.FileName;
            if (form_Main.SRM_ToTalFile.Read_SRM_BayRPositionCTRL(ref srm_CellPosition_CTRL))
            {
                UInt16 TmpTxLen;
                TmpTxLen = (UInt16)(Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CellPosition_Header)) + (srm_CellPosition_CTRL.Header.EndNo - srm_CellPosition_CTRL.Header.StartNo + 1) * Marshal.SizeOf(typeof(int)));
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_95, TmpTxLen, srm_CellPosition_CTRL);
                form_Main.COMMDataManager.ISPolingDelayStop = true;
                form_Main.COMMDataManager.PolingDelayTxTime = DateTime.Now;
                Want_Data = true;
            }
        }

        private void Ctrl_Station_CFG()
        {
            form_Main.SRM_ToTalFile.FileName = openFileDialog1.FileName;
            if (form_Main.SRM_ToTalFile.Read_SRM_StationParam(ref srm_StationParam_CTRL))
            {
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_99, srm_StationParam_CTRL);
                form_Main.COMMDataManager.ISPolingDelayStop = true;
                form_Main.COMMDataManager.PolingDelayTxTime = DateTime.Now;
                Want_Data = true;
            }
        }

        public unsafe bool Ctrl_CellOffset(UInt16 Startindex)
        {
            if (Startindex == 0)
            {
                form_Main.SRM_ToTalFile.FileName = openFileDialog1.FileName;
                if (form_Main.SRM_ToTalFile.Read_SRM_OFFSET_CFG(ref CellOffset_Total))
                {
                    srm_CellOffset_CTRL.Header.DevType = CellOffset_Total.DevType;
                    srm_CellOffset_CTRL.Header.TotalCount = CellOffset_Total.TotalCount;
                }
                else
                {
                    RetryCount = 5;
                    ProcessStep = 133;
                    return false;
                }
            }

            UInt16 TmpTxLen;

            if (srm_CellOffset_CTRL.Header.TotalCount == 0)
            {
                srm_CellOffset_CTRL.Header.ItemCount = 0;
                TmpTxLen = (UInt16)(Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CellOffset_Header)));
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_97, TmpTxLen, srm_CellOffset_CTRL);
                form_Main.COMMDataManager.ISPolingDelayStop = true;
                form_Main.COMMDataManager.PolingDelayTxTime = DateTime.Now;
                Want_Data = true;
            } else
            {
                if (Startindex >= srm_CellOffset_CTRL.Header.TotalCount)
                {
                    RetryCount = 0;
                    ProcessStep = 135;
                    return false;
                }
                else
                {
                    UInt16 EndIndex;

                    srm_CellOffset_CTRL.Header.Nowindex = Startindex;
                    if ((srm_CellOffset_CTRL.Header.TotalCount - srm_CellOffset_CTRL.Header.Nowindex) <= 128)
                    {
                        srm_CellOffset_CTRL.Header.ItemCount = (byte)(srm_CellOffset_CTRL.Header.TotalCount - srm_CellOffset_CTRL.Header.Nowindex);
                    }
                    else
                    {
                        srm_CellOffset_CTRL.Header.ItemCount = 128;
                    }

                    EndIndex = (UInt16)(srm_CellOffset_CTRL.Header.Nowindex + srm_CellOffset_CTRL.Header.ItemCount - 1);
                    TmpTxLen = (UInt16)(Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CellOffset_Header)) + srm_CellOffset_CTRL.Header.ItemCount * Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CellOffsetRec)));

                    fixed (VEXI_DEFS.TSRM_CellOffsetRec* ptr_1 = &srm_CellOffset_CTRL.SRM_CellOffsetRec)
                    {
                        for (int i = Startindex; i <= EndIndex; i++)
                        {
                            (ptr_1 + i - Startindex)->Bay = CellOffset_Total.SRM_CellOffsetRec[i].Bay;
                            (ptr_1 + i - Startindex)->Level = CellOffset_Total.SRM_CellOffsetRec[i].Level;
                            (ptr_1 + i - Startindex)->Left_Travel_Offset = CellOffset_Total.SRM_CellOffsetRec[i].Left_Travel_Offset;
                            (ptr_1 + i - Startindex)->Left_Lift_Offset = CellOffset_Total.SRM_CellOffsetRec[i].Left_Lift_Offset;
                            (ptr_1 + i - Startindex)->Left_Fork_Offset = CellOffset_Total.SRM_CellOffsetRec[i].Left_Fork_Offset;
                            (ptr_1 + i - Startindex)->Right_Travel_Offset = CellOffset_Total.SRM_CellOffsetRec[i].Right_Travel_Offset;
                            (ptr_1 + i - Startindex)->Right_Lift_Offset = CellOffset_Total.SRM_CellOffsetRec[i].Right_Lift_Offset;
                            (ptr_1 + i - Startindex)->Right_Fork_Offset = CellOffset_Total.SRM_CellOffsetRec[i].Right_Fork_Offset;
                        }
                    }

                    form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_97, TmpTxLen, srm_CellOffset_CTRL);
                    form_Main.COMMDataManager.ISPolingDelayStop = true;
                    form_Main.COMMDataManager.PolingDelayTxTime = DateTime.Now;
                    Want_Data = true;
                }
            }
            return true;
        }

        public void Response_SRM_CFGCtrl(byte[] Data)
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

        public void Response_LIFT_PARAM_CFG(byte[] Data)
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

        public void Response_DRIVE_PARAM_CFG(byte[] Data)
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

        public void Response_FORK_PARAM_CFG(byte[] Data)
        {
            if (Data[0] == 0x00)
            {
                ProcessStep = 65;
            }
            else
            {
                ProcessStep = 64;
            }
        }

        public void Response_NoUseRack_CFG(byte[] Data)
        {
            if (Data[0] == 0x00)
            {
                ProcessStep = 75;
            }
            else
            {
                ProcessStep = 74;
            }
        }

        public void Response_SpecialUseRack_CFG(byte[] Data)
        {
            if (Data[0] == 0x00)
            {
                ProcessStep = 85;
            }
            else
            {
                ProcessStep = 84;
            }
        }

        public void Response_Sation_CFG(byte[] Data)
        {
            if (Data[0] == 0x00)
            {
                ProcessStep = 105;
            }
            else
            {
                ProcessStep = 104;
            }
        }

        public void Response_Position_CFG(byte[] Data)
        {
            if (Data[5] == 0x01)
            {
                if (Data[8] == 0x00)
                {
                    RetryCount = 0;
                    ProcessStep = 112;
                }
                else
                {
                    ProcessStep = 116;
                }

            }
            else if (Data[5] == 0x02)
            {
                if (Data[8] == 0x00)
                {
                    ProcessStep = 117;
                }
                else
                {
                    ProcessStep = 116;
                }
            } else if (Data[5] == 0x03)
            {
                if (Data[8] == 0x00)
                {
                    RetryCount = 0;
                    ProcessStep = 122;
                }
                else
                {
                    ProcessStep = 126;
                }

            }
            else if (Data[5] == 0x04)
            {
                if (Data[8] == 0x00)
                {
                    ProcessStep = 127;
                }
                else
                {
                    ProcessStep = 126;
                }
            }
            else
            {
                ProcessStep = 126;
            }
        }

        public void Response_OFFSET_CFG(byte[] Data)
        {
            UInt16 NextIndex;
            if (Data[6] == 0x00)
            {
                if (srm_CellOffset_CTRL.Header.TotalCount == 0)
                {
                    RetryCount = 0;
                    ProcessStep = 135;
                    return;
                }
                else
                {
                    RetryCount = 0;
                    NextIndex = (UInt16)((Data[3] & 0x00FF) | ((Data[4] << 8) & 0xFF00));
                    NextIndex = (UInt16)(NextIndex + Data[5]);
                    ProcessStepTime = DateTime.Now;
                    Ctrl_CellOffset(NextIndex);
                }
            }
            else
            {
                ProcessStep = 134;
            }
        }


        #endregion

        private void btn_LoadTotalFile4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.SRMcfg|*.SRMCFG";

            if (openFileDialog1.FileName == "")
            {
                openFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                form_Main.SRM_ToTalFile.FileName = openFileDialog1.FileName;
                form_Main.SRM_ToTalFile.Read_Header(ConstClass.TYPE_SRM);
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

                DEVMode_Prev = form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.DevMode;

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

                    if (Global_Class.BitStatus(form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.DevMode, 0)) ctrlValue[0] = 2;
                    else if (Global_Class.BitStatus(form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.DevMode, 1)) ctrlValue[0] = 0;
                    else if (Global_Class.BitStatus(form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.DevMode, 3)) ctrlValue[0] = 1;
                    if (Global_Class.BitStatus(form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.DevMode, 2)) ctrlValue[1] |= 0x01;


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
            for (int i = TmpNowIndex + 1; i < 14; i++)
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
            for (int i = TmpNowIndex + 1; i < 14; i++)
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
                        ProcessStep = 140;
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
                        if ((form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.DevMode & 0x08) == 0)
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
                            Ctrl_SRM_CFG();
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
                    }
                    else
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
                    }
                    else
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
                    if (cb_Set[ProcessStep_Simple] != null)
                    {

                        if (cb_Set[ProcessStep_Simple].Checked)
                        {
                            RetryCount++;
                            Ctrl_FORK_PARAM_CFG();
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
                case 61:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 1000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 3;
                    }
                    break;
                case 63:
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
                case 64:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 NACK";
                    RetryCount = 0;
                    ProcessStep = 1;
                    ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    break;
                case 65:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 ACK";
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
                            Ctrl_NoUseRack_CFG();
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
                    break; ;
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
                            Ctrl_SpecalRack_CFG();
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
                    // 이 과정은 없어졌으므로 바로 다음 단계로 넘긴다
                    RetryCount = 0;
                    ProcessStep = 1;
                    ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    break;
                case 100:
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
                case 101:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 1000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 3;
                    }
                    break;
                case 103:
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
                case 104:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 NACK";
                    RetryCount = 0;
                    ProcessStep = 1;
                    ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    break;
                case 105:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 ACK";
                    RetryCount = 0;
                    ProcessStep = 1;
                    ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    break;
                case 110:
                    if (cb_Set[ProcessStep_Simple] != null)
                    {
                        if (cb_Set[ProcessStep_Simple].Checked)
                        {

                            RetryCount++;
                            Ctrl_BayLPosition_CFG();
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
                case 111:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 1000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 4;
                    }
                    break;
                case 112:
                    if (cb_Set[ProcessStep_Simple] != null)
                    {
                        if (cb_Set[ProcessStep_Simple].Checked)
                        {
                            RetryCount++;
                            Ctrl_LevelLPosition_CFG();
                            if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 진행";
                            ProcessStepTime = DateTime.Now;
                            ProcessStep = ProcessStep_Simple * 10 + 3;
                        }
                        else
                        {
                            RetryCount = 0;
                            ProcessStep = 1;
                            ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                        }
                    }
                    break;
                case 113:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 1000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 5;
                    }
                    break;
                case 114:
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
                case 115:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "무응답";
                    if (RetryCount >= 2)
                    {
                        RetryCount = 0;
                        ProcessStep = 1;
                        ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    }
                    else
                    {
                        //RetryCount = 0;
                        ProcessStep = ProcessStep_Simple * 10 + 2;
                    }
                    break;
                case 116:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 NACK";
                    RetryCount = 0;
                    ProcessStep = 1;
                    ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    break;
                case 117:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 ACK";
                    RetryCount = 0;
                    ProcessStep = 1;
                    ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    break;

                case 120:
                    if (cb_Set[ProcessStep_Simple] != null)
                    {
                        if (cb_Set[ProcessStep_Simple].Checked)
                        {

                            RetryCount++;
                            Ctrl_BayRPosition_CFG();
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
                case 121:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 1000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 4;
                    }
                    break;
                case 122:
                    if (cb_Set[ProcessStep_Simple] != null)
                    {
                        if (cb_Set[ProcessStep_Simple].Checked)
                        {
                            RetryCount++;
                            Ctrl_LevelRPosition_CFG();
                            if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 진행";
                            ProcessStepTime = DateTime.Now;
                            ProcessStep = ProcessStep_Simple * 10 + 3;
                        }
                        else
                        {
                            RetryCount = 0;
                            ProcessStep = 1;
                            ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                        }
                    }
                    break;
                case 123:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 1000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 5;
                    }
                    break;
                case 124:
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
                case 125:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "무응답";
                    if (RetryCount >= 2)
                    {
                        RetryCount = 0;
                        ProcessStep = 1;
                        ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    }
                    else
                    {
                        //RetryCount = 0;
                        ProcessStep = ProcessStep_Simple * 10 + 2;
                    }
                    break;
                case 126:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 NACK";
                    RetryCount = 0;
                    ProcessStep = 1;
                    ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    break;
                case 127:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 ACK";
                    RetryCount = 0;
                    ProcessStep = 1;
                    ProcessNextStep = GetNextDownloadIndex(ProcessStep_Simple);
                    break;


                case 130:
                    if (cb_Set[ProcessStep_Simple] != null)
                    {
                        if (cb_Set[ProcessStep_Simple].Checked)
                        {
                            RetryCount++;
                            if (Ctrl_CellOffset(0))
                            {
                                if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 진행";
                                ProcessStepTime = DateTime.Now;
                                ProcessStep = ProcessStep_Simple * 10 + 1;
                            }
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
                case 131:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 12000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 3;
                    }
                    break;
                case 132:
                    RetryCount++;
                    Ctrl_CellOffset(srm_CellOffset_CTRL.Header.Nowindex);
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 진행";
                    ProcessStepTime = DateTime.Now;
                    ProcessStep = ProcessStep_Simple * 10 + 1;
                    break;
                case 133:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "무응답";
                    if (RetryCount >= 2)
                    {
                        RetryCount = 0;
                        ProcessStep = 1;
                        ProcessNextStep = -1;
                    }
                    else
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 2;
                    }
                    break;
                case 134:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 NACK";
                    RetryCount = 0;
                    ProcessStep = 1;
                    ProcessNextStep = -1;
                    break;
                case 135:
                    if (lbl_DownProgress[ProcessStep_Simple] != null) lbl_DownProgress[ProcessStep_Simple].Text = "제어 ACK";
                    RetryCount = 0;
                    ProcessStep = 1;
                    ProcessNextStep = -1;
                    break;
                case 140:
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
                    ProcessStep = 141;
                    break;
                case 141:
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
                    Request_SRM_CFG();
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
                    Request_LIFT_PARAM_CFG();
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
                    Request_DRIVE_PARAM_CFG();
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
                    RetryCount++;
                    Request_FORK_PARAM_CFG();
                    ProcessStep = ProcessStep_Simple * 10 + 1;
                    ProcessStepTime = DateTime.Now;
                    break;
                case 61:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 2000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 3;
                    }
                    break;
                case 63:
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
                case 65:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "응답 ACK";
                    if (lbl_SaveProgress[ProcessStep_Simple] != null) lbl_SaveProgress[ProcessStep_Simple].Text = "저장 완료";
                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    break;
                case 66:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "응답 ACK";
                    if (lbl_SaveProgress[ProcessStep_Simple] != null) lbl_SaveProgress[ProcessStep_Simple].Text = "저장 실패";
                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    break;

                case 70:
                    RetryCount++;
                    Request_NoUseRack_CFG();
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
                    Request_SpecailRack_CFG();
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
                    //이 과정은 없어졌으므로 바로 다음 단계로 넘긴다
                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    break;

                case 100:
                    RetryCount++;
                    Request_Station_CFG();
                    ProcessStep = ProcessStep_Simple * 10 + 1;
                    ProcessStepTime = DateTime.Now;
                    break;
                case 101:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 2000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 3;
                    }
                    break;
                case 103:
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
                case 105:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "응답 ACK";
                    if (lbl_SaveProgress[ProcessStep_Simple] != null) lbl_SaveProgress[ProcessStep_Simple].Text = "저장 완료";
                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    break;
                case 106:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "응답 ACK";
                    if (lbl_SaveProgress[ProcessStep_Simple] != null) lbl_SaveProgress[ProcessStep_Simple].Text = "저장 실패";
                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    break;

                case 110:
                    RetryCount++;
                    Request_CellPosition(1, 0, 255);
                    ProcessStep = ProcessStep_Simple * 10 + 1;
                    ProcessStepTime = DateTime.Now;
                    break;
                case 111:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 2000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 3;
                    }
                    break;
                case 113:
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
                case 115:
                    RetryCount++;
                    Request_CellPosition(2, 0, 127);
                    ProcessStep = ProcessStep_Simple * 10 + 6;
                    ProcessStepTime = DateTime.Now;
                    break;
                case 116:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 2000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 7;
                    }
                    break;
                case 117:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "무응답";
                    if (RetryCount >= 2)
                    {
                        RetryCount = 0;
                        ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    }
                    else
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 5;
                    }
                    break;
                case 118:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "응답 ACK";
                    if (lbl_SaveProgress[ProcessStep_Simple] != null) lbl_SaveProgress[ProcessStep_Simple].Text = "저장 완료";
                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    break;
                case 119:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "응답 ACK";
                    if (lbl_SaveProgress[ProcessStep_Simple] != null) lbl_SaveProgress[ProcessStep_Simple].Text = "저장 실패";
                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    break;

                case 120:
                    RetryCount++;
                    Request_CellPosition(3, 0, 255);
                    ProcessStep = ProcessStep_Simple * 10 + 1;
                    ProcessStepTime = DateTime.Now;
                    break;
                case 121:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 2000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 3;
                    }
                    break;
                case 123:
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
                case 125:
                    RetryCount++;
                    Request_CellPosition(4, 0, 127);
                    ProcessStep = ProcessStep_Simple * 10 + 6;
                    ProcessStepTime = DateTime.Now;
                    break;
                case 126:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 2000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 7;
                    }
                    break;
                case 127:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "무응답";
                    if (RetryCount >= 2)
                    {
                        RetryCount = 0;
                        ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    }
                    else
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 5;
                    }
                    break;
                case 128:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "응답 ACK";
                    if (lbl_SaveProgress[ProcessStep_Simple] != null) lbl_SaveProgress[ProcessStep_Simple].Text = "저장 완료";
                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    break;
                case 129:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "응답 ACK";
                    if (lbl_SaveProgress[ProcessStep_Simple] != null) lbl_SaveProgress[ProcessStep_Simple].Text = "저장 실패";
                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    break;


                case 130:
                    RetryCount++;
                    Request_CellOffset(0);
                    ProcessStep = ProcessStep_Simple * 10 + 1;
                    ProcessStepTime = DateTime.Now;
                    break;
                case 131:
                    ts = DateTime.Now - ProcessStepTime;
                    if (ts.TotalMilliseconds > 2000)
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 3;
                    }
                    break;
                case 132:
                    RetryCount++;
                    Request_CellOffset(srm_CellOffset_Req.ReqIndex);
                    ProcessStep = ProcessStep_Simple * 10 + 1;
                    ProcessStepTime = DateTime.Now;
                    break;
                case 133:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "무응답";
                    if (RetryCount >= 2)
                    {
                        RetryCount = 0;
                        ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    }
                    else
                    {
                        ProcessStep = ProcessStep_Simple * 10 + 2;
                    }
                    break;
                case 135:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "응답 ACK";
                    if (lbl_SaveProgress[ProcessStep_Simple] != null) lbl_SaveProgress[ProcessStep_Simple].Text = "저장 완료";
                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    break;
                case 136:
                    if (lbl_UploadProgress[ProcessStep_Simple] != null) lbl_UploadProgress[ProcessStep_Simple].Text = "응답 ACK";
                    if (lbl_SaveProgress[ProcessStep_Simple] != null) lbl_SaveProgress[ProcessStep_Simple].Text = "저장 실패";
                    RetryCount = 0;
                    ProcessStep = GetNextUploadIndex(ProcessStep_Simple);
                    break;
            }
        }

        private void cbSetAll_CheckedChanged(object sender, EventArgs e)
        {
            for (byte i = 1; i < 14; i++)
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
            for (byte i = 1; i < 14; i++)
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

                saveFileDialog1.Filter = "*.SRMcfg|*.SRMCFG";

                if (saveFileDialog1.FileName == "")
                {
                    saveFileDialog1.InitialDirectory = Application.StartupPath;
                }
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    form_Main.SRM_ToTalFile.FileName = saveFileDialog1.FileName;

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


        private void Request_SRM_CFG()
        {
            form_Main.COMMDataManager.ADD_TxUserZeroData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_25, (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_DevConfigReq)));
            Want_Data = true;
        }

        public unsafe void Process_SRM_CFG_Load(byte[] Data)
        {
            srm_REC_SRMConfig_RES = (VEXI_DEFS.TSRM_DevConfigRes)Global_Class.UTIL_BytesToStructure(Data, typeof(VEXI_DEFS.TSRM_DevConfigRes));

            srm_REC_SRMConfigCtrl.CtrlFlag[0] = 0x80;
            srm_REC_SRMConfigCtrl.Data = srm_REC_SRMConfig_RES;


            form_Main.SRM_ToTalFile.FileName = saveFileDialog1.FileName;
            if (form_Main.SRM_ToTalFile.Write_MCU_CFG(srm_REC_SRMConfigCtrl))
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

        public unsafe void Process_SRMSt_OLDIO()
        {
            if (!IsSRM_New)
            {
                fixed (VEXI_DEFS.REC_DIConfig* DICOnfigPtr = &srm_REC_SRM_IOConfig_RES.DIConfig_123)
                {
                    //123 -> 0
                    //129 ~ 149 -> 6 ~ 26
                    for (int i = 6; i <= 26; i++)
                    {
                        (DICOnfigPtr + i)->EthercatID = 255;
                        (DICOnfigPtr + i)->Pin = 0;
                        (DICOnfigPtr + i)->Type = 0;
                        (DICOnfigPtr + i)->Chattering = 0;
                        (DICOnfigPtr + i)->Dual = 0;
                    }
                };

                fixed (VEXI_DEFS.REC_DOConfig* DOCOnfigPtr = &srm_REC_SRM_IOConfig_RES.DOConfig_1)
                {
                    //1 -> 0
                    //38 ~ 43 -> 37 ~ 42
                    for (int i = 37; i <= 42; i++)
                    {
                        (DOCOnfigPtr + i)->EthercatID = 255;
                        (DOCOnfigPtr + i)->Pin = 0;
                        (DOCOnfigPtr + i)->Type = 0;
                    }
                };

                fixed (VEXI_DEFS.REC_DOConfig* DOCOnfigPtr = &srm_REC_SRM_IOConfig_RES.DOConfig_44)
                {
                    //44 -> 0
                    //44 ~ 69 -> 0 ~ 25
                    for (int i = 0; i <= 25; i++)
                    {
                        (DOCOnfigPtr + i)->EthercatID = 255;
                        (DOCOnfigPtr + i)->Pin = 0;
                        (DOCOnfigPtr + i)->Type = 0;
                    }
                };
            }
        }

        public unsafe void Process_SRMCtrl_OLDIO()
        {
            if (!IsSRM_New)
            {
                fixed (VEXI_DEFS.REC_DIConfig* DICOnfigPtr = &srm_REC_SRM_IOConfigCtrl.DIConfig_123)
                {
                    //123 -> 0
                    //129 ~ 149 -> 6 ~ 26
                    for (int i = 6; i <= 26; i++)
                    {
                        (DICOnfigPtr + i)->EthercatID = 255;
                        (DICOnfigPtr + i)->Pin = 0;
                        (DICOnfigPtr + i)->Type = 0;
                        (DICOnfigPtr + i)->Chattering = 0;
                        (DICOnfigPtr + i)->Dual = 0;
                    }
                };

                fixed (VEXI_DEFS.REC_DOConfig* DOCOnfigPtr = &srm_REC_SRM_IOConfigCtrl.DOConfig_1)
                {
                    //1 -> 0
                    //38 ~ 43 -> 37 ~ 42
                    for (int i = 37; i <= 42; i++)
                    {
                        (DOCOnfigPtr + i)->EthercatID = 255;
                        (DOCOnfigPtr + i)->Pin = 0;
                        (DOCOnfigPtr + i)->Type = 0;
                    }
                };

                fixed (VEXI_DEFS.REC_DOConfig* DOCOnfigPtr = &srm_REC_SRM_IOConfigCtrl.DOConfig_44)
                {
                    //44 -> 0
                    //44 ~ 69 -> 0 ~ 25
                    for (int i = 0; i <= 25; i++)
                    {
                        (DOCOnfigPtr + i)->EthercatID = 255;
                        (DOCOnfigPtr + i)->Pin = 0;
                        (DOCOnfigPtr + i)->Type = 0;
                    }
                };
            }
        }


        public unsafe void Process_IO_CFG_Load(byte[] Data)
        {
            srm_REC_SRM_IOConfig_RES = (VEXI_DEFS.SRM_IOConfig)Global_Class.UTIL_BytesToStructure(Data, Data.Length, typeof(VEXI_DEFS.SRM_IOConfig));
            if (Data.Length <= 844)
            {
                IsSRM_New = false;
                Process_SRMSt_OLDIO();
            } else
            {
                IsSRM_New = true;
            }


            form_Main.SRM_ToTalFile.FileName = saveFileDialog1.FileName;
            if (form_Main.SRM_ToTalFile.Write_IO_CFG(srm_REC_SRM_IOConfig_RES))
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
            form_Main.COMMDataManager.ADD_TxUserZeroData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A1, (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_REC_PARAMReq)));
            Want_Data = true;
        }

        public unsafe void Process_CTRL_PARAM_CFG_Load(byte[] Data)
        {
            srm_CtrlParam_RES = (VEXI_DEFS.TSRM_CTRLParamRes)Global_Class.UTIL_BytesToStructure(Data, typeof(VEXI_DEFS.TSRM_CTRLParamRes));

            srm_CtrlParam_CTRL.CtrlFlag[0] = 0x80;
            srm_CtrlParam_CTRL.CtrlFlag[1] = 0x00;
            srm_CtrlParam_CTRL.CtrlFlag[2] = 0x00;
            srm_CtrlParam_CTRL.CtrlFlag[3] = 0x00;
            srm_CtrlParam_CTRL.CtrlFlag[4] = 0x00;
            srm_CtrlParam_CTRL.ParamItemsRec = srm_CtrlParam_RES;

            form_Main.SRM_ToTalFile.FileName = saveFileDialog1.FileName;
            if (form_Main.SRM_ToTalFile.Write_SRM_CTRL_PARAM(srm_CtrlParam_CTRL))
            {
                ProcessStep = 35;
            }
            else
            {
                ProcessStep = 36;
            }

        }

        private void Request_LIFT_PARAM_CFG()
        {
            form_Main.COMMDataManager.ADD_TxUserZeroData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A5, (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_REC_PARAMReq)));
            Want_Data = true;
        }

        public unsafe void Process_LIFT_PARAM_CFG_Load(byte[] Data)
        {
            srm_LiftParam_RES = (VEXI_DEFS.TSRM_LiftParamRes)Global_Class.UTIL_BytesToStructure(Data, typeof(VEXI_DEFS.TSRM_LiftParamRes));
            
            srm_LiftParam_Ctrl.CtrlFlag[0] = 0x80;
            srm_LiftParam_Ctrl.CtrlFlag[1] = 0x00;
            srm_LiftParam_Ctrl.CtrlFlag[2] = 0x00;
            srm_LiftParam_Ctrl.CtrlFlag[3] = 0x00;
            srm_LiftParam_Ctrl.CtrlFlag[4] = 0x00;
            srm_LiftParam_Ctrl.ParamItemsRec = srm_LiftParam_RES;

            form_Main.SRM_ToTalFile.FileName = saveFileDialog1.FileName;
            if (form_Main.SRM_ToTalFile.Write_SRM_LIFT_PARAM(srm_LiftParam_Ctrl))
            {
                ProcessStep = 45;
            }
            else
            {
                ProcessStep = 46;
            }
        }

        private void Request_DRIVE_PARAM_CFG()
        {
            form_Main.COMMDataManager.ADD_TxUserZeroData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A3, (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_REC_PARAMReq)));
            Want_Data = true;
        }

        public unsafe void Process_DRIVE_PARAM_CFG_Load(byte[] Data)
        {
            srm_DriveParam_RES = (VEXI_DEFS.TSRM_DriveParamRes)Global_Class.UTIL_BytesToStructure(Data, typeof(VEXI_DEFS.TSRM_DriveParamRes));

            srm_DriveParam_CTRL.CtrlFlag[0] = 0x80;
            srm_DriveParam_CTRL.CtrlFlag[1] = 0x00;
            srm_DriveParam_CTRL.CtrlFlag[2] = 0x00;
            srm_DriveParam_CTRL.CtrlFlag[3] = 0x00;
            srm_DriveParam_CTRL.CtrlFlag[4] = 0x00;
            srm_DriveParam_CTRL.ParamItemsRec = srm_DriveParam_RES;

            form_Main.SRM_ToTalFile.FileName = saveFileDialog1.FileName;
            if (form_Main.SRM_ToTalFile.Write_SRM_DRIVE_PARAM(srm_DriveParam_CTRL))
            {
                ProcessStep = 55;
            }
            else
            {
                ProcessStep = 56;
            }
        }

        private void Request_FORK_PARAM_CFG()
        {
            form_Main.COMMDataManager.ADD_TxUserZeroData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A7, (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_REC_PARAMReq)));
            Want_Data = true;
        }

        public unsafe void Process_FORK_PARAM_CFG_Load(byte[] Data)
        {
            srm_ForkParam_RES = (VEXI_DEFS.TSRM_ForkParamRes)Global_Class.UTIL_BytesToStructure(Data, typeof(VEXI_DEFS.TSRM_ForkParamRes));

            srm_ForkParam_CTRL.CtrlFlag[0] = 0x80;
            srm_ForkParam_CTRL.CtrlFlag[1] = 0x00;
            srm_ForkParam_CTRL.CtrlFlag[2] = 0x00;
            srm_ForkParam_CTRL.CtrlFlag[3] = 0x00;
            srm_ForkParam_CTRL.CtrlFlag[4] = 0x00;
            srm_ForkParam_CTRL.ParamItemsRec = srm_ForkParam_RES;

            form_Main.SRM_ToTalFile.FileName = saveFileDialog1.FileName;
            if (form_Main.SRM_ToTalFile.Write_SRM_ForkParamCTRL(srm_ForkParam_CTRL))
            {
                ProcessStep = 65;
            }
            else
            {
                ProcessStep = 66;
            }
        }

        private unsafe void Request_NoUseRack_CFG()
        {
            form_Main.COMMDataManager.ADD_TxUserZeroData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_9C, (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_REC_NoUseRackReq)));
            Want_Data = true;
        }

        public unsafe void Process_NoUseRack_CFG_Load(byte[] Data)
        {
            srm_NoUseRack_RES = (VEXI_DEFS.TSRM_NoUseRack)Global_Class.UTIL_BytesToStructure(Data, typeof(VEXI_DEFS.TSRM_NoUseRack));

            form_Main.SRM_ToTalFile.FileName = saveFileDialog1.FileName;
            if (form_Main.SRM_ToTalFile.Write_SRM_NoUseRack(srm_NoUseRack_RES))
            {
                ProcessStep = 75;
            }
            else
            {
                ProcessStep = 76;
            }
        }

        private unsafe void Request_SpecailRack_CFG()
        {
            form_Main.COMMDataManager.ADD_TxUserZeroData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_9E, (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_REC_SpecialRackReq)));
            Want_Data = true;
        }

        public unsafe void Process_SpecialRack_CFG_Load(byte[] Data)
        {
            srm_SpecialRack_RES = (VEXI_DEFS.TSRM_SpecialRack)Global_Class.UTIL_BytesToStructure(Data, typeof(VEXI_DEFS.TSRM_SpecialRack));

            form_Main.SRM_ToTalFile.FileName = saveFileDialog1.FileName;
            if (form_Main.SRM_ToTalFile.Write_SRM_SpecialRack(srm_SpecialRack_RES))
            {
                ProcessStep = 85;
            }
            else
            {
                ProcessStep = 86;
            }
        }

        private unsafe void Request_Station_CFG()
        {
            form_Main.COMMDataManager.ADD_TxUserZeroData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_98, 0);
            Want_Data = true;
        }

        public void Process_Station_CFG_Load(byte[] Data)
        {
            srm_StationParam_RES = (VEXI_DEFS.TSRM_StationParam)Global_Class.UTIL_BytesToStructure(Data, typeof(VEXI_DEFS.TSRM_StationParam));

            form_Main.SRM_ToTalFile.FileName = saveFileDialog1.FileName;
            if (form_Main.SRM_ToTalFile.Write_SRM_StationParam(srm_StationParam_RES))
            {
                ProcessStep = 105;
            }
            else
            {
                ProcessStep = 106;
            }
        }

        private void Request_CellPosition(byte DataType, byte Startindex, byte Endindex)
        {
            byte[] data = new byte[4];
            data[0] = 1; //SRM
            data[1] = DataType;
            data[2] = Startindex;
            data[3] = Endindex;
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_94, data);
            Want_Data = true;
        }

        public unsafe void Process_CellPosition_Load(byte[] Data)
        {
            //가변길이 데이터로 datas.Length 를 넘겨서 그만큼만 copy 한다
            srm_CellPosition_RES = (VEXI_DEFS.TSRM_CellPositionRES)Global_Class.UTIL_BytesToStructure(Data, Data.Length, typeof(VEXI_DEFS.TSRM_CellPositionRES));

            if ((srm_CellPosition_RES.Header.DataType == 1) || (srm_CellPosition_RES.Header.DataType == 2)) //LEFT
            {
                if ((srm_CellPosition_RES.Header.RackType != 1) ||
                (srm_CellPosition_RES.Header.BayCount > 256) ||
                (srm_CellPosition_RES.Header.LevelCount > 128))
                {
                    //정보 문제로 저장 불가
                    ProcessStep = 119;
                    return;
                }
            }
            else
            {
                if ((srm_CellPosition_RES.Header.RackType != 1) ||
                    (srm_CellPosition_RES.Header.BayCount > 256) ||
                    (srm_CellPosition_RES.Header.LevelCount > 128))
                {
                    //정보 문제로 저장 불가
                    ProcessStep = 129;
                    return;
                }
            }

            if (srm_CellPosition_RES.Header.DataType == 1) //Bay
            {

                srm_BayLPosition_RES = (VEXI_DEFS.TSRM_CellPositionRES)Global_Class.UTIL_BytesToStructure(Data, Data.Length, typeof(VEXI_DEFS.TSRM_CellPositionRES));


                if (srm_BayLPosition_RES.Header.BayCount > 0)
                {
                    if (srm_BayLPosition_RES.Header.EndNo > (srm_BayLPosition_RES.Header.BayCount - 1))
                    {
                        srm_BayLPosition_RES.Header.EndNo = (byte)(srm_BayLPosition_RES.Header.BayCount - 1);
                    }
                }
                else
                {
                    srm_BayLPosition_RES.Header.EndNo = 0;
                }

                ProcessStep = 115;

            }
            else if (srm_CellPosition_RES.Header.DataType == 2) //Level
            {
                srm_LevelLPosition_RES = (VEXI_DEFS.TSRM_CellPositionRES)Global_Class.UTIL_BytesToStructure(Data, Data.Length, typeof(VEXI_DEFS.TSRM_CellPositionRES));

                if (srm_LevelLPosition_RES.Header.LevelCount > 0)
                {
                    if (srm_LevelLPosition_RES.Header.EndNo > (srm_LevelLPosition_RES.Header.LevelCount - 1))
                    {
                        srm_LevelLPosition_RES.Header.EndNo = (byte)(srm_LevelLPosition_RES.Header.LevelCount - 1);
                    }
                }
                else
                {
                    srm_LevelLPosition_RES.Header.EndNo = 0;
                }


                srm_CellPosition_CTRL.Header.BayCount = srm_BayLPosition_RES.Header.BayCount;
                srm_CellPosition_CTRL.Header.LevelCount = srm_BayLPosition_RES.Header.LevelCount;
                srm_CellPosition_CTRL.Header.RackType = srm_BayLPosition_RES.Header.RackType;
                srm_CellPosition_CTRL.Header.DataType = srm_BayLPosition_RES.Header.DataType;
                srm_CellPosition_CTRL.Header.StartNo = srm_BayLPosition_RES.Header.StartNo;
                srm_CellPosition_CTRL.Header.EndNo = srm_BayLPosition_RES.Header.EndNo;
                for (int Loop = 0; Loop < 256; Loop++)
                {
                    srm_CellPosition_CTRL.Position[Loop] = srm_BayLPosition_RES.Position[Loop];
                }

                form_Main.SRM_ToTalFile.FileName = saveFileDialog1.FileName;
                if (form_Main.SRM_ToTalFile.Write_SRM_BayLPositionCTRL(srm_CellPosition_CTRL))
                {
                    srm_CellPosition_CTRL.Header.RackType = srm_LevelLPosition_RES.Header.RackType;
                    srm_CellPosition_CTRL.Header.DataType = srm_LevelLPosition_RES.Header.DataType;
                    srm_CellPosition_CTRL.Header.StartNo = srm_LevelLPosition_RES.Header.StartNo;
                    srm_CellPosition_CTRL.Header.EndNo = srm_LevelLPosition_RES.Header.EndNo;
                    for (int Loop = 0; Loop < 256; Loop++)
                    {
                        srm_CellPosition_CTRL.Position[Loop] = srm_LevelLPosition_RES.Position[Loop];
                    }

                    if (form_Main.SRM_ToTalFile.Write_SRM_LevelLPositionCTRL(srm_CellPosition_CTRL))
                    {
                        ProcessStep = 118;
                    } else
                    {
                        ProcessStep = 119;
                    }
                }
                else
                {
                    ProcessStep = 119;
                }
            } else if (srm_CellPosition_RES.Header.DataType == 3) //Bay
            {

                srm_BayRPosition_RES = (VEXI_DEFS.TSRM_CellPositionRES)Global_Class.UTIL_BytesToStructure(Data, Data.Length, typeof(VEXI_DEFS.TSRM_CellPositionRES));


                if (srm_BayRPosition_RES.Header.BayCount > 0)
                {
                    if (srm_BayRPosition_RES.Header.EndNo > (srm_BayRPosition_RES.Header.BayCount - 1))
                    {
                        srm_BayRPosition_RES.Header.EndNo = (byte)(srm_BayRPosition_RES.Header.BayCount - 1);
                    }
                }
                else
                {
                    srm_BayRPosition_RES.Header.EndNo = 0;
                }

                ProcessStep = 125;

            }
            else if (srm_CellPosition_RES.Header.DataType == 4) //Level
            {
                srm_LevelRPosition_RES = (VEXI_DEFS.TSRM_CellPositionRES)Global_Class.UTIL_BytesToStructure(Data, Data.Length, typeof(VEXI_DEFS.TSRM_CellPositionRES));

                if (srm_LevelRPosition_RES.Header.LevelCount > 0)
                {
                    if (srm_LevelRPosition_RES.Header.EndNo > (srm_LevelRPosition_RES.Header.LevelCount - 1))
                    {
                        srm_LevelRPosition_RES.Header.EndNo = (byte)(srm_LevelRPosition_RES.Header.LevelCount - 1);
                    }
                }
                else
                {
                    srm_LevelRPosition_RES.Header.EndNo = 0;
                }


                srm_CellPosition_CTRL.Header.BayCount = srm_BayRPosition_RES.Header.BayCount;
                srm_CellPosition_CTRL.Header.LevelCount = srm_BayRPosition_RES.Header.LevelCount;
                srm_CellPosition_CTRL.Header.RackType = srm_BayRPosition_RES.Header.RackType;
                srm_CellPosition_CTRL.Header.DataType = srm_BayRPosition_RES.Header.DataType;
                srm_CellPosition_CTRL.Header.StartNo = srm_BayRPosition_RES.Header.StartNo;
                srm_CellPosition_CTRL.Header.EndNo = srm_BayRPosition_RES.Header.EndNo;
                for (int Loop = 0; Loop < 256; Loop++)
                {
                    srm_CellPosition_CTRL.Position[Loop] = srm_BayRPosition_RES.Position[Loop];
                }

                form_Main.SRM_ToTalFile.FileName = saveFileDialog1.FileName;
                if (form_Main.SRM_ToTalFile.Write_SRM_BayRPositionCTRL(srm_CellPosition_CTRL))
                {
                    srm_CellPosition_CTRL.Header.RackType = srm_LevelRPosition_RES.Header.RackType;
                    srm_CellPosition_CTRL.Header.DataType = srm_LevelRPosition_RES.Header.DataType;
                    srm_CellPosition_CTRL.Header.StartNo = srm_LevelRPosition_RES.Header.StartNo;
                    srm_CellPosition_CTRL.Header.EndNo = srm_LevelRPosition_RES.Header.EndNo;
                    for (int Loop = 0; Loop < 256; Loop++)
                    {
                        srm_CellPosition_CTRL.Position[Loop] = srm_LevelRPosition_RES.Position[Loop];
                    }

                    if (form_Main.SRM_ToTalFile.Write_SRM_LevelRPositionCTRL(srm_CellPosition_CTRL))
                    {
                        ProcessStep = 128;
                    }
                    else
                    {
                        ProcessStep = 129;
                    }
                }
                else
                {
                    ProcessStep = 129;
                }
            }

        }

        private void Request_CellOffset(UInt16 Startindex)
        {
            srm_CellOffset_Req.DevType = 1;
            srm_CellOffset_Req.ReqIndex = Startindex;
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_96, srm_CellOffset_Req);
            Want_Data = true;
        }

        public unsafe void Process_CellOffset_Load(byte[] Data)
        {
            srm_CellOffset_RES = (VEXI_DEFS.TSRM_CellOffset)Global_Class.UTIL_BytesToStructure(Data, Data.Length, typeof(VEXI_DEFS.TSRM_CellOffset));

            if ((srm_CellOffset_RES.Header.DevType != 1))
            {
                //정보 문제로 저장 불가
                ProcessStep = 136;
                return;
            }

            CellOffset_Total.DevType = srm_CellOffset_RES.Header.DevType;
            CellOffset_Total.TotalCount = srm_CellOffset_RES.Header.TotalCount;

            int totalindex;
            if (srm_CellOffset_RES.Header.ItemCount > 0)
            {
                fixed (VEXI_DEFS.TSRM_CellOffsetRec* ptr_1 = &srm_CellOffset_RES.SRM_CellOffsetRec)
                {
                    for (int i = 0; i <= srm_CellOffset_RES.Header.ItemCount; i++)
                    {
                        totalindex = srm_CellOffset_RES.Header.Nowindex + i;

                        CellOffset_Total.SRM_CellOffsetRec[totalindex].Bay = (ptr_1 + i)->Bay;
                        CellOffset_Total.SRM_CellOffsetRec[totalindex].Level = (ptr_1 + i)->Level;
                        CellOffset_Total.SRM_CellOffsetRec[totalindex].Left_Travel_Offset = (ptr_1 + i)->Left_Travel_Offset;
                        CellOffset_Total.SRM_CellOffsetRec[totalindex].Left_Lift_Offset = (ptr_1 + i)->Left_Lift_Offset;
                        CellOffset_Total.SRM_CellOffsetRec[totalindex].Left_Fork_Offset = (ptr_1 + i)->Left_Fork_Offset;
                        CellOffset_Total.SRM_CellOffsetRec[totalindex].Right_Travel_Offset = (ptr_1 + i)->Right_Travel_Offset;
                        CellOffset_Total.SRM_CellOffsetRec[totalindex].Right_Lift_Offset = (ptr_1 + i)->Right_Lift_Offset;
                        CellOffset_Total.SRM_CellOffsetRec[totalindex].Right_Fork_Offset = (ptr_1 + i)->Right_Fork_Offset;
                    }
                }
            }

            if ((srm_CellOffset_RES.Header.Nowindex + srm_CellOffset_RES.Header.ItemCount) < srm_CellOffset_RES.Header.TotalCount)
            {
                RetryCount = 0;
                ProcessStepTime = DateTime.Now;
                Request_CellOffset((UInt16)(srm_CellOffset_RES.Header.Nowindex + srm_CellOffset_RES.Header.ItemCount));
            }
            else
            {
                form_Main.SRM_ToTalFile.FileName = saveFileDialog1.FileName;
                if (form_Main.SRM_ToTalFile.Write_SRM_OFFSET_CFG(CellOffset_Total))
                {
                    ProcessStep = 135;
                }
                else
                {
                    ProcessStep = 136;
                }
            }
        }
    }

}
