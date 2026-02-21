# DotNetTrainingBatch5

C# Batch 5 - Sayar Sann Lynn Htun
---------------------------------
Language  >> C#
Framework >> .NET

Console App
Windows Form
ASP.NET CORE WEB API
ASP.NET CORE WEB MVC
Blazor Web Assembly
Blazor Web Server

.NET Framework (1,2,3,3.5,4,4.5,4.6,4.7,4.8)
.NET CORE (1,2,2.2,3,3.1)
.NET >> teach

UI + Business Logic + Data Access => Database

ADO.NET >> CRUD (DataAdapter & DataReader)
Dapper  >> CRUD (Parameter & Model)

This is new version for Batch 5 Homework

Select * from tbl_blog (nolock) = asnotracking() -> efcore (show only commit data)

first user task -> select
second user     -> insert
third user      -> update

commit data    -> sure data
uncommit data  -> not sure data

at Oracle >> only select commit data
thus we need to add commit after update or create (all CRUD)

Day 6 
Ef core database first (manual, auto) / code first

Northwind database


select @@VERSION (check server version)
select @@SERVERNAME (check server name)

dotnet tool install --global dotnet-ef --version 7 (need to run)

dotnet ef dbcontext scaffold "Server=.;Database=DotNetTrainingBatch5;User Id=sa;Password=p@ssw0rd;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDBContext -f




