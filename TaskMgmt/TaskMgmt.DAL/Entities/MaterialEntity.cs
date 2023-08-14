using System;
using System.ComponentModel.DataAnnotations;

namespace TaskMgmt.DAL
{
    public class MaterialEntity
    {
        [Required]
        public Guid ID { get; set; }

        public string Partnumber { get; set; }

        [Required]
        public int ManufacturerCode { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public string UnitOfIssue { get; set; }

    }
}
