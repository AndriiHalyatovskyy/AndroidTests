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
        private string name;
        #endregion

        [OneTimeSetUp]
        public void OneTimeSetUpTest()
        {
            name = RandomString(15);
        }

        [Test] 
        public void LoginPageTest()
        {
            Page.LoginPage.FillLoginPage(country, name, gender);
            Page.LoginPage.ClickLetShopButton();
        }
    }
}
