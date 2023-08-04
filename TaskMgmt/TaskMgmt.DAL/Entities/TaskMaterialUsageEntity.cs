using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace TaskMgmt.DAL
{
    public class TaskMaterialUsageEntity
    {
        [ForeignKey("TaskID")]
        public TaskEntity Task { get; set; }

        [ForeignKey("MaterialID")]
        public MaterialEntity Material { get; set; }

        public int Amount { get; set; }

        public string UnitOfMeasurement { get; set; }

        // FOREIGN KEYS
        public Guid TaskID { get; set; }
        public Guid MaterialID { get; set; }
    }
}
