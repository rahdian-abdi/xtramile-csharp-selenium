using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

[SetUpFixture]
public class BasePage
{
    protected IWebDriver? driver;


    public void InitiateDriver()
    {
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
    }
    public void CloseDriver()
    {
        driver?.Quit();
    }
    public IWebDriver? GetDriver()
    {
        return driver;
    }
    public WebElement? FindBy(By by)
    {
        return (WebElement?)(GetDriver()?.FindElement(by));
    }
    public IList<WebElement>? FindByList(By by)
    {
        var elements = GetDriver()?.FindElements(by);
        if (elements == null)
        {
            return null;
        }

        var list = new List<WebElement>();
        foreach (var element in elements)
        {
            list.Add((WebElement)element);
        }

        return list;
    }
    public void NavigateSite(string url)
    {
        GetDriver()?.Navigate().GoToUrl(url);
    }
    public string? GetCurrentUrl()
    {
        return GetDriver()?.Url;
    }
    public void Click(By by)
    {
        FindBy(by)!.Click();
    }
    public void AcceptAlert()
    {
        IAlert? alert = GetDriver()?.SwitchTo().Alert();
        alert?.Accept();
    }
    public void DismissAlert()
    {
        IAlert? alert = GetDriver()?.SwitchTo().Alert();
        alert?.Dismiss();
    }
    public void Type(By by, string str)
    {
        FindBy(by)!.SendKeys(str);
    }
    public void Clear(By by)
    {
        FindBy(by)!.Clear();
    }
    public void TypeOnAlert(string input)
    {
        IAlert? alert = GetDriver()?.SwitchTo().Alert();
        alert?.SendKeys(input);
    }
    public string GetText(By by)
    {
        return FindBy(by)!.Text;
    }
    public void GoToNewWindows()
    {
        string? currentWindowsHandle = GetDriver()?.CurrentWindowHandle;
        foreach (string windowHandle in GetDriver()?.WindowHandles ?? Enumerable.Empty<string>())
        {
            if (windowHandle != currentWindowsHandle)
            {
                GetDriver()?.SwitchTo().Window(windowHandle);
                break;
            }
        }
    }
    public bool IsAscending(List<String> list)
    {
        return list.SequenceEqual(list.OrderBy(s => s, StringComparer.Ordinal));
    }
    public bool IsDescending(List<String> list)
    {
        return list.SequenceEqual(list.OrderByDescending(s => s, StringComparer.Ordinal));
    }
    public bool IsDescendingDate(List<DateTime> dateStrings)
    {
        return dateStrings
                .OrderByDescending(date => date)
                .SequenceEqual(dateStrings);

    }
    public void SelectDropDown(By by, string selectByText)
    {
        SelectElement select = new(FindBy(by));
        select.SelectByText(selectByText);
    }
    public bool IsDisplayed(By by)
    {
        return FindBy(by)!.Displayed;
    }

}
