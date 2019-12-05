using System.Linq;
using MyShop.Core.Models;

namespace MyShop.Core.Contracts
{
    public interface IRepository<Type> where Type : BaseEntity
    {
        IQueryable<Type> Collection();
        void Commit();
        void Delete(string Id);
        Type Find(string Id);
        void Insert(Type t);
        void Update(Type t);
    }
}