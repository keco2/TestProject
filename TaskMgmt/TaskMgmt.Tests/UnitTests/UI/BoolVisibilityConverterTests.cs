using NUnit.Framework;
using System.Windows;
using TaskMgmt.UI.Converters;

namespace TaskMgmt.UI.UnitTests
{
    [TestFixture]
    public class BoolVisibilityConverterTests
    {
        [TestCase(false, Visibility.Collapsed)]
        [TestCase(true, Visibility.Visible)]
        public void BoolVisibilityConverter_From_ShouldBe(bool inBool, Visibility expected)
        {
            // Setup
            BoolVisibilityConverter bv = new BoolVisibilityConverter();

            // Act
            var result = bv.Convert(inBool, typeof(Visibility), null, null);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
