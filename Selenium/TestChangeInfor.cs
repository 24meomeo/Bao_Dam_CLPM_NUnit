using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace Selenium
{
    public class TestChangeInfor
    {
        public IWebDriver driver = new ChromeDriver();
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("meocon@gmail.com", "123456789", "Yến", "0915779366", "BienHoa", "Pass")]
        public void TestMethodChangePass(string name, string pass, string name2, string phone, string address, string result)
        {
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

            driver.FindElement(By.Id("account-profile-tab")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.Id("FullName")).Click();
            driver.FindElement(By.Id("FullName")).SendKeys(name2);
            driver.FindElement(By.Id("Phone")).Click();
            driver.FindElement(By.Id("Phone")).SendKeys(phone);
            driver.FindElement(By.Id("Address")).Click();
            driver.FindElement(By.Id("Address")).SendKeys(address);

            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("#account-profile span")).Click();
            Thread.Sleep(2000);

            string rs = "";
            string m = driver.FindElement(By.CssSelector(".notyf__message")).Text;
            if (m == "Thay đổi thông tin thành công!")
                rs = "Pass";
            else
                rs = "Fail";

            driver.Quit();
            Assert.AreEqual(result, rs);
        }
    }
}