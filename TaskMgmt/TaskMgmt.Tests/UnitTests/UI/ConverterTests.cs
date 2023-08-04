using NUnit.Framework;
using System.Windows;
using TaskMgmt.UI.Converters;

namespace TaskMgmt.Tests.UnitTests.UI
{
    [TestFixture]
    public class ConverterTests
    {
        [Test]
        public void Convert_FromTrue_ShouldBeVisible()
        {
            // Setup
            BoolVisibilityConverter bv = new BoolVisibilityConverter();

            // Act
            var result = bv.Convert(true, typeof(Visibility), null, null);

            // Assert
            Assert.That(result, Is.EqualTo(Visibility.Visible), "Some useful error message");
        }

        [Test]
        public void Convert_FromFalse_ShouldBeCollapsed()
        {
            // Setup
            BoolVisibilityConverter bv = new BoolVisibilityConverter();

            // Act
            var result = bv.Convert(false, typeof(Visibility), null, null);

            // Assert
            Assert.That(result, Is.EqualTo(Visibility.Collapsed), "Some useful error message");
        }

        [Test]
        public void Convert_FromObjectNull_ShouldBeFalse()
        {
            // Setup
            ObjectNullToFalseConverter objNullToFalseConv = new ObjectNullToFalseConverter();
            object anyObject = null;

            // Act
            var result = objNullToFalseConv.Convert(anyObject, typeof(bool), null, null);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void Convert_FromObjectNotNull_ShouldBeTrue()
        {
            // Setup
            ObjectNullToFalseConverter objNullToFalseConv = new ObjectNullToFalseConverter();
            var anyObject = new object();

            // Act
            var result = objNullToFalseConv.Convert(anyObject, typeof(bool), null, null);

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }
    }
}
