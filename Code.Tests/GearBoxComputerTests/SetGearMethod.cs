using System;
using NUnit.Framework;
using GearboxComputer;
using GearboxContracts;
using Moq;

namespace Code.Tests.GearBoxComputerTests
{
    [TestFixture]
    public class SetGearMethod
    {
        [Test]
        public void ShouldSetProparlyGear_IfPassedGearIsCorrect()
        {
            // Arrange
            var engineType = EngineType.Diesel;
            var gearbox = new Mock<IGearBox>();
            var listener = new Mock<IGearboxComputerListener>();
            var communicator = new Mock<ICarComunication>();

            // Act
            gearbox.Setup(m => m.GearsCount).Returns(6);

            var gearboxComputerLogic = new GearboxComputerLogic(engineType, gearbox.Object, listener.Object, communicator.Object);

            gearboxComputerLogic.SetGear(4);
            var expectedGear = 4;

            // Assert
            Assert.AreEqual(expectedGear, gearboxComputerLogic.CurrentGear);
        }

        [Test]
        [TestCase(6)]
        [TestCase(-2)]
        public void ShouldNotSetGear_IfPassedGearIsBiggerOrLessThanMinusOne(int gear)
        {
            // Arrange
            var engineType = EngineType.Diesel;
            var gearbox = new Mock<IGearBox>();
            var listener = new Mock<IGearboxComputerListener>();
            var communicator = new Mock<ICarComunication>();

            // Act
            gearbox.Setup(m => m.GearsCount).Returns(5);

            var gearboxComputerLogic = new GearboxComputerLogic(engineType, gearbox.Object, listener.Object, communicator.Object);

            gearboxComputerLogic.SetGear(gear);
            var expectedGear = 0;

            // Assert
            Assert.AreEqual(expectedGear, gearboxComputerLogic.CurrentGear);
        }
    }
}
