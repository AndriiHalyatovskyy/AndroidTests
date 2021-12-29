using System;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using VirtualDevice.Configuration;

namespace ClassLibrary1.Utils
{
    public abstract class TestUtils
    {
        private AppiumLocalService service;
        /// <summary>
        /// Starts appium server
        /// </summary>
        private protected void StartServer()
        {
            service = AppiumLocalService.BuildDefaultService();

            if (!service.IsRunning)
                service.Start();
        }

        /// <summary>
        /// Stops appium server
        /// </summary>
        private protected void StopServer()
        {
            if (service != null && service.IsRunning)
            {
                service.Dispose();
                service = null;
            }
        }

        /// <summary>
        /// Read device
        /// </summary>
        private protected void ConfigureDevice(AppiumOptions options)
        {
           
            var device = TestContext.Parameters.Get("device");
            switch (device.ToLower())
            {
                case "fake":
                    options.AddAdditionalCapability(MobileCapabilityType.DeviceName, BuildConfigurator.Read("DeviceName"));
                    options.AddAdditionalCapability("avd", BuildConfigurator.Read("DeviceName").Replace(" ", "_"));
                    break;

                case "real":
                    options.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Android Device");
                    break;

                default:
                    throw new ArgumentException($"Unknown device {device}");

            }
        }

        /// <summary>
        /// Set name to output log file
        /// </summary>
        /// <param name="fileName">Name of file</param>
        private protected void SetOutputLogFileName(string fileName)
        {
            var q = LoggerManager.GetAllRepositories()[0].GetAppenders();
            foreach (FileAppender appender in LoggerManager.GetAllRepositories()[0].GetAppenders())
            {
                appender.File = $"Logs\\{fileName}_{DateTime.Now:dd.MM.YYYY}.log";
                appender.ActivateOptions();
            }
        }

        /// <summary>
        /// Initialized logger settings
        /// </summary>
        private protected void InitLogger()
        {
            XmlConfigurator.Configure();
        }

    }
}
