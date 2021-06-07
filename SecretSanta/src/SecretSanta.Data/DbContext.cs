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

        public DbSet<User> User => Set<User>();
        public DbSet<Group> Group => Set<Group>();

        public DbSet<Gift> Gift => Set<Gift>();
        

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
                .WithOne(g => g.Reciever);
                
            modelBuilder.Entity<User>()
                .HasAlternateKey(User => new { User.FirstName, User.LastName });
            modelBuilder.Entity<Group>()
                .HasAlternateKey(Group => new { Group.Name});
            modelBuilder.Entity<Gift>()
                .HasAlternateKey(Gift => new { Gift.Title });

            //modelBuilder.Entity<User>().HasData(DbData.Users());
        }
    }
}