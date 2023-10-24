using MyApi.Debugging;

namespace MyApi
{
    public class MyApiConsts
    {
        public const string LocalizationSourceName = "MyApi";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "80e7612ade154e1487a4f74a466cb5de";
    }
}
