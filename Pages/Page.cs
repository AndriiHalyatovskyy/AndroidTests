using System;
using System.Collections.Generic;
using ClassLibrary1.Pages;
using ClassLibrary1.Pages.Common;
using ClassLibrary1.Pages.Preference;
using ClassLibrary1.Pages.Views;
using ClassLibrary1.Wrappers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Support.UI;

namespace VirtualDevice.Pages
{
    public class Page
    {
        private const int DEFAULT_WAIT = 20;

        public AndroidDriver<AndroidElement> driver;
        public TouchAction tActions;
        public WebDriverWait Wait;

        private MainPage mainPage;
        private PreferencePage preferencePage;
        private PreferenceDependenciesPage preferenceDependenciesPage;
        private TextPopup textPopupPage;
        private ViewsPage viewsPage;

        public Page(AndroidDriver<AndroidElement> driver)
        {
            this.driver = driver;
            tActions = new TouchAction(this.driver);
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(DEFAULT_WAIT));
        }

        public MainPage MainPage
        {
            get { return mainPage ?? (mainPage = new MainPage(this)); }
        }

        public PreferencePage PreferencePage
        {
            get { return preferencePage ?? (preferencePage = new PreferencePage(this)); }
        }

        public PreferenceDependenciesPage PreferenceDependenciesPage
        {
            get { return preferenceDependenciesPage ?? (preferenceDependenciesPage = new PreferenceDependenciesPage(this)); }
        }

        public TextPopup TextPopupPage
        {
            get { return textPopupPage ?? (textPopupPage = new TextPopup(this)); }
        }

        public ViewsPage ViewsPage
        {
            get { return viewsPage ?? (viewsPage = new ViewsPage(this)); }
        }

        /// <summary>
        /// Waits for element to be present and visible and then clicks it
        /// </summary>
        /// <param name="element"></param>
        /// <param name="scroll"></param>
        /// <param name="scrollDistance"></param>
        /// <param name="scrollToTop"></param>
        public AppiumWebElement Click(By element, /*ScrollOptions scroll = ScrollOptions.none,*/ int scrollDistance = 50, bool scrollToTop = false)
        {
            //WaitForElementPresent(driver.FindElementByAndroidUIAutomator(element));
            //if (scroll != ScrollOptions.none)
            // ScrollToElement(element, scroll, scrollDistance, scrollToTop);
            //  WaitForEnabled(element);
            return JustClick(element);
        }

        /// <summary>
        /// Waits untill element is not present on page
        /// </summary>
        /// <param name="element"></param>
        public void WaitForElementPresent(By element)
        {
            try
            {
                Wait.Until(ExpectedConditions.ElementExists(element));
            }
            catch
            {
            }
        }

        /// <summary>
        /// Waits for the element to be enabled and clickable
        /// </summary>
        /// <param name="elements"></param>
        public void WaitForEnabled(params MobileBy[] elements)
        {
            foreach (var element in elements)
            {
                try
                {
                    Wait.Until(ExpectedConditions.ElementToBeClickable(element));
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Just clicks an element. Does not wait for it to be present and visible.
        /// </summary>
        /// <param name="element"></param>
        public AppiumWebElement JustClick(By element)
        {
            var el = driver.FindElement(element);
            el.Click();
            return el;
        }

        /// <summary>
        /// Clears input
        /// </summary>
        /// <param name="element"></param>
        public void Clear(By element)
        {

            driver.FindElement(element).Clear();
        }

        /// <summary>
        /// Types specified text into input
        /// </summary>
        /// <param name="element"></param>
        /// <param name="text"></param>
        /// <param name="clear"></param>
        public void TypeText(By element, string text, bool clear = true)
        {
            WaitForElementPresent(element);
            if (clear)
            {
                Clear(element);
            }
            driver.FindElement(element).SendKeys(text);
        }

        /// <summary>
        /// Fails test. Throws 'AssertionException' with a specified fail message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="failMessage">Message to be displayed in test results.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public void FailIt(string failMessage, Exception inner)
        {
            throw new AssertionException(failMessage, inner);
        }

        /// <summary>
        /// Waits for element(s) to be visible
        /// </summary>
        /// <param name="elements">Element(s) to wait for</param>
        public void WaitForVisible(params By[] elements)
        {
            foreach (var element in elements)
            {
                try
                {
                    //IE was giving problems without this
                    //I know there is a waitforboth but this should increase IE test success rate
                    Wait.Until(ExpectedConditions.ElementExists(element));
                    Wait.Until(ExpectedConditions.ElementIsVisible(element));
                }
                catch (WebDriverTimeoutException e)
                {
                    FailIt("Timed out after " + Wait.Timeout + " seconds waiting for " + element.ToString() + " to be visible.", e.InnerException);
                }
            }
        }

        /// <summary>
        /// Returns the value for the given html attribute
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attName"></param>
        /// <param name="waitForVisible">True (default) = wait for element visible, false = do not wait for element visible</param>
        /// <returns></returns>
        public string GetAttributeValue(By element, string attName, bool waitForVisible = true)
        {
            WaitForElementPresent(element);
            if (waitForVisible)
            {
                WaitForVisible(element);
            }
            return driver.FindElement(element).GetAttribute(attName);
        }

        /// <summary>
        /// Gets the value of an element - meaning the value of the html attribute 'value'
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public string GetValue(By element)
        {
            return GetAttributeValue(element, "text");
        }

        /// <summary>
        /// Returns a list of elements from a dropdown or list (select element)
        /// </summary>
        /// <param name="by">Select element (dropdown or list)</param>
        public IList<AndroidElement> GetListElements(By by)
        {
            return driver.FindElements(by);
        }

        /// <summary>
        /// Waits for element to be present and visible
        /// </summary>
        /// <param name="element"></param>
        public void WaitForBoth(By element)
        {
            WaitForElementPresent(element);
            WaitForVisible(element);
        }

        /// <summary>
        /// Single tap on element
        /// </summary>
        /// <param name="by">Element selector</param>
        /// <param name="wait">Wait for element to present</param>
        public void SingleTapOnElement(By by, bool wait = true)
        {
            if (wait)
                WaitForBoth(by);
            tActions.Tap(driver.FindElement(by)).Perform();
        }
    }
}
