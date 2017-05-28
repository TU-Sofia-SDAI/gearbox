using NUnit.Framework;
using GearboxComputer;
using Moq;
using GearboxContracts;

namespace Code.Tests.ValidatorTests
{
    [TestFixture]
    public class IsGearboxValidMethod
    {
        [Test]
        [TestCase(0)]
        [TestCase(-6)]

        public void ShouldReturnFalse_IfGearboxCountValueIsLessThanOrEqualToZero(int valueOfGearsCount)
        {
            // Arrange
            var gearbox = new Mock<IGearBox>();
            gearbox.Setup(m => m.GearsCount).Returns(valueOfGearsCount);

            // Act
            var isGearboxValid = Validator.IsGearboxValid(gearbox.Object);

            // Assert
            Assert.IsFalse(isGearboxValid);
        }

        [Test]
        public void ShouldReturnTrue_IfGearboxCountValueIsCorrect()
        {
            // Arrange
            var gearbox = new Mock<IGearBox>();
            gearbox.Setup(m => m.GearsCount).Returns(4);

            // Act
            var isGearboxValid = Validator.IsGearboxValid(gearbox.Object);

            // Assert
            Assert.IsTrue(isGearboxValid);
        }
    }
}
