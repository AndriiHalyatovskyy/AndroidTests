using NUnit.Framework;

namespace ClassLibrary1.ChromeTest
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
        public void Test()
        {
            Page.GooglePage.DoSearsh(textToSearch);
            Page.GooglePage.OpenSearchResultByTitle(websideToOpen);
        }
    }
}
