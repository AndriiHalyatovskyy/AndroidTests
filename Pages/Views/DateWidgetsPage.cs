using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using VirtualDevice.Pages;

namespace ClassLibrary1.Pages.Views
{
    public class DateWidgetsPage : APage<DateWidgetsPageSelectors>
    {
        public DateWidgetsPage(Page p) : base(p, new DateWidgetsPageSelectors())
        {
        }

        public void OpenInlineDateWidget()
        {
            page.SingleTapOnElement(selectors.Inline);
        }

        /// <summary>
        /// Clicks on hour
        /// </summary>
        /// <param name="hour">Hour to click</param>
        public void SelectTimeInline(TimeSpan time)
        {
            page.SingleTapOnElement(selectors.Time(time.Hours));
            page.MoveToElement(selectors.Time(15), selectors.Time(time.Minutes));
        }
    }

    public class DateWidgetsPageSelectors
    {
        public By Inline = MobileBy.AndroidUIAutomator("text(\"2. Inline\")");
        public By Time(int timeInt) => By.XPath($"//*[@content-desc = '{timeInt}']");
    }
}
