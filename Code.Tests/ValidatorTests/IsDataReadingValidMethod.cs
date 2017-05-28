using NUnit.Framework;
using GearboxContracts;
using GearboxComputer;

namespace Code.Tests.ValidatorTests
{
    [TestFixture]
    public class IsDataReadingValidMethod
    {
        [Test]
        public void ShouldReturnFalse_IfDataAccelerationLevelValueIsLessThanZero()
        {
            // Arrange
            var gearBoxComputerData = new GearboxComputerData();
            gearBoxComputerData.AccelerationLevel = -50;
            
            // Act
            var isDataReadingValid = Validator.IsDataReadingValid(gearBoxComputerData);

            // Assert
            Assert.IsFalse(isDataReadingValid);
        }

        [Test]
        public void ShouldReturnFalse_IfDataAccelerationLevelValueIsGreaterThanOneHundred()
        {
            // Arrange
            var gearBoxComputerData = new GearboxComputerData();
            gearBoxComputerData.AccelerationLevel = 150;

            // Act
            var isDataReadingValid = Validator.IsDataReadingValid(gearBoxComputerData);

            // Assert
            Assert.IsFalse(isDataReadingValid);
        }

        [Test]
        public void ShouldReturnFalse_IfDataRPMValueIsLessThanZero()
        {
            // Arrange
            var gearBoxComputerData = new GearboxComputerData();
            gearBoxComputerData.RPM = -10;

            // Act
            var isDataReadingValid = Validator.IsDataReadingValid(gearBoxComputerData);

            // Assert
            Assert.IsFalse(isDataReadingValid);
        }

        [Test]
        public void ShouldReturnFalse_IfDataRPMValueIsGreaterThanNineThousand()
        {
            // Arrange
            var gearBoxComputerData = new GearboxComputerData();
            gearBoxComputerData.RPM = 10000;

            // Act
            var isDataReadingValid = Validator.IsDataReadingValid(gearBoxComputerData);

            // Assert
            Assert.IsFalse(isDataReadingValid);
        }

        [Test]
        public void ShouldReturnFalse_IfDataVehicleSpeedValueIsLessThanZero()
        {
            // Arrange
            var gearBoxComputerData = new GearboxComputerData();
            gearBoxComputerData.VehicleSpeed = -20;

            // Act
            var isDataReadingValid = Validator.IsDataReadingValid(gearBoxComputerData);

            // Assert
            Assert.IsFalse(isDataReadingValid);
        }

        [Test]
        public void ShouldReturnFalse_IfDataVehicleSpeedValueIsGreaterThanThreeHundred()
        {
            // Arrange
            var gearBoxComputerData = new GearboxComputerData();
            gearBoxComputerData.VehicleSpeed = 450;

            // Act
            var isDataReadingValid = Validator.IsDataReadingValid(gearBoxComputerData);

            // Assert
            Assert.IsFalse(isDataReadingValid);
        }

        [Test]
        public void ShouldReturnTrue_IfDataAccelerationLevelValueIsCorrect()
        {
            // Arrange
            var gearBoxComputerData = new GearboxComputerData();
            gearBoxComputerData.AccelerationLevel = 50;

            // Act
            var isDataReadingValid = Validator.IsDataReadingValid(gearBoxComputerData);

            // Assert
            Assert.IsTrue(isDataReadingValid);
        }

        [Test]
        public void ShouldReturnTrue_IfDataRPMValueIsCorrect()
        {
            // Arrange
            var gearBoxComputerData = new GearboxComputerData();
            gearBoxComputerData.RPM = 1500;

            // Act
            var isDataReadingValid = Validator.IsDataReadingValid(gearBoxComputerData);

            // Assert
            Assert.IsTrue(isDataReadingValid);
        }

        [Test]
        public void ShouldReturnTrue_IfDataVehicleSpeedValueIsCorrect()
        {
            // Arrange
            var gearBoxComputerData = new GearboxComputerData();
            gearBoxComputerData.VehicleSpeed = 150;

            // Act
            var isDataReadingValid = Validator.IsDataReadingValid(gearBoxComputerData);

            // Assert
            Assert.IsTrue(isDataReadingValid);
        }
    }
}
