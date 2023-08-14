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
using Async = System.Threading.Tasks;

namespace TaskMgmt.IntegrationTests
{
    [TestFixture]
    public class UIMaterialControlVMTest
    {
        [Test]
        public void MaterialList_VmInit_ShouldLoadDataFromDb()
        {
            //Setup
            using (IUnityContainer iocServer = ResolveTestServerDependencies())
            using (IUnityContainer iocClient = ResolveTestClientDependencies())
            using (UnityServiceHost materialServiceHost = new UnityServiceHost(iocServer, typeof(MaterialService)))
            {
                materialServiceHost.Open();

                var dbStub = iocServer.Resolve<ITaskMgmtDbContext>();
                int anyNumber = 2;
                var dataStub = TestDataGenerator.GenerateListOfT<MaterialEntity>(anyNumber);
                dbStub.AddData(dataStub);

                var materialControlViewModel = iocClient.Resolve<MaterialControlVM>();

                //Act
                var MaterialResult = materialControlViewModel.MaterialList.ToList();

                //Assert
                var anyMaterialIdToCheck = dataStub.First().ID;
                Assert.AreEqual(anyNumber, MaterialResult.Count());
                Assert.AreEqual(1, MaterialResult.Where(t => t.ID == anyMaterialIdToCheck).Count());

                materialServiceHost.Close();
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
                .RegisterType<IMaterialVM, MaterialControlVM>()
                .RegisterType<IMainVM, MainVM>()
                .RegisterType<ITaskVM, TaskControlVM>()
                .RegisterType<IUsageVM, UsageControlVM>()
                .RegisterType<IProxy, Proxy>();

            return container;
        }
    }
}