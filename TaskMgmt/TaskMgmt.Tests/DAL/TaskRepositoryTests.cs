using NUnit.Framework;
using TaskMgmt.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMgmt.DAL.Interface;
using NSubstitute;
using NLog;
using System.Data.Entity;
using TaskMgmt.Tests;
using System.Data.Entity.Infrastructure;

namespace TaskMgmt.DAL.UnitTests
{
    [TestFixture]
    public class TaskRepositoryTests
    {
        [Test]
        public void InsertItemTest_Inserting_InsertsButNotSaves()
        {
            // Setup

            var dbContextMock = Substitute.For<ITaskMgmtDbContext>();
            var loggerMock = Substitute.For<ILogger>();
            var dbsetMock = Substitute.For<DbSet<TaskEntity>>();
            var taskEntityStub = Substitute.For<TaskEntity>();

            dbContextMock.Set<TaskEntity>().Returns(dbsetMock);

            var taskRepository = new TaskRepository(dbContextMock, loggerMock);

            dbsetMock.ClearReceivedCalls();
            dbContextMock.ClearReceivedCalls();
            loggerMock.ClearReceivedCalls();

            // Act

            taskRepository.InsertItem(taskEntityStub);

            // Assert

            dbsetMock.ReceivedWithAnyArgs(1);
            dbsetMock.Received(1).Add(taskEntityStub);

            dbContextMock.ReceivedWithAnyArgs(0);
            dbContextMock.DidNotReceive().SaveChanges();

            loggerMock.ReceivedWithAnyArgs(1);
            loggerMock.Received(1).Info(taskEntityStub.ID.ToString());
        }

        [Test]
        public void GetItemsByIDTest_WithAnyId_ReturnsExpectedItem()
        {
            // Setup

            int anyNumber = 3;
            var dbQueryMockWithTestData = TestDataGenerator.GenerateDbQueryMockWithTestData<TaskEntity>(anyNumber);
            var dbsetMock = Substitute.For<DbSet<TaskEntity>>();
            var dbContextMock = Substitute.For<ITaskMgmtDbContext>();
            var loggerMock = Substitute.For<ILogger>();

            dbsetMock.AsNoTracking().Returns(dbQueryMockWithTestData);
            dbContextMock.Set<TaskEntity>().Returns(dbsetMock);

            dbsetMock.ClearReceivedCalls();
            dbContextMock.ClearReceivedCalls();
            loggerMock.ClearReceivedCalls();

            var taskRepository = new TaskRepository(dbContextMock, loggerMock);
            var anyItem = dbQueryMockWithTestData.Last();

            // Act

            IEnumerable<TaskEntity> result = taskRepository.GetItemsByID(anyItem.ID);

            // Assert

            Assert.AreEqual(1, result.Count(), "Single item is expected.");
            Assert.AreSame(anyItem, result.Single());

            dbsetMock.ReceivedWithAnyArgs(0);
            dbContextMock.ReceivedWithAnyArgs(0);
            dbContextMock.DidNotReceive().SaveChanges();

            loggerMock.ReceivedWithAnyArgs(1);
            loggerMock.Received(1).Info(anyItem.ID.ToString());
        }

        [Test]
        public void GetItemsTest_ReturnsAllItems()
        {
            // Setup

            int anyNumber = 3;
            var dbQueryMockWithTestData = TestDataGenerator.GenerateDbQueryMockWithTestData<TaskEntity>(anyNumber);
            var dbsetMock = Substitute.For<DbSet<TaskEntity>>();
            var dbContextMock = Substitute.For<ITaskMgmtDbContext>();
            var loggerMock = Substitute.For<ILogger>();

            dbsetMock.AsNoTracking().Returns(dbQueryMockWithTestData);
            dbContextMock.Set<TaskEntity>().Returns(dbsetMock);

            dbsetMock.ClearReceivedCalls();
            dbContextMock.ClearReceivedCalls();
            loggerMock.ClearReceivedCalls();

            var taskRepository = new TaskRepository(dbContextMock, loggerMock);

            // Act

            IEnumerable<TaskEntity> result = taskRepository.GetItems();

            // Assert

            Assert.AreEqual(anyNumber, result.Count());

            dbsetMock.ReceivedWithAnyArgs(0);
            dbContextMock.ReceivedWithAnyArgs(0);
            dbContextMock.DidNotReceive().SaveChanges();

            loggerMock.ReceivedWithAnyArgs(1);
            loggerMock.Received(1).Info("All");
        }

        [Ignore("Mocking not possible for DbEntityEntry")]
        [Test]
        public void UpdateItem_ChangedItem_UpdatesItemWithThatId()
        {
            // Setup

            int anyNumber = 3;
            var dbQueryMockWithTestData = TestDataGenerator.GenerateDbQueryMockWithTestData<TaskEntity>(anyNumber);
            var dbsetMock = Substitute.For<DbSet<TaskEntity>>();
            var dbContextMock = Substitute.For<ITaskMgmtDbContext>();
            var loggerMock = Substitute.For<ILogger>();

            dbsetMock.AsNoTracking().Returns(dbQueryMockWithTestData);
            dbContextMock.Set<TaskEntity>().Returns(dbsetMock);

            dbsetMock.ClearReceivedCalls();
            dbContextMock.ClearReceivedCalls();
            loggerMock.ClearReceivedCalls();

            var taskRepository = new TaskRepository(dbContextMock, loggerMock);

            var anyItemRecreated = new TaskEntity();
            anyItemRecreated.ID = dbQueryMockWithTestData.Last().ID;
            anyItemRecreated.Name = "Changed";

            // Act

            taskRepository.UpdateItem(anyItemRecreated);
            //   A) NullReferenceException     -> When setting context.Entry(task).State
            //   B) If mocking DbEntityEntry<> -> Mock-Exception:Parent has no default constructor

            // Assert

            Assert.AreSame(anyItemRecreated.Name, dbsetMock.Where(e => e.ID == anyItemRecreated.ID).Single().Name);

            dbsetMock.ReceivedWithAnyArgs(1);
            dbsetMock.Attach(anyItemRecreated).Received(1);
            dbContextMock.ReceivedWithAnyArgs(1);
            dbContextMock.Entry(anyItemRecreated).Received(1);
            dbContextMock.DidNotReceive().SaveChanges();

            loggerMock.ReceivedWithAnyArgs(1);
            loggerMock.Received(1).Info(anyItemRecreated.ID.ToString());
        }
    }
}