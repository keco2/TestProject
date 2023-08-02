using AutoMapper;
using AutoMapper.Features;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TaskMgmt.DAL;
using TaskMgmt.DAL.Repositories;
using TaskMgmt.Model;

namespace TaskMgmt.WcfService
{
    public class TaskMaterialUsageService : ITaskMaterialUsageService
    {
        private IUnitOfWork unitOfWorkRepo;
        private IMapper mapper;

        public TaskMaterialUsageService()
        {
            unitOfWorkRepo = new UnitOfWorkRepository();

            IConfigurationProvider mapperCfg = new TaskMaterialUsageMapperConfig();
            mapperCfg.AssertConfigurationIsValid();
            mapper = new Mapper(mapperCfg);
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



            var DEBUG_SOURCE = taskEntites.ToList();
            var DEBUG_DEST = mapper.Map<IEnumerable<TaskMaterialUsage>>(taskEntites).ToList();



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
