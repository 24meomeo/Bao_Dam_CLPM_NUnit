using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace Selenium
{
    [TestFixture]
    public class TestSearchProduct
    {
        public IWebDriver driver = new ChromeDriver();
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("keyword", "Dưa Hấu")]
        public void TestMethodSearchProduct(string text, string content)
        {
            // Test tìm kiếm sản phẩm nhưng chưa có thuộc tính result
            driver.Navigate().GoToUrl("https://localhost:5001/admin");
            driver.Manage().Window.Maximize();
            Thread.Sleep(2000);

            IWebElement txtElement = driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div/ul/li[3]"));
            if (txtElement != null)
                txtElement.Click();
            Thread.Sleep(2000);

            txtElement = driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div/ul/li[3]/ul/li/a"));
            if (txtElement != null)
                txtElement.Click();
            Thread.Sleep(2000);

            txtElement = driver.FindElement(By.Name(text));
            if (txtElement != null)
                txtElement.SendKeys(content);
            Thread.Sleep(2000);

            driver.Quit();
            Assert.Pass();
        }
    }
}