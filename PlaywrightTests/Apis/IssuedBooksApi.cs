using PlaywrightTests.Helpers.ApiClient;
using PlaywrightTests.Models;

namespace PlaywrightTests.Apis;

public class IssuedBooksApi
{
    private readonly string _baseUrl;

    public IssuedBooksApi(string baseUrl = "http://localhost:8980/api")
    {
        _baseUrl = baseUrl;
    }

    public async Task<ApiResponse<List<IssuedBooksModel>>> GetIssuedBooksAsync()
    {
        await using var client = await ApiClient.CreateAsync(_baseUrl);
        var req = new ApiRequest { Url = "/api/v1/managebook/issued-books", Method = Methods.GET };
        return await client.SendAsync<List<IssuedBooksModel>>(req);
    }
}
