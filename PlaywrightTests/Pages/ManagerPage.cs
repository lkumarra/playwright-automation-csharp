using Microsoft.Playwright;

namespace PlaywrightTests.Pages;

public class ManagerPage
{
    private readonly IPage _page;

    public ManagerPage(IPage page) => _page = page;

    private ILocator WelcomeMessageLocator => _page.Locator("marquee.heading3");
    private ILocator ManagerIdLocator => _page.Locator("tr.heading3");
    private ILocator MenuOptionsLocator => _page.Locator("ul.menusubnav li");
    private ILocator NewCustomerLocator => _page.GetByText("New Customer");

    public async Task<string?> GetWelcomeMessage() => await WelcomeMessageLocator.TextContentAsync();
    public async Task<string?> GetManagerId() => await ManagerIdLocator.TextContentAsync();
    public async Task<List<string>> GetAllMenuOptions()
    {
        await MenuOptionsLocator.Last.EvaluateAsync("el => el");
        return (await MenuOptionsLocator.AllTextContentsAsync()).ToList();
    }

    public async Task<IPage> ClickOnNewCustomer()
    {
        var tcs = new TaskCompletionSource<IPage>();
        _page.Context.Page += (_, newPage) => tcs.TrySetResult(newPage);
        await NewCustomerLocator.ClickAsync(new LocatorClickOptions { Modifiers = new[] { KeyboardModifier.Meta } });
        var page = await tcs.Task;
        await page.WaitForLoadStateAsync();
        return page;
    }
}
