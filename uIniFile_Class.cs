using System;
using System.Runtime.InteropServices;   //DllImport
using System.Text;


//[IniControl]
//ini 파일 핸들링 클래스
//Static 클래스로 객체 생성없이 사용

namespace VEXI
{
    public static class IniControl
    {
        //------------------------------------------------------------------------------
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern bool WritePrivateProfileStringW(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern uint GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        //------------------------------------------------------------------------------
        public static int ReadInteger(string targetIniFile, string IpAppName, string IpKeyName, int Default)
        {
            try
            {
                string inifile = targetIniFile;    //Path + File

                StringBuilder result = new StringBuilder(255);
                IniControl.GetPrivateProfileString(IpAppName, IpKeyName, "error", result, 255, inifile);

                
                if (result.ToString() == "error")
                {
                    return Default;
                }
                else
                {
                    return Convert.ToInt32(result.ToString());
                }
            }
            catch
            {
                return Default;
            }
        }

        //------------------------------------------------------------------------------
        public static Boolean ReadBool(string targetIniFile, string IpAppName, string IpKeyName)
        {
            string inifile = targetIniFile;    //Path + File
            StringBuilder result = new StringBuilder(255);
            IniControl.GetPrivateProfileString(IpAppName, IpKeyName, "error", result, 255, inifile);

            if (result.ToString() == "True" || result.ToString() == "1")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //------------------------------------------------------------------------------
        public static string ReadString(string targetIniFile, string IpAppName, string IpKeyName, string Default)
        {
            string inifile = targetIniFile;    //Path + File

            StringBuilder result = new StringBuilder(255);
            IniControl.GetPrivateProfileString(IpAppName, IpKeyName, "error", result, 255, inifile);

            if (result.ToString() == "error")
            {
                return Default;
            }
            else
            {
                return result.ToString();
            }

        }

        public static Boolean WriteIni(string targetIniFile, string IpAppName, string IpKeyName, string IpValue)
        {
            string inifile = targetIniFile;    //Path + File

            IniControl.WritePrivateProfileString(IpAppName, IpKeyName, IpValue, inifile);

            return true;
        }

        public static Boolean WriteIni(string targetIniFile, string IpAppName, string IpKeyName, int IpValue)
        {
            string inifile = targetIniFile;    //Path + File

            IniControl.WritePrivateProfileString(IpAppName, IpKeyName, IpValue.ToString(), inifile);

            return true;
        }



    }

}
