using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlaywrightTests.Tests;

[TestClass]
public class BaseTest
{
    protected static IPlaywright? PlaywrightInstance;
    protected IBrowser? Browser;
    protected IBrowserContext? Context;
    protected IPage? Page;

    [AssemblyInitialize]
    public static async Task AssemblyInit(TestContext context)
    {
        PlaywrightInstance = await Microsoft.Playwright.Playwright.CreateAsync();
    }

    [TestInitialize]
    public async Task Setup()
    {
        if (PlaywrightInstance == null)
            PlaywrightInstance = await Microsoft.Playwright.Playwright.CreateAsync();

        Browser = await PlaywrightInstance.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        Context = await Browser.NewContextAsync();
        Page = await Context.NewPageAsync();
    }

    [TestCleanup]
    public async Task TearDown()
    {
        if (Page != null) await Page.CloseAsync();
        if (Context != null) await Context.CloseAsync();
        if (Browser != null) await Browser.CloseAsync();
    }

    [AssemblyCleanup]
    public static void AssemblyCleanup()
    {
        // Playwright IPlaywright does not need explicit dispose in this simple scaffold;
        // leaving as-is to keep sample minimal. In a larger framework, call PlaywrightInstance.Dispose();
    }
}
