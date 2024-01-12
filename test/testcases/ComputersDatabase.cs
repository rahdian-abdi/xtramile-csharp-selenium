namespace xtramiles;

using System;
using NUnit.Framework;
using OpenQA.Selenium;
using NUnit.Allure.Core;

[TestFixture]
[AllureNUnit]
class ComputersDatabase : BasePage
{
    
    [SetUp]
    public void SetUp()
    {
        InitiateDriver();
        NavigateSite("http://computer-database.gatling.io/computers");
    }
    [TearDown]
    public void TearDown()
    {
        CloseDriver();
    }
    [Test, Order(1)]
    public void VerifyThatFilterByNameIsWorking()
    {
        string expected = "Macbook";
        Type(By.Id("searchbox"), expected);
        Click(By.Id("searchsubmit"));
        for (int i = 1; i <= 10; i++)
        {
            string actual = GetText(By.XPath($"//*[@id=\"main\"]/table/tbody/tr[{i}]/td[1]/a"));
            Assert.IsTrue(actual.Contains(expected, StringComparison.OrdinalIgnoreCase));
        }
        
    }
    [Test, Order(2)]
    public void VerifyThatAscendingNameSortIsWorking()
    {
        List<string> product_list = new();
        for (int i = 1; i <= 10; i++)
        {
            string actual = GetText(By.XPath($"//*[@id=\"main\"]/table/tbody/tr[{i}]/td[1]/a"));
            product_list.Add(actual);
        }
        Assert.IsTrue(IsAscending(product_list));
    }
    [Test, Order(3)]
    public void VerifyThatDescendingNameSortIsWorking()
    {
        Click(By.XPath("//*[@id=\"main\"]/table/thead/tr/th[1]/a"));
        List<string> product_list = new();
        for (int i = 1; i <= 10; i++)
        {
            string actual = GetText(By.XPath($"//*[@id=\"main\"]/table/tbody/tr[{i}]/td[1]/a"));
            product_list.Add(actual);
        }
        Assert.IsTrue(IsDescending(product_list));
    }
    [Test, Order(4)]
    public void VerifyThatDescendingIntroducedSortIsWorking()
    {
        Click(By.XPath("//*[@id=\"main\"]/table/thead/tr/th[2]/a"));
        Click(By.XPath("//*[@id=\"main\"]/table/thead/tr/th[2]/a"));
        List<DateTime> dateTimesList = new();
        for (int i = 1; i <= 10; i++)
        {
            string actual = GetText(By.XPath($"//*[@id=\"main\"]/table/tbody/tr[{i}]/td[2]"));
            if (DateTime.TryParseExact(actual, "dd MMM yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
            {
                dateTimesList.Add(parsedDate);
            }
            else
            {
                Console.WriteLine($"Invalid date format: {actual}");
            }
        }
        Assert.IsTrue(IsDescendingDate(dateTimesList));
    }
     [Test, Order(5)]
    public void VerifyThatDescendingDiscontinuedSortIsWorking()
    {
        Click(By.XPath("//*[@id=\"main\"]/table/thead/tr/th[3]/a"));
        Click(By.XPath("//*[@id=\"main\"]/table/thead/tr/th[3]/a"));
        List<DateTime> dateTimesList = new();
        for (int i = 1; i <= 10; i++)
        {
            string actual = GetText(By.XPath($"//*[@id=\"main\"]/table/tbody/tr[{i}]/td[3]"));
            if (DateTime.TryParseExact(actual, "dd MMM yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
            {
                dateTimesList.Add(parsedDate);
            }
            else
            {
                Console.WriteLine($"Invalid date format: {actual}");
            }
        }
        Assert.IsTrue(IsDescendingDate(dateTimesList));
    }
    [Test, Order(6)]
    public void VerifyThatDescendingCompanySortIsWorking()
    {
        Click(By.XPath("//*[@id=\"main\"]/table/thead/tr/th[4]/a"));
        Click(By.XPath("//*[@id=\"main\"]/table/thead/tr/th[4]/a"));
        List<string> product_list = new();
        for (int i = 1; i <= 10; i++)
        {
            string actual = GetText(By.XPath($"//*[@id=\"main\"]/table/tbody/tr[{i}]/td[4]"));
            product_list.Add(actual);
        }
        Assert.IsTrue(IsDescending(product_list));
    }
    [Test, Order(7)]
    public void CreateComputerWithValidInput()
    {
        string[] expected = {"Bumblebee Computers", "1995-08-07", "1995-08-31", "Apple Inc."};
        Click(By.Id("add"));
        Type(By.Id("name"), expected[0]);
        Type(By.Id("introduced"), expected[1]);
        Type(By.Id("discontinued"), expected[2]);
        SelectDropDown(By.Id("company"), expected[3]);
        Click(By.XPath("//*[@id=\"main\"]/form/div/input"));

        Type(By.Id("searchbox"), expected[0]);
        Click(By.Id("searchsubmit"));
        try
        {
            for(int i=1 ; i<=4 ; i++)
            {
                string actual = GetText(By.XPath($"//*[@id=\"main\"]/table/tbody/tr[1]/td[{i}]/a"));
                Assert.IsTrue(string.Equals(actual, expected[i-1]));
            }
            
        }
        catch (NoSuchElementException)
        {
            
            Assert.IsFalse(true, "Computers not created!");
        }
    }
    [Test, Order(8)]
    public void EditComputerWithValidInput()
    {
        // Make Random String
        int leng = 5;
        string alphanumericCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();
        string randomString = "";

        for(int i=0 ; i<leng ; i++)
        {
            int index = random.Next(alphanumericCharacters.Length);
            char randomChar = alphanumericCharacters[index];
            randomString += randomChar;
        } 


        string[] expected = {randomString, "1995-08-07", "1995-08-31", "Thinking Machines"};
        Click(By.XPath("//*[@id=\"main\"]/table/tbody/tr[1]/td[1]/a"));
        Clear(By.Id("name"));
        Clear(By.Id("introduced"));
        Clear(By.Id("discontinued"));
        Type(By.Id("name"), expected[0]);
        Type(By.Id("introduced"), expected[1]);
        Type(By.Id("discontinued"), expected[2]);
        SelectDropDown(By.Id("company"), expected[3]);
        Click(By.XPath("//*[@id=\"main\"]/form/div/input"));

        Type(By.Id("searchbox"), expected[0]);
        Click(By.Id("searchsubmit"));
        try
        {
            for(int i=1 ; i<=4 ; i++)
            {
                string actual = GetText(By.XPath($"//*[@id=\"main\"]/table/tbody/tr[1]/td[{i}]/a"));
                Assert.IsTrue(string.Equals(actual, expected[i-1]));
            }
        }
        catch (NoSuchElementException)
        {
            
            Assert.IsFalse(true, "Computers not updated!");
        }
    }
    [Test, Order(8)]
    public void DeleteComputerWithValidInput()
    {
        string target = "ASCI Blue Mountain";
        Type(By.Id("searchbox"), target);
        Click(By.Id("searchsubmit"));
        Click(By.XPath("//*[@id=\"main\"]/table/tbody/tr[1]/td[1]/a"));
        Click(By.XPath("//*[@id=\"main\"]/form[2]/input"));

        Type(By.Id("searchbox"), target);
        Click(By.Id("searchsubmit"));
        string actual = GetText(By.XPath("//*[@id=\"main\"]/table/tbody/tr[1]/td[1]/a"));
        try
        {
            Assert.IsTrue(IsDisplayed(By.XPath("//*[@id=\"main\"]/div[2]/em")));
        }
        catch (NoSuchElementException)
        {
            
            Assert.IsFalse(true, "Product is not deleted!");
        }
        Assert.AreNotEqual(actual, target);
    }
}
