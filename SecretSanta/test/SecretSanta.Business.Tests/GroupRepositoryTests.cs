using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GroupRepositoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_NullItem_ThrowsArgumentException()
        {
            GroupRepository testrepo = new();

            testrepo.Create(null!);
        }

        [TestMethod]
        public void Create_WithItem_CanGetItem()
        {
            GroupRepository testrepo = new();
            Group user = new()
            {
                Id = 32
            };

            Group createdGroup = testrepo.Create(user);

            Group? retrievedGroup = testrepo.GetItem(createdGroup.Id);
            Assert.AreEqual(user, retrievedGroup);
        }

        [TestMethod]
        public void GetItem_WithBadId_ReturnsNull()
        {
            GroupRepository testrepo = new();

            Group? user = testrepo.GetItem(-1);

            Assert.IsNull(user);
        }

        [TestMethod]
        public void GetItem_WithValidId_ReturnsGroup()
        {
            GroupRepository testrepo = new();
            testrepo.Create(new() 
            { 
                Id = 33,
                Name = "Group",
            });

            Group? user = testrepo.GetItem(33);

            Assert.AreEqual(33, user?.Id);
            Assert.AreEqual("Group", user!.Name);
        }

        [TestMethod]
        public void List_WithGroups_ReturnsAllGroup()
        {
            GroupRepository testrepo = new();
            testrepo.Create(new()
            {
                Id = 33,
                Name = "Group",
            });

            ICollection<Group> users = testrepo.List();

            Assert.AreEqual(MockData.Groups.Count, users.Count);
            foreach(var mockGroup in MockData.Groups.Values)
            {
                Assert.IsNotNull(users.SingleOrDefault(x => x.Name == mockGroup.Name));
            }
        }

        [TestMethod]
        [DataRow(-1, false)]
        [DataRow(32, true)]
        public void Remove_WithInvalidId_ReturnsFalse_And_ValidId_ReturnsTrue(int id, bool expected)
        {
            GroupRepository testrepo = new();
            testrepo.Create(new()
            {
                Id = 33,
                Name = "Group"
            });

            Assert.AreEqual(expected, testrepo.Remove(id));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Save_NullItem_ThrowsArgumentException()
        {
            GroupRepository testrepo = new();

            testrepo.Save(null!);
        }

        [TestMethod]
        public void Save_WithValidItem_SavesItem()
        {
            GroupRepository testrepo = new();

            testrepo.Save(new Group() { Id = 33 });

            Assert.AreEqual(33, testrepo.GetItem(33)?.Id);
        }

        [TestMethod]
        public void GenerateAssignments_WithInvalidId_ReturnsError()
        {
            GroupRepository testrepo = new();

            AssignmentResult result = testrepo.GenerateAssignment(33);

            Assert.AreEqual("Groups must have at least 3 users", result.ErrorMessage);
        }

        [TestMethod]
        public void GenerateAssignments_WithLessThanThreeUsers_ReturnsError()
        {
            GroupRepository testrepo = new();
            testrepo.Create(new()
            {
                Id = 33,
                Name = "Group"
            });

            AssignmentResult result = testrepo.GenerateAssignment(33);

            Assert.AreEqual($"Groups must have at least 3 users", result.ErrorMessage);
        }

        [TestMethod]
        public void GenerateAssignments_WithValidGroup_CreatesAssignments()
        {
            GroupRepository testrepo = new();
            Group group = testrepo.Create(new()
            {
                Id = 33,
                Name = "Group"
            });
            group.Users.Add(new User { FirstName = "John", LastName = "Doe" });
            group.Users.Add(new User { FirstName = "Jane", LastName = "Smith" });
            group.Users.Add(new User { FirstName = "Bob", LastName = "Jones" });

            AssignmentResult result = testrepo.GenerateAssignment(33);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(3, group.Assignments.Count);
            Assert.AreEqual(3, group.Assignments.Select(x => x.Giver.FirstName).Distinct().Count());
            Assert.AreEqual(3, group.Assignments.Select(x => x.Receiver.FirstName).Distinct().Count());
            Assert.IsFalse(group.Assignments.Any(x => x.Giver.FirstName == x.Receiver.FirstName));
        }
    }
}
