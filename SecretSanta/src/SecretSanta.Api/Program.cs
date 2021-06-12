using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SecretSanta.Data;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System.Linq;
using System;

namespace SecretSanta.Api
{
    public class Program
    {

        private static string Template { get; } = "[{Timestamp} {Level:u4}] ({Category}: {SourceContext}) {Message:lj}{NewLine}{Exception}";
        public static void Main(string[] args)
        {
            // adapted from https://nblumhardt.com/2019/10/serilog-in-aspnetcore-3/
            // part 1 of 2 stage init, used to log startup https://github.com/serilog/serilog-aspnetcore#two-stage-initialization
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Category", "Startup")
                .MinimumLevel.Information()
                .WriteTo.Console(
                    outputTemplate: Template,
                    theme: AnsiConsoleTheme.Code)
                .CreateBootstrapLogger();

            Serilog.ILogger Logger = Log.Logger.ForContext<Program>();

            if (args.Any(s => s.Contains("--list")))
            {
                Logger.Information("\n\n" +
                    "   --setdb=\"path\\to\\database.db\"\n" +
                    "       Sets the connection string for the database\n\n" +
                    "   --setlog=\"path\\to\\log.txt\"\n" +
                    "       Sets the logfile\n\n" +
                    "   --DeploySampleData\n" +
                    "       CLEARS database and repopulates with seed data\n");
                Log.CloseAndFlush();
                return;
            }
            if(args.Any(s => s.Contains("DeploySampleData"))){
                Data.DbContext.DeploySampleData();
            }

            Logger.Information("Use --list to display CLI args");

            try
            {
                Logger.Information("Building host");
                CreateHostBuilder(args)
                    .Build()
                    .Run();
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex, "Host build Failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }



        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true);
                    config.AddEnvironmentVariables();
                    config.AddCommandLine(args, new Dictionary<string, string> {
                        { "--setdb", "ConnectionStrings:DbConnection" },
                        { "--setlog", "ConnectionStrings:LogConnection" }
                    });
                })
                // part 2 of 2 stage init, completely overwrites stage1
                .UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .MinimumLevel.Information()
                    .Enrich.WithProperty("Category", "API")
                    .Enrich.FromLogContext()
                    .WriteTo.Console(
                        outputTemplate: Template,
                        theme: AnsiConsoleTheme.Code)
                    .WriteTo.File(
                        context.Configuration.GetConnectionString("LogConnection"),
                        restrictedToMinimumLevel: LogEventLevel.Information,
                        outputTemplate: Template)
                )
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
