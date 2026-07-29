using Microsoft.Data.SqlClient;
using Serilog;
using Serilog.Sinks.MSSqlServer;

//Normal Logger Configuration
//Log.Logger = new LoggerConfiguration()
//    .WriteTo.Console()
//    .CreateLogger();

//Rolling File Logger Configuration
//Log.Logger = new LoggerConfiguration()
//    .WriteTo.Console()
//    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
//    .CreateLogger();

using (var conn = new SqlConnection("Server=KEMPO;Database=DotNetTrainingBatch5;User Id=sa;Password=p@ssw0rd;TrustServerCertificate=True"))
{
    conn.Open(); // If this fails, fix connection string
}


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.MSSqlServer(
        connectionString: "Server=KEMPO;Database=DotNetTrainingBatch5;User Id=sa;Password=p@ssw0rd;TrustServerCertificate=True",
        sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true })
    .CreateLogger();

//Console.ReadKey();
//12min
Log.Information("Starting up");

try
{
    int x = 0;
    Log.Information("Hello, World!");
    x = x / 0; // This will throw a DivideByZeroException
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}