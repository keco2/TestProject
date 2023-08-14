using NLog;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskMgmt.DAL;
using TaskMgmt.DAL.Interface;
using TaskMgmt.DAL.Repositories;
using TaskMgmt.Model;
using TaskMgmt.Tests;
using TaskMgmt.WcfService;
using Unity;
using Unity.Lifetime;

namespace TaskMgmt.IntegrationTests
{
    [TestFixture]
    public class WcfServiceTaskServiceTest
    {
        [Test]
        public void AddTask_DataAddedByTaskService_ShouldBeReturnedByTaskRepo()
        {
            //Setup
            using (IUnityContainer ioc = ResolveTestDependencies())
            {
                var taskService = ioc.Resolve<TaskService>();
                var taskStub = TestDataGenerator.GenerateT<Task>();

                var repo = ioc.Resolve<TaskRepository>();
                var repoItemsBefore = repo.GetItems().ToList();

                //Act
                taskService.AddTask(taskStub);

                //Assert
                var repoItemsAfter = repo.GetItems().ToList();
                Assert.AreEqual(0, repoItemsBefore.Count());
                Assert.AreEqual(1, repoItemsAfter.Count());
                Assert.AreEqual(1, repoItemsAfter.Where(t => t.ID == taskStub.ID).Count());
            }
        }

        [Test]
        public void GetTasks_DataInDb_ShouldBeReturnedByTaskService()
        {
            //Setup
            using (IUnityContainer ioc = ResolveTestDependencies())
            {
                var taskService = ioc.Resolve<TaskService>();
                var dbStub = ioc.Resolve<ITaskMgmtDbContext>();
                int anyNumber = 2;
                var dataStub = TestDataGenerator.GenerateListOfT<TaskEntity>(anyNumber);
                dbStub.AddData(dataStub);

                //Act
                var tasksResult = taskService.GetTasks();

                //Assert
                var anyTaskIdToCheck = dataStub.First().ID;
                Assert.AreEqual(anyNumber, tasksResult.Count());
                Assert.AreEqual(1, tasksResult.Where(t => t.ID == anyTaskIdToCheck).Count());
            }
        }

        private static IUnityContainer ResolveTestDependencies()
        {
            IUnityContainer container = new UnityContainer();
            container
                // Mocks
                .RegisterFactory<ILogger>(l => Substitute.For<ILogger>())
                .RegisterType<ITaskMgmtDbContext, TestDbContext>(new ContainerControlledLifetimeManager())
                // Rest
                .RegisterType<IGenericRepository<TaskMaterialUsageEntity>, TaskMaterialUsageRepository>(new ContainerControlledLifetimeManager())
                .RegisterType<IGenericRepository<MaterialEntity>, MaterialRepository>(new ContainerControlledLifetimeManager())
                .RegisterType<IGenericRepository<TaskEntity>, TaskRepository>(new ContainerControlledLifetimeManager())
                .RegisterType<IUnitOfWork, UnitOfWorkRepository>(new ContainerControlledLifetimeManager())
                .RegisterType<ITaskService, TaskService>(new ContainerControlledLifetimeManager())
                .RegisterType<IMaterialService, MaterialService>(new ContainerControlledLifetimeManager())
                .RegisterType<ITaskMaterialUsageService, TaskMaterialUsageService>(new ContainerControlledLifetimeManager())
                ;
            return container;
        }
    }
}