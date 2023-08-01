﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TaskMgmt.DAL
{
    public class TaskEntity
    {
        [Required]
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string TotalDuration { get; set; }
    }
}
