using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using VirtualDevice.Pages;

namespace ClassLibrary1.Pages.Views
{
    public class ExpandableListsPage : APage<ExpandableListsSelectors>
    {
        public ExpandableListsPage(Page p) : base(p, new ExpandableListsSelectors())
        {
        }

        /// <summary>
        /// Open custom adapter page
        /// </summary>
        public void OpenCustomAdapterPage()
        {
            page.Click(selectors.CustomAdapter);
        }
    }

    public class ExpandableListsSelectors
    {
        public By CustomAdapter = MobileBy.AndroidUIAutomator("text(\"1. Custom Adapter\")");
       // public By CustomAdapter => By.XPath("(//android.widget.TextView[contains(@text, 'Custom Adapter')]");
    }
}
