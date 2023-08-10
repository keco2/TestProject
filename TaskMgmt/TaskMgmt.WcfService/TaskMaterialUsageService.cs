using AutoMapper;
using System;
using System.Collections.Generic;
using TaskMgmt.DAL;
using TaskMgmt.DAL.Interface;
using TaskMgmt.Model;
using TaskMgmt.WcfService.MappersConfigs;
using Unity;

namespace TaskMgmt.WcfService
{
    public class TaskMaterialUsageService : ITaskMaterialUsageService
    {
        private IMapper mapper;

        [Dependency]
        public IUnitOfWork UnitOfWorkRepo { get; set; }

        public TaskMaterialUsageService()
        {
            mapper = new Mapper(new TaskMaterialUsageMapperConfig());
        }

        public IEnumerable<TaskMaterialUsage> GetTaskMaterialUsages()
        {
            var entities = UnitOfWorkRepo.TaskMaterialUsageRepository.GetItems();
            return mapper.Map<IEnumerable<TaskMaterialUsage>>(entities);
        }

        public IEnumerable<TaskMaterialUsage> GetTaskMaterialUsagesByTaskId(string taskId)
        {
            Guid taskGuid = Guid.Parse(taskId);
            var taskEntites = UnitOfWorkRepo.TaskMaterialUsageRepository.GetItemsByID(taskGuid);
            return mapper.Map<IEnumerable<TaskMaterialUsage>>(taskEntites);
        }

        public void AddTaskMaterialUsage(TaskMaterialUsage usage)
        {
            var usageEntity = mapper.Map<TaskMaterialUsageEntity>(usage);
            UnitOfWorkRepo.TaskMaterialUsageRepository.InsertItem(usageEntity);
            UnitOfWorkRepo.SaveChanges();
        }

        public void UpdateTaskMaterialUsage(TaskMaterialUsage usage)
        {
            var usageEntity = mapper.Map<TaskMaterialUsageEntity>(usage);
            UnitOfWorkRepo.TaskMaterialUsageRepository.UpdateItem(usageEntity);
            UnitOfWorkRepo.SaveChanges();
        }

        public void DeleteTaskMaterialUsage(string taskId, string materialId)
        {
            Guid taskGuid = Guid.Parse(taskId);
            Guid materialGuid = Guid.Parse(materialId);
            UnitOfWorkRepo.TaskMaterialUsageRepository.DeleteItem(taskGuid, materialGuid);
            UnitOfWorkRepo.SaveChanges();
        }
    }
}
