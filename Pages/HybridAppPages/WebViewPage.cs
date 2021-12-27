using OpenQA.Selenium;
using VirtualDevice.Pages;

namespace ClassLibrary1.Pages.HybridAppPages
{
    public class WebViewPage : APage<WebViewPageSelectors>
    {
        public WebViewPage(Page p) : base(p, new WebViewPageSelectors())
        {
        }

        /// <summary>
        /// Performs search
        /// </summary>
        /// <param name="textToSearch">Text to search</param>
        public void DoSearch(string textToSearch)
        {
            page.TypeText(selectors.SearchInput, textToSearch, false);
            page.SendKeys(selectors.SearchInput, Keys.Enter);
        }
    }

    public class WebViewPageSelectors
    {
        public By SearchInput = By.XPath("//*[@name = 'q']");
    }
}
