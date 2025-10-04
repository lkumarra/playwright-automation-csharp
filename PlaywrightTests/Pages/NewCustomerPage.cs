using Microsoft.Playwright;

namespace PlaywrightTests.Pages;

public class NewCustomerPage
{
    private readonly IPage _page;
    public NewCustomerPage(IPage page) => _page = page;

    public async Task<string> GetPageTitle() => await _page.TitleAsync();
}
