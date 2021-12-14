using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;
using VirtualDevice.Pages;

namespace ClassLibrary1.Pages.Preference
{
    public class PreferenceDependenciesPage : APage<PreferenceDependenciesPageSelectors>
    {
        public PreferenceDependenciesPage(Page p) : base(p, new PreferenceDependenciesPageSelectors())
        {

        }

        public void ClickOnWiFiCheckBox()
        {
            page.Click(selectors.WifiCheckBox);
        }

        public void OpenWiFiSettings()
        {
            page.Click(selectors.WifiSerttings);
        }
    }

    public class PreferenceDependenciesPageSelectors
    {
        public By WifiCheckBox => By.Id("android:id/checkbox");
        public By WifiSerttings => By.XPath("(//android.widget.RelativeLayout)[2]");
    }
}
