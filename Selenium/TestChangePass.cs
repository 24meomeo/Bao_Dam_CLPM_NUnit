using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace Selenium
{
    public class TestChangePass
    {
        public IWebDriver driver = new ChromeDriver();
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("meocon@gmail.com", "123456789", "123456789", "123456789", "123456789", "Fail")]
        public void TestMethodChangePass(string name, string pass, string passnow, string passChange, string passChangeC, string result)
        {
            // Test đổi mật khẩu, mà thuộc tính result còn đang lỗi
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

            IWebElement btnChangePass = driver.FindElement(By.Id("account-details-tab"));
            if (btnChangePass != null)
                btnChangePass.Click();
            Thread.Sleep(2000);

            txtUserName = driver.FindElement(By.Id("PasswordNow"));
            if (txtUserName != null)
                txtUserName.SendKeys(passnow);

            txtPassword = driver.FindElement(By.Id("Password"));
            if (txtPassword != null)
                txtPassword.SendKeys(passChange);

            IWebElement txtConfirmPass2 = driver.FindElement(By.Id("ConfirmPassword"));
            if (txtConfirmPass2 != null)
                txtConfirmPass2.SendKeys(passChangeC);
            Thread.Sleep(2000);

            IWebElement btnSaveNewPass = driver.FindElement(By.Id("btnSaveNewPass"));
            if (btnSaveNewPass != null)
                btnSaveNewPass.Click();
            Thread.Sleep(2000);


            string rs = "";
            string m = driver.FindElement(By.CssSelector(".notyf__message")).Text;
            if (m == "Đổi mật khẩu thành công")
                rs = "Pass";
            else
                rs = "Fail";

            driver.Quit();
            Assert.AreEqual(result, rs);
        }
    }
}