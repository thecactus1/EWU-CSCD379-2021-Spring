using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DbContext = SecretSanta.Data.DbContext;

namespace SecretSanta.Data
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext, IDisposable
    {
        public DbContext()
            : base(new DbContextOptionsBuilder<DbContext>().UseSqlite("Data Source=main.db").Options)
        {  
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

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
                .HasMany(u => u.Gifts);
                
            modelBuilder.Entity<User>()
                .HasAlternateKey(Users => new { Users.FirstName, Users.LastName });
            modelBuilder.Entity<Group>()
                .HasAlternateKey(Groups => new { Groups.Name});
            modelBuilder.Entity<Gift>()
                .HasAlternateKey(Gifts => new { Gifts.Title });

            //modelBuilder.Entity<User>().HasData(DbData.Users());
        }
    }
}