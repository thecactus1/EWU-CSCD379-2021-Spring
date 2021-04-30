using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using SecretSanta.Api.Controllers;
using System.Collections.Generic;
using SecretSanta.Business;
using SecretSanta.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using SecretSanta.Api.Dto;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UsersControllerTests
    {

        [TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithNullUserRepository_ThrowsAppropriateException()
        {
            //Any of the approachs shown here are fine.
            ArgumentNullException ex = Assert.ThrowsException<ArgumentNullException>(
                () => new UsersController(null!));
            Assert.AreEqual("UserRepository", ex.ParamName);

            try
            {
                new UsersController(null!);
            }
            catch(ArgumentNullException e)
            {
                Assert.AreEqual("UserRepository", e.ParamName);
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
        [DataRow(3)]
        public void Get_WithId_ReturnsUserRepositoryUser(int id)
        {
            //Arrange
            UserRepository repository = new();
            UsersController controller = new(repository);
            User expectedUser = repository.GetItem(id);
            //Act
            ActionResult<User?> result = controller.Get(id);

            //Assert
            Assert.AreEqual(expectedUser, result.Value);
        }

        [TestMethod]
        public void Get_WithNegativeId_ReturnsNotFound()
        {
            //Arrange
            UserRepository repository = new();
            UsersController controller = new(repository);
    

            //Act
            ActionResult<User?> result = controller.Get(-1);

            //Assert
            Assert.IsTrue(result.Result is NotFoundResult);
        }

        [TestMethod]
        public void Delete_WithNegativeandHighIndex_ReturnsNotFound()
        {
            //Arrange
            UserRepository repository = new();
            UsersController controller = new(repository);
            Boolean badreq = false;

            //act
            ActionResult<User?> result = controller.Delete(6);
            ActionResult<User?> result2 = controller.Delete(-1);
            

            //Assert
            Assert.IsTrue(result.Result is NotFoundResult);
            Assert.IsTrue(result2.Result is NotFoundResult);
        }

        [TestMethod]
        [DataRow(3)]
        public void Delete_WithGoodID_ReturnsProperCount(int id)
        {
            //Arrange
            UserRepository repository = new();
            UsersController controller = new(repository);
            IEnumerable<User> ExpectedCount = controller.Get();
            int expected = ExpectedCount.Count<User>()-1;
 
    

            //Act
            controller.Delete(id);
            IEnumerable<User> Final = controller.Get();
            int expected2 = Final.Count<User>();
            Boolean res = (expected == expected2);

            //Assert
            Assert.IsTrue(res);
        }
        [TestMethod]
        public void Post_WithGoodData_ReturnsListWithNewUser()
        {
            User newUser = new User();
            newUser.FirstName = "New";
            newUser.LastName = "Mann";
            //Arrange
            UserRepository repository = new();
            UsersController controller = new(repository);
            IEnumerable<User> List = controller.Get();
 
    

            //Act
            controller.Post(newUser);
            Boolean res = (List.Contains<User>(newUser));

            //Assert
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Post_WithBadData_ReturnsListWithoutNewUser()
        {
            User newUser = null;
            //Arrange
            UserRepository repository = new();
            UsersController controller = new(repository);
            IEnumerable<User> List = controller.Get();
 
    

            //Act
            controller.Post(newUser);
            Boolean res = (!(List.Contains<User>(newUser)));

            //Assert
            Assert.IsTrue(res);
        }

        [TestMethod]
        [DataRow(3)]
        public void Put_WithGoodData_ReturnsListWithUser(int id)
        {
            UpdateUser update = new UpdateUser();
            update.FirstName = "New";
            update.LastName = "Mann";
            //Arrange
            UserRepository repository = new();
            UsersController controller = new(repository);
            
 
    

            //Act
            controller.Put(id, update);
            IEnumerable<User> List = controller.Get();
            Boolean res = (List.ElementAt<User>(id-1).FirstName==update.FirstName && List.ElementAt<User>(id-1).LastName==update.LastName);

            //Assert
            Assert.IsTrue(res);
        }

        [TestMethod]
        [DataRow(3)]
        public void Put_WithBadData_ReturnsListWithoutUser(int id)
        {
            UpdateUser update = null;
            //Arrange
            UserRepository repository = new();
            UsersController controller = new(repository);
            Boolean res = false;
 
    

            //Act
            controller.Put(id, update);
            IEnumerable<User> List = controller.Get();
            try{
            Boolean listcheck = (List.ElementAt<User>(id-1).FirstName==update.FirstName && List.ElementAt<User>(id-1).LastName==update.LastName);
            }catch(Exception e){
                res = true;
            }

            //Assert
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void PUT_OutofIndex_ReturnsNotFoundandThrowsError()
        {
            Boolean badreq = false;
            UpdateUser update = new UpdateUser();
            update.FirstName = "New";
            update.LastName = "Mann";
            //Arrange
            UserRepository repository = new();
            UsersController controller = new(repository);
            
 
    

            //Act
            try{
                controller.Put(-1, update);
            }catch(ArgumentOutOfRangeException e){
                badreq = true;
            }
            ActionResult<User?> result = controller.Put(6, update);
            

            //Assert
            Assert.IsTrue(result.Result is NotFoundResult);
            Assert.IsTrue(badreq);
        }
    }
}