using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class UserRepositoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_NullItem_ThrowsArgumentException()
        {
            DbContext.DeploySampleData();
            UserRepository sut = new();

            sut.Create(null!);
        }

        [TestMethod]
        public void Create_WithItem_CanGetItem()
        {
            UserRepository sut = new();
            User user = new()
            {
                Id = 6,
                FirstName = "Tobias",
                LastName = "Forge"
            };

            User createdUser = sut.Create(user);

            User? retrievedUser = sut.GetItem(createdUser.Id);
            Assert.AreEqual(user.FirstName, retrievedUser.FirstName);
        }

        [TestMethod]
        public void GetItem_WithBadId_ReturnsNull()
        {
            UserRepository sut = new();

            User? user = sut.GetItem(-1);

            Assert.IsNull(user);
        }

        [TestMethod]
        public void GetItem_WithValidId_ReturnsUser()
        {
            UserRepository sut = new();
            sut.Create(new() 
            { 
                Id = 7,
                FirstName = "First",
                LastName = "Last"
            });

            User? user = sut.GetItem(7);

            Assert.AreEqual(7, user?.Id);
            Assert.AreEqual("First", user!.FirstName);
            Assert.AreEqual("Last", user.LastName);
        }

        [TestMethod]
        public void List_WithUsers_ReturnsAllUser()
        {
            UserRepository sut = new();
            sut.Create(new()
            {
                Id = 42,
                FirstName = "First",
                LastName = "Last"
            });

            ICollection<User> users = sut.List();

            Assert.AreEqual(sut.List().Count(), users.Count);
            foreach(var mockUser in sut.List())
            {
                Assert.IsNotNull(users.SingleOrDefault(x => x.FirstName == mockUser.FirstName && x.LastName == mockUser.LastName));
            }
        }

        [TestMethod]
        [DataRow(-1, false)]
        [DataRow(6, true)]
        public void Remove_WithInvalidId_ReturnsTrue(int id, bool expected)
        {
            UserRepository sut = new();
            sut.Create(new User()
            {
                Id = 6,
                FirstName = "Tobias",
                LastName = "Forge"
            });

            Assert.AreEqual(expected, sut.Remove(id));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Save_NullItem_ThrowsNullRef()
        {
            UserRepository sut = new();

            sut.Save(null!);
        }

        [TestMethod]
        public void Save_WithValidItem_SavesItem()
        {
            UserRepository sut = new();

            sut.Save(new User() { Id = 5, FirstName = "Miracle", LastName = "Max" });

            Assert.AreEqual(5, sut.GetItem(5)?.Id);
        }
    }
}
