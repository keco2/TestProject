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
using TaskMgmt.Server;
using TaskMgmt.Tests;
using TaskMgmt.UI.ViewModel;
using TaskMgmt.WcfService;
using Unity;
using Unity.Lifetime;

namespace TaskMgmt.IntegrationTests
{
    [TestFixture]
    public class UITaskControlVMTest
    {
        [Test]
        public void TaskList_UiInit_ShouldLoadTaskListFromDb()
        {
            //Setup
            using (IUnityContainer iocServer = ResolveTestServerDependencies())
            using (IUnityContainer iocClient = ResolveTestClientDependencies())
            using (UnityServiceHost taskServiceHost = new UnityServiceHost(iocServer, typeof(TaskService)))
            {
                taskServiceHost.Open();

                var dbStub = iocServer.Resolve<ITaskMgmtDbContext>();
                int anyNumber = 2;
                var dataStub = TestDataGenerator.GenerateListOfT<TaskEntity>(anyNumber);
                dbStub.AddData(dataStub);

                var taskControlViewModel = iocClient.Resolve<TaskControlVM>();

                //Act
                var tasksResult = taskControlViewModel.TaskList.ToList();

                //Assert
                var anyTaskIdToCheck = dataStub.First().ID;
                Assert.AreEqual(anyNumber, tasksResult.Count());
                Assert.AreEqual(1, tasksResult.Where(t => t.ID == anyTaskIdToCheck).Count());

                taskServiceHost.Close();
            }
        }

        private static IUnityContainer ResolveTestServerDependencies()
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

        private static IUnityContainer ResolveTestClientDependencies()
        {
            IUnityContainer container = new UnityContainer();
            container
                .RegisterType<ITaskVM, TaskControlVM>()
                //.RegisterType<IMainVM, MainView>()
                //.RegisterType<IMainVM, MainVM>()
                //.RegisterType<ITaskVM, TaskControl>()
                //.RegisterType<IUsageVM, UsageControl>()
                //.RegisterType<IUsageVM, UsageControlVM>()
                .RegisterType<IProxy, Proxy>();

            return container;
        }
    }
}