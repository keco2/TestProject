using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskMgmt.Model;

namespace TaskMgmt.DAL.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetItems();
        T GetItemByID(params Guid[] guids);
        void InsertItem(T item);
        void UpdateItem(T item);
        void DeleteItem(params Guid[] guids);
        void Save();
    }
}
