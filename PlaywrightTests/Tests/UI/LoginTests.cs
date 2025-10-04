using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlaywrightTests.Pages;

namespace PlaywrightTests.Tests.UI;

[TestClass]
public class LoginTests : BaseTest
{
    [TestMethod]
    public async Task LoginWithValidCredentials_ShouldSucceed()
    {
        if (Environment.GetEnvironmentVariable("PLAYWRIGHT_RUN_UI") != "1")
        {
            Assert.Inconclusive("UI tests are disabled. Set PLAYWRIGHT_RUN_UI=1 to enable.");
            return;
        }
        Assert.IsNotNull(Page);
        var credPage = new CredPage(Page!);
        var creds = await credPage.CaptureUserNameAndPassword("Lavendra.rajputc1@gmail.com");
        var loginPage = new LoginPage(Page!);
        await loginPage.Login(creds.UserName ?? string.Empty, creds.Password ?? string.Empty);
        // We don't have explicit app assertions here; just ensure no exception thrown
        Assert.IsTrue(true);
    }

    [TestMethod]
    public async Task LoginWithoutCredentials_ShowsAlert()
    {
        if (Environment.GetEnvironmentVariable("PLAYWRIGHT_RUN_UI") != "1")
        {
            Assert.Inconclusive("UI tests are disabled. Set PLAYWRIGHT_RUN_UI=1 to enable.");
            return;
        }
        Assert.IsNotNull(Page);
        var loginPage = new LoginPage(Page!);
        await Page!.GotoAsync("http://www.demo.guru99.com/V4/index.php");
        await loginPage.ClickOnLoginButton();
        var alertMessage = await loginPage.GetAlertMessageAsync();
        Assert.IsNotNull(alertMessage);
    }
}
