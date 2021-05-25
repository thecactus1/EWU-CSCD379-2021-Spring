using System;
using System.Collections.Generic;
using SecretSanta.Data;

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

            MockData.Groups[item.Id] = item;
            return item;
        }

        public Group? GetItem(int id)
        {
            if (MockData.Groups.TryGetValue(id, out Group? user))
            {
                return user;
            }
            return null;
        }

        public ICollection<Group> List()
        {
            return MockData.Groups.Values;
        }

        public bool Remove(int id)
        {
            return MockData.Groups.Remove(id);
        }

        public void Save(Group item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            MockData.Groups[item.Id] = item;
        }


        private void Shuffle<Group>(List<Group> list)
        {
            Random rand = new Random();
            int count = list.Count;
            while(count > 1)
            {
                count--;
                int randNum = rand.Next(count+1);
                Group val = list[randNum];
                list[randNum] = list[count];
                list[count] = val;
            }
        }

        public AssignmentResult GenerateAssignment(int id)
        {
            if(!MockData.Groups.ContainsKey(id)) return AssignmentResult.Error("Group not found");

            

            List<User> userList = MockData.Groups[id].Users;

            if(userList.Count < 3) return AssignmentResult.Error("Groups must have at least 3 users");

            Shuffle(userList);

            for(int i = 0; i < userList.Count; i++)
            {
                if(i < userList.Count - 1)
                    MockData.Groups[id].Assignments.Add(new Assignment(userList[i], userList[i+1]));
                else
                {
                    MockData.Groups[id].Assignments.Add(new Assignment(userList[i], userList[0]));
                }
            }
            return AssignmentResult.Success();
        }
    }
}