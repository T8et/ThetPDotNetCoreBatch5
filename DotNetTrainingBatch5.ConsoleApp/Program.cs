// See https://aka.ms/new-console-template for more information
using DotNetTrainingBatch5.ConsoleApp;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");
//Console.WriteLine("");
Console.ReadKey();

// md = markdown

// C# database to connet

// ADO.ent
// Dapper (ORM - Object Relational Mapping)
// EF Core/ Entity Framework - ORM

// C# -> query

// nuget 

// Ctrl + .
// only ADO.net need to close connection

//SqlDataAdapter >> for group dataset (not many amount number should use)
//SqlDataReader  >> for performance, can use line by line read (not for some group of dataset usage)

//24min

//AdoDotNetExample adn = new AdoDotNetExample();
//adn.Extract();
//adn.Create();
//adn.Read();
//adn.Update();
//adn.Delete();

DapperExample dp = new DapperExample();
dp.Read();
//dp.Create("ThetPan", "ThetPan Blog", "Test Case");
//dp.Update("Kempo Title1","Kempo","Test Kem",12);
//dp.Delete(11);