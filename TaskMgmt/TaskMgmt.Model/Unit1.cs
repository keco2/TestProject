//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace TaskMgmt.Model
//{

//    public interface IUnit
//    {
//        string Base { get; }
//        string[] Variations { get; }
//    }


//    public class Unit<T> : IUnit where T : IUnit, new()
//    {
//        private T instance;
//        private Type[] forms = new Type[] { typeof(UnitWeight), typeof(UnitDistance) };
//        Dictionary<string, Type> matrix = new Dictionary<string, Type>()
//        {
//            { "", typeof(UnitWeight) },
//            { "", typeof(UnitDistance) }
//        };

//        public T Instance { get => instance; set => instance = value; }
//        public string Base { get => instance.Base; }
//        public string[] Variations { get => instance.Variations; }
//        public string[] Forms { get => forms.Select(t => t.Name).ToArray(); }


//        public string Current { get; set; }

//        public Unit(string x)
//        {
//            //switch (x)
//            //{
//            //    "x" : break;
//            //    default:
//            //        break;
//            //}
//        }

//        public Unit()
//        {
//            instance = new T();
//        }
//    }


//    public class UnitDistance : IUnit
//    {
//        private readonly string[] variations = { "mm", "m", "km" };

//        public string[] Variations => variations;

//        public string Base { get => "m"; }
//    }






//    interface IUBase
//    {
//        string Base { get; set;  }
//    }

//    class UBase : IUBase
//    {
//        public string Base { get; set; }
//        public string Current { get; set; }
//        public string[] Variatons { get; set; }
//    }

//    class UX
//    {
//        public UBase Current { get; set; }
//        private enum EUnitBases { g, m };
//        private enum EUnitWeights { mg, g, kg };
//        private enum EUnitDistances { mg, g, kg };

//        //private readonly string[] UnitWeights = { "mg", "g", "kg" };
//        //private readonly string[] UnitDistances = { "mm", "m", "km" };

//        //private const string UnitWeightBase = "g";
//        //private const string UnitDistanceBase = "m";

//        //public string[] Bases = new string[] { UnitWeightBase, UnitDistanceBase }

//        public string[] CurrentVariations
//        {
//            get
//            {
//                switch (Current.g)
//                {
//                    case EUnitWeights.mg:
//                    case EUnitWeights.g:
//                    case EUnitWeights.kg:
//                        return Enum.GetValues(typeof(EUnitWeights)) as string[];
//                    default:
//                        throw new NotSupportedException();
//                }
//            }
//        }
//    }
//}
