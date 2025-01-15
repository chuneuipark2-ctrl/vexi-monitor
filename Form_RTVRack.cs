using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Runtime.InteropServices;   //DllImport
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Collections.Generic;


namespace VEXI
{
    public partial class Form_RTVRack : Form
    {
        public Form_Main form_Main;

        private ListViewNumberColumnSorter ColumnSorter;

        private static VEXI_DEFS.TRTV_PositionSetParam rtv_PositionParam_Res;
        private static VEXI_DEFS.TRTV_PositionSetParam rtv_PositionParam_CTRL;
        private static VEXI_DEFS.TRTV_PositionSetCTRLRes rtv_PositionSet_CtrlRes;

        private static VEXI_DEFS.TRTV_SpeedAreaGroupParam rtv_SpeedAreaGroupParam_Res;
        private static VEXI_DEFS.TRTV_SpeedAreaGroupParam rtv_SpeedAreaGroupParam_CTRL;
        private static VEXI_DEFS.TRTV_SpeedAreaGroupCTRLRes rtv_SpeedAreaGroupSet_CtrlRes;

        private static VEXI_DEFS.TRTV_StationParam rtv_StationParam_Res;
        private static VEXI_DEFS.TRTV_StationParam rtv_StationParam_CTRL;
        private static VEXI_DEFS.TRTV_StationParamCTRLRes rtv_StationParam_CTRLRes;


        private DateTime RxDateTime;
        private byte Retrycount = 0;


        private bool IsIn_Position;
        private bool IsIn_SpeedArea;
        private bool IsIn_Stationinfo;

        private ListViewItem lv_Position_Item;
        private ListViewItem lv_SpeedArea_Item;
        private ListViewItem lv_Station_Item;


        private static Control[] SpeedArea_CtrlBox;
        private static Control[] Station_CtrlBox;

        public Form_RTVRack()
        {
            InitializeComponent();

            ColumnSorter = new ListViewNumberColumnSorter();

            SpeedArea_CtrlBox = new Control[] { null, 
                                                cbGroupType, 
                                                ed_Area_Start, 
                ed_Area_End, 
                ed_Area_MaxSpeed,
                ed_Area_StopDistance,
                ed_Area_StartDistance,
                null, 
                null, 
                cb_Area_Sensor, 
                cb_Area_Region1, 
                cb_Area_Region2, 
                cb_Area_Region3, 
                cbStopSenserUse, 
                cbDeSpeedSenserUse, 
                cbFrontDeSpeedSenserUse, 
                cbRearDeSpeedSenserUse};
            Station_CtrlBox = new Control[] { null, ed_Station_PositionID, ed_Station_InterloackCommID, cb_Station_Output, cb_Station_Input, cb_Station_Direct};

            IsIn_Position = false;
            IsIn_SpeedArea = false;
            IsIn_Stationinfo = false;

        }

        private void Form_RTVRack_Load(object sender, EventArgs e)
        {
            if (this.IsMdiChild)
            {
                form_Main = (Form_Main)this.MdiParent;
            }
            else
            {
                form_Main = (Form_Main)this.Owner;
            }
            Init_RTVPosition();
        }

        #region 컴포넌트 이벤트

        private unsafe void btn_LoadTotalFile2_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();
            
            openFileDialog1.Filter = "*.RTVcfg|*.RTVCFG";

            if (openFileDialog1.FileName == "")
            {
                openFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                form_Main.RTV_ToTalFile.FileName = openFileDialog1.FileName;
                if (form_Main.RTV_ToTalFile.Read_RTV_PositionParam(ref rtv_PositionParam_CTRL))
                {
                    rtv_PositionParam_Res = rtv_PositionParam_CTRL;

                    Init_RTVPosition();
                    Display_RTVPosition();

                    IsIn_Position = true;
                    Enable_Btn();
                }
                else
                {
                    MessageBox.Show("해당 파일안에 [" + tab_Position.Text + "] 데이터가 없습니다");
                }


                //if (form_Main.RTV_ToTalFile.Read_RTV_SpeedAreaParam(ref rtv_SpeedAreaGroupParam_CTRL))
                //{
                //    rtv_SpeedAreaGroupParam_Res = rtv_SpeedAreaGroupParam_CTRL;

                //    Init_RTVSpeedArea();
                //    Display_RTVSpeedArea();

                //    IsIn_SpeedArea = true;
                //    Enable_Btn();
                //}
                //else
                //{
                //    //MessageBox.Show("해당 파일안에 [" + tab_SpeedArea.Text + "] 데이터가 없습니다");
                //}


                //if (form_Main.RTV_ToTalFile.Read_RTV_StationParam(ref rtv_StationParam_CTRL))
                //{
                //    rtv_StationParam_Res = rtv_StationParam_CTRL;

                //    Init_RTVStationParam();
                //    Display_RTVStationParam();

                //    IsIn_Stationinfo = true;

                //    Enable_Btn();
                //}
                //else
                //{
                //    //MessageBox.Show("해당 파일안에 [" + tab_Station.Text + "] 데이터가 없습니다");
                //}
            }
        }

        private unsafe void btn_SaveTotalFile2_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();

            //(lv_Position_Set.Items.Count == 0) 이 상황이 없긴 하다
            if (lv_Position_Set.Items.Count == 0)
            {
                if (form_Main.GlobalObj.MsgBox_Confirm_YN("위치 정보가 작성되지 않았습니다. 이대로 저장하시겠습니까?"))
                {
                }
                else
                {
                    return;
                }

            }

            int check_Result = Check_PositionValue();
            if (check_Result != -1)
            {
                if (form_Main.GlobalObj.MsgBox_Confirm_YN("위치 정보 값이 범위를 벗어났거나 오름차순으로 정의되지 않았습니다. 이대로 저장하시겠습니까?"))
                {
                } else
                {
                    lv_Position_Set.SelectedItems.Clear();
                    lv_Position_Set.Items[check_Result].Selected = true;
                    return;
                }
            }

            saveFileDialog1.Filter = "*.RTVcfg|*.RTVCFG";

            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
            }

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                form_Main.RTV_ToTalFile.FileName = saveFileDialog1.FileName;

                Ctrl_Position(true);

                
                form_Main.RTV_ToTalFile.Write_RTV_PositionParam(rtv_PositionParam_CTRL);
            }
        }

        private unsafe void btn_LoadTotalFile3_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();
            

            openFileDialog1.Filter = "*.RTVcfg|*.RTVCFG";

            if (openFileDialog1.FileName == "")
            {
                openFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                form_Main.RTV_ToTalFile.FileName = openFileDialog1.FileName;
                if (form_Main.RTV_ToTalFile.Read_RTV_SpeedAreaParam(ref rtv_SpeedAreaGroupParam_CTRL))
                {
                    rtv_SpeedAreaGroupParam_Res = rtv_SpeedAreaGroupParam_CTRL;

                    Init_RTVSpeedArea();
                    Display_RTVSpeedArea();

                    IsIn_SpeedArea = true;
                    Enable_Btn();
                }
                else
                {
                    MessageBox.Show("해당 파일안에 [" + tab_SpeedArea.Text + "] 데이터가 없습니다");
                }
            }
        }

        private unsafe void btn_SaveTotalFile3_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();

            if ((lv_SpeedArea.Items.Count == 0))
            {
                if (form_Main.GlobalObj.MsgBox_Confirm_YN("구간 정보가 작성되지 않았습니다. 이대로 저장하시겠습니까?"))
                {
                }
                else
                {
                    return;
                }

            }


            Sort_SpeedAreaLv();

            int check_Result = Check_AreaSpeedValue_Err();
            if (check_Result != -1)
            {
                if (form_Main.GlobalObj.MsgBox_Confirm_YN("구간 값이 범위를 벗어났거나 완전히 작성되지 않았습니다. 이대로 저장하시겠습니까?"))
                {
                    
                }
                else
                {
                    lv_Position_Set.SelectedItems.Clear();
                    lv_Position_Set.Items[check_Result].Selected = true;
                    return;
                }
            } else
            {
                check_Result = Check_AreaSpeedValue_Warn();
                if (check_Result != 0)
                {
                    if (form_Main.GlobalObj.MsgBox_Confirm_YN("곡선 구간 속도가 60m/min 이상으로 설정되었습니다. 이대로 저장하시겠습니까?"))
                    {
                    }
                    else
                    {
                        lv_Position_Set.SelectedItems.Clear();
                        lv_Position_Set.Items[check_Result].Selected = true;
                        return;
                    }
                }
            }

            saveFileDialog1.Filter = "*.RTVcfg|*.RTVCFG";

            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
            }

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                form_Main.RTV_ToTalFile.FileName = saveFileDialog1.FileName;

                Ctrl_SpeedArea(true);


                form_Main.RTV_ToTalFile.Write_RTV_SpeedAreaParam(rtv_SpeedAreaGroupParam_CTRL);

            }
        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hide_AllEdit();
            this.Text = "RTV 위치 설정(" + tabControl1.SelectedTab.Text + ")";
        }

        #endregion

        #region 기능 함수
        public unsafe void Ctrl_SpeedArea(bool IsFileSave)
        {
            byte TmpCount = (byte)lv_SpeedArea.Items.Count;
            byte Tmpbyte = 0;

            //파일 저장일 경우에는 lv_SpeedArea.Items.Count = 0 을 허용하기 때문에 0 일때에 대한 처리도 해준다.
            if (TmpCount > 0)
            {
                fixed (VEXI_DEFS.TRTV_SpeedAreaGroupParam* ptr = &rtv_SpeedAreaGroupParam_CTRL)
                {
                    Global_Class.UTIL_Byteptr_clear((byte*)ptr, Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_SpeedAreaGroupParam)));

                    ptr->Header.AreaCount = (byte)TmpCount;

                    ptr->Header.RTV_Width = (UInt16)Global_Class.UTIL_StrToIntDef(ed_RTV_Width.Text, 0);
                    ptr->Header.LineArea_StopWidth = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Linear_Stop.Text, 0);
                    ptr->Header.LineArea_StartWidth = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Linear_Start.Text, 0);
                    ptr->Header.RoundArea_StopWidth = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Round_Stop.Text, 0);
                    ptr->Header.RoundArea_StartWidth = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Round_Start.Text, 0);
                    
                    ptr->Header.FrontRTVPosition_TimeOut1 = (byte)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_FrontRTVPosition_TimeOut1.Text, (float)0.5) * 10);
                    ptr->Header.FrontRTVPosition_TimeOut2 = (byte)Global_Class.UTIL_StrToIntDef(ed_FrontRTVPosition_TimeOut2.Text, 5);
                    ptr->Header.StopbyLiadrTimeOut = (UInt16)Global_Class.UTIL_StrToIntDef(ed_StopbyLiadrTimeOut.Text, 0);
                    ptr->Header.Stop_OffsetTime = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Stop_OffsetTime.Text, (float)0.5) * 100);
                    ptr->Header.Stop_OffsetMaxDistance = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Stop_OffsetMaxDistance.Text, 0);
                    ptr->Header.Lidar_Off_SafetyTime = (byte)Global_Class.UTIL_StrToIntDef(ed_Lidar_Off_SafetyTime.Text, 0);

                    if (cb_RTV_DriveStopSensor.SelectedIndex >= 0) ptr->Header.DriveStopSensor = (byte)cb_RTV_DriveStopSensor.SelectedIndex;
                    if (cb_RTV_DriveDecelSensor.SelectedIndex >= 0) ptr->Header.DriveDecelSensor = (byte)cb_RTV_DriveDecelSensor.SelectedIndex;

                    ptr->Header.ErrRef_LowSpeedValue = (byte)Global_Class.UTIL_StrToIntDef(ed_ErrRef_LowSpeedValue.Text, 100);
                    ptr->Header.ErrRef_LowSpeedMin = (UInt16)Global_Class.UTIL_StrToIntDef(ed_ErrRef_LowSpeedMin.Text, 40);

                    fixed (VEXI_DEFS.TRTV_SpeedAreaGroupConfigRec* subptr = &rtv_SpeedAreaGroupParam_CTRL.Area1)
                    {

                        for (int i = 0; i < TmpCount; i++)
                        {
                            Tmpbyte = 0;
                            if (lv_SpeedArea.Items[i].SubItems[1].Text == "직선 전진감속")
                            {
                                Tmpbyte |= 0x01;
                            }
                            else if
                              (lv_SpeedArea.Items[i].SubItems[1].Text == "직선 후진감속")
                            {
                                Tmpbyte |= 0x02;
                            }
                            else if (lv_SpeedArea.Items[i].SubItems[1].Text == "곡선")
                            {
                                Tmpbyte |= 0x03;
                            }

                            if (lv_SpeedArea.Items[i].SubItems[13].Text == "사용") // 정지 거리
                            {
                                Tmpbyte |= 0x04;
                            }
                            if (lv_SpeedArea.Items[i].SubItems[14].Text == "사용")
                            {
                                Tmpbyte |= 0x08;
                            }
                            if (lv_SpeedArea.Items[i].SubItems[15].Text == "사용")
                            {
                                Tmpbyte |= 0x10;
                            }
                            if (lv_SpeedArea.Items[i].SubItems[16].Text == "사용") // 후진감속
                            {
                                Tmpbyte |= 0x20;
                            }
                            (subptr + i)->Area_Type = Tmpbyte;

                            (subptr + i)->Start_MM = Global_Class.UTIL_StrToUInt32Def(lv_SpeedArea.Items[i].SubItems[2].Text, 0);
                            (subptr + i)->End_MM = Global_Class.UTIL_StrToUInt32Def(lv_SpeedArea.Items[i].SubItems[3].Text, 0);
                            (subptr + i)->MaxSpeed = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(lv_SpeedArea.Items[i].SubItems[4].Text, 10) * 10);
                            (subptr + i)->StopDistance = (UInt16)Global_Class.UTIL_StrToUInt32Def(lv_SpeedArea.Items[i].SubItems[5].Text, 0);
                            (subptr + i)->StartDistance = (UInt16)Global_Class.UTIL_StrToUInt32Def(lv_SpeedArea.Items[i].SubItems[6].Text, 0);


                            (subptr + i)->PrevAreaIndex = (byte)Global_Class.UTIL_StrToIntDef(lv_SpeedArea.Items[i].SubItems[7].Text, 0);
                            (subptr + i)->NextAreaIndex = (byte)Global_Class.UTIL_StrToIntDef(lv_SpeedArea.Items[i].SubItems[8].Text, 0);

                            if (lv_SpeedArea.Items[i].SubItems[9].Text == "OFF")
                            {
                                (subptr + i)->Sensorindex = 0;
                            }
                            else
                            {
                                (subptr + i)->Sensorindex = (byte)Global_Class.UTIL_StrToIntDef(lv_SpeedArea.Items[i].SubItems[9].Text, 0);
                            }

                            Tmpbyte = 0;
                            if (lv_SpeedArea.Items[i].SubItems[10].Text == "적용")
                            {
                                Tmpbyte |= 0x01;
                            }
                            if (lv_SpeedArea.Items[i].SubItems[11].Text == "적용")
                            {
                                Tmpbyte |= 0x02;
                            }
                            if (lv_SpeedArea.Items[i].SubItems[12].Text == "적용")
                            {
                                Tmpbyte |= 0x04;
                            }

                            (subptr + i)->Region = Tmpbyte;
                        }
                    }
                }
            } else
            {
                fixed (VEXI_DEFS.TRTV_SpeedAreaGroupParam* ptr = &rtv_SpeedAreaGroupParam_CTRL)
                {
                    Global_Class.UTIL_Byteptr_clear((byte*)ptr, Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_SpeedAreaGroupParam)));

                    ptr->Header.AreaCount = (byte)0;

                    ptr->Header.RTV_Width = (UInt16)Global_Class.UTIL_StrToIntDef(ed_RTV_Width.Text, 0);
                    ptr->Header.LineArea_StopWidth = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Linear_Stop.Text, 0);
                    ptr->Header.LineArea_StartWidth = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Linear_Start.Text, 0);
                    ptr->Header.RoundArea_StopWidth = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Round_Stop.Text, 0);
                    ptr->Header.RoundArea_StartWidth = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Round_Start.Text, 0);

                    ptr->Header.FrontRTVPosition_TimeOut1 = (byte)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_FrontRTVPosition_TimeOut1.Text, (float)0.5) * 10);
                    ptr->Header.FrontRTVPosition_TimeOut2 = (byte)Global_Class.UTIL_StrToIntDef(ed_FrontRTVPosition_TimeOut2.Text, 5);
                    ptr->Header.StopbyLiadrTimeOut = (UInt16)Global_Class.UTIL_StrToIntDef(ed_StopbyLiadrTimeOut.Text, 0);
                    ptr->Header.Stop_OffsetTime = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Stop_OffsetTime.Text, (float)0.5) * 100);
                    ptr->Header.Stop_OffsetMaxDistance = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Stop_OffsetMaxDistance.Text, 0);
                    ptr->Header.Lidar_Off_SafetyTime = (byte)Global_Class.UTIL_StrToIntDef(ed_Lidar_Off_SafetyTime.Text, 0);

                    if (cb_RTV_DriveStopSensor.SelectedIndex >= 0) ptr->Header.DriveStopSensor = (byte)cb_RTV_DriveStopSensor.SelectedIndex;
                    if (cb_RTV_DriveDecelSensor.SelectedIndex >= 0) ptr->Header.DriveDecelSensor = (byte)cb_RTV_DriveDecelSensor.SelectedIndex;

                    ptr->Header.ErrRef_LowSpeedValue = (byte)Global_Class.UTIL_StrToIntDef(ed_ErrRef_LowSpeedValue.Text, 100);
                    ptr->Header.ErrRef_LowSpeedMin = (UInt16)Global_Class.UTIL_StrToIntDef(ed_ErrRef_LowSpeedMin.Text, 40);

                }
            }


            if (!IsFileSave)
            {
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_9B, rtv_SpeedAreaGroupParam_CTRL);
                form_Main.COMMDataManager.ISPolingDelayStop = true;
                form_Main.COMMDataManager.PolingDelayTxTime = DateTime.Now;
                RxDateTime = DateTime.Now;
            }
        }

        public unsafe void Ctrl_Position(bool IsFileSave)
        {
            byte TmpCount = 0;
            UInt32 TmpValue_1;

            if (lv_Position_Set.Items.Count > 0)
            {
                for (int i = 0; i < lv_Position_Set.Items.Count; i++)
                {
                    TmpValue_1 = Global_Class.UTIL_StrToUInt32Def(lv_Position_Set.Items[i].SubItems[1].Text, 0);
                    if (TmpValue_1 != 0)
                    {
                        TmpCount = (byte)(i + 1);
                    }
                }


                fixed (VEXI_DEFS.TRTV_PositionSetParam* ptr = &rtv_PositionParam_CTRL)
                {
                    Global_Class.UTIL_Byteptr_clear((byte*)ptr, Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_PositionSetParam)));


                    if (cb_RailType.SelectedIndex >= 0) ptr->Header.RailType = (byte)cb_RailType.SelectedIndex;
                    if (cb_Barcode_Direct.SelectedIndex >= 0) ptr->Header.Barcode_Direct = (byte)cb_Barcode_Direct.SelectedIndex;
                    ptr->Header.Barcode_Ref = Global_Class.UTIL_StrToUInt32Def(ed_Barcode_Ref.Text, 0);

                    ptr->Header.Ref_Start = Global_Class.UTIL_StrToUInt32Def(ed_Ref_Start.Text, 0);
                    ptr->Header.Ref_End = Global_Class.UTIL_StrToUInt32Def(ed_Ref_End.Text, 0);

                    if (cb_Home_SpeedType.SelectedIndex >= 0) ptr->Header.Home_SpeedType = (byte)cb_Home_SpeedType.SelectedIndex;
                    ptr->Header.Home_Position = Global_Class.UTIL_StrToUInt32Def(ed_Home_Position.Text, 0);
                    if (cb_Service_SpeedType.SelectedIndex >= 0) ptr->Header.Service_SpeedType = (byte)cb_Service_SpeedType.SelectedIndex;
                    ptr->Header.Service_Position = Global_Class.UTIL_StrToUInt32Def(ed_Service_Position.Text, 0);

                    ptr->Header.PositionCount = (byte)TmpCount;

                    for (int i = 0; i < 200; i++)
                    {
                        if (i <= ptr->Header.PositionCount)
                        {
                            ptr->Position[i] = Global_Class.UTIL_StrToUInt32Def(lv_Position_Set.Items[i].SubItems[1].Text, 0); ;
                        }
                        else
                        {
                            ptr->Position[i] = 0;
                        }
                    }
                }
            } else
            {
                fixed (VEXI_DEFS.TRTV_PositionSetParam* ptr = &rtv_PositionParam_CTRL)
                {
                    Global_Class.UTIL_Byteptr_clear((byte*)ptr, Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_PositionSetParam)));

                    if (cb_RailType.SelectedIndex >= 0) ptr->Header.RailType = (byte)cb_RailType.SelectedIndex;
                    if (cb_Barcode_Direct.SelectedIndex >= 0) ptr->Header.Barcode_Direct = (byte)cb_Barcode_Direct.SelectedIndex;
                    ptr->Header.Barcode_Ref = Global_Class.UTIL_StrToUInt32Def(ed_Barcode_Ref.Text, 0);

                    ptr->Header.Ref_Start = Global_Class.UTIL_StrToUInt32Def(ed_Ref_Start.Text, 0);
                    ptr->Header.Ref_End = Global_Class.UTIL_StrToUInt32Def(ed_Ref_End.Text, 0);

                    if (cb_Home_SpeedType.SelectedIndex >= 0) ptr->Header.Home_SpeedType = (byte)cb_Home_SpeedType.SelectedIndex;
                    ptr->Header.Home_Position = Global_Class.UTIL_StrToUInt32Def(ed_Home_Position.Text, 0);
                    if (cb_Service_SpeedType.SelectedIndex >= 0) ptr->Header.Service_SpeedType = (byte)cb_Service_SpeedType.SelectedIndex;
                    ptr->Header.Service_Position = Global_Class.UTIL_StrToUInt32Def(ed_Service_Position.Text, 0);

                    ptr->Header.PositionCount = (byte)0;
                }
            }

            if (!IsFileSave)
            {
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_95, rtv_PositionParam_CTRL);
                form_Main.COMMDataManager.ISPolingDelayStop = true;
                form_Main.COMMDataManager.PolingDelayTxTime = DateTime.Now;
                RxDateTime = DateTime.Now;
            }
        }

        private unsafe void Ctrl_StationParam(bool IsFileSave)
        {
            byte TmpCount = (byte)lv_StationParam.Items.Count;
            byte Tmpbyte = 0;

            if (TmpCount > 0)
            {
                fixed (VEXI_DEFS.TRTV_StationParam* ptr = &rtv_StationParam_CTRL)
                {
                    Global_Class.UTIL_Byteptr_clear((byte*)ptr, Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_StationParam)));

                    ptr->Header.stationCount = (byte)TmpCount;

                    if (cb_Station_InterlockType.SelectedIndex >= 0) ptr->Header.InterlockType = (byte)cb_Station_InterlockType.SelectedIndex;
                    if (cb_Station_InterlockDirect.SelectedIndex >= 0) ptr->Header.InterlockSensorDirect = (byte)cb_Station_InterlockDirect.SelectedIndex;
                    if (cb_Station_InterlockManual.SelectedIndex >= 0) ptr->Header.Accept_ManualInterlock = (byte)cb_Station_InterlockManual.SelectedIndex;

                    fixed (VEXI_DEFS.TRTV_StationConfigRec* subptr = &rtv_StationParam_CTRL.Station1)
                    {
                        for (int i = 0; i < TmpCount; i++)
                        {

                            Tmpbyte = 0;
                            if (lv_StationParam.Items[i].SubItems[3].Text == "가능")
                            {
                                Tmpbyte |= 0x04;
                            }
                            if (lv_StationParam.Items[i].SubItems[4].Text == "가능")
                            {
                                Tmpbyte |= 0x02;
                            }
                            if (lv_StationParam.Items[i].SubItems[5].Text == "우측")
                            {
                                Tmpbyte |= 0x01;
                            }
                            (subptr + i)->station_Type = Tmpbyte;

                            (subptr + i)->P_ID = (byte)Global_Class.UTIL_StrToIntDef(lv_StationParam.Items[i].SubItems[1].Text, 0);
                            (subptr + i)->InterlockCommID = (byte)Global_Class.UTIL_StrToIntDef(lv_StationParam.Items[i].SubItems[2].Text, 0);
                        }
                    }
                }
            } else
            {
                fixed (VEXI_DEFS.TRTV_StationParam* ptr = &rtv_StationParam_CTRL)
                {
                    Global_Class.UTIL_Byteptr_clear((byte*)ptr, Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_StationParam)));

                    ptr->Header.stationCount = (byte)0;

                    if (cb_Station_InterlockType.SelectedIndex >= 0) ptr->Header.InterlockType = (byte)cb_Station_InterlockType.SelectedIndex;
                    if (cb_Station_InterlockDirect.SelectedIndex >= 0) ptr->Header.InterlockSensorDirect = (byte)cb_Station_InterlockDirect.SelectedIndex;
                    if (cb_Station_InterlockManual.SelectedIndex >= 0) ptr->Header.Accept_ManualInterlock = (byte)cb_Station_InterlockManual.SelectedIndex;
                }
            }

            if (!IsFileSave)
            {
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_99, rtv_StationParam_CTRL);
                form_Main.COMMDataManager.ISPolingDelayStop = true;
                form_Main.COMMDataManager.PolingDelayTxTime = DateTime.Now;
            }
        }

        private void Init_RTVStationParam()
        {
            cb_Station_InterlockType.SelectedIndex = -1;
            cb_Station_InterlockDirect.SelectedIndex = -1;
            cb_Station_InterlockManual.SelectedIndex = -1;

            lv_StationParam.Items.Clear();
        }

        public unsafe void Display_RTVStationParam()
        {
            //Header 부분 갱신
            cb_Station_InterlockType.SelectedIndex = rtv_StationParam_Res.Header.InterlockType;
            cb_Station_InterlockDirect.SelectedIndex = rtv_StationParam_Res.Header.InterlockSensorDirect;
            cb_Station_InterlockManual.SelectedIndex = rtv_StationParam_Res.Header.Accept_ManualInterlock;

            if (rtv_StationParam_Res.Header.stationCount > 0)
            {
                ListViewItem listviewItem;

                fixed (VEXI_DEFS.TRTV_StationConfigRec* Ptr = &rtv_StationParam_Res.Station1)
                {
                    for (int i = 0; i < rtv_StationParam_Res.Header.stationCount; i++)
                    {
                        listviewItem = lv_StationParam.Items.Add(string.Format("{0}", i + 1));

                        listviewItem.SubItems.Add(string.Format("{0}", (Ptr + i)->P_ID));
                        listviewItem.SubItems.Add(string.Format("{0}", (Ptr + i)->InterlockCommID));

                        if (((Ptr + i)->station_Type & 0x04) == 0)
                        {
                            listviewItem.SubItems.Add("불가");
                        }
                        else
                        {
                            listviewItem.SubItems.Add("가능");
                        }

                        if (((Ptr + i)->station_Type & 0x02) == 0)
                        {
                            listviewItem.SubItems.Add("불가");
                        }
                        else
                        {
                            listviewItem.SubItems.Add("가능");
                        }

                        if (((Ptr + i)->station_Type & 0x01) == 0)
                        {
                            listviewItem.SubItems.Add("좌측");
                        }
                        else
                        {
                            listviewItem.SubItems.Add("우측");
                        }
                    }
                }
            }
        }

        public void Display_RTVStatus()
        {
            if ((form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) && (form_Main.COMMDataManager.CommSt != 0))
            {
                lbl_Drive_Position.Text = String.Format("{0}", form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt.Drive_DisPosition.Now_Position);
            }
            else
            {
                lbl_Drive_Position.Text = "---";
            }
        }
        public unsafe void Display_RTVStationParam(byte[] datas)
        {
            //요청 버튼을 누를 때도 items.clear를 하지만 응답을 받을 때도 한다
            //요청 버튼 누를때 items.clear는 무응답을 나타낼 수 있고
            //응답에서의 items.clear는 응답이 오기전에 요청 버튼을 연속 누르게 되었을 때
            //연속으로 응답이 오면서 중복 표시가 될 수 있는 것을 막아준다
            lv_StationParam.Items.Clear();

            rtv_StationParam_Res = (VEXI_DEFS.TRTV_StationParam)Global_Class.UTIL_BytesToStructure(datas, datas.Length, typeof(VEXI_DEFS.TRTV_StationParam));

            Display_RTVStationParam();

            IsIn_Stationinfo = true;
            Enable_Btn();
        }


        public unsafe void Process_RTVStationParamCtrlRes(byte[] datas)
        {
            RxDateTime = DateTime.Now;


            rtv_StationParam_CTRLRes = (VEXI_DEFS.TRTV_StationParamCTRLRes)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TRTV_StationParamCTRLRes));

            if (rtv_StationParam_CTRLRes.CtrlResult != ConstClass.CODE_ACK)
            {
                switch (rtv_StationParam_CTRLRes.NackReason)
                {
                    case 1: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 스테이션 개수 범위 초과", "W"); break;
                    case 2: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 스테이션 " + rtv_StationParam_CTRLRes.FailNo.ToString() + "인터록 설정 이상", "W"); break;
                    case 3: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 스테이션 " + rtv_StationParam_CTRLRes.FailNo.ToString() + "스테이션 타입 이상", "W"); break;
                    case 9: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 스테이션 " + rtv_StationParam_CTRLRes.FailNo.ToString() + "인터록 센서 번호 설정 이상", "W"); break;
                    case 10: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 셋업모드 아님", "W"); break;
                    default: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 0x" + string.Format("{0:X2}", rtv_StationParam_CTRLRes.NackReason), "W"); break;
                }
            }
        }

        public unsafe void Process_RTVSpeedAreaCtrlRes(byte[] datas)
        {
            RxDateTime = DateTime.Now;

            rtv_SpeedAreaGroupSet_CtrlRes = (VEXI_DEFS.TRTV_SpeedAreaGroupCTRLRes)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TRTV_SpeedAreaGroupCTRLRes));

            if (rtv_SpeedAreaGroupSet_CtrlRes.CtrlResult != ConstClass.CODE_ACK)
            {
                switch (rtv_SpeedAreaGroupSet_CtrlRes.NackReason)
                {
                    case 1: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 설정 개수 범위 초과", "W"); break;
                    case 10: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 셋업모드 아님", "W"); break;
                    default: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 0x" + string.Format("{0:X2}", rtv_SpeedAreaGroupSet_CtrlRes.NackReason), "W"); break;
                }
            }
        }

        public unsafe void Process_RTVPositionCtrlRes(byte[] datas)
        {
            RxDateTime = DateTime.Now;

            rtv_PositionSet_CtrlRes = (VEXI_DEFS.TRTV_PositionSetCTRLRes)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TRTV_PositionSetCTRLRes));

            if (rtv_PositionSet_CtrlRes.CtrlResult != ConstClass.CODE_ACK)
            {
                switch (rtv_PositionSet_CtrlRes.NackReason)
                {
                    case 3: form_Main.GlobalObj.MsgBox_Info("제어 실패 : Count 이상", "W"); break;
                    case 6: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 값 이상 : 값 감소 오류", "W"); break;
                    case 7: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 값 이상 : 설정가능범위 외", "W"); break;
                    case 10: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 셋업모드 아님", "W"); break;
                    default: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 0x" + string.Format("{0:X2}", rtv_PositionSet_CtrlRes.NackReason), "W"); break;
                }
            }
        }


        public unsafe void Display_RTVPosition(byte[] datas)
        {
            rtv_PositionParam_Res = (VEXI_DEFS.TRTV_PositionSetParam)Global_Class.UTIL_BytesToStructure(datas, datas.Length, typeof(VEXI_DEFS.TRTV_PositionSetParam));

            Display_RTVPosition();

            IsIn_Position = true;
            Enable_Btn();
        }


        public void Init_RTVPosition()
        {
            cb_RailType.SelectedIndex = -1;
            cb_Barcode_Direct.SelectedIndex = -1;
            ed_Barcode_Ref.Text = "0";

            ed_Ref_Start.Text = "0";
            ed_Ref_End.Text = "0";

            cb_Home_SpeedType.SelectedIndex = -1;
            ed_Home_Position.Text = "0";
            cb_Service_SpeedType.SelectedIndex = -1;
            ed_Service_Position.Text = "0";

            lv_Position_Set.Items.Clear();
            lv_Position_Set_View.Items.Clear();

            ListViewItem listviewItem;

            for (ushort i = 0; i < 200; i++)
            {
                listviewItem = lv_Position_Set.Items.Add(string.Format("{0}", i + 1));
                listviewItem.SubItems.Add(string.Format("{0}", 0));
            }

            for (ushort i = 0; i < 200; i++)
            {
                listviewItem = lv_Position_Set_View.Items.Add(string.Format("{0}", i + 1));
                listviewItem.SubItems.Add(string.Format("{0}", 0));
            }
        }

        public unsafe void Display_RTVPosition()
        {
            RxDateTime = DateTime.Now;

            cb_RailType.SelectedIndex = rtv_PositionParam_Res.Header.RailType;
            cb_Barcode_Direct.SelectedIndex = rtv_PositionParam_Res.Header.Barcode_Direct;
            ed_Barcode_Ref.Text = string.Format("{0}", rtv_PositionParam_Res.Header.Barcode_Ref);

            ed_Ref_Start.Text = string.Format("{0}", rtv_PositionParam_Res.Header.Ref_Start);
            ed_Ref_End.Text = string.Format("{0}", rtv_PositionParam_Res.Header.Ref_End);

            cb_Home_SpeedType.SelectedIndex = rtv_PositionParam_Res.Header.Home_SpeedType;
            ed_Home_Position.Text = string.Format("{0}", rtv_PositionParam_Res.Header.Home_Position);
            cb_Service_SpeedType.SelectedIndex = rtv_PositionParam_Res.Header.Service_SpeedType;
            ed_Service_Position.Text = string.Format("{0}", rtv_PositionParam_Res.Header.Service_Position);


            if (rtv_PositionParam_Res.Header.PositionCount > 200) return;
            if (rtv_PositionParam_Res.Header.PositionCount == 0) return;

            ListViewItem listviewItem;
            for (ushort i = 0; i < rtv_PositionParam_Res.Header.PositionCount; i++)
            {
                listviewItem = lv_Position_Set.Items[i];
                listviewItem.SubItems[1].Text = string.Format("{0}", rtv_PositionParam_Res.Position[i]);
            }

            for (ushort i = 0; i < rtv_PositionParam_Res.Header.PositionCount; i++)
            {
                listviewItem = lv_Position_Set_View.Items[i];
                listviewItem.SubItems[1].Text = string.Format("{0}", rtv_PositionParam_Res.Position[i]);
            }
        }

        public void Init_RTVSpeedArea()
        {
            //label6.Text = System.DateTime.UtcNow.ToString();
            lv_SpeedArea.Items.Clear();
        }

        public unsafe void Display_RTVSpeedArea(byte[] datas)
        {
            //요청 버튼을 누를 때도 items.clear를 하지만 응답을 받을 때도 한다
            //요청 버튼 누를때 items.clear는 무응답을 나타낼 수 있고
            //응답에서의 items.clear는 응답이 오기전에 요청 버튼을 연속 누르게 되었을 때
            //연속으로 응답이 오면서 중복 표시가 될 수 있는 것을 막아준다
            lv_SpeedArea.Items.Clear();

            rtv_SpeedAreaGroupParam_Res = (VEXI_DEFS.TRTV_SpeedAreaGroupParam)Global_Class.UTIL_BytesToStructure(datas, datas.Length, typeof(VEXI_DEFS.TRTV_SpeedAreaGroupParam));

            Display_RTVSpeedArea();

            IsIn_SpeedArea = true;
            Enable_Btn();
        }

        public unsafe void Display_RTVSpeedArea()
        {
           
            RxDateTime = DateTime.Now;

            UInt32 StartMM = 0;
            UInt32 EndMM = 0;


            if (rtv_PositionParam_Res.Header.RailType == 1)
            {
                StartMM = 0;
                EndMM = rtv_PositionParam_Res.Header.Barcode_Ref;
            }
            else
            {
                StartMM = rtv_PositionParam_Res.Header.Ref_Start;
                EndMM = rtv_PositionParam_Res.Header.Ref_End;
            }


            lbl_AreaSpeed_Start.Text = string.Format("{0}", StartMM);
            lbl_AreaSpeed_End.Text = string.Format("{0}", EndMM);


            if (rtv_SpeedAreaGroupParam_Res.Header.AreaCount == 0)
            {
                IsIn_SpeedArea = true;
                Enable_Btn();
                
                return;
            }
            else
            {
                ListViewItem listviewItem;

                ed_RTV_Width.Text = string.Format("{0}", rtv_SpeedAreaGroupParam_Res.Header.RTV_Width);
                ed_Linear_Stop.Text = string.Format("{0}", rtv_SpeedAreaGroupParam_Res.Header.LineArea_StopWidth);
                ed_Linear_Start.Text = string.Format("{0}", rtv_SpeedAreaGroupParam_Res.Header.LineArea_StartWidth);
                ed_Round_Stop.Text = string.Format("{0}", rtv_SpeedAreaGroupParam_Res.Header.RoundArea_StopWidth);
                ed_Round_Start.Text = string.Format("{0}", rtv_SpeedAreaGroupParam_Res.Header.RoundArea_StartWidth);

                ed_FrontRTVPosition_TimeOut1.Text = string.Format("{0:0.0}", (double)rtv_SpeedAreaGroupParam_Res.Header.FrontRTVPosition_TimeOut1 / 10);
                ed_FrontRTVPosition_TimeOut2.Text = string.Format("{0}", rtv_SpeedAreaGroupParam_Res.Header.FrontRTVPosition_TimeOut2);
                ed_StopbyLiadrTimeOut.Text = string.Format("{0}", rtv_SpeedAreaGroupParam_Res.Header.StopbyLiadrTimeOut);
                ed_Stop_OffsetTime.Text = string.Format("{0:0.00}", (double)rtv_SpeedAreaGroupParam_Res.Header.Stop_OffsetTime / 100);
                ed_Stop_OffsetMaxDistance.Text = string.Format("{0}", rtv_SpeedAreaGroupParam_Res.Header.Stop_OffsetMaxDistance);
                ed_Lidar_Off_SafetyTime.Text = string.Format("{0}", rtv_SpeedAreaGroupParam_Res.Header.Lidar_Off_SafetyTime);

                cb_RTV_DriveStopSensor.SelectedIndex = Math.Min(rtv_SpeedAreaGroupParam_Res.Header.DriveStopSensor, cb_RTV_DriveStopSensor.Items.Count - 1);
                cb_RTV_DriveDecelSensor.SelectedIndex = Math.Min(rtv_SpeedAreaGroupParam_Res.Header.DriveDecelSensor, cb_RTV_DriveDecelSensor.Items.Count - 1);


                ed_ErrRef_LowSpeedValue.Text = string.Format("{0}", rtv_SpeedAreaGroupParam_Res.Header.ErrRef_LowSpeedValue);
                ed_ErrRef_LowSpeedMin.Text = string.Format("{0}", rtv_SpeedAreaGroupParam_Res.Header.ErrRef_LowSpeedMin);


                fixed (VEXI_DEFS.TRTV_SpeedAreaGroupConfigRec* Ptr = &rtv_SpeedAreaGroupParam_Res.Area1)
                {
                    for (int i = 0; i < rtv_SpeedAreaGroupParam_Res.Header.AreaCount; i++)
                    {
                        listviewItem = lv_SpeedArea.Items.Add(string.Format("{0}", i + 1));

                        switch (((Ptr + i)->Area_Type) & 0x03)
                        {
                            case 0: listviewItem.SubItems.Add("직선"); break;
                            case 1: listviewItem.SubItems.Add("직선 전진감속"); break;
                            case 2: listviewItem.SubItems.Add("직선 후진감속"); break;
                            case 3: listviewItem.SubItems.Add("곡선"); break;
                        }

                        listviewItem.SubItems.Add(string.Format("{0}", (Ptr + i)->Start_MM));
                        listviewItem.SubItems.Add(string.Format("{0}", (Ptr + i)->End_MM));
                        listviewItem.SubItems.Add(string.Format("{0:0.0}", (double)(Ptr + i)->MaxSpeed / 10));
                        listviewItem.SubItems.Add(string.Format("{0}", (Ptr + i)->StopDistance));
                        listviewItem.SubItems.Add(string.Format("{0}", (Ptr + i)->StartDistance));
                        listviewItem.SubItems.Add(string.Format("{0}", (Ptr + i)->PrevAreaIndex));
                        listviewItem.SubItems.Add(string.Format("{0}", (Ptr + i)->NextAreaIndex));
                        if ((Ptr + i)->Sensorindex == 0)
                        {
                            listviewItem.SubItems.Add("OFF");
                        } else
                        {
                            listviewItem.SubItems.Add(string.Format("{0}", (Ptr + i)->Sensorindex));
                        }

                        if (((Ptr + i)->Region & 0x01) == 0x00)
                        {
                            listviewItem.SubItems.Add("미적용");
                        } else
                        {
                            listviewItem.SubItems.Add("적용");
                        }
                        if (((Ptr + i)->Region & 0x02) == 0x00)
                        {
                            listviewItem.SubItems.Add("미적용");
                        }
                        else
                        {
                            listviewItem.SubItems.Add("적용");
                        }
                        if (((Ptr + i)->Region & 0x04) == 0x00)
                        {
                            listviewItem.SubItems.Add("미적용");
                        }
                        else
                        {
                            listviewItem.SubItems.Add("적용");
                        }

                        if (((Ptr + i)->Area_Type & 0x04) == 0x00) //정지거리
                        {
                            listviewItem.SubItems.Add("미사용");
                        }
                        else
                        {
                            listviewItem.SubItems.Add("사용");
                        }

                        if (((Ptr + i)->Area_Type & 0x08) == 0x00)
                        {
                            listviewItem.SubItems.Add("미사용");
                        }
                        else
                        {
                            listviewItem.SubItems.Add("사용");
                        }

                        if (((Ptr + i)->Area_Type & 0x10) == 0x00)
                        {
                            listviewItem.SubItems.Add("미사용");
                        }
                        else
                        {
                            listviewItem.SubItems.Add("사용");
                        }

                        if (((Ptr + i)->Area_Type & 0x20) == 0x00) //후진감속
                        {
                            listviewItem.SubItems.Add("미사용");
                        }
                        else
                        {
                            listviewItem.SubItems.Add("사용");
                        }
                    }
                }
            }
        }

        private void Sort_SpeedAreaLv()
        {
            if (lv_SpeedArea.Items.Count > 0)
            {
                lv_SpeedArea.ListViewItemSorter = ColumnSorter;

                ColumnSorter.SortColumn = 2;
                ColumnSorter.Order = SortOrder.Ascending;


                lv_SpeedArea.Sort();


                if (lv_SpeedArea.Items.Count == 1)
                {
                    lv_SpeedArea.Items[0].SubItems[0].Text = "1";
                    lv_SpeedArea.Items[0].SubItems[7].Text = "0";
                    lv_SpeedArea.Items[0].SubItems[8].Text = "0";
                }
                else
                {

                    for (int i = 0; i < lv_SpeedArea.Items.Count; i++)
                    {
                        lv_SpeedArea.Items[i].SubItems[0].Text = string.Format("{0}", i + 1);

                        if (i == 0)
                        {
                            lv_SpeedArea.Items[i].SubItems[7].Text = string.Format("{0}", lv_SpeedArea.Items.Count);
                            lv_SpeedArea.Items[i].SubItems[8].Text = string.Format("{0}", i + 2);
                        }
                        else if (i == lv_SpeedArea.Items.Count - 1)
                        {
                            lv_SpeedArea.Items[i].SubItems[7].Text = string.Format("{0}", i);
                            lv_SpeedArea.Items[i].SubItems[8].Text = string.Format("{0}", 1);
                        }
                        else
                        {
                            lv_SpeedArea.Items[i].SubItems[7].Text = string.Format("{0}", i);
                            lv_SpeedArea.Items[i].SubItems[8].Text = string.Format("{0}", i + 2);
                        }

                    }
                }
            }

            lv_SpeedArea.ListViewItemSorter = null;
        }

        private unsafe void Request_Position()
        {
            VEXI_DEFS.TRTV_PositionSetParamReq req;

            Global_Class.UTIL_Byteptr_clear((byte*)req.Reserved, Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_PositionSetParamReq)));

            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_94, req);
            RxDateTime = DateTime.Now;
        }

        private unsafe void Request_SpeedArea()
        {
            VEXI_DEFS.TRTV_SpeedAreaGroupConfigReq req;

            Global_Class.UTIL_Byteptr_clear((byte*)req.Reserved, Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_SpeedAreaGroupConfigReq)));

            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_9A, req);
            RxDateTime = DateTime.Now;
        }

        private unsafe void Request_Station()
        {
            VEXI_DEFS.TRTV_StationConfigReq req;

            Global_Class.UTIL_Byteptr_clear((byte*)req.Reserved, Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_StationConfigReq)));

            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_98, req);
            RxDateTime = DateTime.Now;
        }

        private void Enable_Btn()
        {

            btn_Position_Load.Enabled = true;
            btn_SpeedArea_Load.Enabled = true;
            btn_StationConfig_Load.Enabled = true;
            if ((form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt.DevMode & 0x08) == 0)
            //if (false)
            {

                btn_Position_Set.Enabled = false;
                btn_SpeedArea_Set.Enabled = false;
                btn_StationConfig_Set.Enabled = false;
            }
            else
            {

                btn_Position_Set.Enabled = IsIn_Position;
                btn_SpeedArea_Set.Enabled = IsIn_SpeedArea;
                btn_StationConfig_Set.Enabled = IsIn_Stationinfo;
            }

        }

        private void Hide_AllEdit()
        {
            cb_Position.Visible = false;

            for (byte i = 0; i <= (lv_SpeedArea.Columns.Count - 1); i++)
            {
                if (SpeedArea_CtrlBox[i] != null)
                {
                    SpeedArea_CtrlBox[i].Visible = false;
                }
            }

            for (byte i = 0; i <= (lv_StationParam.Columns.Count - 1); i++)
            {
                if (Station_CtrlBox[i] != null)
                {
                    Station_CtrlBox[i].Visible = false;
                }
            }
            
        }

        private unsafe void Edit_PositionID(byte EditType)
        {
            UInt16 FromIndex = 0;

            if (EditType == 1)
            {
                if (lv_Position_Set.Items.Count == 1)
                {
                    lv_Position_Set.Items[0].Selected = true;
                }
                if (lv_Position_Set.SelectedItems.Count == 1)
                {
                    //선택된 아이템부터 Position 값을 뒤로 민다.
                    //선택된 아이템의 Position 값은 초기값이 0으로 한다.

                    FromIndex = (UInt16)lv_Position_Set.SelectedItems[0].Index;

                    for (int i = 199; i > FromIndex; i--)
                    {
                        lv_Position_Set.Items[i].SubItems[1].Text = lv_Position_Set.Items[i - 1].SubItems[1].Text;
                    }
                    lv_Position_Set.Items[FromIndex].SubItems[1].Text = "0";

                    lv_Position_Set.SelectedItems.Clear();
                    lv_Position_Set.Items[FromIndex].Selected = true;
                }

            }
            else if (EditType == 2)
            {
                if (lv_Position_Set.Items.Count == 1)
                {
                    lv_Position_Set.Items[0].Selected = true;
                }
                if (lv_Position_Set.SelectedItems.Count == 1)
                {
                    //선택된 아이템 다음부터 Position 값을 뒤로 민다.
                    //선택된 아이템의 다음아이템의 Position 값은 초기값이 0으로 한다.


                    FromIndex = (UInt16)lv_Position_Set.SelectedItems[0].Index;

                    if (FromIndex < 199)
                    {
                        for (int i = 199; i > FromIndex + 1; i--)
                        {
                            lv_Position_Set.Items[i].SubItems[1].Text = lv_Position_Set.Items[i - 1].SubItems[1].Text;
                        }
                        lv_Position_Set.Items[FromIndex + 1].SubItems[1].Text = "0";

                        lv_Position_Set.SelectedItems.Clear();
                        lv_Position_Set.Items[FromIndex + 1].Selected = true;
                    }
                }

            }
            else if (EditType == 3)
            {
                if (lv_Position_Set.SelectedItems.Count == 1)
                {

                    //선택된 아이템부터 Position 값을 앞으로 당긴다
                    //마지막 아이템의 Position 값은 초기값이 0으로 한다.

                    FromIndex = (UInt16)lv_Position_Set.SelectedItems[0].Index;

                    for (int i = FromIndex; i < (200 - 1); i++)
                    {
                        lv_Position_Set.Items[i].SubItems[1].Text = lv_Position_Set.Items[i + 1].SubItems[1].Text;
                    }
                    lv_Position_Set.Items[199].SubItems[1].Text = "0";

                    lv_Position_Set.SelectedItems.Clear();
                    lv_Position_Set.Items[FromIndex].Selected = true;
                }
            }
            else if (EditType == 4)
            {
                UInt32 Value1;
                Int32 Value2;
                if (lv_Position_Set.SelectedItems.Count > 0)
                {

                    Value2 = Global_Class.UTIL_StrToIntDef(ed_PositionOffset.Text, 0);

                    for (int i = 0; i < 200; i++)
                    {
                        if (lv_Position_Set.Items[i].Selected)
                        {
                            Value1 = (UInt32)(Global_Class.UTIL_StrToUInt32Def(lv_Position_Set.Items[i].SubItems[1].Text, 0) + Value2);

                            lv_Position_Set.Items[i].SubItems[1].Text = Value1.ToString();
                        }
                    }
                }
            }
            else if (EditType == 5)
            {
                if (lv_Position_Set.SelectedItems.Count == 1)
                {
                    for (int i = 0; i < 200; i++)
                    {
                        if (lv_Position_Set.Items[i].Selected)
                        {
                            FromIndex = (UInt16)lv_Position_Set.SelectedItems[0].Index;
                            break;
                        }
                    }

                    for (int i = FromIndex; i < 200; i++)
                    {
                        lv_Position_Set.Items[i].SubItems[1].Text = "0";
                    }
                }
            }
        }

        private unsafe void Edit_StationID(byte EditType)
        {
            UInt16 TmpSelectedIndex = 0;
            ListViewItem TmpListItem;

            

            if (EditType == 1)
            {
                if (lv_StationParam.Items.Count == 1)
                {
                    lv_StationParam.Items[0].Selected = true;
                }

                if (lv_StationParam.Items.Count == 0)
                {
                    TmpListItem = lv_StationParam.Items.Add("");
                    TmpListItem.SubItems.Add("0");
                    TmpListItem.SubItems.Add("0");
                    TmpListItem.SubItems.Add("불가");
                    TmpListItem.SubItems.Add("불가");
                    TmpListItem.SubItems.Add("좌측");
                }
                else
                {
                    if (lv_StationParam.SelectedItems.Count == 1)
                    {
                        TmpSelectedIndex = (UInt16)lv_StationParam.SelectedItems[0].Index;

                        TmpListItem = lv_StationParam.Items.Insert(TmpSelectedIndex, "");
                        TmpListItem.SubItems.Add("0");
                        TmpListItem.SubItems.Add("0");
                        TmpListItem.SubItems.Add("불가");
                        TmpListItem.SubItems.Add("불가");
                        TmpListItem.SubItems.Add("좌측");


                        lv_StationParam.SelectedItems.Clear();
                        lv_StationParam.Items[TmpSelectedIndex].Selected = true;
                    }
                }
                for (int i = 0; i < lv_StationParam.Items.Count; i++)
                {
                    lv_StationParam.Items[i].SubItems[0].Text = string.Format("{0}", i + 1);
                }

            }
            else if (EditType == 2)
            {
                if (lv_StationParam.Items.Count == 1)
                {
                    lv_StationParam.Items[0].Selected = true;
                }

                if (lv_StationParam.Items.Count == 0)
                {
                    TmpListItem = lv_StationParam.Items.Add("");
                    TmpListItem.SubItems.Add("0");
                    TmpListItem.SubItems.Add("0");
                    TmpListItem.SubItems.Add("불가");
                    TmpListItem.SubItems.Add("불가");
                    TmpListItem.SubItems.Add("좌측");
                }
                else
                {
                    if (lv_StationParam.SelectedItems.Count == 1)
                    {
                        //선택된 아이템 다음부터 Position 값을 뒤로 민다.
                        //선택된 아이템의 다음아이템의 Position 값은 초기값이 0으로 한다.


                        TmpSelectedIndex = (UInt16)lv_StationParam.SelectedItems[0].Index;

                        if (TmpSelectedIndex == (lv_StationParam.SelectedItems.Count - 1))
                        {
                            TmpListItem = lv_StationParam.Items.Add("");
                            TmpListItem.SubItems.Add("0");
                            TmpListItem.SubItems.Add("0");
                            TmpListItem.SubItems.Add("불가");
                            TmpListItem.SubItems.Add("불가");
                            TmpListItem.SubItems.Add("좌측");
                        }
                        else
                        {
                            TmpListItem = lv_StationParam.Items.Insert(TmpSelectedIndex + 1, "");
                            TmpListItem.SubItems.Add("0");
                            TmpListItem.SubItems.Add("0");
                            TmpListItem.SubItems.Add("불가");
                            TmpListItem.SubItems.Add("불가");
                            TmpListItem.SubItems.Add("좌측");
                        }

                        lv_StationParam.SelectedItems.Clear();
                        lv_StationParam.Items[TmpSelectedIndex + 1].Selected = true;
                    }
                }
                for (int i = 0; i < lv_StationParam.Items.Count; i++)
                {
                    lv_StationParam.Items[i].SubItems[0].Text = string.Format("{0}", i + 1);
                }

            }
            else if (EditType == 3)
            {

                if (lv_StationParam.SelectedItems.Count > 0)
                {
                    for (int i = (Int16)(lv_StationParam.Items.Count - 1); i >= 0; i--)
                    {
                        if (lv_StationParam.Items[i].Selected)
                        {
                            lv_StationParam.Items.RemoveAt(i);
                        }
                    }
                }

                lv_StationParam.SelectedItems.Clear();

                for (int i = 0; i < lv_StationParam.Items.Count; i++)
                {
                    lv_StationParam.Items[i].SubItems[0].Text = string.Format("{0}", i + 1);
                }
            }
            else if (EditType == 4)
            {
                lv_StationParam.Items.Clear();
            }
        }

        #endregion

        #region lv_SpeedArea 이벤트
        private void lv_SpeedArea_Enter(object sender, EventArgs e)
        {
            Hide_AllEdit();
        }

        private void lv_SpeedArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (byte i = 0; i <= (lv_SpeedArea.Columns.Count - 1); i++)
            {
                if (SpeedArea_CtrlBox[i] != null)
                {
                    SpeedArea_CtrlBox[i].Visible = false;
                }
            }
        }

        private void lv_SpeedArea_MouseUp(object sender, MouseEventArgs e)
        {
            // Get the item on the row that is clicked.
            lv_SpeedArea_Item = this.lv_SpeedArea.GetItemAt(e.X, e.Y);

            // Make sure that an item is clicked.

            if (lv_SpeedArea_Item != null)
            {
                //Column 2
                Rectangle ClickedItem;


                for (byte i = 0; i <= (lv_SpeedArea.Columns.Count - 1); i++)
                {
                    if (SpeedArea_CtrlBox[i] != null)
                    {
                        ClickedItem = lv_SpeedArea_Item.SubItems[i].Bounds;

                        //화면에 안보이는 경우
                        if ((ClickedItem.Left + this.lv_SpeedArea.Columns[i].Width) < 0)
                        {
                            SpeedArea_CtrlBox[i].Tag = 1;
                            //return;
                        }
                        else if (ClickedItem.Left < 0) // 해당 컬럼의 Left가 화면을 벗어난 경우
                        {
                            SpeedArea_CtrlBox[i].Tag = 0;
                            if ((ClickedItem.Left + this.lv_SpeedArea.Columns[i].Width) > this.lv_SpeedArea.Width) // 해당 컬럼의 Right가 화면을 벗어난 경우
                            {
                                ClickedItem.Width = this.lv_SpeedArea.Width;
                                ClickedItem.X = 0;
                            }
                            else // 해당 컬럼의 Right가 화면을 벗어나지 않은 경우
                            {
                                ClickedItem.Width = this.lv_SpeedArea.Columns[i].Width + ClickedItem.Left;
                                ClickedItem.X = 2;
                            }
                        }
                        else // 해당 컬럼의 Left가 화면을 벗어나지 않은 경우
                        {
                            SpeedArea_CtrlBox[i].Tag = 0;
                            if ((ClickedItem.Left + this.lv_SpeedArea.Columns[i].Width) > this.lv_SpeedArea.Width)  // 해당 컬럼의 Left가 화면을 벗어난 경우
                            {
                                ClickedItem.Width = this.lv_SpeedArea.Width - ClickedItem.Left;
                            }
                            else // 해당 컬럼의 Right가 화면을 벗어나지 않은 경우
                            {
                                ClickedItem.Width = this.lv_SpeedArea.Columns[i].Width;
                            }
                        }
                        if (Convert.ToByte(SpeedArea_CtrlBox[i].Tag.ToString()) != 1)
                        {
                            // Adjust the top to account for the location of the ListView.
                            ClickedItem.Y += lv_SpeedArea.Top;
                            ClickedItem.X += lv_SpeedArea.Left;

                            // Assign calculated bounds to the TextBox.
                            SpeedArea_CtrlBox[i].Bounds = ClickedItem;

                            // Set default text for TextBox to match the item that is clicked.
                            SpeedArea_CtrlBox[i].Text = lv_SpeedArea_Item.SubItems[i].Text;
                        }
                    }
                }
            }
            else
            {
                for (byte i = 0; i <= (lv_SpeedArea.Columns.Count - 1); i++)
                {
                    if (SpeedArea_CtrlBox[i] != null)
                    {
                        SpeedArea_CtrlBox[i].Visible = false;
                    }

                }
            }
        }

        private void lv_SpeedArea_DoubleClick(object sender, EventArgs e)
        {
            bool once = false;
            if (lv_SpeedArea.SelectedItems.Count == 1)
            {
                for (byte i = 0; i <= (lv_SpeedArea.Columns.Count - 1); i++)
                {
                    if (SpeedArea_CtrlBox[i] != null)
                    {
                        if (Convert.ToByte(SpeedArea_CtrlBox[i].Tag.ToString()) != 1)
                        {
                            SpeedArea_CtrlBox[i].Visible = true;
                            SpeedArea_CtrlBox[i].BringToFront();
                            if (!once)
                            {
                                SpeedArea_CtrlBox[i].Focus();
                                once = true;
                            }
                        }
                    }
                }
            }
        }

        private void btn_Area_ADD_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();

            if (lv_SpeedArea.Items.Count >= 50) return;

            
            ListViewItem listviewItem;

            listviewItem = lv_SpeedArea.Items.Add(string.Format("{0}", lv_SpeedArea.Items.Count + 1));

            listviewItem.SubItems.Add("직선");

            listviewItem.SubItems.Add("0");
            listviewItem.SubItems.Add("0");
            listviewItem.SubItems.Add("0");
            listviewItem.SubItems.Add("0");
            listviewItem.SubItems.Add("0");
            listviewItem.SubItems.Add("0");
            listviewItem.SubItems.Add("0");
            listviewItem.SubItems.Add("OFF");
            listviewItem.SubItems.Add("미적용");
            listviewItem.SubItems.Add("미적용");
            listviewItem.SubItems.Add("미적용");
            listviewItem.SubItems.Add("미사용");
            listviewItem.SubItems.Add("미사용");
            listviewItem.SubItems.Add("미사용");
            listviewItem.SubItems.Add("미사용");
        }

        private void btn_Area_Clear_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();
            lv_SpeedArea.Items.Clear();
        }

        private void btn_Area_DEL_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();
            if (lv_SpeedArea.SelectedItems.Count == 0) return;

            lv_SpeedArea.BeginUpdate();

            for (int i = lv_SpeedArea.Items.Count - 1; i >= 0; i--)
            {
                if (lv_SpeedArea.Items[i].Selected)
                {
                    lv_SpeedArea.Items.RemoveAt(i);
                }
            }
            lv_SpeedArea.EndUpdate();

            Sort_SpeedAreaLv();
        }

        private void btn_SpeedArea_Load_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();

            Init_RTVSpeedArea();

            Request_Position();

            Retrycount = 0;
            Request_SpeedArea();
        }

        private void btn_SpeedArea_Set_Click(object sender, EventArgs e)
        {
            if ((lv_SpeedArea.Items.Count == 0))
            {
                form_Main.GlobalObj.MsgBox_Info("구간 정보가 작성되지 않았습니다.", "W");
                return;
            }

            Sort_SpeedAreaLv();

            int check_Result = Check_AreaSpeedValue_Err();
            if (check_Result != -1)
            {
                if (check_Result < lv_SpeedArea.Items.Count)
                {
                    form_Main.GlobalObj.MsgBox_Info("구간의 시작 값이 끝 값보다 큽니다.", "W");
                    lv_Position_Set.SelectedItems.Clear();
                    lv_Position_Set.Items[check_Result].Selected = true;
                } else
                {
                    form_Main.GlobalObj.MsgBox_Info("정의되지 않은 구간이 존재합니다.", "W");
                }
                return;
            }

            if (Check_AreaSpeedValue_Warn() == 0)
            {
                if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "구간 정보를 장치로 다운로드하시겠습니까?"))
                {
                    Ctrl_SpeedArea(false);

                }
            } else
            {
                if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "곡선 구간 속도가 60m/min 이상으로 설정되었습니다. 구간 정보를 장치로 다운로드하시겠습니까?"))
                {
                    Ctrl_SpeedArea(false);
                }
            }

        }

        private void btn_SpeedArea_FileWrite_Click(object sender, EventArgs e)
        {
        }

        private void btn_SpeedArea_FileRead_Click(object sender, EventArgs e)
        {
        }

        private void ed_Area_Start_VisibleChanged(object sender, EventArgs e)
        {
            if (lv_SpeedArea_Item != null)
            {
                TextBox TmpTextBox = (TextBox)sender;

                for (byte i = 0; i <= (lv_SpeedArea.Columns.Count - 1); i++)
                {
                    if (SpeedArea_CtrlBox[i] != null)
                    {
                        if (TmpTextBox == SpeedArea_CtrlBox[i])
                        {
                            if (!TmpTextBox.Visible)
                            {
                                lv_SpeedArea_Item.SubItems[i].Text = SpeedArea_CtrlBox[i].Text;
                            }
                        }
                    }
                }

                if (!TmpTextBox.Visible)
                {
                    Sort_SpeedAreaLv();
                }
            }
        }

        private void ed_Area_Start_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox ed = sender as TextBox;

            if (ed.Name == "ed_Area_MaxSpeed") // 마이너스 OFF, 소수점 ON
            {
                if (
                (char.IsControl(e.KeyChar)) ||
                (char.IsDigit(e.KeyChar)) ||
                (e.KeyChar == '.') ||
                (e.KeyChar == Convert.ToChar(Keys.Enter)) ||
                (e.KeyChar == Convert.ToChar(Keys.Back)) ||
                (e.KeyChar == Convert.ToChar(Keys.Tab)))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }

            }

            else
            {
                if (
                 (char.IsControl(e.KeyChar)) ||
                 (char.IsDigit(e.KeyChar)) ||
                 (e.KeyChar == Convert.ToChar(Keys.Enter)) ||
                 (e.KeyChar == Convert.ToChar(Keys.Back)) ||
                 (e.KeyChar == Convert.ToChar(Keys.Tab)))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ed.DeselectAll();
                if (ed.Name == "ed_Area_Start") ed_Area_End.Focus();
                else if (ed.Name == "ed_Area_End") ed_Area_MaxSpeed.Focus();
                else if (ed.Name == "ed_Area_MaxSpeed") ed_Area_StopDistance.Focus();
                else if (ed.Name == "ed_Area_StopDistance") ed_Area_StartDistance.Focus();
                else if (ed.Name == "ed_Area_StartDistance") cb_Area_Sensor.Focus();
            }
        }

        private void cbGroupType_VisibleChanged(object sender, EventArgs e)
        {
            if (lv_SpeedArea_Item != null)
            {
                ComboBox TmpComboBox = (ComboBox)sender;

                for (byte i = 0; i <= (lv_SpeedArea.Columns.Count - 1); i++)
                {
                    if (SpeedArea_CtrlBox[i] != null)
                    {
                        if (TmpComboBox == SpeedArea_CtrlBox[i])
                        {
                            if (!TmpComboBox.Visible)
                            {
                                lv_SpeedArea_Item.SubItems[i].Text = SpeedArea_CtrlBox[i].Text;

                            }
                        }
                    }
                }

                if (!TmpComboBox.Visible)
                    Sort_SpeedAreaLv();
            }
        }

        #endregion

        #region Position 컴포넌트 이벤트

        private void btn_AddPosition_Prev_Click(object sender, EventArgs e)
        {
            Edit_PositionID(1);
        }

        private void btn_AddPosition_Next_Click(object sender, EventArgs e)
        {
            Edit_PositionID(2);
        }

        private void btn_DelPosition_Click(object sender, EventArgs e)
        {
            Edit_PositionID(3);
        }
        private void btn_PositionOffset_Click(object sender, EventArgs e)
        {
            Edit_PositionID(4);
        }

        private void Btn_DelMulitPosition_Click(object sender, EventArgs e)
        {
            Edit_PositionID(5);
        }
        private void lv_Position_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb_Position.Visible = false;
        }

        private void cb_Position_1_VisibleChanged(object sender, EventArgs e)
        {
            if (lv_Position_Item != null)
            {
                ComboBox TmpComboBox = (ComboBox)sender;

                if (!TmpComboBox.Visible)
                {
                    lv_Position_Item.SubItems[1].Text = cb_Position.Text;
                }
            }
        }

        private void cb_Position_1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (
                (char.IsDigit(e.KeyChar)) ||
                (e.KeyChar == Convert.ToChar(Keys.Enter)) ||
                (e.KeyChar == Convert.ToChar(Keys.Back)) ||
                (e.KeyChar == Convert.ToChar(Keys.Tab)) ||
                (e.KeyChar == '-')
                )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                cb_Position.Visible = false;
            }
        }

        private void lv_Position_MouseUp(object sender, MouseEventArgs e)
        {
            // Get the item on the row that is clicked.
            lv_Position_Item = lv_Position_Set.GetItemAt(e.X, e.Y);

            // Make sure that an item is clicked.
            if (lv_Position_Item != null)
            {
                //Column 2
                Rectangle ClickedItem;


                ClickedItem = lv_Position_Item.SubItems[1].Bounds;

                //화면에 안보이는 경우
                if ((ClickedItem.Left + this.lv_Position_Set.Columns[1].Width) < 0)
                {
                    cb_Position.Tag = 1;
                    return;
                }
                else if (ClickedItem.Left < 0) // 해당 컬럼의 Left가 화면을 벗어난 경우
                {
                    cb_Position.Tag = 0;
                    if ((ClickedItem.Left + this.lv_Position_Set.Columns[1].Width) > this.lv_Position_Set.Width) // 해당 컬럼의 Right가 화면을 벗어난 경우
                    {
                        ClickedItem.Width = this.lv_Position_Set.Width;
                        ClickedItem.X = 0;
                    }
                    else // 해당 컬럼의 Right가 화면을 벗어나지 않은 경우
                    {
                        ClickedItem.Width = this.lv_Position_Set.Columns[1].Width + ClickedItem.Left;
                        ClickedItem.X = 2;
                    }
                }
                else // 해당 컬럼의 Left가 화면을 벗어나지 않은 경우
                {
                    cb_Position.Tag = 0;
                    if ((ClickedItem.Left + this.lv_Position_Set.Columns[1].Width) > this.lv_Position_Set.Width)  // 해당 컬럼의 Left가 화면을 벗어난 경우
                    {
                        ClickedItem.Width = this.lv_Position_Set.Width - ClickedItem.Left;
                    }
                    else // 해당 컬럼의 Right가 화면을 벗어나지 않은 경우
                    {
                        ClickedItem.Width = this.lv_Position_Set.Columns[1].Width;
                    }
                }

                // Adjust the top to account for the location of the ListView.
                ClickedItem.Y += lv_Position_Set.Top;
                ClickedItem.X += lv_Position_Set.Left;

                // Assign calculated bounds to the ComboBox.
                cb_Position.Bounds = ClickedItem;

                // Set default text for ComboBox to match the item that is clicked.
                cb_Position.Text = lv_Position_Item.SubItems[1].Text;
            }
        }

        private void lv_Position_DoubleClick(object sender, EventArgs e)
        {
            if (lv_Position_Set.SelectedItems.Count == 1)
            {
                if (Convert.ToByte(cb_Position.Tag.ToString()) != 1)
                {
                    cb_Position.Visible = true;
                    cb_Position.BringToFront();
                    cb_Position.Focus();
                }
            }
        }

        private void lv_Position_Enter(object sender, EventArgs e)
        {
            Hide_AllEdit();
            //cb_Bay_Position_1.Visible = false;
        }

        private void btn_Position_Load_Click_1(object sender, EventArgs e)
        {
            Hide_AllEdit();
            Init_RTVPosition();

            Request_Position();
        }

        private int Check_PositionValue()
        {
            if (lv_Position_Set.Items.Count == 0) return -1;


            UInt32 TmpValue_1;
            UInt32 TmpValue_2;


            UInt32 StartMM = 0;
            UInt32 EndMM = 0;

            byte TmpCount = 0;

            for (int i = 0; i < lv_Position_Set.Items.Count; i++)
            {
                TmpValue_1 = Global_Class.UTIL_StrToUInt32Def(lv_Position_Set.Items[i].SubItems[1].Text, 0);
                if (TmpValue_1 != 0)
                {
                    TmpCount = (byte)(i + 1);
                }
            }

            if (TmpCount > 0)
            {
                if (cb_RailType.SelectedIndex == 1) //루프형
                {
                    StartMM = 0;
                    EndMM = Global_Class.UTIL_StrToUInt32Def(ed_Barcode_Ref.Text, 0);

                }
                else
                {
                    StartMM = Global_Class.UTIL_StrToUInt32Def(ed_Ref_Start.Text, 0);
                    EndMM = Global_Class.UTIL_StrToUInt32Def(ed_Ref_End.Text, 0);
                }

                for (int i = 0; i < TmpCount; i++)
                {

                    TmpValue_1 = Global_Class.UTIL_StrToUInt32Def(lv_Position_Set.Items[i].SubItems[1].Text, 0);
                    if (TmpValue_1 < StartMM) return i;
                    if (TmpValue_1 > EndMM) return i;
                }

                TmpValue_1 = 0;
                TmpValue_2 = 0;

                for (int i = 0; i < TmpCount; i++)
                {
                    TmpValue_1 = TmpValue_2;
                    TmpValue_2 = Global_Class.UTIL_StrToUInt32Def(lv_Position_Set.Items[i].SubItems[1].Text, 0);

                    if (i > 0)
                    {
                        if (TmpValue_1 >= TmpValue_2) return i;
                    }
                
                }
            }
            return -1;
        }

        private int Check_StationValue()
        {
            
            if (lv_StationParam.Items.Count == 0) return -1;

            //if (rtv_PositionParam_Res.Header.PositionCount == 0) return -1;

            byte TmpValue_1 = 0;

            for (int i = 0; i < lv_StationParam.Items.Count; i++)
            {
                TmpValue_1 = (Byte)Global_Class.UTIL_StrToIntDef(lv_StationParam.Items[i].SubItems[1].Text, 0);
                if (TmpValue_1 > rtv_PositionParam_Res.Header.PositionCount)
                {
                    return i;
                }
            }

            return -1;
        }

        private int Check_AreaSpeedValue_Err()
        {
            if (lv_SpeedArea.Items.Count == 0) return -1;

            UInt32 TmpValue_1 = 0;
            UInt32 TmpValue_2 = 0;

            UInt32 StartMM = 0;
            UInt32 EndMM = 0;


            UInt32 MinMM = 0;
            UInt32 MaxMM = 0;


            //이전구간의 끝값과 현재 구간의 시작값이 같아야 함
            if (lv_SpeedArea.Items.Count > 1)
            {
                for (int i = 1; i < lv_SpeedArea.Items.Count; i++)
                {
                    TmpValue_1 = Global_Class.UTIL_StrToUInt32Def(lv_SpeedArea.Items[i - 1].SubItems[3].Text, 0);
                    TmpValue_2 = Global_Class.UTIL_StrToUInt32Def(lv_SpeedArea.Items[i].SubItems[2].Text, 0);

                    if (TmpValue_1 != TmpValue_2) return lv_SpeedArea.Items.Count;
                }
            }

            if (rtv_PositionParam_Res.Header.RailType == 1)
            {
                StartMM = 0;
                EndMM = rtv_PositionParam_Res.Header.Barcode_Ref;
            }
            else
            {
                StartMM = rtv_PositionParam_Res.Header.Ref_Start;
                EndMM = rtv_PositionParam_Res.Header.Ref_End;
            }

            if ((StartMM == 0) && (EndMM == 0))
            {
                return -1;
            }

            //전체 주행거리에 대한 구간이 정의가 되어야 함.
            for (int i = 0; i < lv_SpeedArea.Items.Count; i++)
            {
                TmpValue_1 = Global_Class.UTIL_StrToUInt32Def(lv_SpeedArea.Items[i].SubItems[2].Text, 0);
                TmpValue_2 = Global_Class.UTIL_StrToUInt32Def(lv_SpeedArea.Items[i].SubItems[3].Text, 0);

                if (TmpValue_1 > TmpValue_2)
                {
                    if (rtv_PositionParam_Res.Header.RailType == 1)
                    {
                        MinMM = StartMM;
                        MaxMM = EndMM;
                        break;
                    } else
                    {
                        return i;
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        MinMM = TmpValue_1;
                    }

                    if (MaxMM < TmpValue_2)
                    {
                        MaxMM = TmpValue_2;
                    }
                }
            }

            if ((StartMM == MinMM) && (EndMM == MaxMM))
            {
                return -1;
                //for (int i = 0; i < lv_SpeedArea.Items.Count; i++)
                //{
                //    TmpValue_1 = Global_Class.UTIL_StrToUInt32Def(lv_SpeedArea.Items[i].SubItems[2].Text, 0);
                //    TmpValue_2 = Global_Class.UTIL_StrToUInt32Def(lv_SpeedArea.Items[i].SubItems[3].Text, 0);

                //    if (TmpValue_1 < StartMM) return i;
                //    if (TmpValue_2 < StartMM) return i;

                //    if (TmpValue_1 > EndMM) return i;
                //    if (TmpValue_2 > EndMM) return i;
                //}
                
            } else
            {
                return lv_SpeedArea.Items.Count;
            }
        }


        private void btn_Position_Set_Click_1(object sender, EventArgs e)
        {
            Hide_AllEdit();

            //(lv_Position_Set.Items.Count == 0) 이 상황이 없긴 하다
            if (lv_Position_Set.Items.Count == 0)
            {
                form_Main.GlobalObj.MsgBox_Info("위치 정보가 작성되지 않았습니다.", "W");
                return;
            }

            int check_Result = Check_PositionValue();
            if (check_Result != -1)
            {
                form_Main.GlobalObj.MsgBox_Info("위치 정보 값이 범위를 벗어났거나 오름차순으로 정의되지 않았습니다.", "W");
                lv_Position_Set.SelectedItems.Clear();
                lv_Position_Set.Items[check_Result].Selected = true;
                return;
            }


            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "위치 정보를 장치로 다운로드하시겠습니까?"))
            {
                Ctrl_Position(false);

            }
        }

        private int Check_AreaSpeedValue_Warn()
        {
            if (lv_SpeedArea.Items.Count == 0) return 0;

            UInt16 TmpMaxSpeed = 0;


            //전체 주행거리에 대한 구간이 정의가 되어야 함.
            for (int i = 0; i < lv_SpeedArea.Items.Count; i++)
            {
                if (lv_SpeedArea.Items[i].SubItems[1].Text == "곡선")
                {
                    TmpMaxSpeed = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(lv_SpeedArea.Items[i].SubItems[4].Text, 10) * 10);

                    if (TmpMaxSpeed > 600) //곡선 구간 속도가 60m/min 보다 크다면 (설정을 잘못 한 것일 수 있으므로)
                    {
                        return -1;
                    }
                }
            }
            return 0;
        }




        private void btn_PositionConfig_Init_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            byte[] Data = { 0, 0 };

            frameLogin frmLogging = new frameLogin(); frmLogging.ShowDialog();

            if (frmLogging.DialogResult == DialogResult.OK)
            {
                if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, bt.Text + " 을 수행하시겠습니까?"))
                {
                    Data[0] = 0x02;
                    switch (bt.Tag.ToString())
                    {
                        case "1":
                            Data[1] = 0x20; break;
                        case "2":
                            Data[1] = 0xC0; break;
                    }
                    form_Main.Do_Ctrl_Cmd_withbytes(ConstClass.CMD1_00, ConstClass.CMD2_A0, Data);
                }

            }
        }

        #endregion

        private void btn_StationConfig_Load_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();
            Init_RTVStationParam();

            Request_Position();

            Retrycount = 0;
            Request_Station();

        }

        private void btn_StationConfig_Set_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();

            if (lv_StationParam.Items.Count == 0)
            {
                if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "스테이션 구성 정보를 초기화 하시겠습니까?"))
                {
                    Ctrl_StationParam(false);
                }
            }
            else
            {

                int check_Result = Check_StationValue();
                if (check_Result != -1)
                {
                    form_Main.GlobalObj.MsgBox_Info("스테이션 Position ID 값에 문제가 있습니다. ", "W");
                    lv_StationParam.SelectedItems.Clear();
                    lv_StationParam.Items[check_Result].Selected = true;
                    return;
                }

                if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "스테이션 구성 정보를 장치로 다운로드하시겠습니까?"))
                {
                    Ctrl_StationParam(false);
                }
            }
        }

        private void btn_StationConfig_FileRead_Click(object sender, EventArgs e)
        {
        }

        private void btn_LoadTotalFile4_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();

            openFileDialog1.Filter = "*.RTVcfg|*.RTVcfg";

            if (openFileDialog1.FileName == "")
            {
                openFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                form_Main.RTV_ToTalFile.FileName = openFileDialog1.FileName;
                if (form_Main.RTV_ToTalFile.Read_RTV_StationParam(ref rtv_StationParam_CTRL))
                {
                    rtv_StationParam_Res = rtv_StationParam_CTRL;

                    Init_RTVStationParam();
                    Display_RTVStationParam();

                    IsIn_Stationinfo = true;

                    Enable_Btn();
                }
                else
                {
                    MessageBox.Show("해당 파일안에 [" + tab_Station.Text + "] 데이터가 없습니다");
                }
            }
        }

        private void btn_SaveTotalFile4_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();

            if ((lv_StationParam.Items.Count == 0))
            {
                if (form_Main.GlobalObj.MsgBox_Confirm_YN("스테이션 정보가 작성되지 않았습니다. 이대로 저장하시겠습니까?"))
                {
                }
                else
                {
                    return;
                }

            }

            int check_Result = Check_StationValue();
            if (check_Result != -1)
            {
                if (form_Main.GlobalObj.MsgBox_Confirm_YN("스테이션 Position ID 값에 문제가 있습니다. 이대로 저장하시겠습니까?"))
                {

                }
                else
                {
                    lv_StationParam.SelectedItems.Clear();
                    lv_StationParam.Items[check_Result].Selected = true;
                    return;
                }
            }

            Ctrl_StationParam(true);

            saveFileDialog1.Filter = "*.RTVcfg|*.RTVCFG";

            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
            }

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                form_Main.RTV_ToTalFile.FileName = saveFileDialog1.FileName;
                form_Main.RTV_ToTalFile.Write_RTV_StationParam(rtv_StationParam_CTRL);
            }
        }

        private void cb_Station_Output_VisibleChanged(object sender, EventArgs e)
        {
            if (lv_Station_Item != null)
            {
                ComboBox TmpComboBox = (ComboBox)sender;

                for (byte i = 0; i <= (lv_StationParam.Columns.Count - 1); i++)
                {
                    if (Station_CtrlBox[i] != null)
                    {
                        if (TmpComboBox == Station_CtrlBox[i])
                        {
                            if (!TmpComboBox.Visible)
                            {
                                lv_Station_Item.SubItems[i].Text = Station_CtrlBox[i].Text;

                            }
                        }
                    }
                }
            }
        }

        private void lv_StationParam_DoubleClick(object sender, EventArgs e)
        {
            bool once = false;
            if (lv_StationParam.SelectedItems.Count == 1)
            {
                for (byte i = 0; i <= (lv_StationParam.Columns.Count - 1); i++)
                {
                    if (Station_CtrlBox[i] != null)
                    {
                        if (Convert.ToByte(Station_CtrlBox[i].Tag.ToString()) != 1)
                        {
                            Station_CtrlBox[i].Visible = true;
                            Station_CtrlBox[i].BringToFront();
                            if (!once)
                            {
                                Station_CtrlBox[i].Focus();
                                once = true;
                            }
                        }
                    }
                }
            }
        }

        private void lv_StationParam_MouseUp(object sender, MouseEventArgs e)
        {
            // Get the item on the row that is clicked.
            lv_Station_Item = this.lv_StationParam.GetItemAt(e.X, e.Y);

            // Make sure that an item is clicked.
            if (lv_Station_Item != null)
            {
                //Column 2
                Rectangle ClickedItem;


                for (byte i = 0; i <= (lv_StationParam.Columns.Count - 1); i++)
                {
                    if (Station_CtrlBox[i] != null)
                    {
                        ClickedItem = lv_Station_Item.SubItems[i].Bounds;

                        //화면에 안보이는 경우
                        if ((ClickedItem.Left + this.lv_StationParam.Columns[i].Width) < 0)
                        {
                            Station_CtrlBox[i].Tag = 1;
                            return;
                        }
                        else if (ClickedItem.Left < 0) // 해당 컬럼의 Left가 화면을 벗어난 경우
                        {
                            Station_CtrlBox[i].Tag = 0;
                            if ((ClickedItem.Left + this.lv_StationParam.Columns[i].Width) > this.lv_StationParam.Width) // 해당 컬럼의 Right가 화면을 벗어난 경우
                            {
                                ClickedItem.Width = this.lv_StationParam.Width;
                                ClickedItem.X = 0;
                            }
                            else // 해당 컬럼의 Right가 화면을 벗어나지 않은 경우
                            {
                                ClickedItem.Width = this.lv_StationParam.Columns[i].Width + ClickedItem.Left;
                                ClickedItem.X = 2;
                            }
                        }
                        else // 해당 컬럼의 Left가 화면을 벗어나지 않은 경우
                        {
                            Station_CtrlBox[i].Tag = 0;
                            if ((ClickedItem.Left + this.lv_StationParam.Columns[i].Width) > this.lv_StationParam.Width)  // 해당 컬럼의 Left가 화면을 벗어난 경우
                            {
                                ClickedItem.Width = this.lv_StationParam.Width - ClickedItem.Left;
                            }
                            else // 해당 컬럼의 Right가 화면을 벗어나지 않은 경우
                            {
                                ClickedItem.Width = this.lv_StationParam.Columns[i].Width;
                            }
                        }

                        // Adjust the top to account for the location of the ListView.
                        ClickedItem.Y += lv_StationParam.Top;
                        ClickedItem.X += lv_StationParam.Left;

                        // Assign calculated bounds to the TextBox.
                        Station_CtrlBox[i].Bounds = ClickedItem;

                        // Set default text for TextBox to match the item that is clicked.
                        Station_CtrlBox[i].Text = lv_Station_Item.SubItems[i].Text;
                    }
                }
            }
            else
            {
                for (byte i = 0; i <= (lv_StationParam.Columns.Count - 1); i++)
                {
                    if (Station_CtrlBox[i] != null)
                    {
                        Station_CtrlBox[i].Visible = false;
                    }

                }
            }

        }

        private void lv_StationParam_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (byte i = 0; i <= (lv_StationParam.Columns.Count - 1); i++)
            {
                if (Station_CtrlBox[i] != null)
                {
                    Station_CtrlBox[i].Visible = false;
                }
            }
        }

        private void lv_StationParam_Enter(object sender, EventArgs e)
        {
            Hide_AllEdit();
        }

        private void ed_Station_PositionID_VisibleChanged(object sender, EventArgs e)
        {
            if (lv_Station_Item != null)
            {
                TextBox TmpTextBox = (TextBox)sender;

                for (byte i = 0; i <= (lv_StationParam.Columns.Count - 1); i++)
                {
                    if (Station_CtrlBox[i] != null)
                    {
                        if (TmpTextBox == Station_CtrlBox[i])
                        {
                            if (!TmpTextBox.Visible)
                            {
                                lv_Station_Item.SubItems[i].Text = Station_CtrlBox[i].Text;
                            }
                        }
                    }
                }
            }
        }

        private void ed_Station_PositionID_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox ed = sender as TextBox;

            if (
                (char.IsControl(e.KeyChar)) ||
                (char.IsDigit(e.KeyChar)) ||
                (e.KeyChar == Convert.ToChar(Keys.Enter)) ||
                (e.KeyChar == Convert.ToChar(Keys.Back)) ||
                (e.KeyChar == Convert.ToChar(Keys.Tab)) ||
                (e.KeyChar == '-')
                )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ed.DeselectAll();
                if (ed.Name == "ed_Station_PositionID") cb_Station_Output.Focus();
            }
        }

        private void btn_AddStation_Prev_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;

            Edit_StationID(Convert.ToByte(bt.Tag.ToString()));
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
        }

        private void tab_SpeedArea_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "*.ini|*.INI";
            
            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                    for (int i = 0; i < lv_Position_Set_View.Items.Count; i++)
                    {
                        IniControl.WriteIni(saveFileDialog1.FileName, "POSITION", (i + 1).ToString(), lv_Position_Set_View.Items[i].SubItems[1].Text.ToString());
                    }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "*.ini|*.INI";

            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                for (int i = 0; i < lv_SpeedArea.Items.Count; i++)
                {
                    IniControl.WriteIni(saveFileDialog1.FileName, "구간", (i + 1).ToString(), 
                                       lv_SpeedArea.Items[i].SubItems[1].Text.ToString() + "," + 
                                       lv_SpeedArea.Items[i].SubItems[2].Text.ToString() + "," + 
                                       lv_SpeedArea.Items[i].SubItems[3].Text.ToString() + "," + 
                                       lv_SpeedArea.Items[i].SubItems[4].Text.ToString());
                }
            }
        }

        private void ed_RTV_Width_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox ed = sender as TextBox;

            if (ed.Tag.ToString() == "10") // 마이너스 ON, 소수점 OFF
            {
                if (
                (char.IsControl(e.KeyChar)) ||
                (char.IsDigit(e.KeyChar)) ||
                (e.KeyChar == '-') ||
                (e.KeyChar == Convert.ToChar(Keys.Enter)) ||
                (e.KeyChar == Convert.ToChar(Keys.Back)) ||
                (e.KeyChar == Convert.ToChar(Keys.Tab)))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            else if (ed.Tag.ToString() == "11") // 마이너스 ON, 소수점 ON
            {
                if (
                (char.IsControl(e.KeyChar)) ||
                (char.IsDigit(e.KeyChar)) ||
                (e.KeyChar == '-') ||
                (e.KeyChar == '.') ||
                (e.KeyChar == Convert.ToChar(Keys.Enter)) ||
                (e.KeyChar == Convert.ToChar(Keys.Back)) ||
                (e.KeyChar == Convert.ToChar(Keys.Tab)))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            else if (ed.Tag.ToString() == "01") // 마이너스 OFF, 소수점 ON
            {
                if (
                (char.IsControl(e.KeyChar)) ||
                (char.IsDigit(e.KeyChar)) ||
                (e.KeyChar == '.') ||
                (e.KeyChar == Convert.ToChar(Keys.Enter)) ||
                (e.KeyChar == Convert.ToChar(Keys.Back)) ||
                (e.KeyChar == Convert.ToChar(Keys.Tab)))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }

            }
            else if (ed.Tag.ToString() == "00") // 마이너스 OFF, 소수점 OFF
            {
                if (
                (char.IsControl(e.KeyChar)) ||
                (char.IsDigit(e.KeyChar)) ||
                (e.KeyChar == Convert.ToChar(Keys.Enter)) ||
                (e.KeyChar == Convert.ToChar(Keys.Back)) ||
                (e.KeyChar == Convert.ToChar(Keys.Tab)))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string TmpPositionStr = "";
            int TmpPosition = 0;
            for (int i = 0; i < lv_StationParam.Items.Count; i++)
            {
                TmpPositionStr = lv_StationParam.Items[i].SubItems[1].Text;
                TmpPosition = Convert.ToInt32(TmpPositionStr);
                list.Add(lv_StationParam.Items[i].SubItems[0].Text + "," + lv_Position_Set_View.Items[TmpPosition - 1].SubItems[1].Text);
            }
            StreamWriter sw;
            sw = new StreamWriter("Log.txt");
            int nCount = list.Count;
            for (int i = 0; i < nCount; i++)
            {
                list[i] += "\r\n";
                sw.Write(list[i].ToString());
            }
            sw.Close();
        
        }
    }
}
