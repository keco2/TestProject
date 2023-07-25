using System;
using System.Collections.Generic;
using TaskMgmt.Model;

namespace TaskMgmt.DAL
{
    public static class StaticData
    {
        public static List<Task> Tasks = new List<Task>
            {
                new Task { ID = new Guid("00000000-0000-0000-0000-000000000001"), Name = "Task 1" },
                new Task { ID = new Guid("00000000-0000-0000-0000-000000000002"), Name = "Task 2" },
                new Task { ID = new Guid("00000000-0000-0000-0000-000000000003"), Name = "Task 3" }
            };

        public static List<Material> Materials = new List<Material>
        {
                new Material { ID = new Guid("00000000-0000-0000-0000-000000000001"), ManufacturerCode = 1, Partnumber = "ManufacturerCode 1", Price = 1, UniteOfIssue = "kg" },
                new Material { ID = new Guid("00000000-0000-0000-0000-000000000002"), ManufacturerCode = 2, Partnumber = "ManufacturerCode 2", Price = 22, UniteOfIssue = "m" },
                new Material { ID = new Guid("00000000-0000-0000-0000-000000000003"), ManufacturerCode = 3, Partnumber = "ManufacturerCode 3", Price = 333, UniteOfIssue = "l" }
        };
    }
}
