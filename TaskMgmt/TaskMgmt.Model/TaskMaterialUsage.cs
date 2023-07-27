using System;
using System.Runtime.Serialization;

namespace TaskMgmt.Model
{
    [DataContract]
    public class TaskMaterialUsage
    {
        private Task task;
        private Guid taskId;
        private Guid materialId;
        private int amount;
        private string uniteOfMeasurement;

        public TaskMaterialUsage()
        {
        }

        public TaskMaterialUsage(Guid taskId, Guid materialId)
        {
            TaskID = taskId;
            MaterialID = materialId;
        }

        [DataMember]
        public Task Task { get => task; set => task = value; }

        [DataMember]
        public Guid TaskID { get => taskId; set => taskId = value; }

        [DataMember]
        public Guid MaterialID { get => materialId; set => materialId = value; }

        [DataMember]
        public int Amount { get => amount; set => amount = value; }

        [DataMember]
        public string UniteOfMeasurement { get => uniteOfMeasurement; set => uniteOfMeasurement = value; }

    }
}
