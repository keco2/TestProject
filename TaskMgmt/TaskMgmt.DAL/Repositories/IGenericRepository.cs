using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskMgmt.Model;

namespace TaskMgmt.DAL.Repositories
{
    public interface IGenericRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetItems();
        T GetItemByID(Guid taskId);
        void InsertItem(T Ttask);
        void UpdateItem(Guid taskId, T task);
        void DeleteItem(Guid taskId);
        void Save();
    }
}
