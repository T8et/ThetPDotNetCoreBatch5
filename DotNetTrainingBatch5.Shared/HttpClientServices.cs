using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.Shared;

public class HttpClientServices : IRestClientServices
{
    private readonly HttpClient _httpClient;

    public HttpClientServices(string domainUrl)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(domainUrl);
    }

    public async Task<T> SendAsync<T>(string url, ReqType reqType, object? data = null)
    {
        var request = new HttpRequestMessage(new HttpMethod(reqType.ToString()), url);

        if (data != null)
        {
            request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        }

        var response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseContent)!;

        }
        return default!;
    }
}

public enum ReqType
{
    GET,
    POST,
    PUT,
    PATCH,
    DELETE
}
