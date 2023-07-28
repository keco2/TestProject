﻿using System;
using System.Collections.Generic;
using System.Linq;
using TaskMgmt.Common;

namespace TaskMgmt.BLL
{
    public enum UnitEnum { mg, g, kg, t, oz, lb, mm, cm, m, km, l, ml, cm3, m3, gal, pcs, inch, ft };

    public class UnitVariation
    {
        private readonly Dictionary<UnitEnum, UnitEnum[]> _unitVariationSetup = new Dictionary<UnitEnum, UnitEnum[]>()
        {
            { UnitEnum.pcs, new UnitEnum[] { UnitEnum.pcs } },
            { UnitEnum.g, new UnitEnum[] { UnitEnum.g, UnitEnum.mg, UnitEnum.kg, UnitEnum.t, UnitEnum.oz, UnitEnum.lb } },
            { UnitEnum.l, new UnitEnum[] { UnitEnum.ml, UnitEnum.l, UnitEnum.cm3, UnitEnum.m3, UnitEnum.gal } },
            { UnitEnum.m, new UnitEnum[] { UnitEnum.mm, UnitEnum.cm, UnitEnum.m, UnitEnum.km, UnitEnum.inch, UnitEnum.ft } }
        };

        public string[] GetVariations()
        {
            return Enum.GetValues(typeof(UnitEnum))
                .Cast<UnitEnum>()
                .Select(u => u.ToString())
                .ToArray();
        }

        public string[] GetVariations(string baseUnit)
        {
            if (String.IsNullOrEmpty(baseUnit))
            {
                return new string[] { String.Empty };
            }

            UnitEnum unitEnum = baseUnit.ToEnum<UnitEnum>();
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
}
