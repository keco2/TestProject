﻿using System;
using System.Collections.Generic;
using System.Linq;
using TaskMgmt.Common;
using TaskMgmt.Model;

namespace TaskMgmt.BLL
{
    public class UnitVariation
    {
        enum UnitEnum { mg, g, kg, t, oz, lb, mm, cm, m, km, l, ml, cm3, m3, gal, pcs, inch, ft };

        private readonly Dictionary<UnitEnum, UnitEnum[]> _unitVariationSetup = new Dictionary<UnitEnum, UnitEnum[]>()
        {
            { UnitEnum.pcs, new UnitEnum[] { UnitEnum.pcs } },
            { UnitEnum.g, new UnitEnum[] { UnitEnum.g, UnitEnum.mg, UnitEnum.kg, UnitEnum.t, UnitEnum.oz, UnitEnum.lb } },
            { UnitEnum.l, new UnitEnum[] { UnitEnum.ml, UnitEnum.l, UnitEnum.cm3, UnitEnum.m3, UnitEnum.gal } },
            { UnitEnum.m, new UnitEnum[] { UnitEnum.mm, UnitEnum.cm, UnitEnum.m, UnitEnum.km, UnitEnum.inch, UnitEnum.ft } }
        };

        /// <summary>
        /// Get all unit variations
        /// </summary>
        /// <returns></returns>
        public string[] GetVariations()
        {
            return Enum.GetValues(typeof(UnitEnum))
                .Cast<UnitEnum>()
                .Select(u => u.ToString())
                .ToArray();
        }

        /// <summary>
        /// Get variations available for the given base-unit e.g. for gram: [ mg, g, kg, ...]
        /// </summary>
        /// <param name="baseUnit"></param>
        /// <returns></returns>
        public string[] GetVariations(Unit baseUnit)
        {
            if (String.IsNullOrEmpty(baseUnit?.Value))
            {
                return new string[] { String.Empty };
            }

            UnitEnum unitEnum = baseUnit.Value.ToEnum<UnitEnum>();
            return _unitVariationSetup[unitEnum]
                .Select(u => u.ToString())
                .ToArray();
        }

        /// <summary>
        /// Get only base units e.g. [ l, g, m ] but not [ mg, kg, ml, mm, cm ...]
        /// </summary>
        /// <returns></returns>
        public string[] GetBaseUnits()
        {
            return _unitVariationSetup
                .Select(us => us.Key.ToString())
                .ToArray();
        }
    }
}
