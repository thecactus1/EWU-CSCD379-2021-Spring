using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Extensions.Hosting;
using Serilog.Sinks.SystemConsole.Themes;
using DbContext = SecretSanta.Data.DbContext;
using Serilog.Events;

namespace SecretSanta.Data
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext, IDisposable
    {
        private static string Template { get; }
            = "[{Timestamp} {Level:u4}] ({Category}: {SourceContext}) {Message:lj}{NewLine}{Exception}";

        public static ILoggerFactory DbLoggerFactory { get; }
            = LoggerFactory.Create(builder => {
                Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("Category", "Database")
                    .MinimumLevel.Information()
                    .WriteTo.Console(
                        restrictedToMinimumLevel: LogEventLevel.Warning,
                        outputTemplate: Template,
                        theme: AnsiConsoleTheme.Code)
                    .WriteTo.File("db.log",
                        //restrictedToMinimumLevel: LogEventLevel.Information,
                        outputTemplate: Template)
                    .CreateLogger();

                builder.AddSerilog(logger: Log.Logger.ForContext<DbContext>());
            });
        private static Microsoft.Extensions.Logging.ILogger Logger { get; }
            = DbContext.DbLoggerFactory.CreateLogger<DbContext>();

        public DbContext()
            : base(new DbContextOptionsBuilder<DbContext>().UseSqlite("Data Source=main.db").Options)
        { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Group> Groups => Set<Group>();

        public DbSet<Gift> Gifts => Set<Gift>();

        private StreamWriter LogStream { get; } = new StreamWriter("dblog.txt", append: true);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder is null)
            {
                throw new ArgumentNullException(nameof(optionsBuilder));
            }

            optionsBuilder.LogTo(LogStream.WriteLine);
        }

        public override void Dispose()
        {
            base.Dispose();
            LogStream.Dispose();
            GC.SuppressFinalize(this);
        }

        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            await LogStream.DisposeAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }


            modelBuilder.Entity<Group>()
                .HasMany(g => g.Users)
                .WithMany(u => u.Groups);

            modelBuilder.Entity<Group>()
                .HasMany(g => g.Assignments)
                .WithOne(a => a.Group);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Gifts)
                .WithOne(g => g.Receiver);
                
            modelBuilder.Entity<User>()
                .HasAlternateKey(User => new { User.FirstName, User.LastName });
            modelBuilder.Entity<Group>()
                .HasAlternateKey(Group => new { Group.Name});
            modelBuilder.Entity<Gift>()
                .HasAlternateKey(Gift => new { Gift.Title });
        }
    }
}