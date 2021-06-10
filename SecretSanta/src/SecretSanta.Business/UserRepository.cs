﻿using System;
using System.Collections.Generic;
using SecretSanta.Data;
using System.Linq;

namespace SecretSanta.Business
{
    public class UserRepository : IUserRepository
    {
        DbContext dbcontext = new DbContext();
        public User Create(User item)
        {
            if (item is null)
            {
                throw new System.ArgumentNullException(nameof(item));
            }

            dbcontext.Users.Add(item);
            dbcontext.SaveChanges();
            return item;
        }

        public User? GetItem(int id)
        {
            return dbcontext.Users.Find(id);
        }

        public ICollection<User> List()
        {
            return dbcontext.Users.ToList();
        }

        public bool Remove(int id)
        {
            User item = dbcontext.Users.Find(id);
            if (item is null)
            {
                return false;
            }
            dbcontext.Users.Remove(item);
            dbcontext.SaveChanges();
            return true;
        }

        public void Save(User item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            

            //MockData.Groups[item.Id] = item;
            User temp = dbcontext.Users.Find(item.Id);
            if (temp is null)
            {
                Create(item);
            }
            else
            {
                dbcontext.Users.Remove(dbcontext.Users.Find(item.Id));
                Create(item);
            }
            dbcontext.SaveChanges();
        }
    }
}
