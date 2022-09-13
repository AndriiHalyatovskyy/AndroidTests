using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ClassLibrary1.Logging;
using ClassLibrary1.Utils;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using VirtualDevice.Configuration;
using VirtualDevice.Pages;

namespace VirtualDevice.Tests
{
    public abstract class DemoBaseTest : TestUtils
    {
        private AppiumOptions options;
        private AndroidDriver<AndroidElement> driver;
        private Page page;
        private Random random;
        private Stopwatch stopwatchTest, stopwatchClass;
        protected Page Page => page ?? (page = new Page(
                driver: driver));

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            InitLogger();
            SetOutputLogFileName($"{DateTime.UtcNow:dd.MM.yyyy}_{TestContext.CurrentContext.Test.ClassName}");
            StartServer();
            random = new Random();
            options = new AppiumOptions();
            stopwatchTest = new Stopwatch();
            stopwatchClass = new Stopwatch();
            stopwatchClass.Start();

            ConfigureDevice(options);

            driver = new AndroidDriver<AndroidElement>(new Uri(BuildConfigurator.Read("Url")), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            stopwatchClass.Stop();
            Logger.GetLogger.Info($"Class {TestContext.CurrentContext.Test.ClassName} finished in {TimeSpan.FromMilliseconds(stopwatchClass.ElapsedMilliseconds).TotalSeconds} seconds");
            stopwatchClass.Reset();
            StopServer();
            if (driver != null)
            {
                driver.Quit();
            }
        }

        [SetUp]
        public void SetUp()
        {
            Logger.GetLogger.Info($"Test {TestContext.CurrentContext.Test.MethodName} started from {TestContext.CurrentContext.Test.ClassName}");
            stopwatchTest.Start();
        }

        [TearDown]
        public void TearDown()
        {
            stopwatchTest.Stop();
            Logger.GetLogger.Info($"Test {TestContext.CurrentContext.Test.MethodName} finished in {TimeSpan.FromMilliseconds(stopwatchTest.ElapsedMilliseconds).TotalSeconds}");
            stopwatchTest.Reset();
            EndTest();
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
        private void EndTest()
        {
            var directory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent;
            var folder = Path.Combine(directory.FullName, "ScreenShots");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                Logger.GetLogger.Error($"Test {TestContext.CurrentContext.Test.MethodName} failed. With message: {TestContext.CurrentContext.Result.Message}");
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(Path.Combine(folder, $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now.ToString("dd.MM.yyyy")}.png"), ScreenshotImageFormat.Png);
            }
        }
    }
}
