using NUnit.Framework;
using OfficeOpenXml;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml.Linq;

namespace Selenium
{
    public class TestExcel
    {
        public IWebDriver driver = new ChromeDriver();
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCaseSource(nameof(ReadExcel))]
        public void Test1OpenURL(string name, string pass, string result)
        {
            driver.Navigate().GoToUrl("https://localhost:5001");
            driver.Manage().Window.Maximize();

            IWebElement btnClickBt = driver.FindElement(By.Id("settingButton"));
            if (btnClickBt != null)
                btnClickBt.Click();

            IWebElement btnLogin = driver.FindElement(By.XPath("/html/body/div[1]/header/div[2]/div/div/div/div/div[2]/ul/li[1]/ul/li[2]/a"));
            if (btnLogin != null)
                btnLogin.Click();
            Thread.Sleep(2000);

            IWebElement txtUserName = driver.FindElement(By.Id("UserName"));
            if (txtUserName != null)
                txtUserName.SendKeys(name);
            Thread.Sleep(2000);

            IWebElement txtPassword = driver.FindElement(By.Id("Password"));
            if (txtPassword != null)
                txtPassword.SendKeys(pass);

            Thread.Sleep(2000);


            IWebElement btnLogi = driver.FindElement(By.XPath("/html/body/div[1]/main/div/div/div/div/form/div/div/div[3]/button"));
            if (btnLogi != null)
                btnLogi.Click();
            Thread.Sleep(2000);

            UpdateExcel(name, pass, result);

            driver.Quit();

        }

        //[Test]
        //[TestCase("settingButton")]
        //public void Test2ClickIcon(string url)
        //{
        //    IWebElement txtSearchElement = driver.FindElement(By.Id(url));
        //    if (txtSearchElement != null)
        //        txtSearchElement.Click();
        //    Assert.Pass();
        //}
        //[Test]
        //public void Test3ClickLogin()
        //{
        //    IWebElement txtSearchElement = driver.FindElement(By.XPath("/html/body/div[1]/header/div[2]/div/div/div/div/div[2]/ul/li[1]/ul/li[2]/a"));
        //    if (txtSearchElement != null)
        //        txtSearchElement.Click();
        //    Assert.Pass();
        //}
        //[Test]
        //[TestCaseSource(nameof(ReadExcel))]
        //public void Test4CloneExcel(string name, string pass)
        //{

        //    IWebElement txtSearchElement = driver.FindElement(By.Id("UserName"));
        //    if (txtSearchElement != null)
        //        txtSearchElement.SendKeys(name);

        //    txtSearchElement = driver.FindElement(By.Id("Password"));
        //    if (txtSearchElement != null)
        //        txtSearchElement.SendKeys(pass);
        //    Thread.Sleep(2000);
        //    Assert.Pass();
        //}
        //[Test]
        //[TestCase("/html/body/div[1]/main/div/div/div/div/form/div/div/div[3]/button")]
        //public void Test5ClickButtonLogin(string url)
        //{
        //    IWebElement txtSearchElement = driver.FindElement(By.XPath(url));
        //    if (txtSearchElement != null)
        //        txtSearchElement.Click();
        //    Assert.Pass();
        //}
        //[Test]
        //public void Test6CloseURL()
        //{
        //    driver.Quit();
        //    Assert.Pass();
        //}

        public static IEnumerable<object[]> ReadExcel()
        {
            string filePath = @"D:\Data.xlsx";
            FileInfo file = new FileInfo(filePath);
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet excelWorksheet = package.Workbook.Worksheets["Sheet1"];
                int soHang = excelWorksheet.Dimension.End.Row;
                for (int i = 2; i <= soHang; i++)
                {
                    yield return new object[] {
                        excelWorksheet.Cells[i, 1].Value?.ToString().Trim(),
                        excelWorksheet.Cells[i, 2].Value?.ToString().Trim()
                    };
                }
            }
        }
        private void UpdateExcel(string name, string pass, string result)
        {
            string filePath = @"D:\Data.xlsx";
            FileInfo file = new FileInfo(filePath);

            using (ExcelPackage package = new ExcelPackage(file))
            {
                if (package.Workbook != null && package.Workbook.Worksheets.Count > 0)
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];

                    int soHang = worksheet.Dimension.End.Row;

                    for (int i = worksheet.Dimension.Start.Row + 1; i <= soHang; i++)
                    {
                        string _name = worksheet.Cells[i, 1].Value?.ToString();
                        string _email = worksheet.Cells[i, 2].Value?.ToString();
                        string _sub = worksheet.Cells[i, 3].Value?.ToString();
                        string _mess = worksheet.Cells[i, 4].Value?.ToString();
                        string _upload = worksheet.Cells[i, 5].Value?.ToString();

                        //if (_email == email && _name == name && _sub == sub && _mess == mess && _upload == upload)
                        //{
                        //    worksheet.Cells[i, 6].Value = result;
                        //    break;
                        //}
                    }
                    package.Save();
                }
                else
                {

                }
            }
        }
    }
}