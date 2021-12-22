using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Chrome;
using VirtualDevice.Configuration;
using VirtualDevice.Pages;

namespace VirtualDevice.Tests
{
    public abstract class BaseTest
    {
        private AppiumOptions options;
        private ChromeOptions co;
        private AndroidDriver<AndroidElement> driver;
        private Page page;
        private Random random;
        protected Page Page => page ?? (page = new Page(
                driver: driver));

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            random = new Random();
            options = new AppiumOptions();
            co = new ChromeOptions();
            if (BuildConfigurator.Read("Device").ToLower().Equals("fake"))
            {
                options.AddAdditionalCapability(MobileCapabilityType.DeviceName, BuildConfigurator.Read("DeviceName"));
            }
            else if (BuildConfigurator.Read("Device").ToLower().Equals("real"))
            {
                options.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Android Device");
            }
            else
            {
                page.FailIt("Unknown device", new ArgumentException("Unknown device"));
            }


            options.AddAdditionalCapability(MobileCapabilityType.App, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "APK", $"{BuildConfigurator.Read("ApkName")}.apk"));
            options.AddAdditionalCapability(MobileCapabilityType.AutomationName, "uiautomator2");
            options.AddAdditionalCapability("chromedriverExecutable", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "chromedriver.exe"));

            driver = new AndroidDriver<AndroidElement>(new Uri(BuildConfigurator.Read("Url")), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            if (driver != null)
            {
                driver.CloseApp();
                driver.Quit();
            }
        }

        [TearDown]
        public void TearDown()
        {
            driver.ResetApp();
        }

        protected string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
