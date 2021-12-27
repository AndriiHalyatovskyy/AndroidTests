using NUnit.Framework;

namespace VirtualDevice
{
    [TestFixture]
    public class ChromeAppTest : BaseTestChrome
    {
        private string url = "https://www.google.com/";
        private string textToSearch = "Wikipedia";
        private string websideToOpen = "Wikipedia - Home | Facebook";

        [OneTimeSetUp]
        public void OneTimeSetUpTest()
        {
            Page.OpenURL(url);
        }

        [Test]
        public void ChromeTest()
        {
            Page.GooglePage.DoSearsh(textToSearch);
            Page.GooglePage.OpenSearchResultByTitle(websideToOpen);
        }
    }
}
