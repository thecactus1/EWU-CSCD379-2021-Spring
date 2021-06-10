using System;
using System.Collections.Generic;
using SecretSanta.Data;
using System.Linq;

namespace SecretSanta.Business
{
    public class GroupRepository : IGroupRepository
    {
        DbContext dbcontext = new DbContext();
        public Group Create(Group item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            dbcontext.Add<Group>(item);
            foreach (User user in item.Users)
            {
                AddToGroup(item.Id, user.Id);
            }
            dbcontext.SaveChanges();
            return item;
        }

        public Group? GetItem(int id)
        {
            return dbcontext.Groups.Find(id);
        }

        public ICollection<Group> List()
        {
            return dbcontext.Groups.ToList();
        }

        public bool Remove(int id)
        {
            Group group = dbcontext.Groups.Find(id);
            dbcontext.Groups.Remove(group);
            if (group is null)
            {
                return false;
            }
            dbcontext.SaveChanges();
            return true;
            
        }

        public void Save(Group item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            

            //MockData.Groups[item.Id] = item;
            Group temp = dbcontext.Groups.Find(item.Id);
            if (temp is null)
            {
                Create(item);
            }
            else
            {
                dbcontext.Groups.Remove(dbcontext.Groups.Find(item.Id));
                Create(item);
            }
            dbcontext.SaveChanges();
        }

        public AssignmentResult GenerateAssignments(int groupId)
        {
            var group = dbcontext.Groups.Find(groupId);
            if (group == null)
            {
                return AssignmentResult.Error("Group not found");
            }

            Random random = new();
            var groupUsers = new List<User>(dbcontext.Users.ToList());

            if (groupUsers.Count < 3)
            {
                return AssignmentResult.Error($"Group {group.Name} must have at least three users");
            }

            var users = new List<User>();
            //Put the users in a random order
            while(groupUsers.Count > 0)
            {
                int index = random.Next(groupUsers.Count);
                users.Add(groupUsers[index]);
                groupUsers.RemoveAt(index);
            }

            //The assignments are created by linking the current user to the next user.
            group.Assignments.Clear();
            for(int i = 0; i < users.Count; i++)
            {
                int endIndex = (i + 1) % users.Count;
                group.Assignments.Add(new Assignment(users[i], users[endIndex]));
            }
            dbcontext.Groups.Update(group);
            return AssignmentResult.Success();
        }

        public void AddToGroup(int groupId, int userId)
        {
            
            Group group = dbcontext.Groups.Find(groupId);
            User user = dbcontext.Users.Find(userId);
            if (group is null)
            {
                throw new ArgumentNullException(nameof(group));
            }
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            
            group.Users.Add(user);
            dbcontext.SaveChanges();
        }

        public void RemoveFromGroup(int groupId, int userId)
        {
            Group group = dbcontext.Groups.Find(groupId);
            User user = dbcontext.Users.Find(userId);
            if (group is null)
            {
                throw new ArgumentNullException(nameof(group));
            }
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            
            group.Users.Remove(user);
            dbcontext.SaveChanges();
        }
    }
}