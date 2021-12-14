using ClassLibrary1.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using VirtualDevice.Enums;

namespace VirtualDevice.Pages
{
    public  class MainPage : APage<MainPageSelectors>
    {
        public MainPage(Page p) : base(p, new MainPageSelectors())
        {

        }

        /// <summary>
        /// Opens specified page
        /// </summary>
        /// <param name="pageToOpen">Page to open</param>
        public void OpenPage(MainPages pageToOpen)
        {
            page.Click(selectors.GetPage(pageToOpen));
        }
    }

    public class MainPageSelectors
    {
        public By GetPage(MainPages pageName) => MobileBy.AndroidUIAutomator($"text(\"{pageName.GetEnumDescription()}\")");
    }
}
