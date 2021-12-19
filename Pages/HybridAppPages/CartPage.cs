using System.Text.RegularExpressions;
using ClassLibrary1.Pages.Common;
using OpenQA.Selenium;
using VirtualDevice.Pages;

namespace ClassLibrary1.Pages.HybridAppPages
{
    public class CartPage : APage<CartPageSelectors>
    {
        public CartPage(Page p) : base(p, new CartPageSelectors())
        {

        }

        public double GetProductPrice(string productName)
        {
            var price = page.GetAttributeValue(selectors.GetPriceForProduct(productName), "text");
            var result = Regex.Match(price, RegexHelper.ONLY_NUMBERS);
            return double.Parse(result.Value);
        }

        public double GetTotalAmount()
        {
            var price = page.GetAttributeValue(selectors.TotalAmount, "text");
            var result = Regex.Match(price, RegexHelper.ONLY_NUMBERS);
            return double.Parse(result.Value);
        }
    }

    public class CartPageSelectors
    {
        public By TotalAmount = By.Id("com.androidsample.generalstore:id/totalAmountLbl");
        public By GetPriceForProduct(string productName) => By.XPath($"//*[@text = '{productName}']/..//*[contains(@resource-id, 'productPrice')]");
    }
}
