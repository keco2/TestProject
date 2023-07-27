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
        private static Material material1 = new Material { ID = new Guid("00000000-0000-0000-0000-000000000007"), ManufacturerCode = 1, Partnumber = "ManufacturerCode 1", Price = 1, UniteOfIssue = "kg" };
        private static Material material2 = new Material { ID = new Guid("00000000-0000-0000-0000-000000000008"), ManufacturerCode = 2, Partnumber = "ManufacturerCode 2", Price = 22, UniteOfIssue = "m" };
        private static Material material3 = new Material { ID = new Guid("00000000-0000-0000-0000-000000000009"), ManufacturerCode = 3, Partnumber = "ManufacturerCode 3", Price = 333, UniteOfIssue = "l" };

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
            material3
        };

        public static ICollection<TaskMaterialUsage> TaskMaterialUsages = new List<TaskMaterialUsage>
        {
            new TaskMaterialUsage { Task = task1, Material = material1 },
            new TaskMaterialUsage { Task = task1, Material = material3 },
            new TaskMaterialUsage { Task = task2, Material = material1 }
        };
    }
}
