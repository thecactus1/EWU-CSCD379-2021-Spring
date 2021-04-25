using System;
using System.Collections.Generic;
using SecretSanta.Data;
using System.Linq;

namespace SecretSanta.Business
{
    public class UserRepository : IUserRepository
    {
        public User Create(User item)
        {
            DeleteMe.Users.Add(item);
            return item;
        }

        public User? GetItem(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
            return DeleteMe.Users.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<User> List()
        {
            return DeleteMe.Users;
        }

        public bool Remove(int id)
        {
            User? foundUser = DeleteMe.Users.FirstOrDefault(x => x.Id == id);
            if (foundUser is not null)
            {
                DeleteMe.Users.Remove(foundUser);
                return true;
            }
            return false;
        }

        public void Save(User item)
        {
            Remove(item.Id);
            Create(item);
        }
    }
}