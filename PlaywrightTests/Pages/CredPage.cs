using Microsoft.Playwright;
using PlaywrightTests.Models;
using PlaywrightTests.Constants;

namespace PlaywrightTests.Pages;

public class CredPage
{
    private readonly IPage _page;

    public CredPage(IPage page) => _page = page;

    private ILocator EmailLocator => _page.Locator("[name='emailid']");
    private ILocator SubmitButtonLocator => _page.Locator("[name='btnLogin']");
    private ILocator UsernameLocator => _page.Locator("//td[contains(text(), 'User ID')]/following-sibling::td");
    private ILocator PasswordLocator => _page.Locator("//td[contains(text(), 'Password')]/following-sibling::td");

    public async Task EnterEmail(string email) => await EmailLocator.FillAsync(email);
    public async Task ClickOnSubmitButton() => await SubmitButtonLocator.ClickAsync();
    public async Task<string?> GetUserName() => await UsernameLocator.TextContentAsync();
    public async Task<string?> GetPassword() => await PasswordLocator.TextContentAsync();

    public async Task<Credentials> CaptureUserNameAndPassword(string email)
    {
        await _page.GotoAsync(CONSTANTS.CRED_URL);
        await EnterEmail(email);
        await ClickOnSubmitButton();
        var userName = await GetUserName();
        var password = await GetPassword();
        await _page.GotoAsync(CONSTANTS.LOGIN_URL);
        return new Credentials { UserName = userName, Password = password };
    }
}
