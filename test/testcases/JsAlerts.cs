namespace xtramiles;

using NUnit.Allure.Core;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;

[TestFixture]
[AllureNUnit]
class JsAlerts : BasePage
{
    [SetUp]
    public void SetUp()
    {
        InitiateDriver();
        NavigateSite("https://the-internet.herokuapp.com/javascript_alerts");
    }

    [TearDown]
    public void TearDown()
    {
        CloseDriver();
    }
    [Test, Order(1)]
    public void VerifyAcceptFunctionalityOnJsAlert()
    {
        Click(By.XPath("//*[@id=\"content\"]/div/ul/li[1]/button"));
        AcceptAlert();
        string expected = "You successfully clicked an alert";
        string actual = GetText(By.Id("result"));
        Assert.IsTrue(string.Equals(actual, expected, StringComparison.OrdinalIgnoreCase));
    }
    [Test, Order(2)]
    public void VerifyOkFunctionalityOnJsConfirmation()
    {
        Click(By.XPath("//*[@id=\"content\"]/div/ul/li[2]/button"));
        AcceptAlert();
        string expected = "You clicked: Ok";
        string actual = GetText(By.Id("result"));
        Assert.IsTrue(string.Equals(actual, expected, StringComparison.OrdinalIgnoreCase));
        
    }
    [Test, Order(3)]
    public void VerifyCancelFunctionalityOnJsConfirmation()
    {
        Click(By.XPath("//*[@id=\"content\"]/div/ul/li[2]/button"));
        DismissAlert();
        string expected = "You clicked: Cancel";
        string actual = GetText(By.Id("result"));
        Assert.IsTrue(string.Equals(actual, expected, StringComparison.OrdinalIgnoreCase));
    }
    [Test, Order(4)]
    public void VerifyThatUserInputOnPromptIsEqualWithResult()
    {
        Click(By.XPath("//*[@id=\"content\"]/div/ul/li[3]/button"));
        string str = "Bumblebee";
        TypeOnAlert(str);
        AcceptAlert();
        string actual = GetText(By.Id("result"));
        string expected = str;
        Assert.AreEqual(actual, "You entered: " + expected);
    }
    [Test, Order(5)]
    public void VerifyThatFooterLinkIsWorking()
    {
        Click(By.XPath("//*[@id=\"page-footer\"]/div/div/a"));
        GoToNewWindows();
        string expected = "https://elementalselenium.com/";
        Assert.AreEqual(GetCurrentUrl(), expected);
    }

}
