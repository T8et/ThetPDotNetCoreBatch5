// See https://aka.ms/new-console-template for more information
using DotNetTrainingBatch5.JsonObject;
using Newtonsoft.Json;
using System.Net.Http;
using static System.Net.WebRequestMethods;

Console.WriteLine("Hello, World!");

Blog bl = new Blog()
{
    blogid = 1,
    blogtitle = "jsontitle",
    blogauthor = "thetp",
    blogcontent = "thetpcontent"
};

// Encode, Decode, Encryption, Decryption

//string jsonstr = JsonConvert.SerializeObject(bl, Formatting.Indented);
string jsonstr = bl.toJson();
Console.WriteLine(jsonstr);

string jsonstr1 = @"{
	""blogid"": 1,
	""blogtitle"": ""jsontitle"",
	""blogauthor"": ""thetp"",
	""blogcontent"": ""thetpcontent""
}";

var res = JsonConvert.DeserializeObject<Blog>(jsonstr1);
Console.WriteLine(res.blogauthor);



