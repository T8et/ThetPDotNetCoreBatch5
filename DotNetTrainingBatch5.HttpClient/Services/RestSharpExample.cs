using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DotNetTrainingBatch5.ConsoClient.Services;

public class RestSharpExample
{
    private readonly RestClient _http;
    private readonly string EndPoints = "https://jsonplaceholder.typicode.com/users";
    public RestSharpExample()
    {
        _http = new RestClient();
    }

    public async Task Read()
    {
        RestRequest request = new RestRequest(EndPoints, Method.Get);
        var res = await _http.ExecuteAsync(request);
        if (res.IsSuccessStatusCode)
        {
            string jsonRes = res.Content!;
            Console.WriteLine(jsonRes);
        }
        else
        {
            Console.WriteLine("Invalid Request");
        }
    }

    public async Task ReadById(int id)
    {
        RestRequest request = new RestRequest($"{EndPoints}/{id}", Method.Get);
        var res = await _http.ExecuteAsync(request);

        if (res.IsSuccessStatusCode)
        {
            string jsonRes = res.Content!;
            Console.WriteLine(jsonRes);
        }
        else
        {
            Console.WriteLine("Invalid Request");
        }
    }

    public async Task Create(int id, string title, string body)
    {
        ResObj response = new ResObj()
        {
            userId = id,
            title = title,
            body = body
        };

        RestRequest res = new RestRequest(EndPoints, Method.Post);
        res.AddJsonBody(response);

        //var json_res = JsonConvert.SerializeObject(response);
        //var http_ctent = new StringContent(json_res, Encoding.UTF8, Application.Json);
        var create_res = await _http.ExecuteAsync(res);
        if (create_res.IsSuccessStatusCode)
        {
            string jsonRes = create_res.Content!;
            Console.WriteLine(jsonRes);
        }
        else
        {
            Console.WriteLine("Creation Fail!");
        }
    }

    public async Task Update(int id, int uid, string title, string body)
    {
        ResObj response = new ResObj()
        {
            userId = uid,
            title = title,
            body = body
        };

        RestRequest res = new RestRequest($"{EndPoints}/{id}", Method.Put);
        res.AddJsonBody(response);
        //var json_res = JsonConvert.SerializeObject(response);
        //var http_ctent = new StringContent(json_res, Encoding.UTF8, Application.Json);
        var create_res = await _http.ExecuteAsync(res);
        if (create_res.IsSuccessStatusCode)
        {
            string jsonRes = create_res.Content!;
            Console.WriteLine(jsonRes);
        }
        else
        {
            Console.WriteLine("Update Fail!");
        }
    }

    public async Task Delete(int id)
    {
        RestRequest res = new RestRequest($"{EndPoints}/{id}", Method.Delete);
        var create_res = await _http.ExecuteAsync(res);
        if (create_res.IsSuccessStatusCode)
        {
            string jsonRes = create_res.Content!;
            Console.WriteLine(jsonRes);
            Console.WriteLine("Delete Success");
        }
        else
        {
            Console.WriteLine("Delete Fail!");
        }
    }
}