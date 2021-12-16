using System.ComponentModel;
using ClassLibrary1.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using VirtualDevice.Pages;

namespace ClassLibrary1.Pages.Views
{
    public class CustomAdapterPage : APage<CustomAdapterPageSelectors>
    {
        public CustomAdapterPage(Page p) : base(p, new CustomAdapterPageSelectors())
        {
        }

        /// <summary>
        /// Open popep for specified category
        /// </summary>
        /// <param name="option">Category</param>
        public void OpenPopupFor(CustomAdapterOptions option)
        {
            page.LongPressOnElement(selectors.Names(option), false);
        }

        /// <summary>
        /// Returns true if popup is displayed
        /// </summary>
        /// <returns></returns>
        public bool IsPopupDisplayed()
        {
            return page.IsElementVisible(selectors.PopupText);
        }
    }

    public class CustomAdapterPageSelectors
    {
        public By Names(CustomAdapterOptions option) => MobileBy.AndroidUIAutomator($"text(\"{option.GetEnumDescription()}\")");
        public By PopupText = MobileBy.AndroidUIAutomator("text(\"Sample menu\")");
    }

    public enum CustomAdapterOptions
    {
        [Description("People Names")]
        People,
        [Description("Dog Names")]
        Dog
    }
}
