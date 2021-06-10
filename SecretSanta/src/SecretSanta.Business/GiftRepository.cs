using System;
using System.Collections.Generic;
using SecretSanta.Data;
using System.Linq;

namespace SecretSanta.Business
{
    public class GiftRepository : IGiftRepository
    {
        DbContext dbcontext = new DbContext();
        public Gift Create(Gift item)
        {
            if (item is null)
            {
                throw new System.ArgumentNullException(nameof(item));
            }

            dbcontext.Gifts.Add(item);
            dbcontext.SaveChanges();
            return item;
        }

        public Gift? GetItem(int id)
        {
            return dbcontext.Gifts.Find(id);
        }

        public ICollection<Gift> List()
        {
            return dbcontext.Gifts.ToList();
        }

        public bool Remove(int id)
        {
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
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            

            //MockData.Groups[item.Id] = item;
            Gift temp = dbcontext.Gifts.Find(item.Id);
            if (temp is null)
            {
                Create(item);
            }
            else
            {
                dbcontext.Gifts.Remove(dbcontext.Gifts.Find(item.Id));
                Create(item);
            }
            dbcontext.SaveChanges();
        }
    }
}
