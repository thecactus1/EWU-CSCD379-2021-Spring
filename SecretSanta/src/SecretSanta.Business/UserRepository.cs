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
            if (item is null)
            {
                throw new System.ArgumentNullException(nameof(item));
            }
            using DbContext dbcontext = new DbContext();
            dbcontext.Users.Add(item);
            dbcontext.SaveChangesAsync();
            return item;
        }

        public User? GetItem(int id)
        {
            using DbContext dbcontext = new DbContext();
            return dbcontext.Users.Find(id);
        }

        public ICollection<User> List()
        {
            using DbContext dbcontext = new DbContext();
            return dbcontext.Users.ToList();
        }

        public bool Remove(int id)
        {
            using DbContext dbcontext = new DbContext();
            User item = dbcontext.Users.Find(id);
            if (item is null)
            {
                return false;
            }
            dbcontext.Users.Remove(item);
            dbcontext.SaveChangesAsync();
            return true;
        }

        public void Save(User item)
        {
            Remove(item.Id);
            Create(item);
            
        }
    }
}
