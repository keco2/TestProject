using System;
using System.Runtime.Serialization;

namespace TaskMgmt.Model
{
    [DataContract]
    public class Task
    {
        public Task()
        {
            ID = Guid.NewGuid();
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Guid ID { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int TotalDuration { get; set; }
    }
}
