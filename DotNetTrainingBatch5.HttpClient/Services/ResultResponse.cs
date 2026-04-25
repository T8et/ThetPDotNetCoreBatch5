using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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

    public async Task Create(int id, string title,string body)
    {
        ResObj response = new ResObj()
        {
            userId = id,
            title = title,
            body = body
        };
        
        var json_res = JsonConvert.SerializeObject(response);
        var http_ctent = new StringContent(json_res, Encoding.UTF8, Application.Json);
        var create_res = await _http.PostAsync(EndPoints, http_ctent);
        if (create_res.IsSuccessStatusCode) 
        {
            string jsonRes = await create_res.Content.ReadAsStringAsync();
            Console.WriteLine(jsonRes);
        }
        else
        {
            Console.WriteLine("Creation Fail!");
        }
    }

    public async Task Update(int id,int uid, string title, string body)
    {
        ResObj response = new ResObj()
        {
            userId = uid,
            title = title,
            body = body
        };

        var json_res = JsonConvert.SerializeObject(response);
        var http_ctent = new StringContent(json_res, Encoding.UTF8, Application.Json);
        var create_res = await _http.PutAsync($"{EndPoints}/{id}", http_ctent);
        if (create_res.IsSuccessStatusCode)
        {
            string jsonRes = await create_res.Content.ReadAsStringAsync();
            Console.WriteLine(jsonRes);
        }
        else
        {
            Console.WriteLine("Update Fail!");
        }
    }

    public async Task Delete(int id)
    {
        var create_res = await _http.DeleteAsync($"{EndPoints}/{id}");
        if (create_res.IsSuccessStatusCode)
        {
            string jsonRes = await create_res.Content.ReadAsStringAsync();
            Console.WriteLine(jsonRes);
            Console.WriteLine("Delete Success");
        }
        else
        {
            Console.WriteLine("Delete Fail!");
        }
    }
}


public class ResObj
{
    public int userId { get; set; }
    public int id { get; set; }
    public string? title { get; set; }
    public string? body { get; set; }
}

