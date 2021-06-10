using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SecretSanta.Data;
using System;

namespace SecretSanta.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string deploy = "";
            if(args.Length > 0){
                deploy = args[0];
            }
            if(deploy == "--DeploySampleData"){

                Console.WriteLine("Deploying Data...");
                using (var dbcontext = new DbContext()){
                    dbcontext.Database.EnsureDeleted();
                    dbcontext.Database.EnsureCreated();
                    foreach(User i in DbData.Users()){
                        dbcontext.Users.Add(i);
                    }
                    foreach(Group i in DbData.Groups()){
                        dbcontext.Groups.Add(i);
                    }
                    foreach(Gift i in DbData.Gifts()){
                        dbcontext.Gifts.Add(i);
                    }
                    dbcontext.SaveChangesAsync();
                    Console.WriteLine("Data Deployed! Score!");
                }
            }
            var host = CreateHostBuilder(args).Build();

            host.Run();
        }

        

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
