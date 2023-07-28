﻿using System;
using System.Collections.Generic;
using System.Linq;
using TaskMgmt.Common;

namespace TaskMgmt.BLL
{
    public enum UnitEnum { mg, g, kg, mm, m, km, l, ml };

    public class UnitVariation
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
}
