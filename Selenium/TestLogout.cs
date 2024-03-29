using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace Selenium
{
    public class TestLogout
    {
        public IWebDriver driver = new ChromeDriver();
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        [TestCase("trandinhtai169@gmail.com", "123456789", "Pass")]
        public void TestMethodLogout(string name, string pass, string result)
        {
            string rs = "Fail";
            // Test logout nhưng chưa xong thuộc tính result
            driver.Navigate().GoToUrl("https://localhost:5001");
            driver.Manage().Window.Maximize();
            Thread.Sleep(2000);

            IWebElement btnClicksettingButton = driver.FindElement(By.Id("settingButton"));
            if (btnClicksettingButton != null)
                btnClicksettingButton.Click();
            Thread.Sleep(2000);

            IWebElement btnClickheaderlogin = driver.FindElement(By.LinkText("Đăng nhập"));
            if (btnClickheaderlogin != null)
                btnClickheaderlogin.Click();
            Thread.Sleep(2000);

            IWebElement txtUserName = driver.FindElement(By.Id("UserName"));
            if (txtUserName != null)
                txtUserName.SendKeys(name);

            IWebElement txtPassword = driver.FindElement(By.Id("Password"));
            if (txtPassword != null)
                txtPassword.SendKeys(pass);

            Thread.Sleep(2000);

            IWebElement btnLogin = driver.FindElement(By.XPath("/html/body/div[1]/main/div/div/div/div/form/div/div/div[3]/button"));
            if (btnLogin != null)
                btnLogin.Click();
            Thread.Sleep(2000);

            btnClicksettingButton = driver.FindElement(By.Id("settingButton"));
            if (btnClicksettingButton != null)
                btnClicksettingButton.Click();
            Thread.Sleep(2000);

            driver.FindElement(By.LinkText("Đăng xuất")).Click();

            driver.Navigate().GoToUrl("https://localhost:5001/tai-khoan-cua-toi.html");
            Thread.Sleep(2000);

            string currentUrl = driver.Url;
            bool isAccountPage = currentUrl.Contains("dang-nhap.html?ReturnUrl=%2Ftai-khoan-cua-toi.html");
            if (isAccountPage)
                rs = "Pass";
            Thread.Sleep(2000);
            driver.Quit();
            Assert.AreEqual(result, rs);
        }
    }
}