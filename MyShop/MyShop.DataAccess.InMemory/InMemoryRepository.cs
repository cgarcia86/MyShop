using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<Type> where Type : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<Type> items;
        string className;

        public InMemoryRepository()
        {
            className = typeof(Type).Name;
            items = cache[className] as List<Type>;

            if(items == null)
            {
                items = new List<Type>();
            }
        }

        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(Type t)
        {
            items.Add(t);
        }

        public void Update(Type t)
        {
            Type tToUpdate = items.Find(i => i.Id == t.Id);

            if(tToUpdate != null)
            {
                tToUpdate = t;
            }
            else
            {
                throw new Exception(className + "Not Found");
            }
        }

        public Type Find(string Id)
        {
            Type t = items.Find(i => i.Id == Id);

            if(t != null)
            {
                return t;
            }
            else
            {
                throw new Exception(className + "Not Found");
            }
        }

        public IQueryable<Type>Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(string Id)
        {
            Type tToDelete = items.Find(i => i.Id == Id);

            if (tToDelete != null)
            {
                items.Remove(tToDelete);
            }
            else
            {
                throw new Exception(className + "Not Found");
            }
        }

    }
}
