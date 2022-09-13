using System;
using System.IO;
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

            var device = TestContext.Parameters?.Get("device");
            if (string.IsNullOrEmpty(device))
            {
                device = "fake";
            }
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

            options.AddAdditionalCapability(MobileCapabilityType.App, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "APK", $"{BuildConfigurator.Read("DemoApkName")}.apk"));
            options.AddAdditionalCapability(MobileCapabilityType.AutomationName, "uiautomator2");
            options.AddAdditionalCapability("chromedriverExecutable", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "chromedriver.exe"));
        }

        /// <summary>
        /// Checks if output folder exist, and create it if not
        /// </summary>
        /// <param name="folderPath">Path to the folder</param>
        private void CheckLogFolder(string folderPath)
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderPath);

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
        }

        /// <summary>
        /// Set name to output log file
        /// </summary>
        /// <param name="fileName">Name of file</param>
        private protected void SetOutputLogFileName(string fileName)
        {
            var folderPath = $"Logs\\{DateTime.Now:dd.MM.yyyy}\\";
            CheckLogFolder(folderPath);

            foreach (FileAppender appender in LoggerManager.GetAllRepositories()[0].GetAppenders())
            {
                appender.File = $"{folderPath}{fileName}.log";
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
