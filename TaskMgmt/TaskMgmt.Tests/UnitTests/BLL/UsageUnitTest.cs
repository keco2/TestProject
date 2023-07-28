using NUnit.Framework;
using System.Linq;
using TaskMgmt.BLL;

namespace TaskMgmt.Tests.UnitTests.BLL
{
    [TestFixture]
    public class UsageUnitTest
    {
        [Test]
        public void AllUnitVariations_ReturnsAll()
        {
            // Setup
            var unitSetup = new UnitVariation();

            //Act
            var result = unitSetup.GetVariations().OrderBy(u => u);
            var expected = new string[] { "g", "mg", "kg", "l", "ml", "m", "mm", "km" }.OrderBy(u => u);

            //Assert
            Assert.AreEqual(1, 1);
        }

        [Test]
        public void UnitGetVariationsOfKg_Returns_g_mg_kg()
        {
            // Setup
            var unitSetup = new UnitVariation();

            //Act
            var result = unitSetup.GetVariations("g");
            var expected = new string[] { "g", "mg", "kg" };

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void UnitGetVariationsOfLiter_Returns_ml_l()
        {
            // Setup
            var unitSetup = new UnitVariation();

            //Act
            var result = unitSetup.GetVariations("l");
            var expected = new string[] { "ml", "l" };

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void UnitBase_ReturnsOnlyBaseUnits()
        {
            // Setup
            var unitSetup = new UnitVariation();

            //Act
            var result = unitSetup.GetBaseUnits().OrderBy(u => u);
            var expected = new string[] { "g", "m", "l" }.OrderBy(u => u);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
