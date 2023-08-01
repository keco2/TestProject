using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskMgmt.Model;

namespace TaskMgmt.DAL.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetItems();
        IEnumerable<TEntity> GetItemsByID(Guid guid);
        void InsertItem(TEntity item);
        void UpdateItem(TEntity item);
        void DeleteItem(params Guid[] guids);
        void Save();
    }
}
