namespace VEXI
{
    public static class ClientApp
    {
#if VEXI_CLIENT_RTV
        public const byte FixedDeviceType = ConstClass.TYPE_RTV;
        public const string FixedDeviceTypeName = "RTV";
#elif VEXI_CLIENT_EMS
        public const byte FixedDeviceType = ConstClass.TYPE_EMS;
        public const string FixedDeviceTypeName = "EMS";
#else
        public const byte FixedDeviceType = 0;
        public const string FixedDeviceTypeName = null;
#endif
        public static bool IsDeviceTypeLocked
        {
            get
            {
#if VEXI_CLIENT_RTV || VEXI_CLIENT_EMS
                return true;
#else
                return false;
#endif
            }
        }
    }
}