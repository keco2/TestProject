using System;
using System.Collections.Generic;
using System.Linq;
using TaskMgmt.Model;

namespace TaskMgmt.DAL
{
    public static class StaticData
    {
        private static Task task1 = new Task { ID = new Guid("00000000-0000-0000-0000-000000000001"), Name = "Task 1" };
        private static Task task2 = new Task { ID = new Guid("00000000-0000-0000-0000-000000000002"), Name = "Task 2" };
        private static Task task3 = new Task { ID = new Guid("00000000-0000-0000-0000-000000000003"), Name = "Task 3" };
        private static Material material1 = new Material { ID = new Guid("7d6f2f1d-5628-4c5a-a125-3dc534b48a91"), Partnumber = "SCREW001", ManufacturerCode = 100, Price = 25, UniteOfIssue = "Kg"        };
        private static Material material2 = new Material { ID = new Guid("e81d0c3b-9c16-45b1-bd63-42e9784e5f3f"), Partnumber = "OIL123", ManufacturerCode = 80, Price = 35, UniteOfIssue = "Liter"        };
        private static Material material3 = new Material { ID = new Guid("c9efb625-865e-44b3-bd7e-721ef3e94738"), Partnumber = "BOLT009", ManufacturerCode = 45, Price = 10, UniteOfIssue = "Piece"       };
        private static Material material4 = new Material { ID = new Guid("34e8c2e2-5e02-4ce1-97b1-7f4d16250a96"), Partnumber = "RADIATOR202", ManufacturerCode = 75, Price = 70, UniteOfIssue = "Piece"   };
        private static Material material5 = new Material { ID = new Guid("fbf0c02a-125c-4b71-9e47-8a7f916f31e9"), Partnumber = "AIRFILTER087", ManufacturerCode = 60, Price = 15, UniteOfIssue = "Piece"  };
        private static Material material6 = new Material { ID = new Guid("5da2645e-d8a9-4d8a-945b-8dfbf68d8c7f"), Partnumber = "SPARKPLUG342", ManufacturerCode = 40, Price = 12, UniteOfIssue = "Piece"  };

        public static ICollection<Task> Tasks = new List<Task>
        {
            task1,
            task2,
            task3
        };

        public static ICollection<Material> Materials = new List<Material>
        {
            material1,
            material2,
            material3,
            material4,
            material5,
            material6
        };

        public static ICollection<TaskMaterialUsage> TaskMaterialUsages = new List<TaskMaterialUsage>
        {
            new TaskMaterialUsage { Task = task1, Material = material1, Amount = 1, UniteOfMeasurement = "kg" },
            new TaskMaterialUsage { Task = task1, Material = material3, Amount = 2, UniteOfMeasurement = "l" },
            new TaskMaterialUsage { Task = task2, Material = material1, Amount = 4, UniteOfMeasurement = "g" }
        };
    }
}
