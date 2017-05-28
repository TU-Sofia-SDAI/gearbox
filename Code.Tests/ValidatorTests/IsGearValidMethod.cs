using NUnit.Framework;
using GearboxComputer;

namespace Code.Tests.ValidatorTests
{
    [TestFixture]
    public class IsGearValidMethod
    {
        [Test]
        public void ShouldReturnFalse_IfPassedGearIsLessThanMinusOne()
        {
            // Arrange
            int gear = -32;
            int maxGears = 5;

            // Act
            var isValid = Validator.IsGearValid(gear, maxGears);

            // Assert
            Assert.IsFalse(isValid);
        }

        [Test]
        public void ShouldReturnFalse_IfPassedGearIsBiggerThanMaxGears()
        {
            // Arrange
            int gear = 6;
            int maxGears = 5;

            // Act
            var isValid = Validator.IsGearValid(gear, maxGears);

            // Assert
            Assert.IsFalse(isValid);
        }

        [Test]
        [TestCase(-1, 5)]
        [TestCase(0, 5)]
        [TestCase(1, 5)]
        [TestCase(2, 5)]
        [TestCase(3, 5)]
        [TestCase(4, 5)]
        [TestCase(5, 5)]
        public void ShouldReturnTrue_IfPassedGearIsValid(int gear, int maxGears)
        {

            // Arrange & Act
            var isValid = Validator.IsGearValid(gear, maxGears);

            // Assert
            Assert.IsTrue(isValid);
        }
    }
}
