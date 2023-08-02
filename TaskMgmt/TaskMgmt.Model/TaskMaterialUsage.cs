using System;
using System.Runtime.Serialization;

namespace TaskMgmt.Model
{
    [DataContract]
    public class TaskMaterialUsage
    {
        public TaskMaterialUsage()
        {
        }

        public TaskMaterialUsage(Material material)
        {
            Material = material;
        }

        [DataMember]
        public Material Material { get; set; }

        [DataMember]
        public int Amount { get; set; }

        [DataMember]
        public Unit UnitOfMeasurement { get; set; }

    }
}
