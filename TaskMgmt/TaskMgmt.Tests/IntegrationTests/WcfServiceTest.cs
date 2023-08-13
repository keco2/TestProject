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
        public void AddTask_UsingService_ShouldBeFoundInRepo()
        {
            // Setup
            using (IUnityContainer ioc = ResolveTestDependencies())
            {
                var taskService = ioc.Resolve<TaskService>();
                var taskStub = Substitute.For<Task>();
                taskStub.Name = "required field";

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