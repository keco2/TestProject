using System;
using System.Runtime.Serialization;

namespace TaskMgmt.Model
{
    [DataContract]
    public class TaskMaterialUsage
    {
        private Task task;
        private Material material;
        private int amount;
        private Unit unitOfMeasurement;

        public TaskMaterialUsage()
        {
        }

        public TaskMaterialUsage(Task task, Material material)
        {
            Task = task;
            Material = material;
        }

        [DataMember]
        public Task Task { get => task; set => task = value; }

        [DataMember]
        public Material Material { get => material; set => material = value; }

        [DataMember]
        public int Amount { get => amount; set => amount = value; }

        [DataMember]
        public Unit UnitOfMeasurement { get => unitOfMeasurement; set => unitOfMeasurement = value; }

    }
}
