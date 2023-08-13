using NUnit.Framework;
using System;
using System.Linq;
using TaskMgmt.BLL;
using TaskMgmt.Model;

namespace TaskMgmt.BLL.UnitTests
{
    [TestFixture]
    public class UnitVariationTests
    {
        [TestCase("g", new string[] { "g", "mg", "kg", "t", "oz", "lb" })]
        [TestCase("m", new string[] { "mm", "cm", "m", "km", "inch", "ft" })]
        [TestCase("l", new string[] { "ml", "l", "cm3", "m3", "gal" })]
        [TestCase("pcs", new string[] { "pcs" })]
        [TestCase(null, new string[] { "" })]
        public void GetVariations_ForBaseUnit_ReturnsCorrectResult(string baseUnit, string[] expected)
        {
            // Setup
            var unitSetup = new UnitVariation();

            //Act
            var result = unitSetup.GetVariations(new Unit(baseUnit));

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetVariations_ForNull_ReturnsEmpty()
        {
            // Setup
            var unitSetup = new UnitVariation();

            //Act
            var result = unitSetup.GetVariations(null);
            var expected = new string[] { String.Empty };

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetBaseUnits_ReturnsOnlyBaseUnits()
        {
            // Setup
            var unitSetup = new UnitVariation();

            //Act
            var result = unitSetup.GetBaseUnits().OrderBy(u => u);
            var expected = new string[] { "g", "m", "l", "pcs" }.OrderBy(u => u);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
