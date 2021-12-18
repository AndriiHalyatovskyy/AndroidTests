using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using VirtualDevice.Pages;

namespace ClassLibrary1.Pages.Common
{
    public class TextPopup : APage<TextPopupSelectors>
    {
        public TextPopup(Page p) : base(p, new TextPopupSelectors())
        {

        }

        /// <summary>
        /// Types text into input
        /// </summary>
        /// <param name="textToType">Text to type</param>
        /// <param name="clear">Set true to clear text in input</param>
        public void TypeText(string textToType, bool clear = true)
        {
            page.TypeText(selectors.TextInput, textToType, clear);
        }

        /// <summary>
        /// Clicks on OK button
        /// </summary>
        public void ClickOk()
        {
            page.Click(selectors.OkButton);
        }        
        
        /// <summary>
        /// Clicks on Cancel button
        /// </summary>
        public void ClickCancel()
        {
            page.Click(selectors.CancelButton);
        }

        /// <summary>
        /// Returns text from input
        /// </summary>
        public string GetInputValue()
        {
            return page.GetValue(selectors.TextInput);
        }
    }

    public class TextPopupSelectors
    {
        public By TextInput = MobileBy.ClassName("android.widget.EditText");
        public By OkButton = MobileBy.Id("android:id/button1");
        public By CancelButton = MobileBy.XPath("//android.widget.Button[@text = 'CANCEL']");
    }
}
