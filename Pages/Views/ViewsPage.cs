using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using VirtualDevice.Pages;

namespace ClassLibrary1.Pages.Views
{
    public class ViewsPage : APage<ViewsPageSelectors>
    {
        public ViewsPage(Page p) : base(p, new ViewsPageSelectors())
        {

        }

        /// <summary>
        /// Returns count of clickable elements
        /// </summary>
        public int GetCountOfClickableElements()
        {
           return page.GetListElements(selectors.ClickableElements).Count;
        }
    }

    public class ViewsPageSelectors
    {
        public By ClickableElements = MobileBy.AndroidUIAutomator("new UiSelector().clickable(true)");
    }

}
