﻿using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using VirtualDevice.Configuration;

namespace ClassLibrary1.Utils
{
    public abstract class AppiumUtils
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
            if (BuildConfigurator.Read("Device").ToLower().Equals("fake"))
            {
                options.AddAdditionalCapability(MobileCapabilityType.DeviceName, BuildConfigurator.Read("DeviceName"));
                options.AddAdditionalCapability("avd", BuildConfigurator.Read("DeviceName").Replace(" ", "_"));
            }
            else if (BuildConfigurator.Read("Device").ToLower().Equals("real"))
            {
                options.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Android Device");
            }
        }

        private void IsServerRunning(int port)
        {
            //IPEndPoint socket = new IPEndPoint(port);
        }

    }
}