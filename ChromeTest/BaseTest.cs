using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using VirtualDevice.Configuration;
using VirtualDevice.Pages;

namespace ClassLibrary1.ChromeTest
{
    public class BaseTest
    {
        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;
        private Page page;

        protected Page Page => page ?? (page = new Page(
                driver: driver));

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            options = new AppiumOptions();

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

            options.AddAdditionalCapability("chromedriverExecutable", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "chromedriver.exe"));
            options.AddAdditionalCapability(MobileCapabilityType.BrowserName, "Chrome");

            driver = new AndroidDriver<AndroidElement>(new Uri(BuildConfigurator.Read("Url")), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        }

        [TearDown]
        public void TearDown()
        {
            TakeScreenShot();
        }

        /// <summary>
        /// Make a screenshot on test failure
        /// </summary>
        private void TakeScreenShot()
        {
            var directory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent;
            var folder = Path.Combine(directory.FullName, "ScreenShots");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(Path.Combine(folder, $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now.ToString("dd.MM.yyyy")}.png"), ScreenshotImageFormat.Png);
            }
        }
    }
}
