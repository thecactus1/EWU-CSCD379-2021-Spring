using System;
using System.Collections.Generic;
using SecretSanta.Data;
using System.Linq;

namespace SecretSanta.Business
{
    public class GroupRepository : IGroupRepository
    {
        public Group Create(Group item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            using DbContext dbcontext = new DbContext();
            foreach(GroupUser i in dbcontext.GroupUsers.ToList()){
                Console.WriteLine(i.UserId + ", " + i.GroupId);
            }
            foreach(User i in dbcontext.Users.ToList()){
                GroupUser groupuser = dbcontext.GroupUsers.Find(item.Id, i.Id);
                
                if(groupuser != null){
                    Console.WriteLine(groupuser.UserId);
                    item.Users.Add(i);
                    
                }
            }
            dbcontext.Groups.Add(item);
            dbcontext.SaveChangesAsync();
            return item;
        }

        public Group? GetItem(int id)
        {
            using DbContext dbcontext = new DbContext();
            return dbcontext.Groups.Find(id);
        }

        public ICollection<Group> List()
        {
            using DbContext dbcontext = new DbContext();
            return dbcontext.Groups.ToList();
        }

        public bool Remove(int id)
        {
            using DbContext dbcontext = new DbContext();
            Group group = dbcontext.Groups.Find(id);
            dbcontext.Groups.Remove(group);
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
            List<User> users = item.Users;
            foreach(User i in users){
                Console.WriteLine(i.FirstName);
            }
            Remove(item.Id);
            Create(item);
        }

        public AssignmentResult GenerateAssignments(int groupId)
        {
            using DbContext dbcontext = new DbContext();
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
            using DbContext dbcontext = new DbContext();
            Group group = dbcontext.Groups.Find(groupId);
            Console.WriteLine(group.Users.Count);
            User user = dbcontext.Users.Find(userId);
            if(group.Users.Contains(user)){
                return;
            }
            if (group is null)
            {
                throw new ArgumentNullException(nameof(group));
            }
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            GroupUser g = new GroupUser{UserId = userId, GroupId = groupId, Group = group, User = user};
            if(dbcontext.GroupUsers.Find(g.UserId, g.GroupId) != null)
            return;
            Console.WriteLine("saving");
            dbcontext.Add<GroupUser>(g);
            dbcontext.SaveChangesAsync();
            foreach( GroupUser i in dbcontext.GroupUsers.ToList<GroupUser>()){
                Console.WriteLine(i.UserId + ", " + i.GroupId);
            }
            
        }

        public void RemoveFromGroup(int groupId, int userId)
        {
            using DbContext dbcontext = new DbContext();
            Console.WriteLine("NO");
            GroupUser g = dbcontext.GroupUsers.Find(groupId, userId);
            if(dbcontext.GroupUsers.Find(g.UserId, g.GroupId) == null)
            return;
            dbcontext.GroupUsers.Remove(g);
            dbcontext.SaveChangesAsync();
        }
    }
}