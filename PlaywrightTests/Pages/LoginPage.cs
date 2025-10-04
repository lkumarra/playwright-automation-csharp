using Microsoft.Playwright;

namespace PlaywrightTests.Pages;

public class LoginPage
{
    private readonly IPage _page;

    public LoginPage(IPage page) => _page = page;

    private ILocator UserNameInput => _page.Locator("[name='uid']");
    private ILocator PasswordInput => _page.Locator("[name='password']");
    private ILocator LoginButton => _page.Locator("[name='btnLogin']");
    private ILocator ResetButton => _page.Locator("[name='btnReset']");

    public async Task EnterUserName(string userName) => await UserNameInput.FillAsync(userName);
    public async Task EnterPassword(string password) => await PasswordInput.FillAsync(password);
    public async Task ClickOnLoginButton() => await LoginButton.ClickAsync();
    public async Task ClickOnResetButton() => await ResetButton.ClickAsync();

    public async Task Login(string userName, string password)
    {
        await EnterUserName(userName);
        await EnterPassword(password.Trim());
        await ClickOnLoginButton();
    }

    public Task<string> GetAlertMessageAsync()
    {
        var tcs = new TaskCompletionSource<string>();
        void Handler(object? sender, IDialog dialog)
        {
            try
            {
                var message = dialog.Message;
                dialog.AcceptAsync().GetAwaiter().GetResult();
                tcs.TrySetResult(message);
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }
            finally
            {
                _page.Dialog -= Handler;
            }
        }

        _page.Dialog += Handler;
        return tcs.Task;
    }
}
