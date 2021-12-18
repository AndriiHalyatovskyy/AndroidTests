using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using VirtualDevice.Pages;

namespace ClassLibrary1.Pages.HybridAppPages
{
    public class LoginPage : APage<LoginPageSelectors>
    {
        public LoginPage(Page p) : base(p, new LoginPageSelectors())
        {
        }

        /// <summary>
        /// Types username into input
        /// </summary>
        /// <param name="name">User name</param>
        public void SetName(string name)
        {
            page.TypeText(selectors.usernameInput, name);
            page.HideKeyboard();

        }

        /// <summary>
        /// Taps on gender radio button
        /// </summary>
        /// <param name="gender">Male/Female</param>
        public void SetGender(string gender)
        {
            page.SingleTapOnElement(selectors.GetGender(gender));
        }

        /// <summary>
        /// Opens the country dropdown
        /// </summary>
        public void OpenCountryDropDown()
        {
            page.SingleTapOnElement(selectors.countryDropdown);
        }

        /// <summary>
        /// Scrolls and tap on country by name
        /// </summary>
        /// <param name="countryname">Country name</param>
        public void SelectCountry(string countryname)
        {
            page.ScrollToElement(selectors.ScrollToCountry(countryname));
            page.SingleTapOnElement(selectors.Country(countryname));
        }

        /// <summary>
        /// Fills the login page information
        /// </summary>
        /// <param name="country">Country name</param>
        /// <param name="name">User name</param>
        /// <param name="gender">Gender</param>
        public void FillLoginPage(string country, string name, string gender)
        {
            OpenCountryDropDown();
            SelectCountry(country);
            SetName(name);
            SetGender(gender);
        }

        /// <summary>
        /// Taps on Let’s shop button
        /// </summary>
        public void ClickLetShopButton()
        {
            page.SingleTapOnElement(selectors.goButton);
        }
    }

    public class LoginPageSelectors
    {
        public By usernameInput = By.Id("com.androidsample.generalstore:id/nameField");
        public By goButton = By.Id("com.androidsample.generalstore:id/btnLetsShop");
        public By countryDropdown = By.Id("com.androidsample.generalstore:id/spinnerCountry");

        public By Country(string country) => MobileBy.AndroidUIAutomator($"text(\"{country}\")");
        public By GetGender(string gender) => MobileBy.AndroidUIAutomator($"text(\"{gender}\")");
        public By ScrollToCountry(string countryToScroll) => MobileBy.AndroidUIAutomator($"new UiScrollable(new UiSelector()).scrollIntoView(text(\"{countryToScroll}\"));");
    }
}
