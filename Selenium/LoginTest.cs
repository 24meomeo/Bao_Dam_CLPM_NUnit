using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.Text.RegularExpressions;
using System.Threading;

namespace Selenium
{
    internal class LoginTest
    {
        public IWebDriver driver = new EdgeDriver();
        [SetUp]
        public void Setup()
        {

        }
        [Test]
        [TestCase("UserName", "Password", "#%#Yen@gmail.com", "12345", true)]
        public void TestMethodLogin(string txtUser, string txtPass, string name, string pass, bool result)
        {
            CheckCharaters checkCharater = new CheckCharaters();
            driver.Navigate().GoToUrl("https://localhost:5001");
            driver.Manage().Window.Maximize();
            Thread.Sleep(2000);

            IWebElement btnClicksettingButton = driver.FindElement(By.Id("settingButton"));
            if (btnClicksettingButton != null)
                btnClicksettingButton.Click();
            Thread.Sleep(2000);

            IWebElement btnClickheaderlogin = driver.FindElement(By.CssSelector("body > div.main-wrapper > header > div.header-middle.py-5 > div > div > div > div > div.header-right > ul > li.dropdown.d-none.d-md-block > ul > li:nth-child(2) > a"));
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


            bool isErrorMessageDisplayed = true;


            if (name == null || pass == null)
            {
                isErrorMessageDisplayed = false;

            }
            else if ((!Regex.IsMatch(name, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")) && (name != null))
            {
                isErrorMessageDisplayed = false;
            }
            else if (name != null && !checkCharater.ContainsSpecialCharactersForMail(name))
            {
                isErrorMessageDisplayed = false;
            }
            else if (pass.Length < 5)
            {
                isErrorMessageDisplayed = false;
            }
            //else if (name != null && !checkCharater.ContainsSpecialCharacters(nameSP))
            //{
            //    isErrorMessageDisplayed = false;
            //}

            // Lưu ý code khi Tài Khoản có tên HoangNhan@@gmail.com chưa đăng ký thì sẽ không thể phân biệt là 
            // Lỗi do dưa đăng ký hay lỗi bị ký tự đặc biệt

            driver.Quit();
            Assert.AreEqual(result, isErrorMessageDisplayed);
        }
    }
}
