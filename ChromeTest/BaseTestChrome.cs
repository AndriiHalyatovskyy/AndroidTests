using System;
using System.Diagnostics;
using System.IO;
using ClassLibrary1.Utils;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using VirtualDevice.Configuration;
using VirtualDevice.Pages;

namespace VirtualDevice

{
    public class BaseTestChrome : AppiumUtils
    {
        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;
        private Page page;
        private Stopwatch stopwatchTest, stopwatchClass;

        protected Page Page => page ?? (page = new Page(
                driver: driver));

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            stopwatchTest = new Stopwatch();
            stopwatchClass = new Stopwatch();
            stopwatchClass.Start();
            StartServer();
            options = new AppiumOptions();

            ConfigureDevice(options);
            options.AddAdditionalCapability("chromedriverExecutable", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "chromedriver.exe"));
            options.AddAdditionalCapability(MobileCapabilityType.App, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "APK", $"{BuildConfigurator.Read("Chrome")}.apk"));
            options.AddAdditionalCapability(MobileCapabilityType.BrowserName, "Chrome");

            driver = new AndroidDriver<AndroidElement>(new Uri(BuildConfigurator.Read("Url")), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            stopwatchClass.Stop();
            Console.WriteLine($"Class run time in seconds = {TimeSpan.FromMilliseconds(stopwatchClass.ElapsedMilliseconds).TotalSeconds}");
            stopwatchClass.Reset();
            StopServer();
        }

        [SetUp]
        public void SetUp()
        {
            stopwatchTest.Start();
        }

        [TearDown]
        public void TearDown()
        {
            stopwatchTest.Stop();
            Console.WriteLine($"Test run time in seconds = {TimeSpan.FromMilliseconds(stopwatchTest.ElapsedMilliseconds).TotalSeconds}");
            stopwatchTest.Reset();
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
