using System;
using System.ComponentModel.DataAnnotations;
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
        private Unit unitOfIssue;

        public Material()
        {
            ID = Guid.NewGuid();
        }

        [Required]
        [DataMember]
        public Guid ID { get => id; set => id = value; }

        [DataMember]
        public string Partnumber { get => partnumber; set => partnumber = value; }

        [Required]
        [DataMember]
        public int ManufacturerCode { get => manufacturerCode; set => manufacturerCode = value; }

        [Required]
        [DataMember]
        public int Price { get => price; set => price = value; }

        [Required]
        [DataMember]
        public Unit UnitOfIssue { get => unitOfIssue; set => unitOfIssue = value; }

    }
}
