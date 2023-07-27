﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskMgmt.Model;

namespace TaskMgmt.DAL.Repositories
{
    public class TaskMaterialUsageRepository : IGenericRepository<TaskMaterialUsage>, IDisposable
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private bool disposedValue = false;
        private DbContext context;

        public TaskMaterialUsageRepository(DbContext context)
        {
            this.context = context;
        }

        public void DeleteItem(params Guid[] guids)
        {
            Guid taskGuid = guids[0];
            Guid materialGuid = guids[1];
            logger.Info("{0} {1}={2} {3}={4}", nameof(DeleteItem), nameof(taskGuid), taskGuid, nameof(materialGuid), materialGuid);
            var item = context.TaskMaterialUsages.Where(t => t.TaskID == taskGuid && t.MaterialID == materialGuid).Single();
            context.TaskMaterialUsages.Remove(item);
        }

        public TaskMaterialUsage GetItemByID(params Guid[] guids)
        {
            Guid taskGuid = guids[0];
            Guid materialGuid = guids[1];
            logger.Info("{0} {1}={2} {3}={4}", nameof(GetItemByID), nameof(taskGuid), taskGuid, nameof(materialGuid), materialGuid);
            return context.TaskMaterialUsages.Where(t => t.TaskID == taskGuid && t.MaterialID == materialGuid).Single();
        }

        public IEnumerable<TaskMaterialUsage> GetItemsByTaskID(Guid taskGuid)
        {
            logger.Info("{0} {1}={2}", nameof(GetItemsByTaskID), nameof(taskGuid), taskGuid);
            return context.TaskMaterialUsages.Where(t => t.TaskID == taskGuid);
        }

        public IEnumerable<TaskMaterialUsage> GetItems()
        {
            logger.Info("{0}", nameof(GetItems));
            return context.TaskMaterialUsages.ToList();
        }

        public void InsertItem(TaskMaterialUsage item)
        {
            logger.Info("{0} {1}={2}", nameof(InsertItem), nameof(item.MaterialID), item.MaterialID);
            context.TaskMaterialUsages.Add(item);
        }

        public void UpdateItem(TaskMaterialUsage item)
        {
            logger.Info("{0} {1}={2} {3}={4}", nameof(UpdateItem), nameof(item.TaskID), item.TaskID, nameof(item.MaterialID), item.MaterialID);
            TaskMaterialUsage existingItem = context.TaskMaterialUsages.Where(t => t.TaskID == item.TaskID && t.MaterialID == item.MaterialID).Single();
            if (existingItem != null)
            {
                existingItem.Amount = item.Amount;
                existingItem.UniteOfMeasurement = item.UniteOfMeasurement;
            }
        }

        public void Save()
        {
            logger.Info("Save");
            throw new NotImplementedException();
            // Todo - Implement repo.Save - only if real DB provider added
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                disposedValue = true;
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}