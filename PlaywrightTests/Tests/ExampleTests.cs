using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlaywrightTests.Tests;

[TestClass]
public class ExampleTests : BaseTest
{
    [TestMethod]
    public async Task OpenExampleDotCom_ShouldContainExample()
    {
        Assert.IsNotNull(Page, "Page must be initialized");
        await Page!.GotoAsync("https://example.com");
        var title = await Page.TitleAsync();
        StringAssert.Contains(title, "Example");
    }
}
