using NUnit.Framework;
using VirtualDevice.Tests;

namespace ClassLibrary1.Tests
{
    [TestFixture]
    public class HybridAppTest : BaseTest
    {
        #region Test Data
        private readonly string country = "Chad";
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
        }

        [Test]
        public void ToastMessageTest()
        {
            Page.LoginPage.ClickLetShopButton();
            var actualToastMessageText = Page.LoginPage.GetTextfromToastMessage();
            Assert.AreEqual(expectedToastMessageText, actualToastMessageText);
        }
    }
}
