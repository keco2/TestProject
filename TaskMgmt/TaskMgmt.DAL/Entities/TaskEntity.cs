using System;
using System.ComponentModel.DataAnnotations;

namespace TaskMgmt.DAL
{
    public class TaskEntity
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public int TotalDuration { get; set; }
    }
}
