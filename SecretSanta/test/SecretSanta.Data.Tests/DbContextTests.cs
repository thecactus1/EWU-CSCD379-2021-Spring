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
            using DbContext dbContext = new DbContext();
            int beforeCount = dbContext.Gifts.Count();
            dbContext.Gifts.Add(new Gift(){Id = 42, Title = "Gift", Priority = 1, UserId = 1});
            dbContext.SaveChanges();
            Assert.AreEqual<int>(beforeCount+1, dbContext.Gifts.Count());
        }
    }
}
