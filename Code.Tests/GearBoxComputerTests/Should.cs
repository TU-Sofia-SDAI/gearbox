using System;
using NUnit.Framework;
using GearboxComputer;
using GearboxContracts;
using GearboxComputer.GearboxComputer;
using Moq;

namespace Code.Tests.GearBoxComputerTests
{
    [TestFixture]
    public class Should
    {
        [Test]
        public void CreateInstanceOfGearboxComputer()
        {
            // Arrange
            var engineType = EngineType.Diesel;
            var gearbox = new Mock<IGearBox>();
            var listener = new Mock<IGearboxComputerListener>();
            var communicator = new Mock<ICarComunication>();
            gearbox.Setup(m => m.GearsCount).Returns(4);

            // Act
            var gearboxComputer = new GearboxComputerLogic(engineType, gearbox.Object, listener.Object, communicator.Object);

            // Assert
            Assert.IsInstanceOf<GearboxComputerLogic>(gearboxComputer);
        }

        [Test]
        public void ThrowException_WithContainingSpecificMessage()
        {
            // Arrange
            var engineType = EngineType.Diesel;
            var gearbox = new Mock<IGearBox>();
            var listener = new Mock<IGearboxComputerListener>();
            var communicator = new Mock<ICarComunication>();

            // Act & Assert
            Assert.Throws<Exception>(() => new GearboxComputerLogic(engineType, gearbox.Object, listener.Object, communicator.Object));
        }
    }
}
