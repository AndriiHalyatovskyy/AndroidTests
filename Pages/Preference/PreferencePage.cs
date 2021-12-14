using OpenQA.Selenium;
using VirtualDevice.Pages;

namespace ClassLibrary1.Pages
{
    public class PreferencePage : APage<PreferencePageSelectors>
    {
        public PreferencePage(Page p) : base(p, new PreferencePageSelectors())
        {

        }

        /// <summary>
        /// Click on Preference dependencies
        /// </summary>
        public void OpenPreferenceDependencies()
        {
            page.Click(selectors.PreferenceDependencies);
        }
    }

    public class PreferencePageSelectors
    {
        public By PreferenceDependencies = By.XPath("//android.widget.TextView[contains(@text,  'Preference dependencies')]");
    }
}
