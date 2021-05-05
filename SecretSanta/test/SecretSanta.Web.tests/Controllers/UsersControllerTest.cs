using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Web.Api;
using SecretSanta.Web.Tests.Api;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Tests
{
    [TestClass]
    public class UsersControllerTests
    {
        private WebApplicationFactory Factory{get;} = new();

        [TestMethod]
        public async Task Index_WithUsers_InvokesGetAllAsync(){
            //Arrange
            User user1 = new() { Id = 1, FirstName = "Anigo", LastName = "Montoya"};
            User user2 = new() { Id = 2, FirstName = "Princess", LastName = "Buttercup"};
            TestableUsersClient usersClient = Factory.Client;
            usersClient.GetAllUsersReturnValue = new List<User>()
            {
                user1,
                user2
            };

            HttpClient client = Factory.CreateClient();

            //Act
            HttpResponseMessage response = await client.GetAsync("/Users/");

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(1, usersClient.GetAllAsyncInvocationCount);
        }

        [TestMethod]
        public async Task Delete_WithUsers_ReturnsCorrectCount(){
            //Arrange
            User user1 = new() { Id = 1, FirstName = "Anigo", LastName = "Montoya"};
            User user2 = new() { Id = 2, FirstName = "Princess", LastName = "Buttercup"};
            TestableUsersClient usersClient = Factory.Client;
            usersClient.GetAllUsersReturnValue = new List<User>()
            {
                user1,
                user2
            };

            HttpClient client = Factory.CreateClient();
            //act
            usersClient.DeleteAsync(1);

            //Assert
            // /response.EnsureSuccessStatusCode();
            Assert.AreEqual(1, usersClient.DeleteAsyncInvocationCount);
        }

        [TestMethod]
        public async Task Edit_WithUsers_ReturnsCorrectCount(){
            //Arrange
            User user1 = new() { Id = 1, FirstName = "Anigo", LastName = "Montoya"};
            User user2 = new() { Id = 2, FirstName = "Princess", LastName = "Buttercup"};
            TestableUsersClient usersClient = Factory.Client;
            usersClient.GetAllUsersReturnValue = new List<User>()
            {
                user1,
                user2
            };
            UpdateUser newuser = new() {FirstName = "Man", LastName = "Mann"};
            newuser.FirstName = "man";
            newuser.LastName = "mann";


            HttpClient client = Factory.CreateClient();

            //Act
            usersClient.PutAsync(1, newuser);


            //Assert
            Assert.AreEqual(1, usersClient.PutAsyncInvocationCount);
        }

        [TestMethod]
        public async Task Create_WithValidModel_InvokesPostAsync()
        {
            //Arrange
            HttpClient client = Factory.CreateClient();
            TestableUsersClient usersClient = Factory.Client;

            Dictionary<string, string?> values = new()
            {
                { nameof(UserViewModel.FirstName), "Anigo" }
            };
            FormUrlEncodedContent content = new(values!);

            //Act
            HttpResponseMessage response = await client.PostAsync("/Users/Create", content);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(1, usersClient.PostAsyncInvocationCount);
            Assert.AreEqual(1, usersClient.PostAsyncInvokedParameters.Count);
            Assert.AreEqual("Anigo", usersClient.PostAsyncInvokedParameters[0].FirstName);
        }
    }
}
