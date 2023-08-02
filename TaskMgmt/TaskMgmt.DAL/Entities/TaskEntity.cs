using System;
using System.ComponentModel.DataAnnotations;

namespace TaskMgmt.DAL
{
    public class TaskEntity
    {
        [Required]
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int TotalDuration { get; set; }

        // TASK-MATERIAL-USAGE FIELDS

        public int Amount { get; set; }

        public string UnitOfMeasurement { get; set; }

        // FOREIGN KEYS
        //public Guid MaterialFk { get; set; }
        public MaterialEntity Material { get; set; }
    }
}
