using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.ConsoClient.Services;

public class ResultResponse
{
    private readonly HttpClient _http;
    private readonly string EndPoints = "https://jsonplaceholder.typicode.com/posts";
    public ResultResponse()
    {
        _http = new HttpClient();
    }
    
    public async Task Read()
    {
        var res = await _http.GetAsync(EndPoints);

        if (res.IsSuccessStatusCode)
        {
            string jsonRes = await res.Content.ReadAsStringAsync();
            Console.WriteLine(jsonRes);
        }
        else
        {
            Console.WriteLine("Invalid Request");
        }
    }

    public async Task ReadById(int id)
    {
        var res = await _http.GetAsync($"{EndPoints}/{id}");

        if (res.IsSuccessStatusCode)
        {
            string jsonRes = await res.Content.ReadAsStringAsync();
            Console.WriteLine(jsonRes);
        }
        else
        {
            Console.WriteLine("Invalid Request");
        }
    }
}
