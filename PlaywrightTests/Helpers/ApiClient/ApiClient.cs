using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace PlaywrightTests.Helpers.ApiClient;

public class ApiResponse<T>
{
    public int StatusCode { get; set; }
    public IDictionary<string, IEnumerable<string>>? Headers { get; set; }
    public T? Body { get; set; }
}

public enum Methods
{
    GET,
    POST,
    PUT,
    DELETE,
    PATCH,
    HEAD
}

public class ApiRequest
{
    public string Url { get; set; } = string.Empty;
    public Methods Method { get; set; }
    public IDictionary<string, string>? Params { get; set; }
    public IDictionary<string, string>? Headers { get; set; }
    public object? Body { get; set; }
}

public class ApiClient : IAsyncDisposable
{
    private readonly HttpClient _httpClient;

    private ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public static Task<ApiClient> CreateAsync(string baseUrl, IDictionary<string, string>? defaultHeaders = null)
    {
        var client = new HttpClient { BaseAddress = new Uri(baseUrl) };
        if (defaultHeaders != null)
        {
            foreach (var kv in defaultHeaders)
                client.DefaultRequestHeaders.TryAddWithoutValidation(kv.Key, kv.Value);
        }
        return Task.FromResult(new ApiClient(client));
    }

    public async Task<ApiResponse<T>> SendAsync<T>(ApiRequest request)
    {
        var url = request.Url;
        if (request.Params != null && request.Params.Count > 0)
        {
            var query = string.Join('&', request.Params.Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value)}"));
            url = url.Contains('?') ? $"{url}&{query}" : $"{url}?{query}";
        }

        using var httpReq = new HttpRequestMessage(new HttpMethod(request.Method.ToString()), url);

        if (request.Headers != null)
        {
            foreach (var kv in request.Headers)
                httpReq.Headers.TryAddWithoutValidation(kv.Key, kv.Value);
        }

        if (request.Body != null && (request.Method == Methods.POST || request.Method == Methods.PUT || request.Method == Methods.PATCH))
        {
            var json = JsonSerializer.Serialize(request.Body);
            httpReq.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        using var resp = await _httpClient.SendAsync(httpReq);
        var apiResp = new ApiResponse<T>
        {
            StatusCode = (int)resp.StatusCode,
            Headers = resp.Headers.ToDictionary(h => h.Key, h => h.Value)
        };

        if (resp.Content != null)
        {
            var text = await resp.Content.ReadAsStringAsync();
            if (!string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    apiResp.Body = JsonSerializer.Deserialize<T>(text, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                catch
                {
                    // ignore deserialization errors
                }
            }
        }

        return apiResp;
    }

    public ValueTask DisposeAsync()
    {
        _httpClient.Dispose();
        return ValueTask.CompletedTask;
    }
}
