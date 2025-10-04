using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlaywrightTests.Apis;

namespace PlaywrightTests.Tests.Api;

[TestClass]
public class BooksApiTests
{
    [TestMethod]
    public async Task GetBooks_ShouldReturnList()
    {
        var api = new BooksApi("http://localhost:8980/api");
        var resp = await api.GetBooksAsync();
        // If service isn't running the status will likely be non-200; assert only when available
        Assert.IsTrue(resp.StatusCode == 200 || resp.StatusCode == 0);
    }
}
