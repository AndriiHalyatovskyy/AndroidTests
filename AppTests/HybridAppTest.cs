using NUnit.Framework;
using VirtualDevice.Tests;

namespace VirtualDevice
{
    [TestFixture]
    public class HybridAppTest : BaseTest
    {
        #region Test Data
        private readonly string country = "Austria";
        private readonly string gender = "Male";
        private readonly string expectedToastMessageText = "Please enter your name";
        private readonly string productName = "Converse All Star";
        private readonly string secondProductName = "Air Jordan 4 Retro";
        private string name;
        #endregion

        [OneTimeSetUp]
        public void OneTimeSetUpTest()
        {
            name = RandomString(15);
        }

        [Test]
        public void ShopTest()
        {
            Page.LoginPage.FillLoginPage(country, name, gender);
            Page.LoginPage.ClickLetShopButton();
            Page.ShopPage.AddProductsToCart(productName, secondProductName);
            Page.ShopPage.ClickOnCartButton();
            var firstProductPrice = Page.CartPage.GetProductPrice(productName);
            var secondProductPrice = Page.CartPage.GetProductPrice(secondProductName);
            var totalAmount = Page.CartPage.GetTotalAmount();
            Assert.IsTrue(totalAmount == firstProductPrice + secondProductPrice, "Total amount is incorrect");
        }

        [Test]
        public void ToastMessageTest()
        {
            Page.LoginPage.ClickLetShopButton();
            var actualToastMessageText = Page.LoginPage.GetTextfromToastMessage();
            Assert.AreEqual(expectedToastMessageText, actualToastMessageText);
        }

        [Test]
        public void PurchaseTest()
        {
            Page.LoginPage.FillLoginPage(country, name, gender);
            Page.LoginPage.ClickLetShopButton();
            Page.ShopPage.AddProductsToCart(secondProductName);
            Page.ShopPage.ClickOnCartButton();
            Page.CartPage.OpenTermsOfUse();
            Page.CartPage.ClosePopup();
            Page.CartPage.ClickPurchase();
            Page.SwitchToWebView();
            Page.WebViewPage.DoSearch("Hello World!");
        }
    }
}
