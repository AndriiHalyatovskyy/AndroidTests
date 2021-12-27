using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ClassLibrary1.Utils;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using VirtualDevice.Configuration;
using VirtualDevice.Pages;

namespace VirtualDevice.Tests
{
    public abstract class BaseTest : AppiumUtils
    {
        private AppiumOptions options;
        private AndroidDriver<AndroidElement> driver;
        private Page page;
        private Random random;
        protected Page Page => page ?? (page = new Page(
                driver: driver));

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            StartServer();
            random = new Random();
            options = new AppiumOptions();

            ConfigureDevice(options);
            options.AddAdditionalCapability(MobileCapabilityType.App, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "APK", $"{BuildConfigurator.Read("ApkName")}.apk"));
            options.AddAdditionalCapability(MobileCapabilityType.AutomationName, "uiautomator2");
            options.AddAdditionalCapability("chromedriverExecutable", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "chromedriver.exe"));

            driver = new AndroidDriver<AndroidElement>(new Uri(BuildConfigurator.Read("Url")), options);
            //var cmdstr = $"adb -s {BuildConfigurator.Read("DeviceName").Replace(" ", "_")} emu kill";
            //Process.Start("CMD.exe", cmdstr);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            StopServer();
            if (driver != null)
            {
                driver.CloseApp();
                driver.Quit();
            }
        }

        [TearDown]
        public void TearDown()
        {
            TakeScreenShot();
            driver.ResetApp();
        }

        protected string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
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
