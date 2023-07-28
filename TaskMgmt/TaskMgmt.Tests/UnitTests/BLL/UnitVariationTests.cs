using NUnit.Framework;
using System.Linq;
using TaskMgmt.BLL;

namespace TaskMgmt.Tests.UnitTests.BLL
{
    [TestFixture]
    public class UnitVariationTests
    {
        [Test]
        public void GetVariations_ReturnsAllUnits()
        {
            // Setup
            var unitSetup = new UnitVariation();

            //Act
            var result = unitSetup.GetVariations().OrderBy(u => u);
            var expected = new string[] { "cm", "cm3", "ft", "g", "gal", "inch", "kg", "km", "l", "lb", "m", "m3", "mg", "ml", "mm", "oz", "pcs", "t"
 }.OrderBy(u => u);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetVariations_ForKg_Returns3()
        {
            // Setup
            var unitSetup = new UnitVariation();

            //Act
            var result = unitSetup.GetVariations("g");
            var expected = new string[] { "g", "mg", "kg", "t", "oz", "lb" };

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetVariations_ForMeter_Returns4()
        {
            // Setup
            var unitSetup = new UnitVariation();

            //Act
            var result = unitSetup.GetVariations("m");
            var expected = new string[] { "mm", "cm", "m", "km", "inch", "ft" };

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetVariations_ForLiter_Returns2()
        {
            // Setup
            var unitSetup = new UnitVariation();

            //Act
            var result = unitSetup.GetVariations("l");
            var expected = new string[] { "ml", "l", "cm3", "m3", "gal" };

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetVariations_ForPieces_Returns1()
        {
            // Setup
            var unitSetup = new UnitVariation();

            //Act
            var result = unitSetup.GetVariations("pcs");
            var expected = new string[] { "pcs" };

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
            var expected = new string[] { string.Empty };

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetVariations_ForEmpty_ReturnsEmpty()
        {
            // Setup
            var unitSetup = new UnitVariation();

            //Act
            var result = unitSetup.GetVariations("");
            var expected = new string[] { string.Empty };

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
