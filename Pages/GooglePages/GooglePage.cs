using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using VirtualDevice.Pages;

namespace ClassLibrary1.Pages.GooglePages
{
    public class GooglePage : APage<GooglePageSelectors>
    {
        public GooglePage(Page p) : base(p, new GooglePageSelectors())
        {
        }

        /// <summary>
        /// Performs search in google
        /// </summary>
        /// <param name="textToSearch">Text to search</param>
        public void DoSearsh(string textToSearch)
        {
            page.TypeText(selectors.SearchInput, textToSearch);
            page.SendKeys(selectors.SearchInput, Keys.Enter);
        }

        /// <summary>
        /// Opens search result by title
        /// </summary>
        /// <param name="resultTitle"></param>
        public void OpenSearchResultByTitle(string resultTitle)
        {
            page.Click(selectors.GetSearchResultByTitle(resultTitle), ScrollOptions.intoView);
        }
    }

    public class GooglePageSelectors
    {
        public By SearchInput = By.XPath("//input[@name = 'q']");

        public By GetSearchResultByTitle(string title) => By.XPath($"//*[* = '{title}']");
    }
}
