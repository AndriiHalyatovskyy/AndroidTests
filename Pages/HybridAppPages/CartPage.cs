using System.Text.RegularExpressions;
using ClassLibrary1.Pages.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using VirtualDevice.Pages;

namespace ClassLibrary1.Pages.HybridAppPages
{
    public class CartPage : APage<CartPageSelectors>
    {
        public CartPage(Page p) : base(p, new CartPageSelectors())
        {

        }

        /// <summary>
        /// Returns price of specified product as a double
        /// </summary>
        /// <param name="productName">Name</param>
        public double GetProductPrice(string productName)
        {
            var price = page.GetAttributeValue(selectors.GetPriceForProduct(productName), "text");
            var result = Regex.Match(price, RegexHelper.ONLY_NUMBERS);
            return double.Parse(result.Value);
        }

        /// <summary>
        /// Returns total amount af a double
        /// </summary>
        public double GetTotalAmount()
        {
            var price = page.GetAttributeValue(selectors.TotalAmount, "text");
            var result = Regex.Match(price, RegexHelper.ONLY_NUMBERS);
            return double.Parse(result.Value);
        }

        /// <summary>
        /// Long tap on terms of use
        /// </summary>
        public void OpenTermsOfUse()
        {
            page.LongPressOnElement(selectors.TermsButton);
        }

        /// <summary>
        /// Clicks on close button
        /// </summary>
        public void ClosePopup()
        {
            page.Click(selectors.CloseButton);
        }

        /// <summary>
        /// Clicks on purchase button
        /// </summary>
        public void ClickPurchase()
        {
            page.Click(selectors.PurchaseButton);
        }
    }

    public class CartPageSelectors
    {
        public By TotalAmount = By.Id("com.androidsample.generalstore:id/totalAmountLbl");
        public By TermsButton = By.Id("com.androidsample.generalstore:id/termsButton");
        public By CloseButton = MobileBy.AndroidUIAutomator("text(\"CLOSE\")");
        public By PurchaseButton = By.Id("com.androidsample.generalstore:id/btnProceed");
        public By GetPriceForProduct(string productName) => By.XPath($"//*[@text = '{productName}']/..//*[contains(@resource-id, 'productPrice')]");
    }
}
