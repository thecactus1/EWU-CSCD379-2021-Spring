using System;
using System.Collections.Generic;
using SecretSanta.Data;
using System.Linq;

namespace SecretSanta.Business
{
    public class GiftRepository : IGiftRepository
    {
        
        public Gift Create(Gift item)
        {
            if (item is null)
            {
                throw new System.ArgumentNullException(nameof(item));
            }
            using DbContext dbcontext = new DbContext();
            dbcontext.Gifts.Add(item);
            dbcontext.SaveChanges();
            return item;
        }

        public Gift? GetItem(int id)
        {
            using DbContext dbcontext = new DbContext();
            return dbcontext.Gifts.Find(id);
        }

        public ICollection<Gift> List()
        {
            using DbContext dbcontext = new DbContext();
            return dbcontext.Gifts.ToList();
        }

        public bool Remove(int id)
        {
            using DbContext dbcontext = new DbContext();
            Gift item = dbcontext.Gifts.Find(id);
            if (item is null)
            {
                return false;
            }
            dbcontext.Gifts.Remove(item);
            dbcontext.SaveChanges();
            return true;
        }

        public void Save(Gift item)
        {
            Remove(item.Id);
            Create(item);
        }
    }
}
