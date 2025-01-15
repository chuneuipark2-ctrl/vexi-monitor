using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Runtime.InteropServices;   //DllImport
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;



namespace VEXI
{
    public partial class Form_SRMRack : Form
    {
        public Form_Main form_Main;


        private static VEXI_DEFS.TSRM_StationParam srm_StationParam_Res;
        private static VEXI_DEFS.TSRM_StationParam srm_StationParam_CTRL;
        private static VEXI_DEFS.TDEV_CtrlRes_3Byte dev_StationParam_CTRLRes;


        private static VEXI_DEFS.TSRM_CellPositionRES srm_CellPosition_Res;
        private static VEXI_DEFS.TSRM_CellPositionCTRL srm_CellPosition_CTRL;
        private static VEXI_DEFS.TSRM_CellPositionCTRLRes srm_CellPosition_CTRLRes;


        private static VEXI_DEFS.TSRM_CellOffsetREQ srm_CellOffset_Req;
        private static VEXI_DEFS.TSRM_CellOffset srm_CellOffset_Res;
        private static VEXI_DEFS.TSRM_CellOffset srm_CellOffset_CTRL;
        private static VEXI_DEFS.TSRM_CellOffsetCTRLRes srm_CellOffset_CTRLRes;
        private VEXI_DEFS.SRM_CellOffset_Total CellOffset_Total = new VEXI_DEFS.SRM_CellOffset_Total();

        private DateTime RxDateTime;
        private byte Retrycount;


        private bool IsIn_RackPosition;
        private bool IsIn_RackOffset;
        private bool IsIn_Stationinfo;

        private ListViewItem lv_RackOffset_Item;
        private ListViewItem lv_BayPosition_Item;
        private ListViewItem lv_LevelPosition_Item;

        private static RadioButton[] rb_Station_None;
        private static RadioButton[] rb_Station_In;
        private static RadioButton[] rb_Station_Out;
        private static RadioButton[] rb_Station_InOut;
        private static RadioButton[] rb_Station_Virtual;
        
        private static TextBox[] ed_Station_DelayTime;

        private static CheckBox[] cb_UseIsExistItem;

        private static CheckBox[] cb_Station_Item0;
        private static CheckBox[] cb_Station_Item1;
        private static CheckBox[] cb_Station_Item2;
        private static CheckBox[] cb_Station_Item3;
        private static CheckBox[] cb_Station_Item4;
        private static CheckBox[] cb_Station_Item5;
        private static CheckBox[] cb_Station_Item6;
        private static CheckBox[] cb_Station_Item7;

        private static TextBox[] ed_Station_ForkDepth;
        private static TextBox[] ed_Station_Travel;
        private static TextBox[] ed_Station_Lift;
        private static TextBox[] ed_Station_UpOffset;
        private static TextBox[] ed_Station_DownOffset;
        private static TextBox[] ed_Station_InterlockIndex;


        private static TextBox[] Rack_Offset_TextBox;

        private static TextBox[] Bay_Position_TextBox;
        private static TextBox[] Level_Position_TextBox;

        public Form_SRMRack()
        {
            InitializeComponent();

            Bay_Position_TextBox = new TextBox[] { null, ed_Bay_Position_L, ed_Bay_Position_R };
            Level_Position_TextBox = new TextBox[] { null, ed_Level_Position_L, ed_Level_Position_R };

            Rack_Offset_TextBox = new TextBox[] { null, null, ed_Rack_Offset_2, ed_Rack_Offset_3, ed_Rack_Offset_4, ed_Rack_Offset_5, ed_Rack_Offset_6, ed_Rack_Offset_7 };

            cb_UseIsExistItem = new CheckBox[] { cb_Station1_UseIsExistItem, cb_Station2_UseIsExistItem, cb_Station3_UseIsExistItem, cb_Station4_UseIsExistItem,  cb_Station5_UseIsExistItem, cb_Station6_UseIsExistItem, cb_Station7_UseIsExistItem, cb_Station8_UseIsExistItem, cb_Station9_UseIsExistItem, cb_Station10_UseIsExistItem,
                                                cb_Station11_UseIsExistItem, cb_Station12_UseIsExistItem, cb_Station13_UseIsExistItem, cb_Station14_UseIsExistItem, cb_Station15_UseIsExistItem, cb_Station16_UseIsExistItem, cb_Station17_UseIsExistItem, cb_Station18_UseIsExistItem, cb_Station19_UseIsExistItem, cb_Station20_UseIsExistItem,
                                                cb_Station21_UseIsExistItem, cb_Station22_UseIsExistItem, cb_Station23_UseIsExistItem, cb_Station24_UseIsExistItem, cb_Station25_UseIsExistItem, cb_Station26_UseIsExistItem, cb_Station27_UseIsExistItem, cb_Station28_UseIsExistItem, cb_Station29_UseIsExistItem, cb_Station30_UseIsExistItem,
                                                cb_Station31_UseIsExistItem, cb_Station32_UseIsExistItem, cb_Station33_UseIsExistItem, cb_Station34_UseIsExistItem, cb_Station35_UseIsExistItem, cb_Station36_UseIsExistItem, cb_Station37_UseIsExistItem, cb_Station38_UseIsExistItem, cb_Station39_UseIsExistItem, cb_Station40_UseIsExistItem,
                                                cb_Station41_UseIsExistItem, cb_Station42_UseIsExistItem, cb_Station43_UseIsExistItem, cb_Station44_UseIsExistItem, cb_Station45_UseIsExistItem, cb_Station46_UseIsExistItem, cb_Station47_UseIsExistItem, cb_Station48_UseIsExistItem, cb_Station49_UseIsExistItem, cb_Station50_UseIsExistItem};

            cb_Station_Item0 = new CheckBox[] { cb_Station1_Item0, cb_Station2_Item0, cb_Station3_Item0, cb_Station4_Item0,  cb_Station5_Item0, cb_Station6_Item0, cb_Station7_Item0, cb_Station8_Item0, cb_Station9_Item0, cb_Station10_Item0,
                                                cb_Station11_Item0, cb_Station12_Item0, cb_Station13_Item0, cb_Station14_Item0, cb_Station15_Item0, cb_Station16_Item0, cb_Station17_Item0, cb_Station18_Item0, cb_Station19_Item0, cb_Station20_Item0,
                                                cb_Station21_Item0, cb_Station22_Item0, cb_Station23_Item0, cb_Station24_Item0, cb_Station25_Item0, cb_Station26_Item0, cb_Station27_Item0, cb_Station28_Item0, cb_Station29_Item0, cb_Station30_Item0,
                                                cb_Station31_Item0, cb_Station32_Item0, cb_Station33_Item0, cb_Station34_Item0, cb_Station35_Item0, cb_Station36_Item0, cb_Station37_Item0, cb_Station38_Item0, cb_Station39_Item0, cb_Station40_Item0,
                                                cb_Station41_Item0, cb_Station42_Item0, cb_Station43_Item0, cb_Station44_Item0, cb_Station45_Item0, cb_Station46_Item0, cb_Station47_Item0, cb_Station48_Item0, cb_Station49_Item0, cb_Station50_Item0};
            cb_Station_Item1 = new CheckBox[] { cb_Station1_Item1, cb_Station2_Item1, cb_Station3_Item1, cb_Station4_Item1, cb_Station5_Item1, cb_Station6_Item1, cb_Station7_Item1, cb_Station8_Item1, cb_Station9_Item1, cb_Station10_Item1,
                                                cb_Station11_Item1, cb_Station12_Item1, cb_Station13_Item1, cb_Station14_Item1, cb_Station15_Item1, cb_Station16_Item1, cb_Station17_Item1, cb_Station18_Item1, cb_Station19_Item1, cb_Station20_Item1,
                                                cb_Station21_Item1, cb_Station22_Item1, cb_Station23_Item1, cb_Station24_Item1, cb_Station25_Item1, cb_Station26_Item1, cb_Station27_Item1, cb_Station28_Item1, cb_Station29_Item1, cb_Station30_Item1,
                                                cb_Station31_Item1, cb_Station32_Item1, cb_Station33_Item1, cb_Station34_Item1, cb_Station35_Item1, cb_Station36_Item1, cb_Station37_Item1, cb_Station38_Item1, cb_Station39_Item1, cb_Station40_Item1,
                                                cb_Station41_Item1, cb_Station42_Item1, cb_Station43_Item1, cb_Station44_Item1, cb_Station45_Item1, cb_Station46_Item1, cb_Station47_Item1, cb_Station48_Item1, cb_Station49_Item1, cb_Station50_Item1};

            cb_Station_Item2 = new CheckBox[] { cb_Station1_Item2, cb_Station2_Item2, cb_Station3_Item2, cb_Station4_Item2, cb_Station5_Item2, cb_Station6_Item2, cb_Station7_Item2, cb_Station8_Item2, cb_Station9_Item2, cb_Station10_Item2,
                                                cb_Station11_Item2, cb_Station12_Item2, cb_Station13_Item2, cb_Station14_Item2, cb_Station15_Item2, cb_Station16_Item2, cb_Station17_Item2, cb_Station18_Item2, cb_Station19_Item2, cb_Station20_Item2,
                                                cb_Station21_Item2, cb_Station22_Item2, cb_Station23_Item2, cb_Station24_Item2, cb_Station25_Item2, cb_Station26_Item2, cb_Station27_Item2, cb_Station28_Item2, cb_Station29_Item2, cb_Station30_Item2,
                                                cb_Station31_Item2, cb_Station32_Item2, cb_Station33_Item2, cb_Station34_Item2, cb_Station35_Item2, cb_Station36_Item2, cb_Station37_Item2, cb_Station38_Item2, cb_Station39_Item2, cb_Station40_Item2,
                                                cb_Station41_Item2, cb_Station42_Item2, cb_Station43_Item2, cb_Station44_Item2, cb_Station45_Item2, cb_Station46_Item2, cb_Station47_Item2, cb_Station48_Item2, cb_Station49_Item2, cb_Station50_Item2};

            cb_Station_Item3 = new CheckBox[] { cb_Station1_Item3, cb_Station2_Item3, cb_Station3_Item3, cb_Station4_Item3, cb_Station5_Item3, cb_Station6_Item3, cb_Station7_Item3, cb_Station8_Item3, cb_Station9_Item3, cb_Station10_Item3,
                                                cb_Station11_Item3, cb_Station12_Item3, cb_Station13_Item3, cb_Station14_Item3, cb_Station15_Item3, cb_Station16_Item3, cb_Station17_Item3, cb_Station18_Item3, cb_Station19_Item3, cb_Station20_Item3,
                                                cb_Station21_Item3, cb_Station22_Item3, cb_Station23_Item3, cb_Station24_Item3, cb_Station25_Item3, cb_Station26_Item3, cb_Station27_Item3, cb_Station28_Item3, cb_Station29_Item3, cb_Station30_Item3,
                                                cb_Station31_Item3, cb_Station32_Item3, cb_Station33_Item3, cb_Station34_Item3, cb_Station35_Item3, cb_Station36_Item3, cb_Station37_Item3, cb_Station38_Item3, cb_Station39_Item3, cb_Station40_Item3,
                                                cb_Station41_Item3, cb_Station42_Item3, cb_Station43_Item3, cb_Station44_Item3, cb_Station45_Item3, cb_Station46_Item3, cb_Station47_Item3, cb_Station48_Item3, cb_Station49_Item3, cb_Station50_Item3};

            cb_Station_Item4 = new CheckBox[] { cb_Station1_Item4, cb_Station2_Item4, cb_Station3_Item4, cb_Station4_Item4, cb_Station5_Item4, cb_Station6_Item4, cb_Station7_Item4, cb_Station8_Item4, cb_Station9_Item4, cb_Station10_Item4,
                                                cb_Station11_Item4, cb_Station12_Item4, cb_Station13_Item4, cb_Station14_Item4, cb_Station15_Item4, cb_Station16_Item4, cb_Station17_Item4, cb_Station18_Item4, cb_Station19_Item4, cb_Station20_Item4,
                                                cb_Station21_Item4, cb_Station22_Item4, cb_Station23_Item4, cb_Station24_Item4, cb_Station25_Item4, cb_Station26_Item4, cb_Station27_Item4, cb_Station28_Item4, cb_Station29_Item4, cb_Station30_Item4,
                                                cb_Station31_Item4, cb_Station32_Item4, cb_Station33_Item4, cb_Station34_Item4, cb_Station35_Item4, cb_Station36_Item4, cb_Station37_Item4, cb_Station38_Item4, cb_Station39_Item4, cb_Station40_Item4,
                                                cb_Station41_Item4, cb_Station42_Item4, cb_Station43_Item4, cb_Station44_Item4, cb_Station45_Item4, cb_Station46_Item4, cb_Station47_Item4, cb_Station48_Item4, cb_Station49_Item4, cb_Station50_Item4};

            cb_Station_Item5 = new CheckBox[] { cb_Station1_Item5, cb_Station2_Item5, cb_Station3_Item5, cb_Station4_Item5, cb_Station5_Item5, cb_Station6_Item5, cb_Station7_Item5, cb_Station8_Item5, cb_Station9_Item5, cb_Station10_Item5,
                                                cb_Station11_Item5, cb_Station12_Item5, cb_Station13_Item5, cb_Station14_Item5, cb_Station15_Item5, cb_Station16_Item5, cb_Station17_Item5, cb_Station18_Item5, cb_Station19_Item5, cb_Station20_Item5,
                                                cb_Station21_Item5, cb_Station22_Item5, cb_Station23_Item5, cb_Station24_Item5, cb_Station25_Item5, cb_Station26_Item5, cb_Station27_Item5, cb_Station28_Item5, cb_Station29_Item5, cb_Station30_Item5,
                                                cb_Station31_Item5, cb_Station32_Item5, cb_Station33_Item5, cb_Station34_Item5, cb_Station35_Item5, cb_Station36_Item5, cb_Station37_Item5, cb_Station38_Item5, cb_Station39_Item5, cb_Station40_Item5,
                                                cb_Station41_Item5, cb_Station42_Item5, cb_Station43_Item5, cb_Station44_Item5, cb_Station45_Item5, cb_Station46_Item5, cb_Station47_Item5, cb_Station48_Item5, cb_Station49_Item5, cb_Station50_Item5};

            cb_Station_Item6 = new CheckBox[] { cb_Station1_Item6, cb_Station2_Item6, cb_Station3_Item6, cb_Station4_Item6, cb_Station5_Item6, cb_Station6_Item6, cb_Station7_Item6, cb_Station8_Item6, cb_Station9_Item6, cb_Station10_Item6,
                                                cb_Station11_Item6, cb_Station12_Item6, cb_Station13_Item6, cb_Station14_Item6, cb_Station15_Item6, cb_Station16_Item6, cb_Station17_Item6, cb_Station18_Item6, cb_Station19_Item6, cb_Station20_Item6,
                                                cb_Station21_Item6, cb_Station22_Item6, cb_Station23_Item6, cb_Station24_Item6, cb_Station25_Item6, cb_Station26_Item6, cb_Station27_Item6, cb_Station28_Item6, cb_Station29_Item6, cb_Station30_Item6,
                                                cb_Station31_Item6, cb_Station32_Item6, cb_Station33_Item6, cb_Station34_Item6, cb_Station35_Item6, cb_Station36_Item6, cb_Station37_Item6, cb_Station38_Item6, cb_Station39_Item6, cb_Station40_Item6,
                                                cb_Station41_Item6, cb_Station42_Item6, cb_Station43_Item6, cb_Station44_Item6, cb_Station45_Item6, cb_Station46_Item6, cb_Station47_Item6, cb_Station48_Item6, cb_Station49_Item6, cb_Station50_Item6};

            cb_Station_Item7 = new CheckBox[] { cb_Station1_Item7, cb_Station2_Item7, cb_Station3_Item7, cb_Station4_Item7, cb_Station5_Item7, cb_Station6_Item7, cb_Station7_Item7, cb_Station8_Item7, cb_Station9_Item7, cb_Station10_Item7,
                                                cb_Station11_Item7, cb_Station12_Item7, cb_Station13_Item7, cb_Station14_Item7, cb_Station15_Item7, cb_Station16_Item7, cb_Station17_Item7, cb_Station18_Item7, cb_Station19_Item7, cb_Station20_Item7,
                                                cb_Station21_Item7, cb_Station22_Item7, cb_Station23_Item7, cb_Station24_Item7, cb_Station25_Item7, cb_Station26_Item7, cb_Station27_Item7, cb_Station28_Item7, cb_Station29_Item7, cb_Station30_Item7,
                                                cb_Station31_Item7, cb_Station32_Item7, cb_Station33_Item7, cb_Station34_Item7, cb_Station35_Item7, cb_Station36_Item7, cb_Station37_Item7, cb_Station38_Item7, cb_Station39_Item7, cb_Station40_Item7,
                                                cb_Station41_Item6, cb_Station42_Item7, cb_Station43_Item7, cb_Station44_Item7, cb_Station45_Item7, cb_Station46_Item7, cb_Station47_Item7, cb_Station48_Item7, cb_Station49_Item7, cb_Station50_Item7};

            rb_Station_None = new RadioButton[] { rb_Station1_None, rb_Station2_None, rb_Station3_None, rb_Station4_None, rb_Station5_None, rb_Station6_None, rb_Station7_None, rb_Station8_None, rb_Station9_None, rb_Station10_None,
                                                  rb_Station11_None, rb_Station12_None, rb_Station13_None, rb_Station14_None, rb_Station15_None, rb_Station16_None, rb_Station17_None, rb_Station18_None, rb_Station19_None, rb_Station20_None,
                                                  rb_Station21_None, rb_Station22_None, rb_Station23_None, rb_Station24_None, rb_Station25_None, rb_Station26_None, rb_Station27_None, rb_Station28_None, rb_Station29_None, rb_Station30_None,
                                                  rb_Station31_None, rb_Station32_None, rb_Station33_None, rb_Station34_None, rb_Station35_None, rb_Station36_None, rb_Station37_None, rb_Station38_None, rb_Station39_None, rb_Station40_None,
                                                  rb_Station41_None, rb_Station42_None, rb_Station43_None, rb_Station44_None, rb_Station45_None, rb_Station46_None, rb_Station47_None, rb_Station48_None, rb_Station49_None, rb_Station50_None};

            rb_Station_In = new RadioButton[] { rb_Station1_In, rb_Station2_In, rb_Station3_In, rb_Station4_In, rb_Station5_In, rb_Station6_In, rb_Station7_In, rb_Station8_In, rb_Station9_In, rb_Station10_In,
                                                rb_Station11_In, rb_Station12_In, rb_Station13_In, rb_Station14_In, rb_Station15_In, rb_Station16_In, rb_Station17_In, rb_Station18_In, rb_Station19_In, rb_Station20_In,
                                                rb_Station21_In, rb_Station22_In, rb_Station23_In, rb_Station24_In, rb_Station25_In, rb_Station26_In, rb_Station27_In, rb_Station28_In, rb_Station29_In, rb_Station30_In,
                                                rb_Station31_In, rb_Station32_In, rb_Station33_In, rb_Station34_In, rb_Station35_In, rb_Station36_In, rb_Station37_In, rb_Station38_In, rb_Station39_In, rb_Station40_In,
                                                rb_Station41_In, rb_Station42_In, rb_Station43_In, rb_Station44_In, rb_Station45_In, rb_Station46_In, rb_Station47_In, rb_Station48_In, rb_Station49_In, rb_Station50_In };

            rb_Station_Out = new RadioButton[] { rb_Station1_Out, rb_Station2_Out, rb_Station3_Out, rb_Station4_Out, rb_Station5_Out, rb_Station6_Out, rb_Station7_Out, rb_Station8_Out, rb_Station9_Out, rb_Station10_Out,
                                                 rb_Station11_Out, rb_Station12_Out, rb_Station13_Out, rb_Station14_Out, rb_Station15_Out, rb_Station16_Out, rb_Station17_Out, rb_Station18_Out, rb_Station19_Out, rb_Station20_Out,
                                                 rb_Station21_Out, rb_Station22_Out, rb_Station23_Out, rb_Station24_Out, rb_Station25_Out, rb_Station26_Out, rb_Station27_Out, rb_Station28_Out, rb_Station29_Out, rb_Station30_Out,
                                                 rb_Station31_Out, rb_Station32_Out, rb_Station33_Out, rb_Station34_Out, rb_Station35_Out, rb_Station36_Out, rb_Station37_Out, rb_Station38_Out, rb_Station39_Out, rb_Station40_Out,
                                                 rb_Station41_Out, rb_Station42_Out, rb_Station43_Out, rb_Station44_Out, rb_Station45_Out, rb_Station46_Out, rb_Station47_Out, rb_Station48_Out, rb_Station49_Out, rb_Station50_Out};

            rb_Station_InOut = new RadioButton[] { rb_Station1_InOut, rb_Station2_InOut, rb_Station3_InOut, rb_Station4_InOut, rb_Station5_InOut, rb_Station6_InOut, rb_Station7_InOut, rb_Station8_InOut, rb_Station9_InOut, rb_Station10_InOut,
                                                   rb_Station11_InOut, rb_Station12_InOut, rb_Station13_InOut, rb_Station14_InOut, rb_Station15_InOut, rb_Station16_InOut, rb_Station17_InOut, rb_Station18_InOut, rb_Station19_InOut, rb_Station20_InOut,
                                                   rb_Station21_InOut, rb_Station22_InOut, rb_Station23_InOut, rb_Station24_InOut, rb_Station25_InOut, rb_Station26_InOut, rb_Station27_InOut, rb_Station28_InOut, rb_Station29_InOut, rb_Station30_InOut,
                                                   rb_Station31_InOut, rb_Station32_InOut, rb_Station33_InOut, rb_Station34_InOut, rb_Station35_InOut, rb_Station36_InOut, rb_Station37_InOut, rb_Station38_InOut, rb_Station39_InOut, rb_Station40_InOut,
                                                   rb_Station41_InOut, rb_Station42_InOut, rb_Station43_InOut, rb_Station44_InOut, rb_Station45_InOut, rb_Station46_InOut, rb_Station47_InOut, rb_Station48_InOut, rb_Station49_InOut, rb_Station50_InOut};

            rb_Station_Virtual = new RadioButton[] { rb_Station1_Virtual, rb_Station2_Virtual, rb_Station3_Virtual, rb_Station4_Virtual, rb_Station5_Virtual, rb_Station6_Virtual, rb_Station7_Virtual, rb_Station8_Virtual, rb_Station9_Virtual, rb_Station10_Virtual,
                                                     rb_Station11_Virtual, rb_Station12_Virtual, rb_Station13_Virtual, rb_Station14_Virtual, rb_Station15_Virtual, rb_Station16_Virtual, rb_Station17_Virtual, rb_Station18_Virtual, rb_Station19_Virtual, rb_Station20_Virtual,
                                                     rb_Station21_Virtual, rb_Station22_Virtual, rb_Station23_Virtual, rb_Station24_Virtual, rb_Station25_Virtual, rb_Station26_Virtual, rb_Station27_Virtual, rb_Station28_Virtual, rb_Station29_Virtual, rb_Station30_Virtual,
                                                     rb_Station31_Virtual, rb_Station32_Virtual, rb_Station33_Virtual, rb_Station34_Virtual, rb_Station35_Virtual, rb_Station36_Virtual, rb_Station37_Virtual, rb_Station38_Virtual, rb_Station39_Virtual, rb_Station40_Virtual,
                                                     rb_Station41_Virtual, rb_Station42_Virtual, rb_Station43_Virtual, rb_Station44_Virtual, rb_Station45_Virtual, rb_Station46_Virtual, rb_Station47_Virtual, rb_Station48_Virtual, rb_Station49_Virtual, rb_Station50_Virtual};

            ed_Station_DelayTime = new TextBox[] { ed_Station1_DelayTime, ed_Station2_DelayTime, ed_Station3_DelayTime, ed_Station4_DelayTime, ed_Station5_DelayTime, ed_Station6_DelayTime, ed_Station7_DelayTime, ed_Station8_DelayTime, ed_Station9_DelayTime, ed_Station10_DelayTime,
                                                   ed_Station11_DelayTime, ed_Station12_DelayTime, ed_Station13_DelayTime, ed_Station14_DelayTime, ed_Station15_DelayTime, ed_Station16_DelayTime, ed_Station17_DelayTime, ed_Station18_DelayTime, ed_Station19_DelayTime, ed_Station20_DelayTime,
                                                   ed_Station21_DelayTime, ed_Station22_DelayTime, ed_Station23_DelayTime, ed_Station24_DelayTime, ed_Station25_DelayTime, ed_Station26_DelayTime, ed_Station27_DelayTime, ed_Station28_DelayTime, ed_Station29_DelayTime, ed_Station30_DelayTime,
                                                   ed_Station31_DelayTime, ed_Station32_DelayTime, ed_Station33_DelayTime, ed_Station34_DelayTime, ed_Station35_DelayTime, ed_Station36_DelayTime, ed_Station37_DelayTime, ed_Station38_DelayTime, ed_Station39_DelayTime, ed_Station40_DelayTime,
                                                   ed_Station41_DelayTime, ed_Station42_DelayTime, ed_Station43_DelayTime, ed_Station44_DelayTime, ed_Station45_DelayTime, ed_Station46_DelayTime, ed_Station47_DelayTime, ed_Station48_DelayTime, ed_Station49_DelayTime, ed_Station50_DelayTime};

            ed_Station_ForkDepth = new TextBox[] { ed_Station1_ForkDepth, ed_Station2_ForkDepth, ed_Station3_ForkDepth, ed_Station4_ForkDepth, ed_Station5_ForkDepth, ed_Station6_ForkDepth, ed_Station7_ForkDepth, ed_Station8_ForkDepth, ed_Station9_ForkDepth, ed_Station10_ForkDepth,
                                                   ed_Station11_ForkDepth, ed_Station12_ForkDepth, ed_Station13_ForkDepth, ed_Station14_ForkDepth, ed_Station15_ForkDepth, ed_Station16_ForkDepth, ed_Station17_ForkDepth, ed_Station18_ForkDepth, ed_Station19_ForkDepth, ed_Station20_ForkDepth,
                                                   ed_Station21_ForkDepth, ed_Station22_ForkDepth, ed_Station23_ForkDepth, ed_Station24_ForkDepth, ed_Station25_ForkDepth, ed_Station26_ForkDepth, ed_Station27_ForkDepth, ed_Station28_ForkDepth, ed_Station29_ForkDepth, ed_Station30_ForkDepth,
                                                   ed_Station31_ForkDepth, ed_Station32_ForkDepth, ed_Station33_ForkDepth, ed_Station34_ForkDepth, ed_Station35_ForkDepth, ed_Station36_ForkDepth, ed_Station37_ForkDepth, ed_Station38_ForkDepth, ed_Station39_ForkDepth, ed_Station40_ForkDepth,
                                                   ed_Station41_ForkDepth, ed_Station42_ForkDepth, ed_Station43_ForkDepth, ed_Station44_ForkDepth, ed_Station45_ForkDepth, ed_Station46_ForkDepth, ed_Station47_ForkDepth, ed_Station48_ForkDepth, ed_Station49_ForkDepth, ed_Station50_ForkDepth};

            ed_Station_Travel = new TextBox[] { ed_Station1_Travel, ed_Station2_Travel, ed_Station3_Travel, ed_Station4_Travel, ed_Station5_Travel, ed_Station6_Travel, ed_Station7_Travel, ed_Station8_Travel, ed_Station9_Travel, ed_Station10_Travel,
                                                ed_Station11_Travel, ed_Station12_Travel, ed_Station13_Travel, ed_Station14_Travel, ed_Station15_Travel, ed_Station16_Travel, ed_Station17_Travel, ed_Station18_Travel, ed_Station19_Travel, ed_Station20_Travel,
                                                ed_Station21_Travel, ed_Station22_Travel, ed_Station23_Travel, ed_Station24_Travel, ed_Station25_Travel, ed_Station26_Travel, ed_Station27_Travel, ed_Station28_Travel, ed_Station29_Travel, ed_Station30_Travel,
                                                ed_Station31_Travel, ed_Station32_Travel, ed_Station33_Travel, ed_Station34_Travel, ed_Station35_Travel, ed_Station36_Travel, ed_Station37_Travel, ed_Station38_Travel, ed_Station39_Travel, ed_Station40_Travel,
                                                ed_Station41_Travel, ed_Station42_Travel, ed_Station43_Travel, ed_Station44_Travel, ed_Station45_Travel, ed_Station46_Travel, ed_Station47_Travel, ed_Station48_Travel, ed_Station49_Travel, ed_Station50_Travel};

            ed_Station_Lift = new TextBox[] { ed_Station1_Lift, ed_Station2_Lift, ed_Station3_Lift, ed_Station4_Lift,  ed_Station5_Lift, ed_Station6_Lift, ed_Station7_Lift, ed_Station8_Lift, ed_Station9_Lift, ed_Station10_Lift,
                                              ed_Station11_Lift, ed_Station12_Lift, ed_Station13_Lift, ed_Station14_Lift, ed_Station15_Lift, ed_Station16_Lift, ed_Station17_Lift, ed_Station18_Lift, ed_Station19_Lift, ed_Station20_Lift,
                                              ed_Station21_Lift, ed_Station22_Lift, ed_Station23_Lift, ed_Station24_Lift, ed_Station25_Lift, ed_Station26_Lift, ed_Station27_Lift, ed_Station28_Lift, ed_Station29_Lift, ed_Station30_Lift,
                                              ed_Station31_Lift, ed_Station32_Lift, ed_Station33_Lift, ed_Station34_Lift, ed_Station35_Lift, ed_Station36_Lift, ed_Station37_Lift, ed_Station38_Lift, ed_Station39_Lift, ed_Station40_Lift,
                                              ed_Station41_Lift, ed_Station42_Lift, ed_Station43_Lift, ed_Station44_Lift, ed_Station45_Lift, ed_Station46_Lift, ed_Station47_Lift, ed_Station48_Lift, ed_Station49_Lift, ed_Station50_Lift};

            ed_Station_UpOffset = new TextBox[] { ed_Station1_UpOffset, ed_Station2_UpOffset, ed_Station3_UpOffset, ed_Station4_UpOffset, ed_Station5_UpOffset, ed_Station6_UpOffset, ed_Station7_UpOffset, ed_Station8_UpOffset, ed_Station9_UpOffset, ed_Station10_UpOffset,
                                                  ed_Station11_UpOffset, ed_Station12_UpOffset, ed_Station13_UpOffset, ed_Station14_UpOffset, ed_Station15_UpOffset, ed_Station16_UpOffset, ed_Station17_UpOffset, ed_Station18_UpOffset, ed_Station19_UpOffset, ed_Station20_UpOffset,
                                                  ed_Station21_UpOffset, ed_Station22_UpOffset, ed_Station23_UpOffset, ed_Station24_UpOffset, ed_Station25_UpOffset, ed_Station26_UpOffset, ed_Station27_UpOffset, ed_Station28_UpOffset, ed_Station29_UpOffset, ed_Station30_UpOffset,
                                                  ed_Station31_UpOffset, ed_Station32_UpOffset, ed_Station33_UpOffset, ed_Station34_UpOffset, ed_Station35_UpOffset, ed_Station36_UpOffset, ed_Station37_UpOffset, ed_Station38_UpOffset, ed_Station39_UpOffset, ed_Station40_UpOffset,
                                                  ed_Station41_UpOffset, ed_Station42_UpOffset, ed_Station43_UpOffset, ed_Station44_UpOffset, ed_Station45_UpOffset, ed_Station46_UpOffset, ed_Station47_UpOffset, ed_Station48_UpOffset, ed_Station49_UpOffset, ed_Station50_UpOffset};

            ed_Station_DownOffset = new TextBox[] { ed_Station1_DownOffset, ed_Station2_DownOffset, ed_Station3_DownOffset, ed_Station4_DownOffset,  ed_Station5_DownOffset, ed_Station6_DownOffset, ed_Station7_DownOffset, ed_Station8_DownOffset, ed_Station9_DownOffset, ed_Station10_DownOffset,
                                                    ed_Station11_DownOffset, ed_Station12_DownOffset, ed_Station13_DownOffset, ed_Station14_DownOffset, ed_Station15_DownOffset, ed_Station16_DownOffset, ed_Station17_DownOffset, ed_Station18_DownOffset, ed_Station19_DownOffset, ed_Station20_DownOffset,
                                                    ed_Station21_DownOffset, ed_Station22_DownOffset, ed_Station23_DownOffset, ed_Station24_DownOffset, ed_Station25_DownOffset, ed_Station26_DownOffset, ed_Station27_DownOffset, ed_Station28_DownOffset, ed_Station29_DownOffset, ed_Station30_DownOffset,
                                                    ed_Station31_DownOffset, ed_Station32_DownOffset, ed_Station33_DownOffset, ed_Station34_DownOffset, ed_Station35_DownOffset, ed_Station36_DownOffset, ed_Station37_DownOffset, ed_Station38_DownOffset, ed_Station39_DownOffset, ed_Station40_DownOffset,
                                                    ed_Station41_DownOffset, ed_Station42_DownOffset, ed_Station43_DownOffset, ed_Station44_DownOffset, ed_Station45_DownOffset, ed_Station46_DownOffset, ed_Station47_DownOffset, ed_Station48_DownOffset, ed_Station49_DownOffset, ed_Station50_DownOffset};

            ed_Station_InterlockIndex = new TextBox[] { ed_Station1_InterlockIndex, ed_Station2_InterlockIndex, ed_Station3_InterlockIndex, ed_Station4_InterlockIndex, ed_Station5_InterlockIndex, ed_Station6_InterlockIndex, ed_Station7_InterlockIndex, ed_Station8_InterlockIndex, ed_Station9_InterlockIndex, ed_Station10_InterlockIndex,
                                                        ed_Station11_InterlockIndex, ed_Station12_InterlockIndex, ed_Station13_InterlockIndex, ed_Station14_InterlockIndex, ed_Station15_InterlockIndex, ed_Station16_InterlockIndex, ed_Station17_InterlockIndex, ed_Station18_InterlockIndex, ed_Station19_InterlockIndex, ed_Station20_InterlockIndex,
                                                        ed_Station21_InterlockIndex, ed_Station22_InterlockIndex, ed_Station23_InterlockIndex, ed_Station24_InterlockIndex, ed_Station25_InterlockIndex, ed_Station26_InterlockIndex, ed_Station27_InterlockIndex, ed_Station28_InterlockIndex, ed_Station29_InterlockIndex, ed_Station30_InterlockIndex,
                                                        ed_Station31_InterlockIndex, ed_Station32_InterlockIndex, ed_Station33_InterlockIndex, ed_Station34_InterlockIndex, ed_Station35_InterlockIndex, ed_Station36_InterlockIndex, ed_Station37_InterlockIndex, ed_Station38_InterlockIndex, ed_Station39_InterlockIndex, ed_Station40_InterlockIndex,
                                                        ed_Station41_InterlockIndex, ed_Station42_InterlockIndex, ed_Station43_InterlockIndex, ed_Station44_InterlockIndex, ed_Station45_InterlockIndex, ed_Station46_InterlockIndex, ed_Station47_InterlockIndex, ed_Station48_InterlockIndex, ed_Station49_InterlockIndex, ed_Station50_InterlockIndex};


            IsIn_RackPosition = false;
            IsIn_RackOffset = false;
            IsIn_Stationinfo = false;

        }


        #region 컴포넌트 이벤트
        private void Form_SRMRack_Load(object sender, EventArgs e)
        {
            if (this.IsMdiChild)
            {
                form_Main = (Form_Main)this.MdiParent;
            }
            else
            {
                form_Main = (Form_Main)this.Owner;
            }
            Display_SRMPositionConfig_Init();
            //form_Main.COMMDataManager.ISPolingStop = true;
        }

        private void btn_CellPosition_Set_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();

            if ((lv_Bay_Position.Items.Count == 0) && (lv_Level_Position.Items.Count == 0))
            {
                form_Main.GlobalObj.MsgBox_Info("위치 정보가 작성되지 않았습니다.", "W");
                return;
            }

            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "셀 위치 정보를 장치로 다운로드하시겠습니까?"))
            {
                Disable_Btn();

                Ctrl_CellPosition(false, 1, 0, (byte)(lv_Bay_Position.Items.Count - 1));

                NoAnswerTimer.Enabled = true;
                NoAnswerTimer.Tag = 10;
            }
        }

        private void btn_CellPosition_Load_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();
            //흐름
            //Bay 0,0 으로 Bay Count 와 Level Count 알아내기
            //최대 256 단위로 요청할 것이므로 0 ~ Bay Count - 1  혹은 0 ~ 255 로 요청
            //Level 0 ~ Level Count - 1 로 요청 (Level은 최대가 128 이므로 나눠 물을 필요가 없다)

            Disable_Btn();
            //Request_CellPosition(1, 0, 0);
            Request_CellPosition(1, 0, 255);
            NoAnswerTimer.Enabled = true;
            NoAnswerTimer.Tag = 11;
        }
        private void btn_CellOffset_Load_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();

            lv_Rack_Offset.Items.Clear();
            lblOffsetProgress.Text = "0/0";
            progressBar1.Value = 0;

            Disable_Btn();

            Retrycount = 0;
            Request_CellOffset(0);
            NoAnswerTimer.Enabled = true;
            NoAnswerTimer.Tag = 21;
        }

        //통합파일에서 불러오는 것으로 수정함
        private void btn_CellPosition_FileRead_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();
            LoadFromoFile_Position();
        }

        private void BntInitPosition_Click(object sender, EventArgs e)
        {
            ListViewItem listviewItem;
            int TmpBayCnt, TmpLevelCnt;

            TmpBayCnt = Convert.ToInt32(edPositionBayCount.Text);
            TmpLevelCnt = Convert.ToInt32(edPositionLevelCount.Text);

            if (TmpBayCnt < 0) return;
            if (TmpLevelCnt < 0) return;

            if (TmpBayCnt > 256) TmpBayCnt = 256;
            if (TmpLevelCnt > 128) TmpLevelCnt = 128;

            if (lv_Bay_Position.Items.Count > TmpBayCnt)
            {
                for (int i = lv_Bay_Position.Items.Count; i > TmpBayCnt; i--)
                {
                    lv_Bay_Position.Items.RemoveAt(i - 1);
                }
            }
            else if (lv_Bay_Position.Items.Count < TmpBayCnt)
            {
                for (int i = lv_Bay_Position.Items.Count; i < TmpBayCnt; i++)
                {
                    listviewItem = lv_Bay_Position.Items.Add(string.Format("{0}", i + 1));
                    listviewItem.SubItems.Add(string.Format("{0}", 0));
                    listviewItem.SubItems.Add(string.Format("{0}", 0));
                }
            }


            if (lv_Level_Position.Items.Count > TmpLevelCnt)
            {
                for (int i = lv_Level_Position.Items.Count; i > TmpLevelCnt; i--)
                {
                    lv_Level_Position.Items.RemoveAt(i - 1);
                }
            }
            else if (lv_Level_Position.Items.Count < TmpLevelCnt)
            {
                for (int i = lv_Level_Position.Items.Count; i < TmpLevelCnt; i++)
                {
                    listviewItem = lv_Level_Position.Items.Add(string.Format("{0}", i + 1));
                    listviewItem.SubItems.Add(string.Format("{0}", 0));
                    listviewItem.SubItems.Add(string.Format("{0}", 0));
                }
            }

            if ((lv_Bay_Position.Items.Count > 0) && (lv_Level_Position.Items.Count > 0))
            {
                Enable_Btn();
            }
        }

        private void btn_StationConfig_Load_Click(object sender, EventArgs e)
        {
            Display_SRMStationConfig_Init();
            //1 : SRM   2 : RTV
            form_Main.COMMDataManager.ADD_TxUserZeroData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_98, 0);
        }

        private void btn_StationConfig_Set_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "스테이션 구성 정보를 장치로 다운로드하시겠습니까?"))
            {
                Ctrl_StationConfig(false);
            }
        }

        //통합파일에서 불러오는 것으로 수정함
        //개별파일에서 불러오는 소스는 남겨놓음
        private void btn_StationConfig_FileRead_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.Station_cfg|*.STATION_CFG";

            if (openFileDialog1.FileName == "")
            {
                openFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] Savebytes;

                using (BinaryReader br = new BinaryReader(File.Open(openFileDialog1.FileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        //저장할 때 SRM_StationParam 사이즈만큼 저장?
                        ushort Len = (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_StationParam));
                        if (br.BaseStream.Length == Len)
                        {
                            Savebytes = br.ReadBytes(Len);
                            Display_SRMStationParam(Savebytes);
                            IsIn_Stationinfo = true;
                            Enable_Btn();
                        }
                        else
                        {
                            MessageBox.Show("현재 프로토콜과 맞지 않는 파일입니다");
                        }
                    }
                    finally
                    {
                        br.Close();
                    }
                }
            }
        }

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
                if (form_Main.SRM_ToTalFile.Read_SRM_StationParam(ref srm_StationParam_CTRL))
                {
                    srm_StationParam_Res = srm_StationParam_CTRL;
                    Display_SRMStationParam();
                    IsIn_Stationinfo = true;
                    Enable_Btn();
                }
                else
                {
                    MessageBox.Show("해당 파일안에 [" + tab_Station.Text + "] 데이터가 없습니다");
                }
            }
        }

        //통합파일에 저장하는 것으로 수정함
        //개별파일에 저장하는 소스는 남겨놓음
        private void btn_StationConfig_FileWrite_Click(object sender, EventArgs e)
        {
            Ctrl_StationConfig(true);

            saveFileDialog1.Filter = "*.Station_cfg|*.STATION_CFG";

            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
            }

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (BinaryWriter br = new BinaryWriter(File.Open(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write)))
                {
                    try
                    {
                        br.Seek(0, SeekOrigin.Begin);
                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_StationParam))];
                        Global_Class.UTIL_StructObjectToByteArray(srm_StationParam_CTRL, Savebytes);
                        br.Write(Savebytes);
                    }
                    finally
                    {
                        br.Close();
                    }

                }
            }
        }

        private void btn_SaveTotalFile4_Click(object sender, EventArgs e)
        {
            Ctrl_StationConfig(true);

            saveFileDialog1.Filter = "*.SRMcfg|*.SRMCFG";

            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
            }

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                form_Main.SRM_ToTalFile.FileName = saveFileDialog1.FileName;
                form_Main.SRM_ToTalFile.Write_SRM_StationParam(srm_StationParam_CTRL);
            }
        }


        private void btn_SaveTotalFile3_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();

            Check_zeroOffset();

            saveFileDialog1.Filter = "*.SRMcfg|*.SRMCFG";

            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
            }

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                form_Main.SRM_ToTalFile.FileName = saveFileDialog1.FileName;

                if (lv_Rack_Offset.Items.Count == 0)
                {
                    CellOffset_Total.DevType = 1;
                    CellOffset_Total.TotalCount = 0;

                    form_Main.SRM_ToTalFile.Write_SRM_OFFSET_CFG(CellOffset_Total);
                }
                else
                {
                    Ctrl_TotalOffset();
                    form_Main.SRM_ToTalFile.Write_SRM_OFFSET_CFG(CellOffset_Total);

                }
            }
        }

        private void btn_RackConfig_Init_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            byte[] Data = { 0, 0 };

            frameLogin frmLogging = new frameLogin(); frmLogging.ShowDialog();

            if (frmLogging.DialogResult == DialogResult.OK)
            {
                if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, bt.Text + " 을 수행하시겠습니까?"))
                {
                    Data[0] = 0x01;
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


        private void btn_LoadTotalFile3_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();

            openFileDialog1.Filter = "*.SRMcfg|*.SRMCFG";

            if (openFileDialog1.FileName == "")
            {
                openFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                form_Main.SRM_ToTalFile.FileName = openFileDialog1.FileName;
                if (form_Main.SRM_ToTalFile.Read_SRM_OFFSET_CFG(ref CellOffset_Total))
                {
                    Display_totalOffsetRec();
                }
                else
                {
                    MessageBox.Show("해당 파일안에 [" + tab_RackOffset.Text + "] 데이터가 없습니다");
                }
            }
        }
        private unsafe void btn_LoadTotalFile2_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();
            Display_SRMPositionConfig_Init();


            openFileDialog1.Filter = "*.SRMcfg|*.SRMCFG";

            if (openFileDialog1.FileName == "")
            {
                openFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                form_Main.SRM_ToTalFile.FileName = openFileDialog1.FileName;
                if (form_Main.SRM_ToTalFile.Read_SRM_LevelLPositionCTRL(ref srm_CellPosition_CTRL))
                {
                    srm_CellPosition_Res.Header.DataType = srm_CellPosition_CTRL.Header.DataType;
                    srm_CellPosition_Res.Header.RackType = srm_CellPosition_CTRL.Header.RackType;
                    srm_CellPosition_Res.Header.StartNo = srm_CellPosition_CTRL.Header.StartNo;
                    srm_CellPosition_Res.Header.EndNo = srm_CellPosition_CTRL.Header.EndNo;
                    srm_CellPosition_Res.Header.LevelCount = (ushort)(srm_CellPosition_CTRL.Header.EndNo - srm_CellPosition_CTRL.Header.StartNo + 1);
                    for (int Loop = 0; Loop < 256; Loop++)
                    {
                        srm_CellPosition_Res.Position[Loop] = srm_CellPosition_CTRL.Position[Loop];
                    }
                    Display_SRMRackLevelPosition(true, srm_CellPosition_Res);

                    if (form_Main.SRM_ToTalFile.Read_SRM_BayLPositionCTRL(ref srm_CellPosition_CTRL))
                    {
                        srm_CellPosition_Res.Header.DataType = srm_CellPosition_CTRL.Header.DataType;
                        srm_CellPosition_Res.Header.RackType = srm_CellPosition_CTRL.Header.RackType;
                        srm_CellPosition_Res.Header.StartNo = srm_CellPosition_CTRL.Header.StartNo;
                        srm_CellPosition_Res.Header.EndNo = srm_CellPosition_CTRL.Header.EndNo;
                        srm_CellPosition_Res.Header.BayCount = (ushort)(srm_CellPosition_CTRL.Header.EndNo - srm_CellPosition_CTRL.Header.StartNo + 1);
                        for (int Loop = 0; Loop < 256; Loop++)
                        {
                            srm_CellPosition_Res.Position[Loop] = srm_CellPosition_CTRL.Position[Loop];
                        }
                        Display_SRMRackBayPosition(true, srm_CellPosition_Res);


                        if (form_Main.SRM_ToTalFile.Read_SRM_LevelRPositionCTRL(ref srm_CellPosition_CTRL))
                        {
                            srm_CellPosition_Res.Header.DataType = srm_CellPosition_CTRL.Header.DataType;
                            srm_CellPosition_Res.Header.RackType = srm_CellPosition_CTRL.Header.RackType;
                            srm_CellPosition_Res.Header.StartNo = srm_CellPosition_CTRL.Header.StartNo;
                            srm_CellPosition_Res.Header.EndNo = srm_CellPosition_CTRL.Header.EndNo;
                            srm_CellPosition_Res.Header.LevelCount = (ushort)(srm_CellPosition_CTRL.Header.EndNo - srm_CellPosition_CTRL.Header.StartNo + 1);
                            for (int Loop = 0; Loop < 256; Loop++)
                            {
                                srm_CellPosition_Res.Position[Loop] = srm_CellPosition_CTRL.Position[Loop];
                            }
                            Display_SRMRackLevelPosition(false, srm_CellPosition_Res);


                            if (form_Main.SRM_ToTalFile.Read_SRM_BayRPositionCTRL(ref srm_CellPosition_CTRL))
                            {
                                srm_CellPosition_Res.Header.DataType = srm_CellPosition_CTRL.Header.DataType;
                                srm_CellPosition_Res.Header.RackType = srm_CellPosition_CTRL.Header.RackType;
                                srm_CellPosition_Res.Header.StartNo = srm_CellPosition_CTRL.Header.StartNo;
                                srm_CellPosition_Res.Header.EndNo = srm_CellPosition_CTRL.Header.EndNo;
                                srm_CellPosition_Res.Header.BayCount = (ushort)(srm_CellPosition_CTRL.Header.EndNo - srm_CellPosition_CTRL.Header.StartNo + 1);
                                for (int Loop = 0; Loop < 256; Loop++)
                                {
                                    srm_CellPosition_Res.Position[Loop] = srm_CellPosition_CTRL.Position[Loop];
                                }
                                Display_SRMRackBayPosition(false, srm_CellPosition_Res);

                                IsIn_RackPosition = true;
                                Enable_Btn();
                            }
                            else
                            {
                                MessageBox.Show("해당 파일안에 [" + tab_Station.Text + "] 데이터가 없습니다");
                            }
                        } else
                        {
                            MessageBox.Show("해당 파일안에 [" + tab_Station.Text + "] 데이터가 없습니다");
                        }
                    }
                    else
                    {
                        MessageBox.Show("해당 파일안에 [" + tab_Station.Text + "] 데이터가 없습니다");
                    }
                }
                else
                {
                    MessageBox.Show("해당 파일안에 [" + tab_Station.Text + "] 데이터가 없습니다");
                }


                //if (form_Main.SRM_ToTalFile.Read_SRM_StationParam(ref srm_StationParam_CTRL))
                //{
                //    srm_StationParam_Res = srm_StationParam_CTRL;
                //    Display_SRMStationParam();
                //    IsIn_Stationinfo = true;
                //    Enable_Btn();
                //}
                //else
                //{
                    
                //}
            }
        }

        //통합파일로 저장하는 것으로 변경함
        private void btn_CellPosition_FileWrite_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();
            //셀 포지션은 구조체 자체로 저장할 수 없다
            //구조체 안에 전체 상태(제어)가 들어가는 것이 아니기 때문이다.
            //그래서 셀 포지션의 파일 저장은 화면상의 값을 파일로 옮기고
            //불러오기 역시 파일을 읽어 화면상에 값을 옮기는 방식으로 해야한다.
            //파일은 바이너리가 아닌 INI 파일 형태로 하는 것으로 하겠다 (구조 잡아서 바이너리 형태로 해도 무관하나 이런 경우에는 INI 파일이 좀 더 쉽다)
            SaveToFile_Position();
        }

        private unsafe void btn_SaveTotalFile2_Click(object sender, EventArgs e)
        {
            bool EmptySave = false;
            Hide_AllEdit();

            if ((lv_Bay_Position.Items.Count == 0) && (lv_Level_Position.Items.Count == 0))
            {
                if (form_Main.GlobalObj.MsgBox_Confirm_YN("위치 정보가 작성되지 않았습니다. 이대로 저장하시겠습니까?"))
                {
                    EmptySave = true;
                } else
                {
                    return;
                }

            }

            saveFileDialog1.Filter = "*.SRMcfg|*.SRMCFG";

            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
            }

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                form_Main.SRM_ToTalFile.FileName = saveFileDialog1.FileName;

                if (EmptySave)
                {
                    srm_CellPosition_CTRL.Header.StartNo = 0;
                    srm_CellPosition_CTRL.Header.EndNo = 0;
                    srm_CellPosition_CTRL.Header.RackType = 1;

                    srm_CellPosition_CTRL.Position[0] = 0;

                    srm_CellPosition_CTRL.Header.DataType = 1;
                    form_Main.SRM_ToTalFile.Write_SRM_BayLPositionCTRL(srm_CellPosition_CTRL);

                    srm_CellPosition_CTRL.Header.DataType = 2;
                    form_Main.SRM_ToTalFile.Write_SRM_LevelLPositionCTRL(srm_CellPosition_CTRL);

                    srm_CellPosition_CTRL.Header.DataType = 3;
                    form_Main.SRM_ToTalFile.Write_SRM_BayRPositionCTRL(srm_CellPosition_CTRL);

                    srm_CellPosition_CTRL.Header.DataType = 4;
                    form_Main.SRM_ToTalFile.Write_SRM_LevelRPositionCTRL(srm_CellPosition_CTRL);
                }
                else
                {
                    Ctrl_CellPosition(true, 1, 0, (byte)(lv_Bay_Position.Items.Count - 1));
                    form_Main.SRM_ToTalFile.Write_SRM_BayLPositionCTRL(srm_CellPosition_CTRL);
                    Ctrl_CellPosition(true, 2, 0, (byte)(lv_Level_Position.Items.Count - 1));
                    form_Main.SRM_ToTalFile.Write_SRM_LevelLPositionCTRL(srm_CellPosition_CTRL);
                    Ctrl_CellPosition(true, 3, 0, (byte)(lv_Bay_Position.Items.Count - 1));
                    form_Main.SRM_ToTalFile.Write_SRM_BayRPositionCTRL(srm_CellPosition_CTRL);
                    Ctrl_CellPosition(true, 4, 0, (byte)(lv_Level_Position.Items.Count - 1));
                    form_Main.SRM_ToTalFile.Write_SRM_LevelRPositionCTRL(srm_CellPosition_CTRL);
                }
            }
        }


        private void btn_CellOffset_Set_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "셀 오프셋 정보를 장치로 다운로드하시겠습니까?"))
            {
                if (lv_Rack_Offset.Items.Count == 0)
                {
                    byte[] Data = { 0x01, 0x20 };
                    form_Main.Do_Ctrl_Cmd_withbytes(ConstClass.CMD1_00, ConstClass.CMD2_A0, Data);
                }
                else
                {
                    Hide_AllEdit();

                    Check_zeroOffset();

                    lblOffsetProgress.Text = "0/0";
                    progressBar1.Value = 0;

                    Disable_Btn();

                    Retrycount = 0;
                    Ctrl_CellOffset(0);

                    NoAnswerTimer.Enabled = true;
                    NoAnswerTimer.Tag = 20;
                }
            }
        }

        //통합파일에서 불러오는 것으로 수정함
        private void btn_CellOffset_FileRead_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();

            LoadFromoFile_Offset();
        }

        //통합파일에 저장하는 것으로 수정함
        //개별파일에 저장하는 소스는 남겨놓음
        private void btn_CellOffset_FileWrite_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();

            SaveToFile_Offset();
        }

        private void btn_Position_AllEdit_Click(object sender, EventArgs e)
        {
            int TmpPosition = 0;

            TmpPosition = Global_Class.UTIL_StrToIntDef(ed_Bay_L_Ref.Text, 0);
            for (ushort i = 0; i < lv_Bay_Position.Items.Count; i++)
            {
                if (i==0)
                {
                } else
                {
                    if ((i % 2) == 1)
                    {
                        TmpPosition = TmpPosition + Global_Class.UTIL_StrToIntDef(ed_Bay_Gap_1.Text, 0);
                    } else
                    {
                        TmpPosition = TmpPosition + Global_Class.UTIL_StrToIntDef(ed_Bay_Gap_2.Text, 0);
                    }
                }
                lv_Bay_Position.Items[i].SubItems[1].Text = string.Format("{0}", TmpPosition);
            }

            TmpPosition = Global_Class.UTIL_StrToIntDef(ed_Bay_R_Ref.Text, 0);
            for (ushort i = 0; i < lv_Bay_Position.Items.Count; i++)
            {
                if (i == 0)
                {
                }
                else
                {
                    if ((i % 2) == 1)
                    {
                        TmpPosition = TmpPosition + Global_Class.UTIL_StrToIntDef(ed_Bay_Gap_1.Text, 0);
                    }
                    else
                    {
                        TmpPosition = TmpPosition + Global_Class.UTIL_StrToIntDef(ed_Bay_Gap_2.Text, 0);
                    }
                }
                lv_Bay_Position.Items[i].SubItems[2].Text = string.Format("{0}", TmpPosition);
            }

            for (ushort i = 0; i < lv_Level_Position.Items.Count; i++)
            {
                int Value = (Global_Class.UTIL_StrToIntDef(ed_Level_L_Ref.Text, 0) + Global_Class.UTIL_StrToIntDef(ed_Level_Gap.Text, 0) * i);
                lv_Level_Position.Items[i].SubItems[1].Text = string.Format("{0}", Value);
            }

            for (ushort i = 0; i < lv_Level_Position.Items.Count; i++)
            {
                int Value = (Global_Class.UTIL_StrToIntDef(ed_Level_R_Ref.Text, 0) + Global_Class.UTIL_StrToIntDef(ed_Level_Gap.Text, 0) * i);
                lv_Level_Position.Items[i].SubItems[2].Text = string.Format("{0}", Value);
            }
        }
        private void btn_RackOffset_ADD_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();

            int From_Bay, From_Level;
            int To_Bay, To_Level;
            int refreshCount = 0;

            From_Bay = 0;
            From_Level = 0;
            To_Bay = 0;
            To_Level = 0;

            int.TryParse(ed_RackOffset_Bay_From.Text, out From_Bay);
            int.TryParse(ed_RackOffset_Bay_To.Text, out To_Bay);
            int.TryParse(ed_RackOffset_Level_From.Text, out From_Level);
            int.TryParse(ed_RackOffset_Level_To.Text, out To_Level);

            if ((From_Bay > 256) || (To_Bay > 256))
            {
                return;
            }

            if (From_Bay == 0)
            {
                return;
            }

            if (To_Bay == 0)
            {
                To_Bay = From_Bay;
            }


            if ((From_Level > 128) || (To_Level > 128))
            {
                return;
            }

            if (From_Level == 0)
            {
                return;
            }

            if (To_Level == 0)
            {
                To_Level = From_Level;
            }


            if ((From_Bay > To_Bay) || (From_Level > To_Level))
            {
                return;
            }

            for (int i = From_Bay; i <= To_Bay; i++)
            {
                lv_Rack_Offset.BeginUpdate();
                for (int j = From_Level; j <= To_Level; j++)
                {
                    refreshCount++;
                    ADD_Offset((ushort)i, (ushort)j);
                }
                lv_Rack_Offset.EndUpdate();

                if (refreshCount >= 3000)
                {
                    refreshCount = 0;
                    lv_Rack_Offset.Refresh();
                }
            }
        }

        private void btn_RackOffset_Clear_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();
            lv_Rack_Offset.Items.Clear();
        }

        private void btn_RackOffset_DEL_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();

            if (lv_Rack_Offset.SelectedItems.Count == 0) return;


            lv_Rack_Offset.BeginUpdate();

            for (int i = lv_Rack_Offset.Items.Count - 1; i >= 0; i--)
            {
                if (lv_Rack_Offset.Items[i].Selected)
                {
                    lv_Rack_Offset.Items.RemoveAt(i);
                }
            }
            lv_Rack_Offset.EndUpdate();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hide_AllEdit();
            this.Text = "SRM 위치 설정(" + tabControl1.SelectedTab.Text + ")";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DateTime.Compare(RxDateTime, DateTime.Now) > 0)
            {
                RxDateTime = DateTime.Now;
            }
            TimeSpan ts = DateTime.Now - RxDateTime;

            if (ts.TotalSeconds >= 2)
            {
                if (Convert.ToByte(NoAnswerTimer.Tag.ToString()) == 21) //Load
                {
                    if (Retrycount < 3)
                    {
                        Retrycount++;
                        Request_CellOffset(srm_CellOffset_Req.ReqIndex);
                    }
                    else
                    {
                        Enable_Btn();
                        NoAnswerTimer.Enabled = false;
                    }
                }
                else if (Convert.ToByte(NoAnswerTimer.Tag.ToString()) == 20)
                {
                    if (Retrycount < 5)
                    {
                        Retrycount++;
                        Ctrl_CellOffset(srm_CellOffset_CTRL.Header.Nowindex);
                    }
                    else
                    {
                        Enable_Btn();
                        NoAnswerTimer.Enabled = false;
                    }
                }
                else
                {
                    Enable_Btn();
                    NoAnswerTimer.Enabled = false;
                }
            }
        }

        #endregion


        #region lv_Rack_Offset 이벤트
        private void lv_Rack_Offset_DoubleClick(object sender, EventArgs e)
        {
            bool once = false;
            if (lv_Rack_Offset.SelectedItems.Count == 1)
            {
                for (byte i = 0; i <= (lv_Rack_Offset.Columns.Count - 1); i++)
                {
                    if (Rack_Offset_TextBox[i] != null)
                    {
                        if (Convert.ToByte(Rack_Offset_TextBox[i].Tag.ToString()) != 1)
                        {
                            Rack_Offset_TextBox[i].Visible = true;
                            Rack_Offset_TextBox[i].BringToFront();
                            if (!once)
                            {
                                Rack_Offset_TextBox[i].Focus();
                                once = true;
                            }
                        }
                    }
                }
            }
        }

        private void lv_Rack_Offset_Enter(object sender, EventArgs e)
        {
            Hide_AllEdit();

        }

        private void lv_Rack_Offset_MouseUp(object sender, MouseEventArgs e)
        {
            // Get the item on the row that is clicked.
            lv_RackOffset_Item = this.lv_Rack_Offset.GetItemAt(e.X, e.Y);

            // Make sure that an item is clicked.
            if (lv_RackOffset_Item != null)
            {
                //Column 2
                Rectangle ClickedItem;


                for (byte i = 0; i <= (lv_Rack_Offset.Columns.Count - 1); i++)
                {
                    if (Rack_Offset_TextBox[i] != null)
                    {
                        ClickedItem = lv_RackOffset_Item.SubItems[i].Bounds;

                        //화면에 안보이는 경우
                        if ((ClickedItem.Left + this.lv_Rack_Offset.Columns[i].Width) < 0)
                        {
                            Rack_Offset_TextBox[i].Tag = 1;
                            return;
                        }
                        else if (ClickedItem.Left < 0) // 해당 컬럼의 Left가 화면을 벗어난 경우
                        {
                            Rack_Offset_TextBox[i].Tag = 0;
                            if ((ClickedItem.Left + this.lv_Rack_Offset.Columns[i].Width) > this.lv_Rack_Offset.Width) // 해당 컬럼의 Right가 화면을 벗어난 경우
                            {
                                ClickedItem.Width = this.lv_Rack_Offset.Width;
                                ClickedItem.X = 0;
                            }
                            else // 해당 컬럼의 Right가 화면을 벗어나지 않은 경우
                            {
                                ClickedItem.Width = this.lv_Rack_Offset.Columns[i].Width + ClickedItem.Left;
                                ClickedItem.X = 2;
                            }
                        }
                        else // 해당 컬럼의 Left가 화면을 벗어나지 않은 경우
                        {
                            Rack_Offset_TextBox[i].Tag = 0;
                            if ((ClickedItem.Left + this.lv_Rack_Offset.Columns[i].Width) > this.lv_Rack_Offset.Width)  // 해당 컬럼의 Left가 화면을 벗어난 경우
                            {
                                ClickedItem.Width = this.lv_Rack_Offset.Width - ClickedItem.Left;
                            }
                            else // 해당 컬럼의 Right가 화면을 벗어나지 않은 경우
                            {
                                ClickedItem.Width = this.lv_Rack_Offset.Columns[i].Width;
                            }
                        }

                        // Adjust the top to account for the location of the ListView.
                        ClickedItem.Y += lv_Rack_Offset.Top;
                        ClickedItem.X += lv_Rack_Offset.Left;

                        // Assign calculated bounds to the TextBox.
                        Rack_Offset_TextBox[i].Bounds = ClickedItem;

                        // Set default text for TextBox to match the item that is clicked.
                        Rack_Offset_TextBox[i].Text = lv_RackOffset_Item.SubItems[i].Text;
                    }
                }
            }
            else
            {
                for (byte i = 0; i <= (lv_Rack_Offset.Columns.Count - 1); i++)
                {
                    if (Rack_Offset_TextBox[i] != null)
                    {
                        Rack_Offset_TextBox[i].Visible = false;
                    }

                }
            }

        }

        private void lv_Rack_Offset_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (byte i = 0; i <= (lv_Rack_Offset.Columns.Count - 1); i++)
            {
                if (Rack_Offset_TextBox[i] != null)
                {
                    Rack_Offset_TextBox[i].Visible = false;
                }
            }
        }

        private void ed_Rack_Offset_2_VisibleChanged(object sender, EventArgs e)
        {
            if (lv_RackOffset_Item != null)
            {
                TextBox TmpTextBox = (TextBox)sender;

                for (byte i = 0; i <= (lv_Rack_Offset.Columns.Count - 1); i++)
                {
                    if (Rack_Offset_TextBox[i] != null)
                    {
                        if (TmpTextBox == Rack_Offset_TextBox[i])
                        {
                            if (!TmpTextBox.Visible)
                            {
                                lv_RackOffset_Item.SubItems[i].Text = Rack_Offset_TextBox[i].Text;
                            }
                        }
                    }
                }
            }
        }

        private void ed_Rack_Offset_2_KeyPress(object sender, KeyPressEventArgs e)
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
                if (ed.Name == "ed_Rack_Offset_2") ed_Rack_Offset_3.Focus();
                else if (ed.Name == "ed_Rack_Offset_3") ed_Rack_Offset_4.Focus();
                else if (ed.Name == "ed_Rack_Offset_4") ed_Rack_Offset_5.Focus();
                else if (ed.Name == "ed_Rack_Offset_5") ed_Rack_Offset_6.Focus();
                else if (ed.Name == "ed_Rack_Offset_6") ed_Rack_Offset_7.Focus();
                else if (ed.Name == "ed_Rack_Offset_7")
                {
                    //ed_Rack_Offset_2.Visible = false;
                    //ed_Rack_Offset_3.Visible = false;
                    //ed_Rack_Offset_4.Visible = false;
                    //ed_Rack_Offset_5.Visible = false;
                    //ed_Rack_Offset_6.Visible = false;
                    //ed_Rack_Offset_7.Visible = false;
                }
            }
        }

        #endregion

        #region Position 리스트뷰 이벤트
        private void lv_Bay_Position_DoubleClick(object sender, EventArgs e)
        {
            bool once = false;
            if (lv_Bay_Position.SelectedItems.Count == 1)
            {
                for (byte i = 0; i <= (lv_Bay_Position.Columns.Count - 1); i++)
                {
                    if (Bay_Position_TextBox[i] != null)
                    {
                        if (Convert.ToByte(Bay_Position_TextBox[i].Tag.ToString()) != 1)
                        {
                            Bay_Position_TextBox[i].Visible = true;
                            Bay_Position_TextBox[i].BringToFront();
                            if (!once)
                            {
                                Bay_Position_TextBox[i].Focus();
                                once = true;
                            }
                        }
                    }
                }
            }
        }

        private void lv_Level_Position_DoubleClick(object sender, EventArgs e)
        {
            bool once = false;
            if (lv_Level_Position.SelectedItems.Count == 1)
            {
                for (byte i = 0; i <= (lv_Level_Position.Columns.Count - 1); i++)
                {
                    if (Level_Position_TextBox[i] != null)
                    {
                        if (Convert.ToByte(Level_Position_TextBox[i].Tag.ToString()) != 1)
                        {
                            Level_Position_TextBox[i].Visible = true;
                            Level_Position_TextBox[i].BringToFront();
                            if (!once)
                            {
                                Level_Position_TextBox[i].Focus();
                                once = true;
                            }
                        }
                    }
                }
            }
        }

        private void lv_Bay_Position_Enter(object sender, EventArgs e)
        {
            Hide_AllEdit();

        }

        private void lv_Level_Position_Enter(object sender, EventArgs e)
        {
            Hide_AllEdit();

        }
        private void lv_Bay_Position_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (byte i = 0; i <= (lv_Bay_Position.Columns.Count - 1); i++)
            {
                if (Bay_Position_TextBox[i] != null)
                {
                    Bay_Position_TextBox[i].Visible = false;
                }
            }
        }

        private void lv_Level_Position_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (byte i = 0; i <= (lv_Level_Position.Columns.Count - 1); i++)
            {
                if (Level_Position_TextBox[i] != null)
                {
                    Level_Position_TextBox[i].Visible = false;
                }
            }
        }

        private void Process_Level_Position_TextBox()
        {
            // Make sure that an item is clicked.
            if (lv_LevelPosition_Item != null)
            {
                //Column 2
                Rectangle ClickedItem;


                for (byte i = 0; i <= (lv_Level_Position.Columns.Count - 1); i++)
                {
                    if (Level_Position_TextBox[i] != null)
                    {
                        ClickedItem = lv_LevelPosition_Item.SubItems[i].Bounds;

                        //화면에 안보이는 경우
                        if ((ClickedItem.Left + this.lv_Level_Position.Columns[i].Width) < 0)
                        {
                            Level_Position_TextBox[i].Tag = 1;
                            return;
                        }
                        else if (ClickedItem.Left < 0) // 해당 컬럼의 Left가 화면을 벗어난 경우
                        {
                            Level_Position_TextBox[i].Tag = 0;
                            if ((ClickedItem.Left + this.lv_Level_Position.Columns[i].Width) > this.lv_Level_Position.Width) // 해당 컬럼의 Right가 화면을 벗어난 경우
                            {
                                ClickedItem.Width = this.lv_Level_Position.Width;
                                ClickedItem.X = 0;
                            }
                            else // 해당 컬럼의 Right가 화면을 벗어나지 않은 경우
                            {
                                ClickedItem.Width = this.lv_Level_Position.Columns[i].Width + ClickedItem.Left;
                                ClickedItem.X = 2;
                            }
                        }
                        else // 해당 컬럼의 Left가 화면을 벗어나지 않은 경우
                        {
                            Level_Position_TextBox[i].Tag = 0;
                            if ((ClickedItem.Left + this.lv_Level_Position.Columns[i].Width) > this.lv_Level_Position.Width)  // 해당 컬럼의 Left가 화면을 벗어난 경우
                            {
                                ClickedItem.Width = this.lv_Level_Position.Width - ClickedItem.Left;
                            }
                            else // 해당 컬럼의 Right가 화면을 벗어나지 않은 경우
                            {
                                ClickedItem.Width = this.lv_Level_Position.Columns[i].Width;
                            }
                        }

                        // Adjust the top to account for the location of the ListView.
                        ClickedItem.Y += lv_Level_Position.Top;
                        ClickedItem.X += lv_Level_Position.Left;

                        // Assign calculated bounds to the TextBox.
                        Level_Position_TextBox[i].Bounds = ClickedItem;

                        // Set default text for TextBox to match the item that is clicked.
                        Level_Position_TextBox[i].Text = lv_LevelPosition_Item.SubItems[i].Text;
                    }
                }
            }
            else
            {
                for (byte i = 0; i <= (lv_Level_Position.Columns.Count - 1); i++)
                {
                    if (Level_Position_TextBox[i] != null)
                    {
                        Level_Position_TextBox[i].Visible = false;
                    }

                }
            }
        }

        private void Process_Bay_Position_TextBox()
        {
            // Make sure that an item is clicked.
            if (lv_BayPosition_Item != null)
            {
                //Column 2
                Rectangle ClickedItem;


                for (byte i = 0; i <= (lv_Bay_Position.Columns.Count - 1); i++)
                {
                    if (Bay_Position_TextBox[i] != null)
                    {
                        ClickedItem = lv_BayPosition_Item.SubItems[i].Bounds;

                        //화면에 안보이는 경우
                        if ((ClickedItem.Left + this.lv_Bay_Position.Columns[i].Width) < 0)
                        {
                            Bay_Position_TextBox[i].Tag = 1;
                            return;
                        }
                        else if (ClickedItem.Left < 0) // 해당 컬럼의 Left가 화면을 벗어난 경우
                        {
                            Bay_Position_TextBox[i].Tag = 0;
                            if ((ClickedItem.Left + this.lv_Bay_Position.Columns[i].Width) > this.lv_Bay_Position.Width) // 해당 컬럼의 Right가 화면을 벗어난 경우
                            {
                                ClickedItem.Width = this.lv_Bay_Position.Width;
                                ClickedItem.X = 0;
                            }
                            else // 해당 컬럼의 Right가 화면을 벗어나지 않은 경우
                            {
                                ClickedItem.Width = this.lv_Bay_Position.Columns[i].Width + ClickedItem.Left;
                                ClickedItem.X = 2;
                            }
                        }
                        else // 해당 컬럼의 Left가 화면을 벗어나지 않은 경우
                        {
                            Bay_Position_TextBox[i].Tag = 0;
                            if ((ClickedItem.Left + this.lv_Bay_Position.Columns[i].Width) > this.lv_Bay_Position.Width)  // 해당 컬럼의 Left가 화면을 벗어난 경우
                            {
                                ClickedItem.Width = this.lv_Bay_Position.Width - ClickedItem.Left;
                            }
                            else // 해당 컬럼의 Right가 화면을 벗어나지 않은 경우
                            {
                                ClickedItem.Width = this.lv_Bay_Position.Columns[i].Width;
                            }
                        }

                        // Adjust the top to account for the location of the ListView.
                        ClickedItem.Y += lv_Bay_Position.Top;
                        ClickedItem.X += lv_Bay_Position.Left;

                        // Assign calculated bounds to the TextBox.
                        Bay_Position_TextBox[i].Bounds = ClickedItem;

                        // Set default text for TextBox to match the item that is clicked.
                        Bay_Position_TextBox[i].Text = lv_BayPosition_Item.SubItems[i].Text;
                    }
                }
            }
            else
            {
                for (byte i = 0; i <= (lv_Bay_Position.Columns.Count - 1); i++)
                {
                    if (Bay_Position_TextBox[i] != null)
                    {
                        Bay_Position_TextBox[i].Visible = false;
                    }

                }
            }
        }
        private void lv_Bay_Position_MouseUp(object sender, MouseEventArgs e)
        {
            // Get the item on the row that is clicked.
            lv_BayPosition_Item = this.lv_Bay_Position.GetItemAt(e.X, e.Y);

            Process_Bay_Position_TextBox();

        }

        private void lv_Level_Position_MouseUp(object sender, MouseEventArgs e)
        {
            // Get the item on the row that is clicked.
            lv_LevelPosition_Item = this.lv_Level_Position.GetItemAt(e.X, e.Y);

            Process_Level_Position_TextBox();
            
        }

        private void cb_Bay_Position_1_VisibleChanged(object sender, EventArgs e)
        {
            
        }
        private void cb_Level_Position_1_VisibleChanged(object sender, EventArgs e)
        {
            

        }

        private void cb_Bay_Position_1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void cb_Level_Position_1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void btn_SpeedEdit_Click(object sender, EventArgs e)
        {
            int No = 0;
            int kind = 0;
            int item = 0;
            int value = 0;

            int.TryParse(ed_No.Text, out No);
            int.TryParse(ed_Value.Text, out value);
            kind = cb_Kind.SelectedIndex;
            item = cb_Item.SelectedIndex;

            if ((No == 0) || (kind == -1) || (item == -1)) return;
            if (lv_Rack_Offset.Items.Count == 0) return;

            for (int i = 0; i < lv_Rack_Offset.Items.Count; i++)
            {
                switch (kind)
                {
                    case 0: //Bay
                        if (lv_Rack_Offset.Items[i].SubItems[0].Text == No.ToString())
                        {
                            switch (item)
                            {
                                case 0: lv_Rack_Offset.Items[i].SubItems[2].Text = value.ToString(); break;
                                case 1: lv_Rack_Offset.Items[i].SubItems[3].Text = value.ToString(); break;
                                case 2: lv_Rack_Offset.Items[i].SubItems[4].Text = value.ToString(); break;
                                case 3: lv_Rack_Offset.Items[i].SubItems[5].Text = value.ToString(); break;
                                case 4: lv_Rack_Offset.Items[i].SubItems[6].Text = value.ToString(); break;
                                case 5: lv_Rack_Offset.Items[i].SubItems[7].Text = value.ToString(); break;
                            }
                        }
                            break;
                    case 1: //Level
                        if (lv_Rack_Offset.Items[i].SubItems[1].Text == No.ToString())
                        {
                            switch (item)
                            {
                                case 0: lv_Rack_Offset.Items[i].SubItems[2].Text = value.ToString(); break;
                                case 1: lv_Rack_Offset.Items[i].SubItems[3].Text = value.ToString(); break;
                                case 2: lv_Rack_Offset.Items[i].SubItems[4].Text = value.ToString(); break;
                                case 3: lv_Rack_Offset.Items[i].SubItems[5].Text = value.ToString(); break;
                                case 4: lv_Rack_Offset.Items[i].SubItems[6].Text = value.ToString(); break;
                                case 5: lv_Rack_Offset.Items[i].SubItems[7].Text = value.ToString(); break;
                            }
                        }
                        break;
                }

            }

        }
        #endregion


        #region 기능 함수
        private unsafe void Ctrl_TotalOffset()
        {
            CellOffset_Total.DevType = 1;
            CellOffset_Total.TotalCount = (ushort)lv_Rack_Offset.Items.Count;


            for (int i = 0; i < lv_Rack_Offset.Items.Count; i++)
            {
                CellOffset_Total.SRM_CellOffsetRec[i].Bay = (byte)(Global_Class.UTIL_StrToIntDef(lv_Rack_Offset.Items[i].SubItems[0].Text, 1) - 1);
                CellOffset_Total.SRM_CellOffsetRec[i].Level = (byte)(Global_Class.UTIL_StrToIntDef(lv_Rack_Offset.Items[i].SubItems[1].Text, 1) - 1);
                CellOffset_Total.SRM_CellOffsetRec[i].Left_Travel_Offset = (sbyte)Global_Class.UTIL_StrToIntDef(lv_Rack_Offset.Items[i].SubItems[2].Text, 0);
                CellOffset_Total.SRM_CellOffsetRec[i].Left_Lift_Offset = (sbyte)Global_Class.UTIL_StrToIntDef(lv_Rack_Offset.Items[i].SubItems[3].Text, 0);
                CellOffset_Total.SRM_CellOffsetRec[i].Left_Fork_Offset = (sbyte)Global_Class.UTIL_StrToIntDef(lv_Rack_Offset.Items[i].SubItems[4].Text, 0);
                CellOffset_Total.SRM_CellOffsetRec[i].Right_Travel_Offset = (sbyte)Global_Class.UTIL_StrToIntDef(lv_Rack_Offset.Items[i].SubItems[5].Text, 0);
                CellOffset_Total.SRM_CellOffsetRec[i].Right_Lift_Offset = (sbyte)Global_Class.UTIL_StrToIntDef(lv_Rack_Offset.Items[i].SubItems[6].Text, 0);
                CellOffset_Total.SRM_CellOffsetRec[i].Right_Fork_Offset = (sbyte)Global_Class.UTIL_StrToIntDef(lv_Rack_Offset.Items[i].SubItems[7].Text, 0);
            }
        }


        public unsafe void Ctrl_CellOffset(UInt16 Startindex)
        {
            UInt16 TmpTxLen;

            if (srm_CellOffset_CTRL.Header.Nowindex != Startindex) Retrycount = 0;

            fixed (VEXI_DEFS.TSRM_CellOffset* ptr = &srm_CellOffset_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)ptr, Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CellOffset)));
                ptr->Header.DevType = 1;
                ptr->Header.TotalCount = (UInt16)lv_Rack_Offset.Items.Count;
                ptr->Header.Nowindex = Startindex;
                if ((lv_Rack_Offset.Items.Count - Startindex) <= 128)
                {
                    ptr->Header.ItemCount = (byte)(lv_Rack_Offset.Items.Count - Startindex);
                }
                else
                {
                    ptr->Header.ItemCount = 128;
                }

                UInt16 EndIndex = (UInt16)(Startindex + ptr->Header.ItemCount - 1);

                if (ptr->Header.ItemCount > 0)
                {

                    fixed (VEXI_DEFS.TSRM_CellOffsetRec* ptr_1 = &srm_CellOffset_CTRL.SRM_CellOffsetRec)
                    {
                        for (int i = Startindex; i <= EndIndex; i++)
                        {
                            (ptr_1 + i - Startindex)->Bay = (byte)(Global_Class.UTIL_StrToIntDef(lv_Rack_Offset.Items[i].SubItems[0].Text, 1) - 1);
                            (ptr_1 + i - Startindex)->Level = (byte)(Global_Class.UTIL_StrToIntDef(lv_Rack_Offset.Items[i].SubItems[1].Text, 1) - 1);
                            (ptr_1 + i - Startindex)->Left_Travel_Offset = (sbyte)Global_Class.UTIL_StrToIntDef(lv_Rack_Offset.Items[i].SubItems[2].Text, 0);
                            (ptr_1 + i - Startindex)->Left_Lift_Offset = (sbyte)Global_Class.UTIL_StrToIntDef(lv_Rack_Offset.Items[i].SubItems[3].Text, 0);
                            (ptr_1 + i - Startindex)->Left_Fork_Offset = (sbyte)Global_Class.UTIL_StrToIntDef(lv_Rack_Offset.Items[i].SubItems[4].Text, 0);
                            (ptr_1 + i - Startindex)->Right_Travel_Offset = (sbyte)Global_Class.UTIL_StrToIntDef(lv_Rack_Offset.Items[i].SubItems[5].Text, 0);
                            (ptr_1 + i - Startindex)->Right_Lift_Offset = (sbyte)Global_Class.UTIL_StrToIntDef(lv_Rack_Offset.Items[i].SubItems[6].Text, 0);
                            (ptr_1 + i - Startindex)->Right_Fork_Offset = (sbyte)Global_Class.UTIL_StrToIntDef(lv_Rack_Offset.Items[i].SubItems[7].Text, 0);
                        }
                    }
                }

                TmpTxLen = (UInt16)(Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CellOffset_Header)) + ptr->Header.ItemCount * Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CellOffsetRec)));



                lblOffsetProgress.Text = (ptr->Header.Nowindex + ptr->Header.ItemCount).ToString() + " / " + ptr->Header.TotalCount.ToString();
                if (progressBar1.Maximum != ptr->Header.TotalCount) progressBar1.Maximum = ptr->Header.TotalCount;
                progressBar1.Value = (ptr->Header.Nowindex + ptr->Header.ItemCount);
            }


            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_97, TmpTxLen, srm_CellOffset_CTRL);
            form_Main.COMMDataManager.ISPolingDelayStop = true;
            form_Main.COMMDataManager.PolingDelayTxTime = DateTime.Now;
            RxDateTime = DateTime.Now;
        }

        public unsafe void Ctrl_CellPosition(bool IsFileSave, byte DataType, byte StartNo, byte EndNo)
        {
            UInt16 TmpTxLen;

            fixed (VEXI_DEFS.TSRM_CellPositionCTRL* ptr = &srm_CellPosition_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)ptr, Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CellPositionCTRL)));

                ptr->Header.RackType = 1; //SRM
                ptr->Header.BayCount = (ushort)lv_Bay_Position.Items.Count;
                ptr->Header.LevelCount = (ushort)lv_Level_Position.Items.Count;
                ptr->Header.DataType = DataType;
                ptr->Header.StartNo = StartNo;
                ptr->Header.EndNo = EndNo;

                for (int i = ptr->Header.StartNo; i <= ptr->Header.EndNo; i++)
                {
                    switch (DataType)
                    {
                        case 1: //Bay L
                            ptr->Position[i - ptr->Header.StartNo] = Global_Class.UTIL_StrToIntDef(lv_Bay_Position.Items[i].SubItems[1].Text, 0);
                            break;
                        case 2: //Level L
                            ptr->Position[i - ptr->Header.StartNo] = Global_Class.UTIL_StrToIntDef(lv_Level_Position.Items[i].SubItems[1].Text, 0);
                            break;
                        case 3: //Bay R
                            ptr->Position[i - ptr->Header.StartNo] = Global_Class.UTIL_StrToIntDef(lv_Bay_Position.Items[i].SubItems[2].Text, 0);
                            break;
                        case 4: //Level R
                            ptr->Position[i - ptr->Header.StartNo] = Global_Class.UTIL_StrToIntDef(lv_Level_Position.Items[i].SubItems[2].Text, 0);
                            break;
                    }
                }

                TmpTxLen = (UInt16)(Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CellPosition_Header)) + (ptr->Header.EndNo - ptr->Header.StartNo + 1) * Marshal.SizeOf(typeof(int)));
            }

            if (!IsFileSave)
            {
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_95, TmpTxLen, srm_CellPosition_CTRL);
                form_Main.COMMDataManager.ISPolingDelayStop = true;
                form_Main.COMMDataManager.PolingDelayTxTime = DateTime.Now;
                RxDateTime = DateTime.Now;
            }
        }

        private unsafe void Ctrl_StationConfig(bool IsFileSave)
        {
            fixed (VEXI_DEFS.TSRM_StationParam* ptr = &srm_StationParam_CTRL)
            {

                Global_Class.UTIL_Byteptr_clear((byte*)ptr, Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_StationParam)));


                ptr->Header.stationCount = 50;

                if (rb_Station_Interlock0.Checked) ptr->Header.InterlockType = 0;
                else if (rb_Station_Interlock1.Checked) ptr->Header.InterlockType = 1;
                else if (rb_Station_Interlock2.Checked) ptr->Header.InterlockType = 2;


                if (ptr->Header.stationCount > 0)
                {

                    fixed (VEXI_DEFS.TSRM_StationConfigRec* ptr2 = &srm_StationParam_CTRL.Station1)
                    {
                        for (byte i = 0; i < ptr->Header.stationCount; i++)
                        {
                            if (rb_Station_None[i].Checked)
                            {
                                (ptr2 + i)->station_Type = 0;
                            }
                            else if (rb_Station_In[i].Checked)
                            {
                                (ptr2 + i)->station_Type = 1;
                            }
                            else if (rb_Station_Out[i].Checked)
                            {
                                (ptr2 + i)->station_Type = 2;
                            }
                            else if (rb_Station_InOut[i].Checked)
                            {
                                (ptr2 + i)->station_Type = 3;
                            }
                            else if (rb_Station_Virtual[i].Checked)
                            {
                                (ptr2 + i)->station_Type = 4;
                            }

                            if (cb_Station_Item0[i].Checked) (ptr2 + i)->Item_Type = (byte)((ptr2 + i)->Item_Type | 0x01);
                            if (cb_Station_Item1[i].Checked) (ptr2 + i)->Item_Type = (byte)((ptr2 + i)->Item_Type | 0x02);
                            if (cb_Station_Item2[i].Checked) (ptr2 + i)->Item_Type = (byte)((ptr2 + i)->Item_Type | 0x04);
                            if (cb_Station_Item3[i].Checked) (ptr2 + i)->Item_Type = (byte)((ptr2 + i)->Item_Type | 0x08);
                            if (cb_Station_Item4[i].Checked) (ptr2 + i)->Item_Type = (byte)((ptr2 + i)->Item_Type | 0x10);
                            if (cb_Station_Item5[i].Checked) (ptr2 + i)->Item_Type = (byte)((ptr2 + i)->Item_Type | 0x20);
                            if (cb_Station_Item6[i].Checked) (ptr2 + i)->Item_Type = (byte)((ptr2 + i)->Item_Type | 0x40);
                            if (cb_Station_Item7[i].Checked) (ptr2 + i)->Item_Type = (byte)((ptr2 + i)->Item_Type | 0x80);


                            (ptr2 + i)->Delay_Time = (byte)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Station_DelayTime[i].Text, 0) * 10);

                            (ptr2 + i)->ForkDepth = (short)Global_Class.UTIL_StrToIntDef(ed_Station_ForkDepth[i].Text, 0);
                            (ptr2 + i)->Travel = Global_Class.UTIL_StrToIntDef(ed_Station_Travel[i].Text, 0);
                            (ptr2 + i)->Lift = Global_Class.UTIL_StrToIntDef(ed_Station_Lift[i].Text, 0);
                            (ptr2 + i)->LevelUp_Offset = (byte)Global_Class.UTIL_StrToIntDef(ed_Station_UpOffset[i].Text, 0);
                            (ptr2 + i)->LevelDn_Offset = (sbyte)Global_Class.UTIL_StrToIntDef(ed_Station_DownOffset[i].Text, 0);


                            (ptr2 + i)->interLockNo = (byte)Global_Class.UTIL_StrToIntDef(ed_Station_InterlockIndex[i].Text, 0);
                            if (cb_UseIsExistItem[i].Checked)
                            {
                                (ptr2 + i)->UseIsExistItem = 1;
                            }
                        }

                        //TmpTxLen = (UInt16)(Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_StationConfigHeaderRec)) + ptr->Header.stationCount * Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_StationConfigRec)));
                    }
                }
            }

            if (!IsFileSave)
            {
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_99, srm_StationParam_CTRL);
                form_Main.COMMDataManager.ISPolingDelayStop = true;
                form_Main.COMMDataManager.PolingDelayTxTime = DateTime.Now;
            }
        }

        private void Display_SRMPositionConfig_Init()
        {

            lbl_Bay_Count.Text = "";
            lbl_Level_Count.Text = "";

        }

        private void Display_SRMStationConfig_Init()
        {

            //Header
            rb_Station_Interlock0.Checked = true;
            rb_Station_Interlock1.Checked = false;
            rb_Station_Interlock2.Checked = false;

            //Station array
            for (byte i = 0; i < 50; i++)
            {
                rb_Station_None[i].Checked = true;

                cb_UseIsExistItem[i].Checked = false;

                cb_Station_Item0[i].Checked = false;
                cb_Station_Item1[i].Checked = false;
                cb_Station_Item2[i].Checked = false;
                cb_Station_Item3[i].Checked = false;
                cb_Station_Item4[i].Checked = false;
                cb_Station_Item5[i].Checked = false;
                cb_Station_Item6[i].Checked = false;
                cb_Station_Item7[i].Checked = false;


                ed_Station_DelayTime[i].Text = "";
                ed_Station_ForkDepth[i].Text = "";
                ed_Station_Travel[i].Text = "";
                ed_Station_Lift[i].Text = "";
                ed_Station_UpOffset[i].Text = "";
                ed_Station_DownOffset[i].Text = "";
                ed_Station_InterlockIndex[i].Text = "";
            }
        }

        public unsafe void Display_SRMStationParam()
        {
            //Header 부분 갱신
            rb_Station_Interlock0.Checked = (srm_StationParam_Res.Header.InterlockType == 0);
            rb_Station_Interlock1.Checked = (srm_StationParam_Res.Header.InterlockType == 1);
            rb_Station_Interlock2.Checked = (srm_StationParam_Res.Header.InterlockType == 2);

            //Station array
            fixed (VEXI_DEFS.TSRM_StationConfigRec* Ptr_1 = &srm_StationParam_Res.Station1)
            {
                if ((srm_StationParam_Res.Header.stationCount > 0) && (srm_StationParam_Res.Header.stationCount <= 50))
                {
                    //for (byte i = 0; i < srm_StationParam_Res.Header.stationCount; i++)
                    for (byte i = 0; i < 50; i++)
                    {
                        if (i < srm_StationParam_Res.Header.stationCount)
                        {
                            rb_Station_None[i].Checked = ((Ptr_1 + i)->station_Type == 0);
                            rb_Station_In[i].Checked = ((Ptr_1 + i)->station_Type == 1);
                            rb_Station_Out[i].Checked = ((Ptr_1 + i)->station_Type == 2);
                            rb_Station_InOut[i].Checked = ((Ptr_1 + i)->station_Type == 3);
                            rb_Station_Virtual[i].Checked = ((Ptr_1 + i)->station_Type == 4);

                            cb_Station_Item0[i].Checked = (((Ptr_1 + i)->Item_Type & 0x01) != 0);
                            cb_Station_Item1[i].Checked = (((Ptr_1 + i)->Item_Type & 0x02) != 0);
                            cb_Station_Item2[i].Checked = (((Ptr_1 + i)->Item_Type & 0x04) != 0);
                            cb_Station_Item3[i].Checked = (((Ptr_1 + i)->Item_Type & 0x08) != 0);
                            cb_Station_Item4[i].Checked = (((Ptr_1 + i)->Item_Type & 0x10) != 0);
                            cb_Station_Item5[i].Checked = (((Ptr_1 + i)->Item_Type & 0x20) != 0);
                            cb_Station_Item6[i].Checked = (((Ptr_1 + i)->Item_Type & 0x40) != 0);
                            cb_Station_Item7[i].Checked = (((Ptr_1 + i)->Item_Type & 0x80) != 0);

                            

                            ed_Station_DelayTime[i].Text = string.Format("{0:0.0}", (double)(Ptr_1 + i)->Delay_Time / 10);
                            ed_Station_ForkDepth[i].Text = String.Format("{0}", (Ptr_1 + i)->ForkDepth);
                            ed_Station_Travel[i].Text = String.Format("{0}", (Ptr_1 + i)->Travel);
                            ed_Station_Lift[i].Text = String.Format("{0}", (Ptr_1 + i)->Lift);
                            ed_Station_UpOffset[i].Text = String.Format("{0}", (Ptr_1 + i)->LevelUp_Offset);
                            ed_Station_DownOffset[i].Text = String.Format("{0}", (Ptr_1 + i)->LevelDn_Offset);
                            ed_Station_InterlockIndex[i].Text = String.Format("{0}", (Ptr_1 + i)->interLockNo);

                            cb_UseIsExistItem[i].Checked = ((Ptr_1 + i)->UseIsExistItem == 1);
                        }
                        else
                        {
                            rb_Station_None[i].Checked = true;
                            rb_Station_In[i].Checked = false;
                            rb_Station_Out[i].Checked = false;
                            rb_Station_InOut[i].Checked = false;
                            rb_Station_Virtual[i].Checked = false;

                            cb_UseIsExistItem[i].Checked = false;

                            cb_Station_Item0[i].Checked = false;
                            cb_Station_Item1[i].Checked = false;
                            cb_Station_Item2[i].Checked = false;
                            cb_Station_Item3[i].Checked = false;
                            cb_Station_Item4[i].Checked = false;
                            cb_Station_Item5[i].Checked = false;
                            cb_Station_Item6[i].Checked = false;
                            cb_Station_Item7[i].Checked = false;

                            ed_Station_DelayTime[i].Text = "0";
                            ed_Station_ForkDepth[i].Text = "0";
                            ed_Station_Travel[i].Text = "0";
                            ed_Station_Lift[i].Text = "0";
                            ed_Station_UpOffset[i].Text = "0";
                            ed_Station_DownOffset[i].Text = "0";
                            ed_Station_InterlockIndex[i].Text = "0";
                        }
                    }
                }
            }


        }

        public unsafe void Display_SRMStationParam(byte[] datas)
        {
            srm_StationParam_Res = (VEXI_DEFS.TSRM_StationParam)Global_Class.UTIL_BytesToStructure(datas, datas.Length, typeof(VEXI_DEFS.TSRM_StationParam));

            Display_SRMStationParam();

            IsIn_Stationinfo = true;
            Enable_Btn();
        }


        public unsafe void Process_SRMStationParamCtrlRes(byte[] datas)
        {
            RxDateTime = DateTime.Now;

            dev_StationParam_CTRLRes = (VEXI_DEFS.TDEV_CtrlRes_3Byte)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TDEV_CtrlRes_3Byte));

            if (dev_StationParam_CTRLRes.CtrlResult != ConstClass.CODE_ACK)
            {


                switch (dev_StationParam_CTRLRes.NackReason)
                {
                    case 1: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 스테이션 개수 범위 초과", "W"); break;
                    case 2: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 스테이션 " + dev_StationParam_CTRLRes.NackItem.ToString() + " 인터록 설정 이상", "W"); break;
                    case 3: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 스테이션 " + dev_StationParam_CTRLRes.NackItem.ToString() + " 스테이션 타입 이상", "W"); break;
                    case 4: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 스테이션 " + dev_StationParam_CTRLRes.NackItem.ToString() + " 주행 위치 설정 이상", "W"); break;
                    case 5: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 스테이션 " + dev_StationParam_CTRLRes.NackItem.ToString() + " 승강 위치 설정 이상", "W"); break;
                    case 6: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 스테이션 " + dev_StationParam_CTRLRes.NackItem.ToString() + " 포크 위치 설정 이상", "W"); break;
                    case 7: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 스테이션 " + dev_StationParam_CTRLRes.NackItem.ToString() + " 상승 정위치 오프셋 설정 이상", "W"); break;
                    case 8: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 스테이션 " + dev_StationParam_CTRLRes.NackItem.ToString() + " 하강 정위치 오프셋 설정 이상", "W"); break;
                    case 9: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 스테이션 " + dev_StationParam_CTRLRes.NackItem.ToString() + " 인터록 센서 번호 설정 이상", "W"); break;
                    case 10: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 셋업모드 아님", "W"); break;
                    default: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 0x" + string.Format("{0:X2}", dev_StationParam_CTRLRes.NackReason), "W"); break;
                }
            }
        }

        public unsafe void Process_SRMRackOffsetCtrlRes(byte[] datas)
        {
            RxDateTime = DateTime.Now;

            srm_CellOffset_CTRLRes = (VEXI_DEFS.TSRM_CellOffsetCTRLRes)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TSRM_CellOffsetCTRLRes));

            if (srm_CellOffset_CTRLRes.DevType != 1)
            {
                Enable_Btn();
                NoAnswerTimer.Enabled = false;
                form_Main.GlobalObj.MsgBox_Info("제어 실패 : RackType 오류", "W");
            }
            else
            {
                if (srm_CellOffset_CTRLRes.CtrlResult == ConstClass.CODE_ACK)
                {
                    if ((srm_CellOffset_CTRLRes.Startindex + srm_CellOffset_CTRLRes.TxCount) >= srm_CellOffset_CTRLRes.TotalCount)
                    {
                        Enable_Btn();
                        NoAnswerTimer.Enabled = false;
                    }
                    else
                    {
                        Ctrl_CellOffset((UInt16)(srm_CellOffset_CTRLRes.Startindex + srm_CellOffset_CTRLRes.TxCount));
                    }
                }
                else
                {
                    Enable_Btn();
                    NoAnswerTimer.Enabled = false;
                    switch (srm_CellOffset_CTRLRes.NackReason)
                    {
                        case 1: form_Main.GlobalObj.MsgBox_Info("Rack Type 이상", "W"); break;
                        case 10: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 셋업모드 아님", "W"); break;
                        default: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 0x" + string.Format("{0:X2}", srm_CellOffset_CTRLRes.NackReason), "W"); break;

                    }

                }
            }
        }

        public unsafe void Process_SRMRackPositionCtrlRes(byte[] datas)
        {
            RxDateTime = DateTime.Now;

            srm_CellPosition_CTRLRes = (VEXI_DEFS.TSRM_CellPositionCTRLRes)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TSRM_CellPositionCTRLRes));

            if (srm_CellPosition_CTRLRes.Header.RackType != 1)
            {
                Enable_Btn();
                NoAnswerTimer.Enabled = false;
                form_Main.GlobalObj.MsgBox_Info("제어 실패 : RackType 오류", "W");
            }
            else
            {
                if (srm_CellPosition_CTRLRes.CtrlResult == ConstClass.CODE_ACK)
                {
                    switch (srm_CellPosition_CTRLRes.Header.DataType)
                    {
                        case 1:
                            Ctrl_CellPosition(false, 2, 0, (byte)(lv_Level_Position.Items.Count - 1));
                            break;
                        case 2:
                            Ctrl_CellPosition(false, 3, 0, (byte)(lv_Bay_Position.Items.Count - 1));
                            break;
                        case 3:
                            Ctrl_CellPosition(false, 4, 0, (byte)(lv_Level_Position.Items.Count - 1));
                            break;
                        case 4:
                            Enable_Btn();
                            NoAnswerTimer.Enabled = false;
                            break;
                    }
                }
                else
                {
                    Enable_Btn();
                    NoAnswerTimer.Enabled = false;
                    switch (srm_CellPosition_CTRLRes.NackReason)
                    {
                        case 1: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 요청정보이상 (RackType)", "W"); break;
                        case 2: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 요청정보이상 (DataType)", "W"); break;
                        case 3: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 요청정보이상 (Start No)", "W"); break;
                        case 4: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 요청정보이상 (End No)", "W"); break;
                        case 5: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 설정값이상(개수 오류)", "W"); break;
                        case 6: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 설정값이상(값 감소)", "W"); break;
                        case 7: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 설정값이상(설정가능범위 외)", "W"); break;
                        case 10: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 셋업모드 아님", "W"); break;
                        default: form_Main.GlobalObj.MsgBox_Info("제어 실패 : 0x" + string.Format("{0:X2}", srm_CellPosition_CTRLRes.NackReason), "W"); break;
                    }

                }
            }
        }

        public unsafe void Display_SRMRackLevelPosition(bool InitFlag,  VEXI_DEFS.TSRM_CellPositionRES LevelPositionRec)
        {
            ListViewItem listviewItem;

            if (InitFlag)
            {
                lv_Level_Position.Items.Clear();
                for (ushort i = 0; i < LevelPositionRec.Header.LevelCount; i++)
                {
                    listviewItem = lv_Level_Position.Items.Add(string.Format("{0}", i + 1));
                    listviewItem.SubItems.Add(string.Format("{0}", 0));
                    listviewItem.SubItems.Add(string.Format("{0}", 0));
                }
            }


            //Display
            for (ushort i = LevelPositionRec.Header.StartNo; i <= LevelPositionRec.Header.EndNo; i++)
            {
                if (LevelPositionRec.Header.DataType == 2)
                {
                    lv_Level_Position.Items[i].SubItems[1].Text = string.Format("{0}", LevelPositionRec.Position[i - LevelPositionRec.Header.StartNo]);
                } else if (LevelPositionRec.Header.DataType == 4)
                {
                    lv_Level_Position.Items[i].SubItems[2].Text = string.Format("{0}", LevelPositionRec.Position[i - LevelPositionRec.Header.StartNo]);
                }
            }

        }

        public unsafe void Display_SRMRackBayPosition(bool InitFlag, VEXI_DEFS.TSRM_CellPositionRES BayPositionRec)
        {
            ListViewItem listviewItem;

            if (InitFlag)
            {
                lv_Bay_Position.Items.Clear();
                for (ushort i = 0; i < BayPositionRec.Header.BayCount; i++)
                {
                    listviewItem = lv_Bay_Position.Items.Add(string.Format("{0}", i + 1));
                    listviewItem.SubItems.Add(string.Format("{0}", 0));
                    listviewItem.SubItems.Add(string.Format("{0}", 0));
                }
            }


            //Display
            for (ushort i = BayPositionRec.Header.StartNo; i <= BayPositionRec.Header.EndNo; i++)
            {
                if (BayPositionRec.Header.DataType == 1)
                {
                    lv_Bay_Position.Items[i].SubItems[1].Text = string.Format("{0}", BayPositionRec.Position[i - BayPositionRec.Header.StartNo]);
                } else if (BayPositionRec.Header.DataType == 3)
                {
                    lv_Bay_Position.Items[i].SubItems[2].Text = string.Format("{0}", BayPositionRec.Position[i - BayPositionRec.Header.StartNo]);
                }
            }
        }

        public unsafe void Display_SRMRackPosition(byte[] datas)
        {
            //가변길이 데이터로 datas.Length 를 넘겨서 그만큼만 copy 한다
            srm_CellPosition_Res = (VEXI_DEFS.TSRM_CellPositionRES)Global_Class.UTIL_BytesToStructure(datas, datas.Length, typeof(VEXI_DEFS.TSRM_CellPositionRES));

            RxDateTime = DateTime.Now;

            if ((srm_CellPosition_Res.Header.RackType != 1) ||
                //(srm_CellPosition_Res.Header.BayCount == 0) ||
                //(srm_CellPosition_Res.Header.LevelCount == 0) ||
                (srm_CellPosition_Res.Header.BayCount > 256) ||
                (srm_CellPosition_Res.Header.LevelCount > 128))
            {
                Enable_Btn();
                NoAnswerTimer.Enabled = false;
                form_Main.GlobalObj.MsgBox_Confirm_OK("랙 구성정보가 올바르지 않습니다.");
                return;
            }

            lbl_Bay_Count.Text = string.Format("{0}", srm_CellPosition_Res.Header.BayCount);
            lbl_Level_Count.Text = string.Format("{0}", srm_CellPosition_Res.Header.LevelCount);

            edPositionBayCount.Text = string.Format("{0}", srm_CellPosition_Res.Header.BayCount);
            edPositionLevelCount.Text = string.Format("{0}", srm_CellPosition_Res.Header.LevelCount);


            ListViewItem listviewItem;

            if (srm_CellPosition_Res.Header.DataType == 1) //Bay
            {
                lv_Bay_Position.Items.Clear();
                if (srm_CellPosition_Res.Header.BayCount > 0)
                {
                    for (ushort i = 0; i < srm_CellPosition_Res.Header.BayCount; i++)
                    {
                        listviewItem = lv_Bay_Position.Items.Add(string.Format("{0}", i + 1));
                        listviewItem.SubItems.Add(string.Format("{0}", 0));
                        listviewItem.SubItems.Add(string.Format("{0}", 0));
                    }

                    if (srm_CellPosition_Res.Header.EndNo > (srm_CellPosition_Res.Header.BayCount - 1))
                    {
                        srm_CellPosition_Res.Header.EndNo = (byte)(srm_CellPosition_Res.Header.BayCount - 1);
                    }

                    for (ushort i = srm_CellPosition_Res.Header.StartNo; i <= srm_CellPosition_Res.Header.EndNo; i++)
                    {
                        lv_Bay_Position.Items[i].SubItems[1].Text = string.Format("{0}", srm_CellPosition_Res.Position[i - srm_CellPosition_Res.Header.StartNo]);

                    }
                }
                else
                {
                    srm_CellPosition_Res.Header.EndNo = 0;
                }


                Request_CellPosition(2, 0, 127);
            }
            else if (srm_CellPosition_Res.Header.DataType == 2) //Level
            {
                //Display 초기화
                lv_Level_Position.Items.Clear();

                if (srm_CellPosition_Res.Header.LevelCount > 0)
                {
                    for (ushort i = 0; i < srm_CellPosition_Res.Header.LevelCount; i++)
                    {
                        listviewItem = lv_Level_Position.Items.Add(string.Format("{0}", i + 1));
                        listviewItem.SubItems.Add(string.Format("{0}", 0));
                        listviewItem.SubItems.Add(string.Format("{0}", 0));
                    }
                    if (srm_CellPosition_Res.Header.EndNo > (srm_CellPosition_Res.Header.LevelCount - 1))
                    {
                        srm_CellPosition_Res.Header.EndNo = (byte)(srm_CellPosition_Res.Header.LevelCount - 1);
                    }

                    for (ushort i = srm_CellPosition_Res.Header.StartNo; i <= srm_CellPosition_Res.Header.EndNo; i++)
                    {
                        lv_Level_Position.Items[i].SubItems[1].Text = string.Format("{0}", srm_CellPosition_Res.Position[i - srm_CellPosition_Res.Header.StartNo]);
                    }

                }
                else
                {
                    srm_CellPosition_Res.Header.EndNo = 0;
                }

                Request_CellPosition(3, 0, 255);
            } else if (srm_CellPosition_Res.Header.DataType == 3) //Bay
            {
                if (srm_CellPosition_Res.Header.BayCount > 0)
                {
                    if (srm_CellPosition_Res.Header.EndNo > (srm_CellPosition_Res.Header.BayCount - 1))
                    {
                        srm_CellPosition_Res.Header.EndNo = (byte)(srm_CellPosition_Res.Header.BayCount - 1);
                    }

                    for (ushort i = srm_CellPosition_Res.Header.StartNo; i <= srm_CellPosition_Res.Header.EndNo; i++)
                    {
                        lv_Bay_Position.Items[i].SubItems[2].Text = string.Format("{0}", srm_CellPosition_Res.Position[i - srm_CellPosition_Res.Header.StartNo]);

                    }
                }
                else
                {
                    srm_CellPosition_Res.Header.EndNo = 0;
                }


                Request_CellPosition(4, 0, 127);
            }
            else if (srm_CellPosition_Res.Header.DataType == 4) //Level
            {
                if (srm_CellPosition_Res.Header.LevelCount > 0)
                {
                    if (srm_CellPosition_Res.Header.EndNo > (srm_CellPosition_Res.Header.LevelCount - 1))
                    {
                        srm_CellPosition_Res.Header.EndNo = (byte)(srm_CellPosition_Res.Header.LevelCount - 1);
                    }

                    for (ushort i = srm_CellPosition_Res.Header.StartNo; i <= srm_CellPosition_Res.Header.EndNo; i++)
                    {
                        lv_Level_Position.Items[i].SubItems[2].Text = string.Format("{0}", srm_CellPosition_Res.Position[i - srm_CellPosition_Res.Header.StartNo]);
                    }

                }
                else
                {
                    srm_CellPosition_Res.Header.EndNo = 0;
                }

                //셀 포지션 요청 종결
                IsIn_RackPosition = true;
                Enable_Btn();
                NoAnswerTimer.Enabled = false;
            }
        }

        public unsafe void Display_SRMRackOffset(byte[] datas)
        {
            RxDateTime = DateTime.Now;

            //가변길이 데이터로 datas.Length 를 넘겨서 그만큼만 copy 한다
            srm_CellOffset_Res = (VEXI_DEFS.TSRM_CellOffset)Global_Class.UTIL_BytesToStructure(datas, datas.Length, typeof(VEXI_DEFS.TSRM_CellOffset));

            if ((srm_CellOffset_Res.Header.DevType != 1))
            {
                Enable_Btn();
                NoAnswerTimer.Enabled = false;
                form_Main.GlobalObj.MsgBox_Confirm_OK("랙타입 정보가 올바르지 않습니다.");
                return;
            }

            if (srm_CellOffset_Res.Header.TotalCount == 0)
            {
                IsIn_RackOffset = true;
                Enable_Btn();
                NoAnswerTimer.Enabled = false;
                return;
            }
            if (srm_CellOffset_Res.Header.ItemCount == 0) return;

            ListViewItem listviewItem;

            //Todo :
            fixed (VEXI_DEFS.TSRM_CellOffsetRec* Ptr = &srm_CellOffset_Res.SRM_CellOffsetRec)
            {
                for (int i = 0; i < srm_CellOffset_Res.Header.ItemCount; i++)
                {
                    listviewItem = lv_Rack_Offset.Items.Add(string.Format("{0}", (Ptr + i)->Bay + 1));
                    listviewItem.SubItems.Add(string.Format("{0}", (Ptr + i)->Level + 1));
                    listviewItem.SubItems.Add(string.Format("{0}", (Ptr + i)->Left_Travel_Offset));
                    listviewItem.SubItems.Add(string.Format("{0}", (Ptr + i)->Left_Lift_Offset));
                    listviewItem.SubItems.Add(string.Format("{0}", (Ptr + i)->Left_Fork_Offset));
                    listviewItem.SubItems.Add(string.Format("{0}", (Ptr + i)->Right_Travel_Offset));
                    listviewItem.SubItems.Add(string.Format("{0}", (Ptr + i)->Right_Lift_Offset));
                    listviewItem.SubItems.Add(string.Format("{0}", (Ptr + i)->Right_Fork_Offset));
                }
            }

            lblOffsetProgress.Text = lv_Rack_Offset.Items.Count.ToString() + " / " + srm_CellOffset_Res.Header.TotalCount.ToString();
            if (progressBar1.Maximum != srm_CellOffset_Res.Header.TotalCount) progressBar1.Maximum = srm_CellOffset_Res.Header.TotalCount;
            progressBar1.Value = lv_Rack_Offset.Items.Count;

            if ((srm_CellOffset_Res.Header.Nowindex + srm_CellOffset_Res.Header.ItemCount) < srm_CellOffset_Res.Header.TotalCount)
            {
                Request_CellOffset((UInt16)(srm_CellOffset_Res.Header.Nowindex + srm_CellOffset_Res.Header.ItemCount));
            }
            else
            {
                IsIn_RackOffset = true;
                Enable_Btn();
                NoAnswerTimer.Enabled = false;
            }
        }

        private void Check_zeroOffset()
        {
            if (lv_Rack_Offset.Items.Count > 0)
            {
                for (int i = lv_Rack_Offset.Items.Count - 1; i >= 0; i--)
                {
                    {
                        if ((lv_Rack_Offset.Items[i].SubItems[2].Text == "0") &&
                            (lv_Rack_Offset.Items[i].SubItems[3].Text == "0") &&
                            (lv_Rack_Offset.Items[i].SubItems[4].Text == "0") &&
                            (lv_Rack_Offset.Items[i].SubItems[5].Text == "0") &&
                            (lv_Rack_Offset.Items[i].SubItems[6].Text == "0") &&
                            (lv_Rack_Offset.Items[i].SubItems[7].Text == "0"))
                        {
                            lv_Rack_Offset.Items.RemoveAt(i);
                        }
                    }
                }
            }
        }

        private int FindIndex_Offset(ushort Bay, ushort Level, out bool IsExist)
        {
            //Offset은 Bay-Level 기준으로 Sorting 되서 전송되어야 함으로 ListView에 ADD할 때부터 정렬하여 넣어준다.
            //가장 간단한 방법은 첨부터 검색해서 ADD될 위치를 찾는것이지만 속도가 느리므로 2진 검색을 활용하여 ADD될 자리를 찾는다.
            int index = -1;
            int Compare_Bay, Compare_Level;
            int totalCount = lv_Rack_Offset.Items.Count;
            int Startindex, EndIndex, CheckIndex;

            IsExist = false;

            if (totalCount == 0)
            {
                return -1;
            }
            else
            {
                //맨앞에 넣으면 되는지 (맨앞이랑 같은지도 비교)
                int.TryParse(lv_Rack_Offset.Items[0].SubItems[0].Text, out Compare_Bay);
                int.TryParse(lv_Rack_Offset.Items[0].SubItems[1].Text, out Compare_Level);
                if (Bay < Compare_Bay)
                {
                    return 0;
                }
                else if (Bay == Compare_Bay)
                {
                    if (Level < Compare_Level)
                    {
                        return 0;
                    }
                    else if (Level == Compare_Level)
                    {
                        IsExist = true;
                        return 0;
                    }
                }

                //맨끝에 넣으면 되는지  (맨뒤랑 같은지도 비교)
                int.TryParse(lv_Rack_Offset.Items[totalCount - 1].SubItems[0].Text, out Compare_Bay);
                int.TryParse(lv_Rack_Offset.Items[totalCount - 1].SubItems[1].Text, out Compare_Level);
                if (Bay > Compare_Bay)
                {
                    return -1;
                }
                else if (Bay == Compare_Bay)
                {
                    if (Level > Compare_Level)
                    {
                        return -1;
                    }
                    else if (Level == Compare_Level)
                    {
                        IsExist = true;
                        return (lv_Rack_Offset.Items.Count - 1);
                    }
                }

                Startindex = 0;
                EndIndex = totalCount - 1;


                while (EndIndex - Startindex > 3)
                {
                    CheckIndex = (EndIndex - Startindex) / 2 + Startindex;

                    int.TryParse(lv_Rack_Offset.Items[CheckIndex].SubItems[0].Text, out Compare_Bay);
                    int.TryParse(lv_Rack_Offset.Items[CheckIndex].SubItems[1].Text, out Compare_Level);
                    if (Bay < Compare_Bay)
                    {
                        EndIndex = CheckIndex;
                    }
                    else if (Bay == Compare_Bay)
                    {
                        if (Level < Compare_Level)
                        {
                            EndIndex = CheckIndex;
                        }
                        else if (Level == Compare_Level)
                        {
                            IsExist = true;
                            return CheckIndex;
                        }
                    }
                    if (Bay > Compare_Bay)
                    {
                        Startindex = CheckIndex;
                    }
                    else if (Bay == Compare_Bay)
                    {
                        if (Level > Compare_Level)
                        {
                            Startindex = CheckIndex;
                        }
                        else if (Level == Compare_Level)
                        {
                            IsExist = true;
                            return CheckIndex;
                        }
                    }
                }
                for (int i = Startindex; i <= EndIndex; i++)
                {
                    int.TryParse(lv_Rack_Offset.Items[i].SubItems[0].Text, out Compare_Bay);
                    int.TryParse(lv_Rack_Offset.Items[i].SubItems[1].Text, out Compare_Level);
                    if (Bay > Compare_Bay)
                    {
                        index = i;
                    }
                    else if (Bay == Compare_Bay)
                    {
                        if (Level > Compare_Level)
                        {

                            index = i;
                        }
                        else if (Level == Compare_Level)
                        {
                            IsExist = true;
                            return i;
                        }
                        else
                        {
                            index = i;
                            return i;
                        }
                    }
                    else
                    {
                        index = i;
                        return i;
                    }
                }

            }
            return index;
        }


        private void ADD_Offset(ushort Bay, ushort Level)
        {
            ListViewItem listviewItem;
            bool IsExist;
            int ResultIndex;

            lv_Rack_Offset.SelectedItems.Clear();

            ResultIndex = FindIndex_Offset(Bay, Level, out IsExist);

            if (IsExist)
            {
                lv_Rack_Offset.Items[ResultIndex].Selected = true;
                return;
            }
            else
            {
                if (ResultIndex == -1)
                {
                    listviewItem = lv_Rack_Offset.Items.Add(Bay.ToString());
                }
                else
                {
                    listviewItem = lv_Rack_Offset.Items.Insert(ResultIndex, Bay.ToString());
                }
            }

            listviewItem.SubItems.Add(Level.ToString());
            listviewItem.SubItems.Add("0");
            listviewItem.SubItems.Add("0");
            listviewItem.SubItems.Add("0");
            listviewItem.SubItems.Add("0");
            listviewItem.SubItems.Add("0");
            listviewItem.SubItems.Add("0");

            listviewItem.Selected = true;

        }

        private void Request_CellPosition(byte DataType, byte Startindex, byte Endindex)
        {
            byte[] data = new byte[4];

            data[0] = 1; //SRM
            data[1] = DataType;
            data[2] = Startindex;
            data[3] = Endindex;

            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_94, data);
            RxDateTime = DateTime.Now;
        }

        private void Request_CellOffset(UInt16 Startindex)
        {
            if (srm_CellOffset_Req.ReqIndex != Startindex) Retrycount = 0;
            srm_CellOffset_Req.DevType = 1;
            srm_CellOffset_Req.ReqIndex = Startindex;
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_96, srm_CellOffset_Req);
            RxDateTime = DateTime.Now;
        }

        private void Enable_Btn()
        {

            btn_CellPosition_Load.Enabled = true;
            btn_CellOffset_Load.Enabled = true;
            btn_StationConfig_Load.Enabled = true;
            if ((form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.DevMode & 0x08) == 0)
            {

                btn_CellPosition_Set.Enabled = false;
                btn_CellOffset_Set.Enabled = false;
                btn_StationConfig_Set.Enabled = false;

                btn_CellPosition_Init.Enabled = false;
                btn_RackOffset_Init.Enabled = false;
            }
            else
            {

                btn_CellPosition_Init.Enabled = true;
                btn_RackOffset_Init.Enabled = true;

                btn_CellPosition_Set.Enabled = ((IsIn_RackPosition) || ((lv_Bay_Position.Items.Count > 0) && (lv_Level_Position.Items.Count > 0)));
                btn_CellOffset_Set.Enabled = IsIn_RackOffset;
                btn_StationConfig_Set.Enabled = IsIn_Stationinfo;
            }
        }

        private void Disable_Btn()
        {

            btn_CellPosition_Load.Enabled = false;
            btn_CellPosition_Set.Enabled = false;
            btn_CellOffset_Load.Enabled = false;
            btn_CellOffset_Set.Enabled = false;
            btn_StationConfig_Load.Enabled = false;
            btn_StationConfig_Set.Enabled = false;
        }

        private void Hide_AllEdit()
        {
            for (byte i = 0; i <= (lv_Rack_Offset.Columns.Count - 1); i++)
            {
                if (Rack_Offset_TextBox[i] != null)
                {
                    Rack_Offset_TextBox[i].Visible = false;
                }
            }

            for (byte i = 0; i <= (lv_Bay_Position.Columns.Count - 1); i++)
            {
                if (Bay_Position_TextBox[i] != null)
                {
                    Bay_Position_TextBox[i].Visible = false;
                }
            }
            for (byte i = 0; i <= (lv_Level_Position.Columns.Count - 1); i++)
            {
                if (Level_Position_TextBox[i] != null)
                {
                    Level_Position_TextBox[i].Visible = false;
                }
            }
        }

        private void SaveToFile_Offset()
        {
            Check_zeroOffset();

            if ((lv_Rack_Offset.Items.Count == 0))
            {
                form_Main.GlobalObj.MsgBox_Info("OFFSET 정보가 작성되지 않았습니다.", "W");
                return;
            }


            //방법 1 : serialize Save => 파일 크기는 커지지만 속도는 빠르다. 이렇게 생성된 바이너리 파일은 로딩이 아니고서는 볼수가 없다.
            //saveFileDialog1.Filter = "*.Offset_cfg|*.OFFSET_CFG";
            //if (saveFileDialog1.FileName == "")
            //{
            //    saveFileDialog1.InitialDirectory = Application.StartupPath;
            //}
            //if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    SerializeListViewItems(lv_Rack_Offset.Items, saveFileDialog1.FileName);
            //}

            //방법 2 : 바이너리 버퍼 Save => Serialize 보다 속도는 느리지만 파일 크기가(8*Offset Count)가 월등히 작다. 화면에 로딩하지 않아도 바이너리 Viewer로 파일 열어보면 데이터 구분 가능
            byte[] Data = new byte[256 * 128 * 8];
            saveFileDialog1.Filter = "*.Offset_cfg|*.OFFSET_CFG";

            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                for (int i = 0; i < lv_Rack_Offset.Items.Count; i++)
                {
                    Data[i * 8] = (byte)(Convert.ToUInt16(lv_Rack_Offset.Items[i].SubItems[0].Text) - 1);
                    Data[i * 8 + 1] = (byte)(Convert.ToUInt16(lv_Rack_Offset.Items[i].SubItems[1].Text) - 1);
                    Data[i * 8 + 2] = (byte)(Convert.ToSByte(lv_Rack_Offset.Items[i].SubItems[2].Text));
                    Data[i * 8 + 3] = (byte)(Convert.ToSByte(lv_Rack_Offset.Items[i].SubItems[3].Text));
                    Data[i * 8 + 4] = (byte)(Convert.ToSByte(lv_Rack_Offset.Items[i].SubItems[4].Text));
                    Data[i * 8 + 5] = (byte)(Convert.ToSByte(lv_Rack_Offset.Items[i].SubItems[5].Text));
                    Data[i * 8 + 6] = (byte)(Convert.ToSByte(lv_Rack_Offset.Items[i].SubItems[6].Text));
                    Data[i * 8 + 7] = (byte)(Convert.ToSByte(lv_Rack_Offset.Items[i].SubItems[7].Text));
                }


                using (BinaryWriter br = new BinaryWriter(File.Open(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write)))
                {
                    try
                    {
                        br.Seek(0, SeekOrigin.Begin);
                        br.Write(Data, 0, lv_Rack_Offset.Items.Count * 8);
                    }
                    finally
                    {
                        br.Close();
                    }

                }
            }


            //방법 3 : TEXT Save =>  Offset 갯수가 많아지면 파일 크기, 처리 시간 문제가 된다, Text 파일이므로 화면에 로딩하지 않더라도 메모장 같은데서 볼 수 있다.
            //saveFileDialog1.Filter = "*.Offset_cfg|*.OFFSET_CFG";

            //if (saveFileDialog1.FileName == "")
            //{
            //    saveFileDialog1.InitialDirectory = Application.StartupPath;
            //}
            //if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    saveFileDialog1.FileName = "";

            //    IniControl.WriteIni(saveFileDialog1.FileName, "OFFSET COUNT", "TOTAL COUNT", lv_Rack_Offset.Items.Count);

            //    if (lv_Rack_Offset.Items.Count > 0)
            //    {
            //        for (int i = 0; i < lv_Rack_Offset.Items.Count; i++)
            //        {
            //            IniControl.WriteIni(saveFileDialog1.FileName, "OFFSET_" + (i + 1).ToString(), "BAY", lv_Rack_Offset.Items[i].SubItems[0].Text.ToString());
            //            IniControl.WriteIni(saveFileDialog1.FileName, "OFFSET_" + (i + 1).ToString(), "LEVEL", lv_Rack_Offset.Items[i].SubItems[1].Text.ToString());
            //            IniControl.WriteIni(saveFileDialog1.FileName, "OFFSET_" + (i + 1).ToString(), "L_Travel", lv_Rack_Offset.Items[i].SubItems[2].Text.ToString());
            //            IniControl.WriteIni(saveFileDialog1.FileName, "OFFSET_" + (i + 1).ToString(), "L_Lift", lv_Rack_Offset.Items[i].SubItems[3].Text.ToString());
            //            IniControl.WriteIni(saveFileDialog1.FileName, "OFFSET_" + (i + 1).ToString(), "L_Fork", lv_Rack_Offset.Items[i].SubItems[4].Text.ToString());
            //            IniControl.WriteIni(saveFileDialog1.FileName, "OFFSET_" + (i + 1).ToString(), "R_Travel", lv_Rack_Offset.Items[i].SubItems[5].Text.ToString());
            //            IniControl.WriteIni(saveFileDialog1.FileName, "OFFSET_" + (i + 1).ToString(), "R_Lift", lv_Rack_Offset.Items[i].SubItems[6].Text.ToString());
            //            IniControl.WriteIni(saveFileDialog1.FileName, "OFFSET_" + (i + 1).ToString(), "R_Fork", lv_Rack_Offset.Items[i].SubItems[7].Text.ToString());
            //        }
            //    }
            //}
        }

        private void SerializeListViewItems(ListView.ListViewItemCollection colLvi, String sFileName)
        {
            FileStream oFS = null;
            BinaryFormatter oBinFormat = null;

            try
            {
                // 저장할 파일 스트림 객체를 생성한다.
                oFS = File.Open(sFileName, FileMode.Create, FileAccess.Write);
                // 바이너리 형식으로 직렬화를 수행한다.
                oBinFormat = new BinaryFormatter();

                //리스트뷰 아이템 갯수를 먼저 직렬화한다.
                oBinFormat.Serialize(oFS, colLvi.Count);
                //갯수만큼 각 아이템을 직렬화한다.
                for (int i = 0; i < colLvi.Count; ++i)
                {
                    oBinFormat.Serialize(oFS, colLvi[i]);
                }
            }
            catch
            {
                // Do Nothing!
            }
            finally
            {
                if (oFS != null)

                    oFS.Close();

            }
        }

        private void DeserializeListViewItems(ListView lv, String sFileName)
        {
            FileStream oFS = null;
            BinaryFormatter oBinFormat = null;
            int refreshcount = 0;
            try
            {
                // 불러올 파일 스트림 객체를 생성한다.
                oFS = File.Open(sFileName, FileMode.Open, FileAccess.Read);
                // 바이너리 형식으로 역 직렬화를 수행한다.
                oBinFormat = new BinaryFormatter();

                // 리스트뷰 아이템 갯수를 역직렬화 한다.   
                int nCount = (int)oBinFormat.Deserialize(oFS);
                // 갯수만큼 각 아이템을 역 직렬화 한다.
                for (int i = 0; i < nCount; ++i)
                {
                    refreshcount++;

                    Object obj = oBinFormat.Deserialize(oFS);
                    lv.Items.Add((ListViewItem)obj);

                    if (refreshcount >= 3000)
                    {
                        refreshcount = 0;
                        lv.Refresh();
                    }

                }
            }
            catch
            {
                // Do Nothing!
            }
            finally
            {
                if (oFS != null)
                    oFS.Close();
            }
        }

        private void Display_totalOffsetRec()
        {
            lv_Rack_Offset.Items.Clear();
            IsIn_RackOffset = true;
            Enable_Btn();

            if (CellOffset_Total.TotalCount == 0) return;

            ListViewItem listviewItem;


            lv_Rack_Offset.BeginUpdate();
            for (int i = 0; i < CellOffset_Total.TotalCount; i++)
            {

                listviewItem = lv_Rack_Offset.Items.Add(string.Format("{0}", CellOffset_Total.SRM_CellOffsetRec[i].Bay + 1));
                listviewItem.SubItems.Add(string.Format("{0}", CellOffset_Total.SRM_CellOffsetRec[i].Level + 1));
                listviewItem.SubItems.Add(string.Format("{0}", CellOffset_Total.SRM_CellOffsetRec[i].Left_Travel_Offset));
                listviewItem.SubItems.Add(string.Format("{0}", CellOffset_Total.SRM_CellOffsetRec[i].Left_Lift_Offset));
                listviewItem.SubItems.Add(string.Format("{0}", CellOffset_Total.SRM_CellOffsetRec[i].Left_Fork_Offset));
                listviewItem.SubItems.Add(string.Format("{0}", CellOffset_Total.SRM_CellOffsetRec[i].Right_Travel_Offset));
                listviewItem.SubItems.Add(string.Format("{0}", CellOffset_Total.SRM_CellOffsetRec[i].Right_Lift_Offset));
                listviewItem.SubItems.Add(string.Format("{0}", CellOffset_Total.SRM_CellOffsetRec[i].Right_Fork_Offset));
            }
            lv_Rack_Offset.EndUpdate();
        }

        //통합파일에서 불러오는 것으로 수정함
        //개별파일에서 불러오는 소스는 남겨놓음
        private void LoadFromoFile_Offset()
        {
            lv_Rack_Offset.Items.Clear();

            //방법 1 : serialize Save => 파일 크기는 커지지만 속도는 빠르다. 이렇게 생성된 바이너리 파일은 로딩이 아니고서는 볼수가 없다.
            //openFileDialog1.Filter = "*.Offset_cfg|*.Offset_cfg";

            //if (openFileDialog1.FileName == "")
            //{
            //    openFileDialog1.InitialDirectory = Application.StartupPath;
            //}

            //if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    DeserializeListViewItems(lv_Rack_Offset, openFileDialog1.FileName);
            //}

            //방법 2 : 바이너리 버퍼 Save => Serialize 보다 속도는 느리지만 그에 비해 파일 크기가 월등히 작다. 화면에 로딩하지 않아도 바이너리 Viewer로 파일 열어보면 데이터 구분 가능
            int RefreshCount = 0;
            ListViewItem listviewItem;
            openFileDialog1.Filter = "*.Offset_cfg|*.OFFSET_CFG";

            if (openFileDialog1.FileName == "")
            {
                openFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (BinaryReader br = new BinaryReader(File.Open(openFileDialog1.FileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        byte[] Savebytes;
                        lv_Rack_Offset.BeginUpdate();
                        while (br.BaseStream.Position < br.BaseStream.Length)
                        {
                            RefreshCount++;
                            Savebytes = br.ReadBytes(8);
                            listviewItem = lv_Rack_Offset.Items.Add(string.Format("{0}", Savebytes[0] + 1));
                            listviewItem.SubItems.Add(string.Format("{0}", Savebytes[1] + 1));
                            listviewItem.SubItems.Add(string.Format("{0}", (sbyte)Savebytes[2]));
                            listviewItem.SubItems.Add(string.Format("{0}", (sbyte)Savebytes[3]));
                            listviewItem.SubItems.Add(string.Format("{0}", (sbyte)Savebytes[4]));
                            listviewItem.SubItems.Add(string.Format("{0}", (sbyte)Savebytes[5]));
                            listviewItem.SubItems.Add(string.Format("{0}", (sbyte)Savebytes[6]));
                            listviewItem.SubItems.Add(string.Format("{0}", (sbyte)Savebytes[7]));

                            if (RefreshCount >= 3000)
                            {
                                RefreshCount = 0;
                                lv_Rack_Offset.Refresh();
                            }

                        }
                        lv_Rack_Offset.EndUpdate();
                    }
                    finally
                    {
                        br.Close();
                    }

                    IsIn_RackOffset = true;
                    Enable_Btn();
                }
            }

            //방법 3 : TEXT Save =>  Offset 갯수가 많아지면 파일 크기, 처리 시간 문제가 된다, Text 파일이므로 화면에 로딩하지 않더라도 메모장 같은데서 볼 수 있다.
            //ushort TmptotalCount = 0;
            //ushort TmpBay;
            //ushort TmpLevel;
            //sbyte L_Travel;
            //sbyte L_Lift;
            //sbyte L_Fork;
            //sbyte R_Travel;
            //sbyte R_Lift;
            //sbyte R_Fork;

            //ListViewItem listviewItem;

            //lv_Rack_Offset.Items.Clear();

            //openFileDialog1.Filter = "*.Offset_cfg|*.Offset_cfg";

            //if (openFileDialog1.FileName == "")
            //{
            //    openFileDialog1.InitialDirectory = Application.StartupPath;
            //}

            //if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{

            //    TmptotalCount = (ushort)IniControl.ReadInteger(saveFileDialog1.FileName, "OFFSET COUNT", "TOTAL COUNT", 0);

            //    if (TmptotalCount > 0)
            //    {
            //        for (int i = 0; i < TmptotalCount; i++)
            //        {

            //            TmpBay = (ushort)IniControl.ReadInteger(saveFileDialog1.FileName, "OFFSET_" + (i + 1).ToString(), "BAY",0xFFFF);
            //            TmpLevel = (ushort)IniControl.ReadInteger(saveFileDialog1.FileName, "OFFSET_" + (i + 1).ToString(), "LEVEL", 0xFFFF);
            //            L_Travel = (sbyte)IniControl.ReadInteger(saveFileDialog1.FileName, "OFFSET_" + (i + 1).ToString(), "L_Travel", 0);
            //            L_Lift = (sbyte)IniControl.ReadInteger(saveFileDialog1.FileName, "OFFSET_" + (i + 1).ToString(), "L_Lift", 0);
            //            L_Fork = (sbyte)IniControl.ReadInteger(saveFileDialog1.FileName, "OFFSET_" + (i + 1).ToString(), "L_Fork", 0);
            //            R_Travel = (sbyte)IniControl.ReadInteger(saveFileDialog1.FileName, "OFFSET_" + (i + 1).ToString(), "R_Travel", 0);
            //            R_Lift = (sbyte)IniControl.ReadInteger(saveFileDialog1.FileName, "OFFSET_" + (i + 1).ToString(), "R_Lift", 0);
            //            R_Fork = (sbyte)IniControl.ReadInteger(saveFileDialog1.FileName, "OFFSET_" + (i + 1).ToString(), "R_Fork", 0);

            //            if ((TmpBay != 0xFFFF) && (TmpLevel != 0xFFFF))
            //            {
            //                listviewItem = lv_Rack_Offset.Items.Add(string.Format("{0}", TmpBay));
            //                listviewItem.SubItems.Add(string.Format("{0}", TmpLevel));
            //                listviewItem.SubItems.Add(string.Format("{0}", L_Travel));
            //                listviewItem.SubItems.Add(string.Format("{0}", L_Lift));
            //                listviewItem.SubItems.Add(string.Format("{0}", L_Fork));
            //                listviewItem.SubItems.Add(string.Format("{0}", R_Travel));
            //                listviewItem.SubItems.Add(string.Format("{0}", R_Lift));
            //                listviewItem.SubItems.Add(string.Format("{0}", R_Fork));
            //            }
            //        }
            //    }

            //}
        }

        //통합파일로 저장하는 것으로 변경함
        //INI 파일에 저장하는 소스는 남겨놓음
        private void SaveToFile_Position()
        {
            if ((lv_Bay_Position.Items.Count == 0) && (lv_Level_Position.Items.Count == 0))
            {
                form_Main.GlobalObj.MsgBox_Info("위치 정보가 작성되지 않았습니다.", "W");
                return;
            }

            saveFileDialog1.Filter = "*.CSV|*.csv";

            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                IniControl.WriteIni(saveFileDialog1.FileName, "RACK CONFIG", "BAY_COUNT", lv_Bay_Position.Items.Count);
                IniControl.WriteIni(saveFileDialog1.FileName, "RACK CONFIG", "LEVEL_COUNT", lv_Level_Position.Items.Count);

                if (lv_Bay_Position.Items.Count > 0)
                {
                    for (int i = 0; i < lv_Bay_Position.Items.Count; i++)
                    {
                        IniControl.WriteIni(saveFileDialog1.FileName, "BAY POSITION", (i + 1).ToString(), lv_Bay_Position.Items[i].SubItems[1].Text.ToString() + "," + lv_Bay_Position.Items[i].SubItems[2].Text.ToString());
                        //IniControl.WriteIni(saveFileDialog1.FileName, "BAY R POSITION", (i + 1).ToString(), lv_Bay_Position.Items[i].SubItems[2].Text.ToString());
                    }
                }
                if (lv_Level_Position.Items.Count > 0)
                {
                    for (int i = 0; i < lv_Level_Position.Items.Count; i++)
                    {
                        IniControl.WriteIni(saveFileDialog1.FileName, "LEVEL POSITION", (i + 1).ToString(), lv_Level_Position.Items[i].SubItems[1].Text.ToString() + "," + lv_Level_Position.Items[i].SubItems[2].Text.ToString());
                        //IniControl.WriteIni(saveFileDialog1.FileName, "LEVEL R POSITION", (i + 1).ToString(), lv_Level_Position.Items[i].SubItems[2].Text.ToString());
                    }
                }
            }
        }

        //통합파일에서 불러오는 것으로 수정함
        //INI에서 불러오는 소스는 남겨놓음
        private void LoadFromoFile_Position()
        {
            ushort TmpBayCount = 0;
            ushort TmpLevelCount = 0;
            int TmpPosition = 0;
            ListViewItem listviewItem;

            lv_Bay_Position.Items.Clear();
            lv_Level_Position.Items.Clear();

            openFileDialog1.Filter = "*.CSV|*.csv";

            if (openFileDialog1.FileName == "")
            {
                openFileDialog1.InitialDirectory = Application.StartupPath;
            }

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TmpBayCount = (ushort)IniControl.ReadInteger(openFileDialog1.FileName, "RACK CONFIG", "BAY_COUNT", 0);
                TmpLevelCount = (ushort)IniControl.ReadInteger(openFileDialog1.FileName, "RACK CONFIG", "LEVEL_COUNT", 0);

                if (TmpBayCount > 0)
                {
                    for (int i = 0; i < TmpBayCount; i++)
                    {
                        listviewItem = lv_Bay_Position.Items.Add(string.Format("{0}", i + 1));
                        TmpPosition = IniControl.ReadInteger(openFileDialog1.FileName, "BAY L POSITION", (i + 1).ToString(), 0);
                        listviewItem.SubItems.Add(string.Format("{0}", TmpPosition));
                        TmpPosition = IniControl.ReadInteger(openFileDialog1.FileName, "BAY R POSITION", (i + 1).ToString(), 0);
                        listviewItem.SubItems.Add(string.Format("{0}", TmpPosition));
                    }
                }

                if (TmpLevelCount > 0)
                {
                    for (int i = 0; i < TmpLevelCount; i++)
                    {
                        
                        listviewItem = lv_Level_Position.Items.Add(string.Format("{0}", i + 1));
                        TmpPosition = IniControl.ReadInteger(openFileDialog1.FileName, "LEVEL L POSITION", (i + 1).ToString(), 0);
                        listviewItem.SubItems.Add(string.Format("{0}", TmpPosition));
                        TmpPosition = IniControl.ReadInteger(openFileDialog1.FileName, "LEVEL R POSITION", (i + 1).ToString(), 0);
                        listviewItem.SubItems.Add(string.Format("{0}", TmpPosition));
                    }
                }

                IsIn_RackPosition = true;
                Enable_Btn();
            }
        }

        #endregion

        private void cb_Bay_Position_1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ed_Bay_Position_L_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox ed = sender as TextBox;
            int TmpIndex = -1;
            int Scrollindex = -1;

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
                
                ed_Bay_Position_L.Visible = false;
                ed_Bay_Position_R.Visible = false;

                TmpIndex = lv_Bay_Position.SelectedItems[lv_Bay_Position.SelectedItems.Count - 1].Index;
                Scrollindex = TmpIndex - lv_Bay_Position.TopItem.Index;

                if (Scrollindex <= 0)
                {
                    Scrollindex = 1;
                }


                if (TmpIndex < (lv_Bay_Position.Items.Count - 1))
                {
                    lv_Bay_Position.SelectedItems.Clear();
                    lv_Bay_Position.Items[TmpIndex + 1].Selected = true;

                    if ((TmpIndex + 1 - Scrollindex) >= 0)
                    {
                        lv_Bay_Position.TopItem = lv_Bay_Position.Items[TmpIndex + 1 - Scrollindex];
                    }
                    else
                    {

                    }

                    lv_BayPosition_Item = lv_Bay_Position.Items[TmpIndex + 1];

                    Process_Bay_Position_TextBox();

                    if (lv_Bay_Position.SelectedItems.Count == 1)
                    {
                        for (byte i = 0; i <= (lv_Bay_Position.Columns.Count - 1); i++)
                        {
                            if (Bay_Position_TextBox[i] != null)
                            {
                                if (Convert.ToByte(Bay_Position_TextBox[i].Tag.ToString()) != 1)
                                {
                                    Bay_Position_TextBox[i].Visible = true;
                                    Bay_Position_TextBox[i].BringToFront();
                                    if (Bay_Position_TextBox[i].Name == ed.Name)
                                    {
                                        Bay_Position_TextBox[i].Focus();
                                    }
                                    
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ed_Bay_Position_L_VisibleChanged(object sender, EventArgs e)
        {
            if (lv_BayPosition_Item != null)
            {
                TextBox TmpTextBox = (TextBox)sender;

                for (byte i = 0; i <= (lv_Bay_Position.Columns.Count - 1); i++)
                {
                    if (Bay_Position_TextBox[i] != null)
                    {
                        if (TmpTextBox == Bay_Position_TextBox[i])
                        {
                            if (!TmpTextBox.Visible)
                            {
                                lv_BayPosition_Item.SubItems[i].Text = Bay_Position_TextBox[i].Text;
                            }
                        }
                    }
                }
            }
        }

        private void ed_Level_Position_L_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox ed = sender as TextBox;
            int TmpIndex = -1;
            int Scrollindex = -1;

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

                ed_Level_Position_L.Visible = false;
                ed_Level_Position_R.Visible = false;

                TmpIndex = lv_Level_Position.SelectedItems[lv_Level_Position.SelectedItems.Count - 1].Index;
                Scrollindex = TmpIndex - lv_Level_Position.TopItem.Index;

                if (Scrollindex <= 0)
                {
                    Scrollindex = 1;
                }


                if (TmpIndex < (lv_Level_Position.Items.Count - 1))
                {
                    lv_Level_Position.SelectedItems.Clear();
                    lv_Level_Position.Items[TmpIndex + 1].Selected = true;

                    if ((TmpIndex + 1 - Scrollindex) >= 0)
                    {
                        lv_Level_Position.TopItem = lv_Level_Position.Items[TmpIndex + 1 - Scrollindex];
                    }
                    else
                    {

                    }

                    lv_LevelPosition_Item = lv_Level_Position.Items[TmpIndex + 1];

                    Process_Level_Position_TextBox();

                    if (lv_Level_Position.SelectedItems.Count == 1)
                    {
                        for (byte i = 0; i <= (lv_Level_Position.Columns.Count - 1); i++)
                        {
                            if (Level_Position_TextBox[i] != null)
                            {
                                if (Convert.ToByte(Level_Position_TextBox[i].Tag.ToString()) != 1)
                                {
                                    Level_Position_TextBox[i].Visible = true;
                                    Level_Position_TextBox[i].BringToFront();
                                    if (Level_Position_TextBox[i].Name == ed.Name)
                                    {
                                        Level_Position_TextBox[i].Focus();
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }

        private void ed_Level_Position_L_VisibleChanged(object sender, EventArgs e)
        {
            if (lv_LevelPosition_Item != null)
            {
                TextBox TmpTextBox = (TextBox)sender;

                for (byte i = 0; i <= (lv_Level_Position.Columns.Count - 1); i++)
                {
                    if (Level_Position_TextBox[i] != null)
                    {
                        if (TmpTextBox == Level_Position_TextBox[i])
                        {
                            if (!TmpTextBox.Visible)
                            {
                                lv_LevelPosition_Item.SubItems[i].Text = Level_Position_TextBox[i].Text;
                            }
                        }
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBayLPosition_Click(object sender, EventArgs e)
        {
            UInt32 TmpValue;

            if (lbPositionValues.Items.Count >= lv_Bay_Position.Items.Count)
            {
                for (int i=0; i< lv_Bay_Position.Items.Count; i++)
                {
                    TmpValue = Global_Class.UTIL_StrToUInt32Def(lbPositionValues.Items[i].ToString(), 0);
                    if ((sender as Button).Tag.ToString() == "1")
                    {
                        lv_Bay_Position.Items[i].SubItems[1].Text = TmpValue.ToString();
                    } else if ((sender as Button).Tag.ToString() == "2")
                    {
                        lv_Bay_Position.Items[i].SubItems[2].Text = TmpValue.ToString();
                    }
                }
            } else
            {
                form_Main.GlobalObj.MsgBox_Info("위치값 갯수가 Bay Count와 일치하지 않습니다.", "W");
                return;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                lbPositionValues.Items.Clear();
                // Getting Text from Clip board
                string s = Clipboard.GetText();
                //Parsing criteria: New Line
                string[] lines = s.Split('\n');
                foreach (string ln in lines)
                {
                    lbPositionValues.Items.Add(ln.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLevelLPosition_Click(object sender, EventArgs e)
        {
            UInt32 TmpValue;

            if (lbPositionValues.Items.Count >= lv_Level_Position.Items.Count)
            {
                for (int i = 0; i < lv_Level_Position.Items.Count; i++)
                {
                    TmpValue = Global_Class.UTIL_StrToUInt32Def(lbPositionValues.Items[i].ToString(), 0);
                    if ((sender as Button).Tag.ToString() == "1")
                    {
                        lv_Level_Position.Items[i].SubItems[1].Text = TmpValue.ToString();
                    }
                    else if ((sender as Button).Tag.ToString() == "2")
                    {
                        lv_Level_Position.Items[i].SubItems[2].Text = TmpValue.ToString();
                    }
                }
            }
            else
            {
                form_Main.GlobalObj.MsgBox_Info("위치값 갯수가 Level Count와 일치하지 않습니다.", "W");
                return;
            }
        }

        private void Form_SRMRack_FormClosing(object sender, FormClosingEventArgs e)
        {
            //form_Main.COMMDataManager.ISPolingStop = false;
        }
    }
}
