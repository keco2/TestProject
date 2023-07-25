using System;
using System.Runtime.Serialization;

namespace TaskMgmt.Model
{
    [DataContract]
    public class Task
    {
        private string name;
        private Guid id;

        public Task()
        {
            ID = Guid.NewGuid();
        }

        [DataMember]
        public string Name { get => name; set => name = value; }

        [DataMember]
        public Guid ID { get => id; set => id = value; }
    }
}
