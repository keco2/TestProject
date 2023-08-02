using NUnit.Framework;
using TaskMgmt.WcfService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskMgmt.Model;
using TaskMgmt.DAL;
using AutoMapper;

namespace TaskMgmt.Tests.UnitTests.WcfService
{
    [TestFixture]
    public class MapperConfigTests
    {
        [Test]
        public void TaskMapperConfig_MapTaskToTaskEntity_ShouldbeEqual()
        {
            // Setup

            Guid guidExpected = new Guid("00000000-0000-0000-0000-000000000001");
            string nameExpected = "name";
            string descExpected = "description";
            int totaldurationExpected = 11;

            var taskSource = new Task()
            {
                ID = guidExpected,
                Name = nameExpected,
                Description = descExpected,
                TotalDuration = totaldurationExpected
            };

            IMapper mapper = new Mapper(new TaskMapperConfig());

            // Act
            TaskEntity taskResult = mapper.Map<TaskEntity>(taskSource);

            // Assert
            Assert.AreEqual(taskSource.ID, taskResult.ID);
            Assert.AreEqual(taskSource.Name, taskResult.Name);
            Assert.AreEqual(taskSource.Description, taskResult.Description);
            Assert.AreEqual(taskSource.TotalDuration, taskResult.TotalDuration);
        }

        [Test]
        public void TaskMapperConfig_MapTaskEntityToTask_ShouldbeEqual()
        {
            // Setup

            Guid guidExpected = new Guid("00000000-0000-0000-0000-000000000001");
            string nameExpected = "name";
            string descExpected = "description";
            int totaldurationExpected = 11;

            var taskSource = new TaskEntity()
            {
                ID = guidExpected,
                Name = nameExpected,
                Description = descExpected,
                TotalDuration = totaldurationExpected
            };

            var mapper = new Mapper(new TaskMapperConfig());

            // Act
            Task taskResult = mapper.Map<Task>(taskSource);

            // Assert
            Assert.AreEqual(taskSource.ID, taskResult.ID);
            Assert.AreEqual(taskSource.Name, taskResult.Name);
            Assert.AreEqual(taskSource.Description, taskResult.Description);
            Assert.AreEqual(taskSource.TotalDuration, taskResult.TotalDuration);
        }

        [Test]
        public void MaterialMapperConfig_MapMaterialToMaterialEntity_ShouldbeEqual()
        {
            // Setup

            Guid guidExpected = new Guid("00000000-0000-0000-0000-000000000001");
            int manufacturerCodeExpected = 11;
            string partnumberExpected = "partnumberExpected";
            int priceExpected = 123;
            Unit unitOfIssueExpected = new Unit("kg");

            var materialSource = new Material()
            {
                ID = guidExpected,
                ManufacturerCode = manufacturerCodeExpected,
                Partnumber = partnumberExpected,
                Price = priceExpected,
                UnitOfIssue = unitOfIssueExpected
            };

            IMapper mapper = new Mapper(new MaterialMapperConfig());

            // Act
            MaterialEntity materialResult = mapper.Map<MaterialEntity>(materialSource);

            // Assert
            Assert.AreEqual(guidExpected, materialResult.ID);
            Assert.AreEqual(manufacturerCodeExpected, materialResult.ManufacturerCode);
            Assert.AreEqual(partnumberExpected, materialResult.Partnumber);
            Assert.AreEqual(priceExpected, materialResult.Price);
            Assert.AreEqual(unitOfIssueExpected.Value, materialResult.UnitOfIssue);
        }

        [Test]
        public void MaterialMapperConfig_MapMaterialEntityToMaterial_ShouldbeEqual()
        {
            // Setup

            Guid guidExpected = new Guid("00000000-0000-0000-0000-000000000001");
            int manufacturerCodeExpected = 11;
            string partnumberExpected = "partnumberExpected";
            int priceExpected = 123;
            Unit unitOfIssueExpected = new Unit("kg");

            var materialSource = new MaterialEntity()
            {
                ID = guidExpected,
                ManufacturerCode = manufacturerCodeExpected,
                Partnumber = partnumberExpected,
                Price = priceExpected,
                UnitOfIssue = unitOfIssueExpected.Value
            };

            IMapper mapper = new Mapper(new MaterialMapperConfig());

            // Act
            Material materialResult = mapper.Map<Material>(materialSource);

            // Assert
            Assert.AreEqual(guidExpected, materialResult.ID);
            Assert.AreEqual(manufacturerCodeExpected, materialResult.ManufacturerCode);
            Assert.AreEqual(partnumberExpected, materialResult.Partnumber);
            Assert.AreEqual(priceExpected, materialResult.Price);
            Assert.AreEqual(unitOfIssueExpected.Value, materialResult.UnitOfIssue.Value);
        }

        [Test]
        public void TaskMaterialUsageMapperConfig_MapUsageEntityToUsage_ShouldbeEqual()
        {
            // Setup

            Guid taskGuidExpected = new Guid("00000000-0000-0000-0000-000000000001");
            string nameExpected = "name";
            string descExpected = "description";
            int totaldurationExpected = 11;

            Guid materialGuidExpected = new Guid("00000000-0000-0000-0000-000000000002");
            int manufacturerCodeExpected = 11;
            string partnumberExpected = "partnumberExpected";
            int priceExpected = 123;
            Unit unitOfIssueExpected = new Unit("kg");

            int amountExpected = 2;
            Unit unitOfMeasurementExpected = new Unit("g");

            var taskSource = new TaskEntity()
            {
                ID = taskGuidExpected,
                Name = nameExpected,
                Description = descExpected,
                TotalDuration = totaldurationExpected
            };

            var materialSource = new MaterialEntity()
            {
                ID = materialGuidExpected,
                ManufacturerCode = manufacturerCodeExpected,
                Partnumber = partnumberExpected,
                Price = priceExpected,
                UnitOfIssue = unitOfIssueExpected.Value
            };

            var usageSource = new TaskMaterialUsageEntity()
            {
                Task = taskSource,
                TaskID = taskSource.ID,
                Material = materialSource,
                MaterialID = materialSource.ID,
                Amount = amountExpected,
                UnitOfMeasurement = unitOfMeasurementExpected.Value
            };

            IMapper mapper = new Mapper(new TaskMaterialUsageMapperConfig());

            // Act
            TaskMaterialUsage usageResult = mapper.Map<TaskMaterialUsage>(usageSource);

            // Assert
            Assert.AreEqual(materialGuidExpected, usageResult.Material.ID);
            Assert.AreEqual(manufacturerCodeExpected, usageResult.Material.ManufacturerCode);
            Assert.AreEqual(partnumberExpected, usageResult.Material.Partnumber);
            Assert.AreEqual(priceExpected, usageResult.Material.Price);
            Assert.AreEqual(unitOfIssueExpected.Value, usageResult.Material.UnitOfIssue.Value);

            Assert.AreEqual(taskGuidExpected, usageResult.Task.ID);
            Assert.AreEqual(nameExpected, usageResult.Task.Name);
            Assert.AreEqual(descExpected, usageResult.Task.Description);
            Assert.AreEqual(totaldurationExpected, usageResult.Task.TotalDuration);

            Assert.AreEqual(amountExpected, usageResult.Amount);
            Assert.AreEqual(unitOfMeasurementExpected.Value, usageResult.UnitOfMeasurement.Value);
        }
    }
}