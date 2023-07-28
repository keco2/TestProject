using System;
using System.Runtime.Serialization;

namespace TaskMgmt.Model
{
    [DataContract]
    public class Unit
    {
        private string value;

        public Unit(string value)
        {
            Value = value;
        }

        [DataMember]
        public string Value { get => this.value; set => this.value = value; }

        public override bool Equals(Object obj)
        {
            var unitobj = obj as Unit;

            if (unitobj == null)
            {
                return false;
            }

            return this.Value.Equals(unitobj.Value);
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}
