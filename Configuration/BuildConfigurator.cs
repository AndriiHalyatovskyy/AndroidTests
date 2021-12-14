using System.Configuration;

namespace VirtualDevice.Configuration
{
    public static class BuildConfigurator
    {
        public static string Read(string key) => ConfigurationManager.AppSettings[key];
    }
}
