using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using SecretSanta.Api.Controllers;
using System.Collections.Generic;
using SecretSanta.Business;
using SecretSanta.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using moq;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UsersControllerTests
    {

        Mock<IUserRepository> mockrepo = new Mock<IUserRepository>();
        Mock<User> mockvaliduser = new Mock<User>();
        Mock<User> mockinvaliduser = new Mock<User>();

        [TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithNullUserRepository_ThrowsAppropriateException()
        {
            //Any of the approachs shown here are fine.
            ArgumentNullException ex = Assert.ThrowsException<ArgumentNullException>(
                () => new UsersController(null!));
            Assert.AreEqual("userRepository", ex.ParamName);

            try
            {
                new UsersController(null!);
            }
            catch(ArgumentNullException e)
            {
                Assert.AreEqual("userRepository", e.ParamName);
                return;
            }
            Assert.Fail("No exception thrown");
        }

        [TestMethod]
        public void Get_WithData_ReturnsUsers()
        {
            //Arrange
            UsersController controller = new(new UserRepository());

            //Act
            IEnumerable<User> events = controller.Get();

            //Assert
            Assert.IsTrue(events.Any());
        }
    
        [TestMethod]
        [DataRow(42)]
        [DataRow(98)]
        public void Get_WithId_ReturnsUserRepositoryUser(int id)
        {
            //Arrange
            TestableUserRepository repository = new();
            UsersController controller = new(repository);
            User expectedUser = new();
            repository.GetItemUser = expectedUser;

            //Act
            ActionResult<User?> result = controller.Get(id);

            //Assert
            Assert.AreEqual(id, repository.GetItemId);
            Assert.AreEqual(expectedUser, result.Value);
        }

        [TestMethod]
        public void Get_WithNegativeId_ReturnsNotFound()
        {
            //Arrange
            TestableUserRepository repository = new();
            UsersController controller = new(repository);
            User expectedUser = new();
            repository.GetItemUser = expectedUser;

            //Act
            ActionResult<User?> result = controller.Get(-1);

            //Assert
            Assert.IsTrue(result.Result is NotFoundResult);
        }

        private class TestableUserRepository : IUserRepository
        {
            public User Create(User item)
            {
                throw new System.NotImplementedException();
            }

            public User? GetItemUser { get; set; }
            public int GetItemId { get; set; }
            public User? GetItem(int id)
            {
                GetItemId = id;
                return GetItemUser;
            }

            public ICollection<User> List()
            {
                throw new System.NotImplementedException();
            }

            public bool Remove(int id)
            {
                throw new System.NotImplementedException();
            }

            public void Save(User item)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}