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
    public class UnityServiceHostTest
    {
        [Test]
        public void OpenCloseTest_TaskServiceHostStateChange_ShouldChangeState()
        {
            //Setup
            using (IUnityContainer iocServer = ResolveTestServerDependencies())
            using (UnityServiceHost taskServiceHost = new UnityServiceHost(iocServer, typeof(TaskService)))
            {

                //Act
                taskServiceHost.Open();
                var stateWhenOpened = taskServiceHost.State;
                taskServiceHost.Close();
                var stateWhenClosed = taskServiceHost.State;

                //Assert
                Assert.AreEqual(System.ServiceModel.CommunicationState.Opened, stateWhenOpened);
                Assert.AreEqual(System.ServiceModel.CommunicationState.Closed, stateWhenClosed);
            }
        }

        private static IUnityContainer ResolveTestServerDependencies()
        {
            IUnityContainer container = new UnityContainer();
            container
                // Dummies
                .RegisterFactory<ILogger>(l => Substitute.For<ILogger>())
                .RegisterFactory<ITaskMgmtDbContext>(db => Substitute.For<ITaskMgmtDbContext>())
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