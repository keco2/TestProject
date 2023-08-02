using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskMgmt.DAL
{
    public class MaterialEntity
    {
        [Required]
        public Guid ID { get; set; }

        public string Partnumber { get; set; }

        public int ManufacturerCode { get; set; }

        public int Price { get; set; }

        public string UnitOfIssue { get; set; }

        // NAVIGATION PROPERTY
        public ICollection<TaskEntity> Tasks { get; internal set; }
    }
}
