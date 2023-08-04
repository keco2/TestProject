using AutoMapper;
using System;
using System.Collections.Generic;
using TaskMgmt.DAL;
using TaskMgmt.Model;
using TaskMgmt.WcfService.MappersConfigs;

namespace TaskMgmt.WcfService
{
    public class TaskMaterialUsageService : ITaskMaterialUsageService
    {
        private IUnitOfWork unitOfWorkRepo;
        private IMapper mapper;

        public TaskMaterialUsageService()
        {
            unitOfWorkRepo = new UnitOfWorkRepository();
            mapper = new Mapper(new TaskMaterialUsageMapperConfig());
        }

        public IEnumerable<TaskMaterialUsage> GetTaskMaterialUsages()
        {
            var entities = unitOfWorkRepo.TaskMaterialUsageRepository.GetItems();
            return mapper.Map<IEnumerable<TaskMaterialUsage>>(entities);
        }

        public IEnumerable<TaskMaterialUsage> GetTaskMaterialUsagesByTaskId(string taskId)
        {
            Guid taskGuid = Guid.Parse(taskId);
            var taskEntites = unitOfWorkRepo.TaskMaterialUsageRepository.GetItemsByID(taskGuid);
            return mapper.Map<IEnumerable<TaskMaterialUsage>>(taskEntites);
        }

        public void AddTaskMaterialUsage(TaskMaterialUsage usage)
        {
            var usageEntity = mapper.Map<TaskMaterialUsageEntity>(usage);
            unitOfWorkRepo.TaskMaterialUsageRepository.InsertItem(usageEntity);
            unitOfWorkRepo.SaveChanges();
        }

        public void UpdateTaskMaterialUsage(TaskMaterialUsage usage)
        {
            var usageEntity = mapper.Map<TaskMaterialUsageEntity>(usage);
            unitOfWorkRepo.TaskMaterialUsageRepository.UpdateItem(usageEntity);
            unitOfWorkRepo.SaveChanges();
        }

        public void DeleteTaskMaterialUsage(string taskId, string materialId)
        {
            Guid taskGuid = Guid.Parse(taskId);
            Guid materialGuid = Guid.Parse(materialId);
            unitOfWorkRepo.TaskMaterialUsageRepository.DeleteItem(taskGuid, materialGuid);
            unitOfWorkRepo.SaveChanges();
        }
    }
}
