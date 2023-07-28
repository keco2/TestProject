using System;
using System.Runtime.Serialization;

namespace TaskMgmt.Model
{
    [DataContract]
    public class Material
    {
        private Guid id;
        private string partnumber;
        private int manufacturerCode;
        private int price;
        private Unit uniteOfIssue;

        public Material()
        {
            ID = Guid.NewGuid();
        }

        [DataMember]
        public Guid ID { get => id; set => id = value; }

        [DataMember]
        public string Partnumber { get => partnumber; set => partnumber = value; }

        [DataMember]
        public int ManufacturerCode { get => manufacturerCode; set => manufacturerCode = value; }

        [DataMember]
        public int Price { get => price; set => price = value; }

        [DataMember]
        public Unit UniteOfIssue { get => uniteOfIssue; set => uniteOfIssue = value; }

    }
}
