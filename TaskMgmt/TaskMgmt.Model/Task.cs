using System;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace TaskMgmt.Model
{
    [DataContract]
    public class Task
    {
        public Task()
        {
            ID = Guid.NewGuid();
        }

        [Required]
        [DataMember]
        public string Name { get; set; }

        [Required]
        [DataMember]
        public Guid ID { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int TotalDuration { get; set; }
    }
}
