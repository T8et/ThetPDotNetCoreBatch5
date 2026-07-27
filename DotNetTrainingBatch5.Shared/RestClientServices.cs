using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.Shared
{
    public class RestClientServices : IRestClientServices
    {
        private readonly RestClient restClient;

        public RestClientServices(RestClient restClient)
        {
            this.restClient = restClient;
        }

        public async Task<T> SendAsync<T>(string url, ReqType method, object? data = null)
        {

            RestRequest request = new RestRequest(url, (Method)method);

            if (data != null)
            {
                request.AddJsonBody(data);
            }

            var response = await restClient.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(response.Content)!;
            }
            else
            {
                throw new Exception($"Request failed with status code: {response.StatusCode}");
            }
        }
    }
}
