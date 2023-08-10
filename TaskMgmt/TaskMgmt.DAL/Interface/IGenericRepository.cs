using System;
using System.Collections.Generic;

namespace TaskMgmt.DAL.Interface
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
