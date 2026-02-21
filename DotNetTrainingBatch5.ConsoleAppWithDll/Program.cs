// See https://aka.ms/new-console-template for more information
using DotNetTrainingBatch5.Database.Models;

Console.WriteLine("Hello, World!");
Console.ReadKey();

AppDBContext db = new AppDBContext();
var list = db.TblBlogs.ToList();

foreach(var item in list)
{
    Console.WriteLine(item.BlogId + "|" + item.BlogTitle + "|" + item.BlogAuthor + "|" + item.BlogContent);
}
