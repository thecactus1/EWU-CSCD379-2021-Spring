using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using SecretSanta.Api.Controllers;
using System.Collections.Generic;
using SecretSanta.Business;
using SecretSanta.Data;
using Microsoft.AspNetCore.Mvc;
using System;

namespace SecretSanta.Business.Tests.Controllers
{
    [TestClass]
    public class UserRepositoryTests{

        [TestMethod]
        public void Create_WithGoodData_ReturnsListWithUser(){
            UserRepository repo = new UserRepository();
            
            User newUser = new User();
            newUser.FirstName = "New";
            newUser.LastName = "Mann";
            
            User created = repo.Create(newUser);
            Assert.AreEqual(newUser, created);
        }
        [TestMethod]
         public void Create_WithGoodData_ReturnsListWithoutUser(){
            UserRepository repo = new UserRepository();
            
            User newUser = new User();
            newUser.FirstName = "New";
            newUser.LastName = "Mann";
            
            User created = repo.Create(newUser);
            int id = created.Id;
            Assert.IsTrue(!(repo.GetItem(id)==newUser));
        }
        [TestMethod]
        public void GetItem_BadID_ThrowsExcept(){
            UserRepository repo = new UserRepository();
            Boolean res = false;
            try{
                repo.GetItem(-1);
            }catch(ArgumentOutOfRangeException e){
                res = true;
            }
            Assert.IsTrue(res);
        }

        [TestMethod]
        [DataRow(3)]
        public void GetItem_GoodID_ReturnsUser(int id){
            UserRepository repo = new UserRepository();
            User test = null;
            Boolean res = true;
            try{
                test = repo.GetItem(id);
            }catch(ArgumentOutOfRangeException e){
                res = false;
            }
            Assert.IsNotNull(test);
            Assert.IsTrue(res);
        }
        [TestMethod]
        public void GetItemList_WithData_ReturnsList(){
            UserRepository repo = new();

            //Act
            ICollection<User> Users = repo.List();

            //Assert
            Assert.IsTrue(Users.Any());
        }

        [TestMethod]
        public void Delete_WithGoodId_ReturnsTrue(){
            UserRepository repo = new();
            Boolean res = repo.Remove(1);
            
            Assert.IsTrue(res);
        }
        [TestMethod]
        public void Delete_WithBadId_ReturnsFalse(){
            UserRepository repo = new();
            Boolean res = repo.Remove(-1);
            
            Assert.IsTrue(!res);
        }
        [TestMethod]
        [DataRow(3)]
        public void Save_WithGoodUser_ReturnsSameUser(int id)
        {
            User update = new User();
            update.FirstName = "New";
            update.LastName = "Mann";
            update.Id = id;
            //Arrange
            UserRepository repo = new();
            repo.Save(update);

            //Assert
            Assert.IsTrue(repo.GetItem(id)==update);
        }
    }
}