using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace Selenium
{
    public class TestViewOrder
    {
        public IWebDriver driver = new ChromeDriver();
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("UserName", "Password", "trandinhtai169@gmail.com", "1234567")]
        public void TestMethodChangePass(string txtUser, string txtPass, string name, string pass)
        {
            // Test xem đơn hàng nhưng chưa có thuộc tính result
            driver.Navigate().GoToUrl("https://localhost:5001");
            driver.Manage().Window.Maximize();
            Thread.Sleep(2000);

            IWebElement btnClicksettingButton = driver.FindElement(By.Id("settingButton"));
            if (btnClicksettingButton != null)
                btnClicksettingButton.Click();
            Thread.Sleep(2000);

            IWebElement btnClickheaderlogin = driver.FindElement(By.Id("header-login"));
            if (btnClickheaderlogin != null)
                btnClickheaderlogin.Click();
            Thread.Sleep(2000);

            IWebElement txtUserName = driver.FindElement(By.Id(txtUser));
            if (txtUserName != null)
                txtUserName.SendKeys(name);

            IWebElement txtPassword = driver.FindElement(By.Id(txtPass));
            if (txtPassword != null)
                txtPassword.SendKeys(pass);

            Thread.Sleep(2000);

            IWebElement btnLogin = driver.FindElement(By.XPath("/html/body/div[1]/main/div/div/div/div/form/div/div/div[3]/button"));
            if (btnLogin != null)
                btnLogin.Click();
            Thread.Sleep(2000);

            driver.FindElement(By.Id("account-orders-tab")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.CssSelector("tr:nth-child(3) .xemdonhang")).Click();
            Thread.Sleep(2000);

            driver.Quit();
            Assert.Pass();
        }
    }
}