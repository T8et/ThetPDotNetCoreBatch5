namespace DotNetTrainingBatch5.Shared
{
    public interface IRestClientServices
    {
        Task<T> SendAsync<T>(string url, ReqType method, object? data = null);
    }
}