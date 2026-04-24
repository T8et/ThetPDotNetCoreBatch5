// See https://aka.ms/new-console-template for more information
using DotNetTrainingBatch5.ConsoClient.Services;

Console.WriteLine("Hello, World!");

ResultResponse response = new ResultResponse();
await response.Read();
await response.ReadById(102);

