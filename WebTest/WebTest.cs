using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebTest
{
    [TestFixture]
    public class TestLanding
    {
        private IWebDriver driver_;

        [SetUp]
        public void Setup()
        {
            driver_ = new ChromeDriver();
        }

        [Test]
        public void DefaultValues()
        {
            // Test what happens when the page initially loads
        }

        [Test]
        public void NoTypeSelected()
        {
            // Test what happens when a user tries to send a message
            // with no type selected
        }

        [Test]
        public void NoFrom()
        {
            // Test what happens when a user tries to send a message
            // with no from defined
        }

        [Test]
        public void NoTo()
        {
            // Test what happens when a user tries to send a message
            // with no to selected
        }

        // Anything else you want to add!

        [TearDown]
        public void Teardown()
        {
            driver_.Close();
        }
    }
}
