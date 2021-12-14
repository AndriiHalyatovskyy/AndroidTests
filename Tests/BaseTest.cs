using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using VirtualDevice.Configuration;
using VirtualDevice.Pages;

namespace VirtualDevice.Tests
{
    public abstract class BaseTest
    {
        private AppiumOptions options;
        private AndroidDriver<AndroidElement> driver;
        private Page page;
        private Random random;
        protected Page Page => page ?? (page = new Page(
                driver: driver));

        [OneTimeSetUp]
        public void OneTimeSetUpTest()
        {
            random = new Random();
            options = new AppiumOptions();
            options.AddAdditionalCapability(MobileCapabilityType.DeviceName, BuildConfigurator.Read("DeviceName"));
           // options.AddAdditionalCapability(MobileCapabilityType.App, Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase), "APK", BuildConfigurator.Read("ApkName")));
            options.AddAdditionalCapability(MobileCapabilityType.App, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "APK", BuildConfigurator.Read("ApkName")));
            options.AddAdditionalCapability(MobileCapabilityType.AutomationName, "uiautomator2");
            driver = new AndroidDriver<AndroidElement>(new Uri(BuildConfigurator.Read("Url")) , options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        }

        [OneTimeTearDown]
        public void OneTimeTearDownTest()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }

        protected string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
