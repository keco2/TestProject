using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMgmt.Model
{

    class Unit
    {

    }

    public static class UnitExtensions
    {
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }

    public interface IStrategy
    {
        UnitEnum Base { get; }
        UnitEnum[] Variations { get; }
    }

    public enum UnitEnum { mg, g, kg, mm, m, km, l, ml };

    public class UnitSetup
    {
        private readonly Dictionary<UnitEnum, UnitEnum[]> _unitVariationSetup = new Dictionary<UnitEnum, UnitEnum[]>()
        {
            { UnitEnum.g, new UnitEnum[] { UnitEnum.g, UnitEnum.mg, UnitEnum.kg } },
            { UnitEnum.l, new UnitEnum[] { UnitEnum.ml, UnitEnum.l } },
            { UnitEnum.m, new UnitEnum[] { UnitEnum.mm, UnitEnum.m, UnitEnum.km } }
        };

        public string[] GetVariations()
        {
            return Enum.GetValues(typeof(UnitEnum))
                .Cast<UnitEnum>()
                .Select(u => u.ToString())
                .ToArray();
        }

        public string[] GetVariations(string unitString)
        {
            UnitEnum unitEnum = unitString.ToEnum<UnitEnum>();
            return _unitVariationSetup[unitEnum]
                .Select(u => u.ToString())
                .ToArray();
        }

        public string[] GetBaseUnits()
        {
            return _unitVariationSetup
                .Select(us => us.Key.ToString())
                .ToArray();
        }
    }

    public class UnitWeight : IStrategy
    {
        //private readonly string[] variations = { "mg", "g", "kg" };
        //public string[] Variations => variations;
        //public string Base { get => "g"; }
        public UnitEnum Base { get => UnitEnum.g; }

        public UnitEnum[] Variations { get => new UnitEnum[] { UnitEnum.mg, UnitEnum.g, UnitEnum.kg }; }

        public UnitWeight()
        {
        }
    }
}
