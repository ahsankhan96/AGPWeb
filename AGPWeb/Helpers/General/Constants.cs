namespace AGPWeb.Helpers.General
{
    public class Constants
    {
        internal static string WebRootPath;
        public static string EncKey = "AaECAwQFBgcICQoLDA0ODw==";
        public static String LoginKey = "PleniageLogin";
        public static String JWTLoginKey = "DHPJWTLogin";
        public static string DateFormat = "yyyy-MM-dd";
        public static string TimeFormat = "HH:mm";
        public static string AppName = "Pleniage";

        public static string JWTKey = "1novo systems2@2lROCKS:bp%&1d&?$0'S5G=<^]7SOiczS#qq}><~";
        public static string JWTIssuer = "http://inovosystems.co.za";
        public static int JwtExpireDays = 40;
        public static string AppType = "MP3";//Reading

    }
    public enum UserStatusEnum
    {
        Pending_Approval,
        Rejected,
        Active,
        UnActive
    }
    public enum RoleEnum
    {
        Admin = 1
    }
}
