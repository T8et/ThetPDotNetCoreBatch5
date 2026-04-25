// See https://aka.ms/new-console-template for more information
using DotNetTrainingBatch5.ConsoClient.Services;

Console.WriteLine("Hello, World!");

ResultResponse response = new ResultResponse();
await response.Read();
//await response.ReadById(102);
//await response.Update(100, 1, "hello", "hello test");
//await response.Delete(10);

//await response.Create(102, "New Api", "Api Test");