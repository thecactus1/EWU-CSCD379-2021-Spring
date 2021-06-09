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
            return dbcontext.Group.Find(id);
        }

        public ICollection<Group> List()
        {
            return dbcontext.Group.ToList();
        }

        public bool Remove(int id)
        {
            Group group = dbcontext.Group.Find(id);
            dbcontext.Group.Remove(group);
            if (group is null)
            {
                return false;
            }
            dbcontext.SaveChangesAsync();
            return true;
            
        }

        public void Save(Group item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            dbcontext.Groups[item.Id] = item;
            dbcontext.sav
        }

        public AssignmentResult GenerateAssignments(int groupId)
        {
            if (!MockData.Groups.TryGetValue(groupId, out Group? group))
            {
                return AssignmentResult.Error("Group not found");
            }

            Random random = new();
            var groupUsers = new List<User>(group.Users);

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
            return AssignmentResult.Success();
        }

        public void AddToGroup(int groupId, int userId)
        {
            
            Group group = dbcontext.Group.Find(groupId);
            User user = dbcontext.User.Find(userId);
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
            Group group = dbcontext.Group.Find(groupId);
            User user = dbcontext.User.Find(userId);
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