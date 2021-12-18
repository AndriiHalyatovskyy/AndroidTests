using System.Configuration;

namespace VirtualDevice.Configuration
{
    public static class BuildConfigurator
    {
        public static string Read(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key];
            }
            catch
            {
                throw new ConfigurationErrorsException("Incorrect argument key");
            }
        }
    }
}
