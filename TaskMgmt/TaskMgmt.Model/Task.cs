using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMgmt.Model
{
    [DataContract]
    public class Task
    {
        private string name;
        private Guid id;

        [DataMember]
        public string Name { get => name; set => name = value; }

        [DataMember]
        public Guid ID { get => id; set => id = value; }
    }
}
