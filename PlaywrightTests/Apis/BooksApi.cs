using PlaywrightTests.Helpers.ApiClient;
using PlaywrightTests.Models;

namespace PlaywrightTests.Apis;

public class BooksApi
{
    private readonly string _baseUrl;

    public BooksApi(string baseUrl = "http://localhost:8980/api")
    {
        _baseUrl = baseUrl;
    }

    public async Task<ApiResponse<List<BooksModel>>> GetBooksAsync()
    {
        await using var client = await ApiClient.CreateAsync(_baseUrl);
        var req = new ApiRequest { Url = "/api/v1/books", Method = Methods.GET };
        return await client.SendAsync<List<BooksModel>>(req);
    }
}
