using NUnit.Framework;
using OfficeOpenXml;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Linq;

namespace Selenium
{
    public class TestLoginExcel
    {
        public IWebDriver driver = new ChromeDriver();
        [SetUp]
        public void Setup()
        {
        }

        [Test]

        [TestCaseSource(nameof(ReadExcel), new object[] { @"D:\TestCase.xlsx"})]

        public void TestScriptContactUs(string email, string password, string expectedResult)
        {
            CheckCharaters checkCharaters = new CheckCharaters();
            EdgeOptions options = new EdgeOptions();
            options.AddArgument("--incognito");
            //IWebDriver driver = new EdgeDriver();
            string actualResult = "Pass";
            driver.Navigate().GoToUrl("https://localhost:5001/");
            driver.Manage().Window.Maximize();
            Thread.Sleep(2000);

            IWebElement menuElement = driver.FindElement(By.Id("settingButton"));
            if (menuElement == null)
            {
                actualResult = "Fail";
            }
            menuElement.Click();



            IWebElement txtName = driver.FindElement(By.LinkText("Đăng nhập"));
            if (txtName == null)
                actualResult = "Fail";
            txtName.Click();

            IWebElement txtEmail = driver.FindElement(By.Id("UserName"));
            if (txtEmail == null)
                actualResult = "Fail";
            txtEmail.SendKeys(email);
            Thread.Sleep(500);

            IWebElement txtPassword = driver.FindElement(By.Id("Password"));
            if (txtPassword == null)
                actualResult = "Fail";
            txtPassword.SendKeys(password);
            Thread.Sleep(500);

            IWebElement btnLogin = driver.FindElement(By.CssSelector("body > div.main-wrapper > main > div > div > div > div > form > div > div > div.col-12 > button"));
            if (btnLogin == null)
                actualResult = "Fail";
            btnLogin.Click();
            Thread.Sleep(500);


            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                actualResult = "Fail";
            }
            else if ((!Regex.IsMatch(email, @"^(.+)@(.+)$")))
            {
                actualResult = "Fail";
            }
            else if (email != "" && checkCharaters.ContainsSpecialCharactersForMail(email))
            {
                actualResult = "Fail";
            }
            else if (password.Length < 8)
            {
                actualResult = "Fail";
            }

            Thread.Sleep(2000);

            UpdateExcel(actualResult);
            Assert.That(expectedResult, Is.EqualTo(actualResult));

        }
        public static IEnumerable<object[]> ReadExcel(string filePath)
        {

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            FileInfo file = new FileInfo(filePath);
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet excelWorksheet = package.Workbook.Worksheets["login"];
                int soDong = excelWorksheet.Dimension.End.Row;
                for (int i = 2; i <= soDong; i++)
                {
                    yield return new object[] {
                        excelWorksheet.Cells[i,2].Value?.ToString().Trim(),
                        excelWorksheet.Cells[i,3].Value?.ToString().Trim(),
                        excelWorksheet.Cells[i,4].Value?.ToString().Trim(),
                    };

                }
            }
        }
        public static void UpdateExcel(string acResult)
        {
            string filePath = @"D:\TestCase.xlsx";
            FileInfo file = new FileInfo(filePath);

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["login"];

                int soDong = worksheet.Dimension.End.Row;

                for (int i = 2; i <= soDong; i++)
                {
                    worksheet.Cells[i, 5].Value = acResult;
                }
                package.Save();
            }
        }
    }
}