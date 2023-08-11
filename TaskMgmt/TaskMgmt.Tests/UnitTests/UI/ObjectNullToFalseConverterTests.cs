using NUnit.Framework;
using TaskMgmt.UI.Converters;

namespace TaskMgmt.Tests.UnitTests.UI
{
    [TestFixture]
    public class ObjectNullToFalseConverterTests
    {

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
