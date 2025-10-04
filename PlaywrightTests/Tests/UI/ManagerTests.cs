using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlaywrightTests.Pages;
using PlaywrightTests.Constants;

namespace PlaywrightTests.Tests.UI;

[TestClass]
public class ManagerTests : BaseTest
{
    [TestMethod]
    public async Task ManagerLoginAndVerify_ShouldWork()
    {
        if (Environment.GetEnvironmentVariable("PLAYWRIGHT_RUN_UI") != "1")
        {
            Assert.Inconclusive("UI tests are disabled. Set PLAYWRIGHT_RUN_UI=1 to enable.");
            return;
        }
        Assert.IsNotNull(Page);
        var credPage = new CredPage(Page!);
        var creds = await credPage.CaptureUserNameAndPassword(CONSTANTS.EMAIL);
        var loginPage = new LoginPage(Page!);
        await loginPage.Login(creds.UserName ?? string.Empty, creds.Password ?? string.Empty);
        var managerPage = new ManagerPage(Page!);
        var welcome = await managerPage.GetWelcomeMessage();
        var managerId = await managerPage.GetManagerId();
        var menu = await managerPage.GetAllMenuOptions();
        // Basic assertions to ensure things are present (if page loaded correctly)
        Assert.IsNotNull(welcome);
        Assert.IsNotNull(managerId);
        Assert.IsNotNull(menu);
    }
}
