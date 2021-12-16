using System;
using ClassLibrary1.Pages.Views;
using NUnit.Framework;
using VirtualDevice.Enums;
using VirtualDevice.Pages;
using VirtualDevice.Tests;

namespace VirtualDevice
{
    [TestFixture]
    public class Test1 : BaseTest
    {
        #region Test data
        private string expectedWifiName;
        private readonly TimeSpan time = new TimeSpan(8, 45, 0);
        #endregion

        [OneTimeSetUp]
        public void OneTimeSetUpTest()
        {
            expectedWifiName = RandomString(10);
        }

        [Test]
        public void FirstTest()
        {
            Page.MainPage.OpenPage(MainPages.Preference);
            Page.PreferencePage.OpenPreferenceDependencies();
            Page.PreferenceDependenciesPage.ClickOnWiFiCheckBox();
            Page.PreferenceDependenciesPage.OpenWiFiSettings();
            Page.TextPopupPage.TypeText(expectedWifiName);
            Page.TextPopupPage.ClickOk();
            Page.PreferenceDependenciesPage.OpenWiFiSettings();
            var actualWifiName = Page.TextPopupPage.GetInputValue();
            Assert.AreEqual(expectedWifiName, actualWifiName, $"Expected wifi name: {expectedWifiName}, but was: {actualWifiName}");
        }

        [Test]
        public void ClickableElementsTest()
        {
            Page.MainPage.OpenPage(MainPages.Views);
            var clickableElements = Page.ViewsPage.GetCountOfClickableElements();
        }

        [Test]
        public void LongPressTest()
        {
            Page.MainPage.OpenPage(MainPages.Views);
            Page.ViewsPage.OpenExpandableList();
            Page.ExpandableListsPage.OpenCustomAdapterPage();
            Page.CustomAdapterPage.OpenPopupFor(CustomAdapterOptions.People);
            var isDisplayed = Page.CustomAdapterPage.IsPopupDisplayed();
            Assert.IsTrue(isDisplayed, "Popup is not displayed");
        }

        [Test]
        public void SwipeTest()
        {
            Page.MainPage.OpenPage(MainPages.Views);
            Page.ViewsPage.OpenDateWidgets();
            Page.DateWidgetsPage.OpenInlineDateWidget();
            Page.DateWidgetsPage.SelectTimeInline(time);
        }

        [Test]
        public void ScrollingTest()
        {
            Page.MainPage.OpenPage(MainPages.Views);
            Page.ViewsPage.ScrollTo("WebView");
        }
    }
}
