using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;   //DllImport
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Concurrent; // ConcurrentDictionary
using System.Threading;
using System.Threading.Tasks;
namespace VEXI
{
    public partial class Form_FunctionTest : Form
    {
        public Form_Main form_Main;
        private static VEXI_DEFS.TDEV_REC_BasicCtrl dev_REC_BasicCtrl;
        StringBuilder sb_Data = new StringBuilder();
        StringBuilder sb_packet = new StringBuilder();
        List<TPacketClass> input = new List<TPacketClass>();
        List<TPacketClass> output = new List<TPacketClass>();

        

        private byte Testbyte;

        public Form_FunctionTest()
        {
            InitializeComponent();
        }

        #region TEST 코드
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Text = Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_TestStRes)).ToString();
        }



        private void button11_Click_1(object sender, EventArgs e)
        {
            if (form_Main != null)
            {
                form_Main.Debug_Insert("TEST");
            }
            IniControl.WriteIni(Application.StartupPath + "\\CONFIG\\Setup.ini", "TEST", "test1", 999);
            IniControl.WriteIni(Application.StartupPath + "\\CONFIG\\Setup.ini", "TEST", "test2", "안녕하세요");
            int i = IniControl.ReadInteger(Application.StartupPath + "\\CONFIG\\Setup.ini", "TEST", "test1", 0);
            button11.Text = i.ToString();
        }


        private void button13_Click(object sender, EventArgs e)
        {
            Test_strToByte();
            Test_ByteToPVersion();
        }


        private void button9_Click(object sender, EventArgs e)
        {
            if (form_Main != null)
            {
                byte readresult;
                byte readValue;
                ushort count;
                ushort i;
                for (byte TmpCommID = ConstClass.COMM_SERIAL; TmpCommID <= ConstClass.COMM_TEST; TmpCommID++)
                {
                    readresult = 0;
                    readValue = 0;
                    count = form_Main.COMMDataManager.ReadDataCount(TmpCommID);
                    if (count > 0)
                    {
                        for (i = 0; i < count; i++)
                        {
                            readresult = form_Main.COMMDataManager.Readbyte(TmpCommID, ref readValue);
                            if (readresult > 0)
                            {
                                listBox1.Items.Add(readValue.ToString("X2"));
                            }
                            else
                            {
                                listBox1.Items.Add("이상");
                            }
                        }
                    }

                }
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("---TEST_TPacketRecToBytes_SendSerial---");
            TEST_TPacketRecToBytes_SendSerial();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("---Test_List_ADD---");
            Test_List_ADD();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("---Test_List_ItemValueChange---");
            Test_List_ItemValueChange();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Test_Readbinary_fromFile();
        }


        private unsafe void button14_Click(object sender, EventArgs e)
        {
            //GUILogManager LogList = new GUILogManager(Application.StartupPath);
            //GUILogManager.GUILOGRec TestLogItem;

            //for (int i = 1; i <= 10; i++)
            //{
            //    TestLogItem.LogTime = DateTime.Now;
            //    Global_Class.UTIL_Byteptr_clear((byte*)TestLogItem.Data, 100);
            //    TestLogItem.Data[0] = (byte)i;
            //    LogList.ADDLog(TestLogItem);
            //}
            //LogList.SaveToFile();
            //LogList = null;

            //GUILogManager LogList2 = new GUILogManager(Application.StartupPath);
            //GUILogManager.GUILOGRec SearchLogItem = new GUILogManager.GUILOGRec();
            //LogList2.LoadFromFile(DateTime.Now);

            //for (int i = 0; i < LogList2.SearchListCount; i++)
            //{
            //    if (LogList2.SearchLogItem(i, ref SearchLogItem))
            //    {
            //        listBox1.Items.Add(SearchLogItem.LogTime.ToString("yyyyMMdd hh:mm:ss") + " " + SearchLogItem.Data[0].ToString());
            //    }
            //}
            //LogList2 = null;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Add("---Test_StructureToByte---");
            Test_StructureToByte();
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Add("---TEST_COMMDataManager_Serial_Send---");
            TEST_COMMDataManager_Serial_Send();
        }

        private void Test_Readbinary_fromFile()
        {
            openFileDialog1.Filter = "*.bin|*.BIN";
            openFileDialog1.InitialDirectory = Application.StartupPath;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Test_ReadbinaryFromFile(openFileDialog1.FileName);
            }
        }

        private void Test_ReadbinaryFromFile(string tmpfileName)
        {
            using (BinaryReader br = new BinaryReader(File.Open(tmpfileName, FileMode.Open, FileAccess.Read)))
            {


                string TmpStr = "";
                try
                {
                    long dataLength = br.BaseStream.Length;

                    byte[] readData;

                    while (br.BaseStream.Position < dataLength)
                    {
                        readData = br.ReadBytes(10);

                        TmpStr = "";
                        for (int i = 0; i < readData.Length; i++)
                        {
                            TmpStr = TmpStr + ' ' + string.Format("{0:X2}", readData[i]);
                        }
                        listBox1.Items.Add(TmpStr);

                    }
                }
                finally
                {
                    br.Close();
                }
            }
        }

        private void TEST_Display_bytes(byte[] Tmpdatas, int len = 0)
        {
            sb_packet.Clear();
            int tmpLen;
            if (len == 0)
            {
                tmpLen = Tmpdatas.Length;
            }
            else
            {
                tmpLen = len;
            }
            for (int i = 0; i < tmpLen; i++)
            {
                sb_packet.Append(string.Format("{0:X2} ", Tmpdatas[i]));

                if (((i + 1) % 16) == 0)
                {
                    listBox1.Items.Add("");
                    listBox1.Items.Add(sb_packet.ToString());
                    sb_packet.Clear();
                }
            }
            if (sb_packet.Length != 0)
            {
                listBox1.Items.Add("");
                listBox1.Items.Add(sb_packet.ToString());
            }
        }


        private void Test_strToByte()
        {
                byte[] a = new byte[5];
                string b = "12345";
                Global_Class.UTIL_StrToByteArray(b, a, 0);
            TEST_Display_bytes(a);

                char c = '나';
                byte len = 0;
                a = Global_Class.UTIL_CharToUniCodeBytes(c, 0, ref len);
            TEST_Display_bytes(a, len);

                a = Global_Class.UTIL_CharToDefaultBytes(c, 0, ref len);
            TEST_Display_bytes(a, len);

                a = Global_Class.UTIL_CharToUTF8Bytes(c, 0, ref len);
            TEST_Display_bytes(a, len);


                byte[] m = new byte[4];
                m[0] = 0x00;
                m[1] = 0xAC;
                m[2] = 0x98;
                m[3] = 0xB0;
                listBox1.Items.Add("Unicode " + Global_Class.UTIL_UnicodeStringOfBytes(m));

                byte[] n = new byte[4];
                n[0] = 0xB0;
                n[1] = 0xA1;
                n[2] = 0xB3;
                n[3] = 0xAA;
                listBox1.Items.Add("Default " + Global_Class.UTIL_DefaultStringOfBytes(n));

                byte[] k = new byte[6];
                k[0] = 0xEA;
                k[1] = 0xB0;
                k[2] = 0x80;
                k[3] = 0xEB;
                k[4] = 0x82;
                k[5] = 0x98;
                listBox1.Items.Add("UTF8 " + Global_Class.UTIL_GetUTF8StringOfBytes(k));

        }

        private void Test_ByteToPVersion()
        {
            byte a = 0xEF;
            button13.Text = Global_Class.UTIL_ByteToPVerstr(a);
        }

        private void Test_List_ADD()
        {
            byte[] Srcbytes = { 0x16, 0x16, 0x16, 0x16, 0x11, 0x12, 0x13, 0x14, 0x00, 0x00, 0x00, 0x30, 0x06, 0x00, 0x31, 0x99, 0x99, 0x99, 0x99, 0x01, 0x23, 0x80, 0xF5 };
            byte[] Srcbytes2 = { 0x16, 0x16, 0x16, 0x16, 0x11, 0x12, 0x13, 0x14, 0x00, 0x00, 0x00, 0x30, 0x06, 0x00, 0x31, 0x99, 0x99, 0x99, 0x99, 0x02, 0x23, 0x80, 0xF5 };

            //TPacketClass TmpObj = new TPacketClass(Srcbytes);
            //input.Add(TmpObj);

            //TmpObj.SetTotalbytes(Srcbytes2);
            //input.Add(TmpObj);

            input.Add(new TPacketClass(Srcbytes));
            input.Add(new TPacketClass(Srcbytes2));
        }

        private void Test_List_ItemValueChange()
        {
            byte[] Srcbytes3 = { 0x16, 0x16, 0x16, 0x16, 0x11, 0x12, 0x13, 0x14, 0x00, 0x00, 0x00, 0x30, 0x06, 0x00, 0x31, 0x99, 0x99, 0x99, 0x99, 0x03, 0x23, 0x80, 0xF5 };
            input[0].SetTotalbytes(Srcbytes3);


            byte[] bytes = input[0].GetTotalBytes();
            string TmpStr = "";

            for (int i = 0; i < bytes.Length; i++)
            {
                TmpStr = TmpStr + string.Format(" {0:X2}", bytes[i]);
            }

            TmpStr = TmpStr + '/';

            bytes = input[1].GetTotalBytes();
            for (int i = 0; i < bytes.Length; i++)
            {
                TmpStr = TmpStr + string.Format(" {0:X2}", bytes[i]);
            }

            listBox1.Items.Add(TmpStr);
        }

        private void Test_StructureToByte()
        {
            if (form_Main != null)
            {
                byte[] Viewbytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_BasicStRes))];
                Global_Class.UTIL_StructObjectToByteArray(form_Main.COMMDataManager.DevRec.dev_REC_BasicSt, Viewbytes);
                TEST_Display_bytes(Viewbytes);
            }
        }

        public unsafe void TEST_Clear_stc_REC_BasicCtrl()
        {
            
            fixed (byte* pb = &dev_REC_BasicCtrl.CtrlFlag[0])
            {
                for (int i = 0; i < Marshal.SizeOf(dev_REC_BasicCtrl); i++)
                {
                    *(pb + i) = 0x00;

                }
            }
        }

        private unsafe void TEST_COMMDataManager_Serial_Send()
        {
            if (form_Main != null)
            {

                dev_REC_BasicCtrl.CtrlFlag[0] = 0xFF;
                TEST_Clear_stc_REC_BasicCtrl();
                form_Main.COMMDataManager.Serial_Send(0x15, 0x16, dev_REC_BasicCtrl);
            }
        }
        private void TEST_TPacketRecToBytes_SendSerial()
        {
            if (form_Main != null)
            {
                TPacketRec TestPacketStrunct = new TPacketRec();

                TestPacketStrunct.fpacketHeader.StartCode = new byte[4];

                TestPacketStrunct.fpacketHeader.StartCode[0] = 0x91;
                TestPacketStrunct.fpacketHeader.StartCode[1] = 0x92;
                TestPacketStrunct.fpacketHeader.StartCode[2] = 0x93;
                TestPacketStrunct.fpacketHeader.StartCode[3] = 0x94;

                TestPacketStrunct.fpacketHeader.SrcType = 0x11;
                TestPacketStrunct.fpacketHeader.SrcID = 0x12;
                TestPacketStrunct.fpacketHeader.DstType = 0x13;
                TestPacketStrunct.fpacketHeader.DstID = 0x14;

                TestPacketStrunct.fpacketHeader.Seq = 0x00;
                TestPacketStrunct.fpacketHeader.bypass1 = 0x00;
                TestPacketStrunct.fpacketHeader.bypass2 = 0x00;
                TestPacketStrunct.fpacketHeader.CMD1 = 0x30;
                TestPacketStrunct.fpacketHeader.Len = 0x0006;
                TestPacketStrunct.fpacketHeader.CMD2 = 0x31;
                TestPacketStrunct.fpacketBody.Data = new byte[5];
                //TestPacketStrunct.fpacketBody.Data = new byte[ConstClass.U_PACKET_DATAMAX_SIZE - 1];
                TestPacketStrunct.fpacketBody.Data[0] = 0x11;
                TestPacketStrunct.fpacketBody.Data[1] = 0x12;
                TestPacketStrunct.fpacketBody.Data[2] = 0x13;
                TestPacketStrunct.fpacketBody.Data[3] = 0x14;
                TestPacketStrunct.fpacketBody.Data[4] = 0x15;

                TestPacketStrunct.fpacketFooter.CRC = 0x0000;
                TestPacketStrunct.fpacketFooter.ETX = 0xF5;

                //21
                int len_Header = Marshal.SizeOf(typeof(TPacketHeaderRec));
                int len_Body = TestPacketStrunct.fpacketHeader.Len - 1;//TestPacketStrunct.fpacketBody.Data.Length;
                int len_Footer = Marshal.SizeOf(typeof(TPacketFooterRec));
                int len_Total = len_Header + len_Body + len_Footer;

                byte[] bytes = new byte[len_Total];


                IntPtr buff;

                //MarshalAs
                buff = Marshal.AllocHGlobal(len_Header);
                Marshal.StructureToPtr(TestPacketStrunct.fpacketHeader, buff, false);
                Marshal.Copy(buff, bytes, 0, len_Header);
                Marshal.FreeHGlobal(buff);

                //MarshalAs No
                Array.Copy(TestPacketStrunct.fpacketBody.Data, 0, bytes, len_Header, len_Body);


                buff = Marshal.AllocHGlobal(len_Footer);
                Marshal.StructureToPtr(TestPacketStrunct.fpacketFooter, buff, false);
                Marshal.Copy(buff, bytes, len_Header + len_Body, len_Footer);
                Marshal.FreeHGlobal(buff);


                TPacketClass TmpPacketObj = new TPacketClass(bytes);
                bytes = TmpPacketObj.GetTotalBytes();

                form_Main.COMMDataManager.Serial_Send(bytes, (ushort)bytes.Length);

                string TmpStr = "";

                if (listBox1.Items.Count > 100)
                {
                    listBox1.Items.Clear();
                }

                for (int i = 0; i < bytes.Length; i++)
                {
                    TmpStr = TmpStr + string.Format(" {0:X2}", bytes[i]);
                }
                listBox1.Items.Add("TX " + TmpStr);
            }
        }

        #endregion

        private unsafe void button5_Click(object sender, EventArgs e)
        {
            fixed (VEXI_DEFS.TDEV_REC_BasicStRes* DevSt = &form_Main.COMMDataManager.DevRec.dev_REC_BasicSt)
            {

                DevSt->ProjectID[0] = 0x66;
                DevSt->ProjectID[1] = 0x31;
                DevSt->ProjectID[2] = 0x32;
                DevSt->ProjectID[3] = 0x33;
                DevSt->ProjectID[4] = 0x34;
                DevSt->ProjectID[5] = 0x35;
                DevSt->GroupID = 8;
                DevSt->HogiID = 999;
                DevSt->ModeSwitch = 0x0F;
                DevSt->IDSwitch = 0x89;

                byte[] convertedArray = new byte[6];
                //System.Runtime.InteropServices.Marshal.Copy(new IntPtr(DevSt->ProjectID), convertedArray, 0, 6);
                System.Runtime.InteropServices.Marshal.Copy((IntPtr)DevSt->ProjectID, convertedArray, 0, 6);
                label1.Text = System.Text.Encoding.ASCII.GetString(convertedArray);

                label1.Text = label1.Text + " " + string.Format("{0}", DevSt->GroupID);
                label1.Text = label1.Text + " " + string.Format("{0}", DevSt->HogiID);


            }
        }

        private byte GetTestbyte
        {
            get { return Testbyte++; }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            byte i;// = (byte)Convert.ToSByte("-1");


            i = GetTestbyte;
            button6.Text = i.ToString() + " " + Testbyte.ToString();


        }

        private unsafe void button12_Click(object sender, EventArgs e)
        {
            VEXI_DEFS.TLogUnionRec LogUnion = new VEXI_DEFS.TLogUnionRec();

            
            Global_Class.UTIL_Byteptr_LinearTest((byte*)&LogUnion.LogSRM01Rec.Log.ItemCell.Station, Marshal.SizeOf(typeof(VEXI_DEFS.TLogUnionRec)));

            label1.Text = string.Format("{0:X2} {1:X2} {2:X2} {3:X2} {4:X2} {5:X2} {6:X2} {7:X2}",
                          LogUnion.LogRec_30.Logvalue_0,
                          LogUnion.LogRec_30.Logvalue_1,
                          LogUnion.LogRec_30.Logvalue_2,
                          LogUnion.LogRec_30.Logvalue_3,
                          LogUnion.LogRec_30.Logvalue_4,
                          LogUnion.LogRec_30.Logvalue_5,
                          LogUnion.LogRec_30.Logvalue_6,
                          LogUnion.LogRec_30.Logvalue_7);
        }

        class Base
        {
            public int MyNo = 0;

            public Base()
            {
                MyNo = 0;
            }

            public Base(int TmpNo)
            {
                MyNo = TmpNo;
            }
            public virtual void Init()
            {
                Console.WriteLine("Base Init");
            }

            public virtual void Start()
            {
                Console.WriteLine("Base Start");
            }

        }

        class SubBase : Base
        {
            public new void Init()
            {
                Console.WriteLine("SubBase Init");
            }

            public override void Start()
            {
                Console.WriteLine("SubBase Start");
            }
        }
        private void button15_Click(object sender, EventArgs e)
        {
            Base p1 = new Base();
            Console.WriteLine("---");
            p1.Init();
            p1.Start();

            Base p2 = new SubBase(); //업캐스팅
            Console.WriteLine("---");
            p2.Init();
            p2.Start();

            SubBase p3 = new SubBase();
            Console.WriteLine("---");
            p3.Init();
            p3.Start();

            //Base Init
            //Base Start
            //Base Init
            //SubBase Start
            //SubBase Init
            //SubBase Start
        }
        private static void PrintArray(System.Array array)
        {
            foreach (var e in array)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine();
        }

        private static void Print(int Value)
        {
            Console.WriteLine(Value);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            int base_num = 10;
            double base_Double = 10.1;
            Console.WriteLine("Power {0} of {1} : {2}", 2, base_num, base_num.Power(2));

            int[] scores = new int[30];
            Random rand = new Random();
            for (int i =0; i < scores.Length; i++)
            {
                scores[i] = rand.Next(0, 20);
            }

            //방법1
            Array.ForEach<int>(scores, new Action<int>(Print));
            Console.WriteLine();
            //방법2
            Action<int> action = new Action<int>(Print);
            Array.ForEach(scores, action);
            Console.WriteLine();
            //방법3
            PrintArray(scores);

            Array.Sort(scores, 0, 10);
            Array.ForEach<int>(scores, new Action<int>(Print));


            //FindIndex
            int index_Find = Array.FindIndex(scores, element => element == 18);
            Console.WriteLine("{0}", index_Find);
            index_Find = Array.FindLastIndex(scores, element => element == 18);
            Console.WriteLine("{0}", index_Find);

            //FindIndex는 클래스 배열에서도 사용 가능
            Base[] baseArray = { new Base(11), new Base(12), new Base(13),new Base(14), new Base(15) };
            index_Find = Array.FindIndex(baseArray, element => element.MyNo == 12);
            Console.WriteLine("{0}", index_Find);


            if (Array.TrueForAll(scores, element => element < 20))
            {
                Console.WriteLine("TRUE");
            }
            else
            {
                Console.WriteLine("FALSE");
            }

            if (Array.TrueForAll(scores, element => element < 10))
            {
                Console.WriteLine("TRUE");
            } else
            {
                Console.WriteLine("FALSE");
            }

            Array myIntArray = Array.CreateInstance(typeof(int), 5);

            myIntArray.SetValue(8, 0);
            myIntArray.SetValue(2, 1);
            myIntArray.SetValue(6, 2);
            myIntArray.SetValue(3, 3);
            myIntArray.SetValue(7, 4);

            //BinarySearch 메서드를 사용하려면 배열을 오름차순으로 정렬해야 함
            Array.Sort(myIntArray);
            object myObjectOdd = 3;
            int myIndex = Array.BinarySearch(myIntArray, myObjectOdd);
            if (myIndex < 0)
            {
                Console.WriteLine("The object to search for ({0}) is not found. The next larger object is at index {1}.", myObjectOdd, ~myIndex);
            }
            else
            {
                Console.WriteLine("The object to search for ({0}) is at index {1}.", myObjectOdd, myIndex);
            }

            //ArrayList
            ArrayList aList = new ArrayList() { baseArray[1], baseArray[2], baseArray[3] };
            aList.Add(baseArray[4]);
            aList.Insert(0, baseArray[0]);
            aList.RemoveAt(3);

            for (int j = 0; j < aList.Count; j++)
            {
                Console.WriteLine("{0}", (aList[j] as Base).MyNo);
            }

            //Queue
            Queue que = new Queue();
            que.Enqueue(1);
            que.Enqueue(2);
            que.Enqueue(3);
            que.Enqueue(4);
            que.Enqueue(5);
            que.Enqueue(6);

            var Quefront = que.Peek();
            while (que.Count > 0 )
            {
                Quefront = que.Dequeue();
                Console.WriteLine("{0}", Quefront);
            }

            Console.WriteLine("");
            //Stack
            Stack stack = new Stack();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);
            stack.Push(6);

            var Stackfront = stack.Peek();
            while (stack.Count > 0)
            {
                Stackfront = stack.Pop();
                Console.WriteLine("{0}", Stackfront);
            }

            //Hashtable
            Hashtable ht = new Hashtable();
            ht.Add("irina", "Irina SP");
            ht.Add("tom", "Tom Cr");
            ht.Add(1, "1111");

            if (ht.Contains("tom"))
            {
                Console.WriteLine(ht["tom"]);
            }

            if (ht.Contains(1))
            {
                Console.WriteLine(ht[1]);
            }

            //Dictionary
            Dictionary<int, string> emp = new Dictionary<int, string>();
            emp.Add(1001, "Jane");
            emp.Add(1002, "Tom");
            emp.Add(1003, "Cindy");

            string name = emp[1002];
            Console.WriteLine(name);

            
        }

        private void button17_Click(object sender, EventArgs e)
        {
            //ConcurrentDictionary
            var dict = new ConcurrentDictionary<int, string>();

            Task t1 = Task.Factory.StartNew(() =>
            {
                int key = 1;
                while (key <= 20)
                {
                    if (dict.TryAdd(key, "D" + key))
                    {
                        Console.WriteLine("t1 {0},{1}", key, "D" + key);
                        key++;
                    }
                    Thread.Sleep(10);
                }
            });

            Task t2 = Task.Factory.StartNew(() =>
            {
                int key = 1;
                string val;
                while (key <= 20)
                {
                    //Console.WriteLine("t2 Total Count {0}", dict.Count);
                    if (dict.TryGetValue(key, out val))
                    {
                        Console.WriteLine("t2 {0},{1}", key, val);
                        key++;
                    }
                    else
                    {
                        if (key == dict.Count)
                        {

                            Console.WriteLine("**************** t2 {0},NoFound {1}", key, dict.Count);
                        }
                    }
                    Thread.Sleep(10);
                }
            });
            t1.Wait();
            Console.WriteLine("t1 Task Done");
            t2.Wait();
            Console.WriteLine("t2 Task Done");


            //Dictionary
            Dictionary<int, string> taskemp = new Dictionary<int, string>();

            Task t3 = Task.Factory.StartNew(() =>
            {
                int key = 1;
                while (key <= 20)
                {
                    taskemp.Add(key, "D" + key);
                    Console.WriteLine("t3 {0},{1}", key, "D" + key);
                    key++;
                    Thread.Sleep(10);
                }
            });

            Task t4 = Task.Factory.StartNew(() =>
            {
                int key = 1;

                while (key <= 20)
                {
                    //Console.WriteLine("t4 Total Count {0}", taskemp.Count);
                    if (taskemp.ContainsKey(key))
                    {
                        Console.WriteLine("t4 {0},{1}", key, taskemp[key]);
                        key++;
                    }
                    else
                    {
                        if (key == taskemp.Count)
                        {
                            Console.WriteLine("**************** t4 {0},NoFound {1}", key, taskemp.Count);
                        }

                        
                    }
                    Thread.Sleep(10);
                }
            });
            t3.Wait();
            Console.WriteLine("t3 Task Done");
            t4.Wait();
            Console.WriteLine("t4 Task Done");




        }

        Dictionary<int, string> _mydic = new Dictionary<int, string>();
        ConcurrentDictionary<int, string> _mydictConcu = new ConcurrentDictionary<int, string>();

        void InsertData1()
        {
            for (int i = 1; i <= 10; i++)
            {
                if (!_mydic.ContainsKey(i))
                {
                    _mydic.Add(i, "D1 " + (i));
                    _mydic.Add(100 * (i), "D1 " + (100 * (i)));
                }
                   
            }
        }

        void InsertData2()
        {
            for (int i = 11; i <= 20; i++)
            {
                if (!_mydic.ContainsKey(i))
                {
                    _mydic.Add(i, "D2 " + (i));
                    _mydic.Add(100 * (i), "D2 " + (100 * (i)));
                }

            }
        }

        void InsertData3()
        {
            for (int i = 21; i <= 30; i++)
            {
                if (!_mydic.ContainsKey(i))
                {
                    _mydic.Add(i, "D3 " + (i));
                    _mydic.Add(100 * (i), "D3 " + (100 * (i)));
                }

            }
        }

        void InsertDataConcu1()
        {
            string val;
            for (int i = 1; i <= 10; i++)
            {
                if (!_mydictConcu.TryGetValue(i, out val))
                {
                    _mydictConcu.TryAdd(i, "D1 " + (i));
                    _mydictConcu.TryAdd(100 * (i), "D1 " + (100 * (i)));
                }
            }
        }

        void InsertDataConcu2()
        {
            string val;
            for (int i = 11; i <= 20; i++)
            {
                if (!_mydictConcu.TryGetValue(i, out val))
                {
                    _mydictConcu.TryAdd(i, "D2 " + (i));
                    _mydictConcu.TryAdd(100 * (i), "D2 " + (100 * (i)));
                }
            }
        }

        void InsertDataConcu3()
        {
            string val;
            for (int i = 21; i <= 30; i++)
            {
                if (!_mydictConcu.TryGetValue(i, out val))
                {
                    _mydictConcu.TryAdd(i, "D3 " + (i));
                    _mydictConcu.TryAdd(100 * (i), "D3 " + (100 * (i)));
                }
            }
        }
        void CheckList()
        {
            foreach (KeyValuePair<int, string> entry in _mydic)
            {
                Console.WriteLine("_mydic Key: " + entry.Key + ", Value: " + entry.Value);
            }

            foreach (KeyValuePair<int, string> entry in _mydictConcu)
            {
                Console.WriteLine("_mydictConcu Key: " + entry.Key + ", Value: " + entry.Value);
            }

        }


        private void button18_Click(object sender, EventArgs e)
        {
            //Dictionary vs ConcurrentDictionary 차이는 다중쓰레드 사용시 드러난다
            Thread mythread1 = new Thread(new ThreadStart(InsertData1));
            Thread mythread2 = new Thread(new ThreadStart(InsertData2));
            Thread mythread3 = new Thread(new ThreadStart(InsertData3));
            mythread1.Start();
            mythread2.Start();
            mythread3.Start();



            Thread mythread11 = new Thread(new ThreadStart(InsertDataConcu1));
            Thread mythread21 = new Thread(new ThreadStart(InsertDataConcu2));
            Thread mythread31 = new Thread(new ThreadStart(InsertDataConcu3));

            mythread11.Start();
            mythread21.Start();
            mythread31.Start();



            mythread1.Join();
            mythread2.Join();
            mythread3.Join();
            mythread11.Join();
            mythread21.Join();
            mythread31.Join();

            CheckList();
            //Console.WriteLine(string.Format("Result in Dictionary : {0}", _mydic.Values.Count));
            //Console.WriteLine("********************************************");
            //Console.WriteLine(string.Format("Result in Concurrent Dictionary : {0}", _mydictConcu.Values.Count));
            //Console.ReadKey();
        }
    }

    //확장 메서드
    static class IntegerExtenction
    {
        public static int Power(this int Myint, int exponent)
        {
            int result = Myint;
            //for (int i = 1; i < exponent; ++i)
            for (int i = 1; i < exponent; i++)
            {
                result = result * Myint;

            }
            return result;
        }
    }
}
