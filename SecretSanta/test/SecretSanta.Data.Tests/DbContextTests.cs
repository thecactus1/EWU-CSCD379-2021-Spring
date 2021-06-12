using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class DbContextTests
    {
        [TestMethod]
        public void AddGift()
        {
            DbContext.DeploySampleData();
            using DbContext dbContext = new DbContext();
            
            int beforeCount = dbContext.Gifts.Count();
            Console.WriteLine(beforeCount);
            dbContext.Gifts.Add(new Gift(){Id = 5, Title = "Gift", Description = "A gift", Url = "A gift", Priority = 1, UserId = 1});
            dbContext.SaveChanges();
            Assert.AreEqual<int>(beforeCount+1, dbContext.Gifts.Count());
        }
        [TestMethod]
        public void AddGroup()
        {
            DbContext.DeploySampleData();
            using DbContext dbContext = new DbContext();
            
            int beforeCount = dbContext.Groups.Count();
            Console.WriteLine(beforeCount);
            dbContext.Groups.Add(new Group(){Id = 5, Name = "Group"});
            dbContext.SaveChanges();
            Assert.AreEqual<int>(beforeCount+1, dbContext.Groups.Count());
        }
        [TestMethod]
        public void AddUser()
        {
            DbContext.DeploySampleData();
            using DbContext dbContext = new DbContext();
            
            int beforeCount = dbContext.Users.Count();
            Console.WriteLine(beforeCount);
            dbContext.Users.Add(new User(){Id = 6, FirstName = "User", LastName = "Greatest"});
            dbContext.SaveChanges();
            Assert.AreEqual<int>(beforeCount+1, dbContext.Users.Count());
        }

        [TestMethod]
        public void RemoveGift()
        {
            DbContext.DeploySampleData();
            using DbContext dbContext = new DbContext();
            
            int beforeCount = dbContext.Gifts.Count();
            Console.WriteLine(beforeCount);
            dbContext.Gifts.Remove(dbContext.Gifts.Find(1));
            dbContext.SaveChanges();
            Assert.AreEqual<int>(beforeCount-1, dbContext.Gifts.Count());
        }

        [TestMethod]
        public void RemoveGroup()
        {
            DbContext.DeploySampleData();
            using DbContext dbContext = new DbContext();
            
            int beforeCount = dbContext.Groups.Count();
            Console.WriteLine(beforeCount);
            dbContext.Groups.Remove(dbContext.Groups.Find(1));
            dbContext.SaveChanges();
            Assert.AreEqual<int>(beforeCount-1, dbContext.Groups.Count());
        }

                [TestMethod]
        public void RemoveUser()
        {
            DbContext.DeploySampleData();
            using DbContext dbContext = new DbContext();
            
            int beforeCount = dbContext.Users.Count();
            Console.WriteLine(beforeCount);
            dbContext.Users.Remove(dbContext.Users.Find(1));
            dbContext.SaveChanges();
            Assert.AreEqual<int>(beforeCount-1, dbContext.Users.Count());
        }
    }
}
