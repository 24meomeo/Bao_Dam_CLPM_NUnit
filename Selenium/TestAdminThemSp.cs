﻿using NUnit.Framework;
using OfficeOpenXml;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace Selenium
{
    class TestAdminThemSp
    {
        public IWebDriver driver = new ChromeDriver();
        [SetUp]
        public void Setup() { }
        [Test]
        [TestCaseSource(nameof(ReadExcel), new object[] { @"D:\TestCase.xlsx" })]
        //[TestCase("Đậu Thảo", "đậu xanh nè", "10000", "10", "Lúa Mì", "10", @"D:\anh.jpg", "On", "Pass")]
        //[TestCase("Đậu Xanh", "đậu xanh nè", "10", "10", "Lúa Mì", "10", @"D:\anh.jpg", "On",  false)]
        public void TestAddProduct(string tenSP, string desc, string price, string discount, string selectText, string stock, string upload, string congTacPL,
        string expectedResult)
        {
            bool _expectedResult;
            if (expectedResult == "Pass")
                _expectedResult = true;
            else
                _expectedResult = false;
 
            CheckCharaters checkCharater = new CheckCharaters();
 
            driver.Navigate().GoToUrl("https://localhost:5001/admin");
            Thread.Sleep(2000);

            // click quản lý sản phẩm
            IWebElement btnClickSP = driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div/ul/li[3]/a"));
            btnClickSP.Click();

            // click  dropdown danh sách sản phẩm
            IWebElement btnClickDropDown = driver.FindElement(By.LinkText("Danh Sách Sản Phẩm"));
            btnClickDropDown.Click();

            // click add product
            IWebElement btnClickAddProduct = driver.FindElement(By.XPath("/html/body/div[1]/div/div[3]/div/div[2]/div/div[1]/div[2]/a"));
            btnClickAddProduct.Click();

            // nhập tên sản phẩm
            IWebElement inputTextName = driver.FindElement(By.ClassName("form-control"));
            inputTextName.SendKeys(tenSP);

            // nhập desc
            IWebElement inputTypeDesc = driver.FindElement(By.Id("ShortDesc"));
            inputTypeDesc.SendKeys(desc);

            // nhập giá
            IWebElement inputTypePrice = driver.FindElement(By.Id("Price"));
            inputTypePrice.SendKeys(price);

            // nhập discount
            IWebElement inputTypeDiscount = driver.FindElement(By.Id("Discount"));
            inputTypeDiscount.SendKeys(discount);
            Thread.Sleep(1000);

            //chọn danh mục
            //SelectElement selectelement = new SelectElement(driver.FindElement(By.XPath("//*[@id=\"CatId\"]")));
            SelectElement selectelement = new SelectElement(driver.FindElement(By.XPath("//div[contains(@class,'page-container')]/div[contains(@class,'main-content')]/form[contains(@method,'post')]/div[contains(@class,'tab-content m-t-15')]/div[contains(@class,'tab-pane fade show active')]/div[contains(@class,'card')]/div[contains(@class,'card-body')]/div[contains(@class,'row')]/div[contains(@class,'form-group col-6')]/select[contains(@class,'custom-select')]")));

            selectelement.SelectByText(selectText);

            // nhập số lượng tồn
            IWebElement inputStock = driver.FindElement(By.Id("UnitslnStock"));
            inputStock.SendKeys(stock);

            // chọn hình
            //string link =  + upload;
            IWebElement btnUpload = driver.FindElement(By.Name("fThumb"));
            btnUpload.SendKeys(upload);
            // select public
            bool ktraPL = false;
            if (congTacPL == "Off")
                ktraPL = true;
            IWebElement btnSelectPublic = driver.FindElement(By.XPath("//*[@id=\"product-edit-basic\"]/div/div/div[7]/div/div[1]/div"));
           /* Console.WriteLine(btnSelectPublic.Selected);*/ // Các toggle button mặc định là false
            if (btnSelectPublic.Selected != ktraPL)
                btnSelectPublic.Click();

            // click create
            IWebElement btnClickCreate = driver.FindElement(By.CssSelector("button[class='btn btn-primary']"));
            //div[contains(@class,'page-container')]/div[contains(@class,'main-content')]/form[contains(@method,'post')]/div[contains(@class,'page-header no-gutters has-tab')]/div[contains(@class,'d-md-flex m-b-15 align-items-center justify-content-between')]/div[contains(@class,'m-b-15')]/button[contains(@class,'btn btn-primary')]
            //IWebElement btnClickCreate = driver.FindElement(By.XPath("//div[contains(@class,'page-container')]/div[contains(@class,'main-content')]/div[contains(@class,'card')]/div[contains(@class,'card-body')]/div[contains(@class,'row m-b-30')]/div[contains(@class,'col-lg-4 text-right')]/a[contains(@class,'btn btn-primary')]"));
            //IWebElement btnClickCreate = driver.FindElement(By.XPath("//div[contains(@class,'page-container')]/div[contains(@class,'main-content')]/form[contains(@method,'post')]/div[contains(@class,'page-header no-gutters has-tab')]/div[contains(@class,'d-md-flex m-b-15 align-items-center justify-content-between')]/div[contains(@class,'m-b-15')]/button[contains(@class,'btn btn-primary')]"));
            btnClickCreate.Click();


            bool isErrorMessageDisplayed = true;

            int _price = 0;
            int _discount = 0;
            int _stock = 0;
            if (!checkCharater.CheckStrNumberCharacters(price) || !checkCharater.CheckStrNumberCharacters(discount) || !checkCharater.CheckStrNumberCharacters(stock))
            {
                isErrorMessageDisplayed = false;
            }
            else
            {
                _price = int.Parse(price);
                _discount = int.Parse(discount);
                _stock = int.Parse(stock);
            }

            if (tenSP == "null" || desc == "null" || price == "null" || stock == "null" || upload == "null")
            {
                isErrorMessageDisplayed = false;
            }
            else if (!checkCharater.ContainsStrCharacterAndNumber(tenSP))
            {
                isErrorMessageDisplayed = false;
            }
            else if (checkCharater.CheckSpaceCharacters(tenSP) || checkCharater.CheckSpaceCharacters(desc) || checkCharater.CheckSpaceCharacters(price)
                || checkCharater.CheckSpaceCharacters(stock) || checkCharater.CheckSpaceCharacters(discount))
            {
                isErrorMessageDisplayed = false;
            }
            else if (checkCharater.CheckFile(upload))
            {
                isErrorMessageDisplayed = false;
                Console.WriteLine("Đuôi file không đúng");

            }
            else if (tenSP.Length > 80 || desc.Length < 5)
            {
                isErrorMessageDisplayed = false;
            }
            else if (_price < 0)
            {
                isErrorMessageDisplayed = false;
            }
            else if (_discount < 0 || _discount > 100)
            {
                isErrorMessageDisplayed = false;
            }
            else if (_stock < 1 || _stock > 2000)
            {
                isErrorMessageDisplayed = false;
            }


            Console.WriteLine(isErrorMessageDisplayed);

            if (isErrorMessageDisplayed == true)
            {
                int collumns = driver.FindElements(By.XPath("//div[contains(@class,'page-container')]/div[contains(@class,'main-content')]/div[contains(@class,'card')]/div[contains(@class,'card-body')]/div[contains(@class,'table-responsive')]/table[contains(@class,'table table-hover e-commerce-table')]/thead/tr/th")).Count;
                int rows = driver.FindElements(By.XPath("//div[contains(@class,'page-container')]/div[contains(@class,'main-content')]/div[contains(@class,'card')]/div[contains(@class,'card-body')]/div[contains(@class,'table-responsive')]/table[contains(@class,'table table-hover e-commerce-table')]/tbody/tr")).Count;

                //for (int i = 1; i < rows; i++) { 
                //    for (int j = 1; j < collumns; i++)
                //    {
                //        String data = driver
                //    }
                //}

                //Lấy dòng 1
                //div[contains(@class,'page-container')]/div[contains(@class,'main-content')]/div[contains(@class,'card')]/div[contains(@class,'card-body')]/div[contains(@class,'table-responsive')]/table[contains(@class,'table table-hover e-commerce-table')]/tbody/tr[1]

                IWebElement tableElement = driver.FindElement(By.XPath("//div[contains(@class,'page-container')]/div[contains(@class,'main-content')]/div[contains(@class,'card')]/div[contains(@class,'card-body')]/div[contains(@class,'table-responsive')]/table[contains(@class,'table table-hover e-commerce-table')]/tbody"));

                //IWebElement tableR1 = tableElement.FindElements(By.TagName("tr"))[1];
                //Console.WriteLine("Nó đây nè" + tableR1.Text);

                IList<IWebElement> tableRow = tableElement.FindElements(By.TagName("tr"));
                IList<IWebElement> rowTD;
                IWebElement detailElement = null;
                bool found = false;
                foreach (IWebElement row in tableRow)
                {
                    //Duyệt từng dòng và rowTD là danh sách dữ liệu từng ô của từng dòng đang duyệt
                    rowTD = row.FindElements(By.TagName("td"));

                    Console.WriteLine("" + row.Text);
                    //Console.WriteLine("" + rowTD[0].Text);
                    //Console.WriteLine("" + rowTD[7].Text);
                    //Console.WriteLine("" + rowTD[2].Text);

                    for (int i = 1; i < collumns;)
                    {
                        if (rowTD[i].Text.Equals(tenSP))
                        {
                            Console.WriteLine("Có:" + rowTD[i].Text);
                            found = true;
                            detailElement = rowTD[7].FindElement(By.TagName("a"));
                            break;
                        }
                        else
                            i++;
                    }
                    if (found)
                    {
                        break;
                    }
                }
                Thread.Sleep(2000);
                if (found)
                {
                    detailElement.Click();
                    Thread.Sleep(2000);
                    IWebElement tableElementDetail = driver.FindElement(By.XPath("//div[contains(@class,'page-container')]/div[contains(@class,'main-content')]/div[contains(@class,'card-body')]/div[contains(@class,'table-responsive')]/table[contains(@class,'product-info-table m-t-20')]/tbody"));
                    IList<IWebElement> tableRowDetail = tableElementDetail.FindElements(By.TagName("tr"));
                    IList<IWebElement> rowTDDetail;
                    IWebElement rowTDetailTenSP = tableRowDetail[1].FindElements(By.TagName("td"))[1];
                    IWebElement rowTDetailDesc = tableRowDetail[2].FindElements(By.TagName("td"))[1];
                    IWebElement rowTDetailPrice = tableRowDetail[3].FindElements(By.TagName("td"))[1];
                    IWebElement rowTDetailDiscount = tableRowDetail[4].FindElements(By.TagName("td"))[1];
                    IWebElement rowTDetailSelectText = tableRowDetail[8].FindElements(By.TagName("td"))[1];
                    if (!rowTDetailTenSP.Text.Equals(tenSP))
                    {
                        Console.WriteLine("Không Có:" + rowTDetailTenSP.Text);
                        isErrorMessageDisplayed = false;
                        found = false;
                    }
                    if (!rowTDetailDesc.Text.Equals(desc))
                    {
                        Console.WriteLine("Không Có:" + rowTDetailDesc.Text);
                        isErrorMessageDisplayed = false;
                        found = false;

                    }
                    if (!rowTDetailPrice.Text.Equals(price))
                    {
                        Console.WriteLine("Không Có:" + rowTDetailPrice.Text);
                        isErrorMessageDisplayed = false;
                        found = false;

                    }
                    if (!rowTDetailDiscount.Text.Equals(discount))
                    {
                        Console.WriteLine("Không Có:" + rowTDetailDiscount.Text);
                        isErrorMessageDisplayed = false;
                        found = false;
                    }
                    if (!rowTDetailSelectText.Text.Equals(selectText))
                    {
                        Console.WriteLine("Không Có:" + rowTDetailSelectText.Text);
                        isErrorMessageDisplayed = false;
                        found = false;
                    }
                    
                    if(found)
                    {

                        // vào trang khách hàng
                        driver.Navigate().GoToUrl("https://localhost:5001");
                        Thread.Sleep(1000);

                        // click tất cả sản phẩm
                        IWebElement btnClickDMSP = driver.FindElement(By.XPath("/html/body/div[1]/header/div[3]/div/div/div/div/div/nav/ul/li[3]/a"));
                        btnClickDMSP.Click();

                        //lấy cái sản phẩm đầu tiên
                        IWebElement tableElement1 = driver.FindElement(By.Id("list-view"));

                        IWebElement tableR1 = tableElement1.FindElement(By.CssSelector("#list-view > div"));

                        IWebElement tableR2 = tableR1.FindElements(By.TagName("div"))[1];

                        IWebElement tableR2Detail = tableR2.FindElement(By.LinkText(tenSP));

                        if (!tableR2Detail.Text.Equals(tenSP))
                        {
                            Console.WriteLine("Không Có:" + tableR2Detail.Text);
                            isErrorMessageDisplayed = false;
                        }
                        else
                        {
                            tableR2Detail.Click();
                        }

                    }
                    Thread.Sleep(5000);
                }
            }
            Console.WriteLine(isErrorMessageDisplayed);
            //string actualResult = "True";
            //if (isErrorMessageDisplayed == false)
            //{
            //    actualResult = "Fail";
            //}
            //else if (isErrorMessageDisplayed == true)
            //{
            //    actualResult = "Pass";
            //}
            //Console.WriteLine(actualResult);
            UpdateExcel(isErrorMessageDisplayed);
            //driver.Quit();
            Assert.AreEqual(_expectedResult, isErrorMessageDisplayed);
        }
        public static IEnumerable<object[]> ReadExcel(string filePath)
        {

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            FileInfo file = new FileInfo(filePath);
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet excelWorksheet = package.Workbook.Worksheets["SanPham"];
                int soDong = excelWorksheet.Dimension.End.Row;
                for (int i = 2; i <= soDong; i++)
                {
                    yield return new object[] {
                        excelWorksheet.Cells[i,2].Value?.ToString().Trim(),
                        excelWorksheet.Cells[i,3].Value?.ToString().Trim(),
                        excelWorksheet.Cells[i,4].Value?.ToString().Trim(),
                        excelWorksheet.Cells[i,5].Value?.ToString().Trim(),
                        excelWorksheet.Cells[i,6].Value?.ToString().Trim(),
                        excelWorksheet.Cells[i,7].Value?.ToString().Trim(),
                        excelWorksheet.Cells[i,8].Value?.ToString().Trim(),
                        excelWorksheet.Cells[i,9].Value?.ToString().Trim(),
                        excelWorksheet.Cells[i,10].Value?.ToString().Trim()
                    };

                }
            }
        }
        public static void UpdateExcel(bool isErrorMessageDisplayed)
        {
            string filePath = @"D:\TestCase.xlsx";
            FileInfo file = new FileInfo(filePath);
            string actualResult = "True";
            if (isErrorMessageDisplayed == false)
            {
                actualResult = "Fail";
            }
            else if (isErrorMessageDisplayed == true)
            {
                actualResult = "Pass";
            }

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["SanPham"];

                int soDong = worksheet.Dimension.End.Row;

                for (int i = 2; i <= soDong; i++)
                {
                    worksheet.Cells[i, 11].Value = actualResult;
                }
                package.Save();
            }
        }

    }
}
