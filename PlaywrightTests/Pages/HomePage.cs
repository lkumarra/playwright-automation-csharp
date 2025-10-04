using Microsoft.Playwright;

namespace PlaywrightTests.Pages;

public class HomePage
{
    private readonly IPage _page;

    public HomePage(IPage page)
    {
        _page = page;
    }

    public async Task<string> GetTitleAsync() => await _page.TitleAsync();

    public async Task GotoAsync(string url) => await _page.GotoAsync(url);
}
