using System;
using NUnit.Framework;
using GearboxContracts;
using Moq;
using GearboxComputer;
using System.Collections.Generic;

namespace Code.Tests.GearBoxComputerTests
{
    [TestFixture]
    public class CalculateMethod
    {
        [Test]
        [TestCase(120, 1000, 150)] // Acceleration level is invalid.
        [TestCase(80, -200, 150)] // RPM is invalid.
        [TestCase(80, 1000, 400)] // Vehicle speed is invalid.

        public void ShouldNotCallListenerReceiveMethod_BecauseThePassedValuesAreNotValid(int acceleraionLevel, int rpm, int vehicleSpeed)
        {
            // Arrange
            var engineType = EngineType.Diesel;
            var gearbox = new Mock<IGearBox>();
            var listener = new Mock<IGearboxComputerListener>();
            var communicator = new Mock<ICarComunication>();

            // Act
            gearbox.Setup(m => m.GearsCount).Returns(5);
            listener.Setup(m => m.Receive(2, 6)).Verifiable();

            var gearboxComputerLogic = new GearboxComputerLogic(engineType, gearbox.Object, listener.Object, communicator.Object);
            var gearboxComputerData = new GearboxComputerData();
            gearboxComputerData.AccelerationLevel = acceleraionLevel;
            gearboxComputerData.DrivingMode = DrivingMode.Normal;
            gearboxComputerData.RPM = rpm;
            gearboxComputerData.VehicleSpeed = vehicleSpeed;

            gearboxComputerLogic.Calculate(gearboxComputerData);

            // Assert
            listener.Verify(m => m.Receive(2, 6), Times.Exactly(0));
        }

        [Test]
        [TestCase(50, 6000, 100, EngineType.Gasoline, DrivingMode.Sport)]
        [TestCase(50, 2000, 100, EngineType.Diesel, DrivingMode.Economycal)]

        public void ShouldCallListenerReceiveMethod_BecauseThePassedValuesAreValid_AndAccelerationLevelIsLessThanSixtyFive(int acceleraionLevel, int rpm, int vehicleSpeed, EngineType engineType, DrivingMode drivingMode)
        {
            // Arrange
            var gearbox = new Mock<IGearBox>();
            var listener = new Mock<IGearboxComputerListener>();
            var communicator = new Mock<ICarComunication>();

            // Act
            gearbox.Setup(m => m.GearsCount).Returns(5);
            listener.Setup(m => m.Receive(2, 3)).Verifiable();

            var gearboxComputerLogic = new GearboxComputerLogic(engineType, gearbox.Object, listener.Object, communicator.Object);
            gearboxComputerLogic.SetGear(2);
            var gearboxComputerData = new GearboxComputerData();
            gearboxComputerData.AccelerationLevel = acceleraionLevel;
            gearboxComputerData.DrivingMode = drivingMode;
            gearboxComputerData.RPM = rpm;
            gearboxComputerData.VehicleSpeed = vehicleSpeed;

            gearboxComputerLogic.Calculate(gearboxComputerData);

            // Assert
            listener.Verify(m => m.Receive(2, 3), Times.Exactly(1));
        }

        [Test]
        [TestCase(70, 2000, 100, EngineType.Gasoline, DrivingMode.Economycal)]
        [TestCase(90, 2000, 100, EngineType.Diesel, DrivingMode.Normal)]
        [TestCase(70, 2000, 100, EngineType.Diesel, DrivingMode.Normal)]

        public void ShouldCallListenerReceiveMethod_BecauseThePassedValuesAreValid_AndAccelerationLevelIsBiggerThanSixtyFive(int acceleraionLevel, int rpm, int vehicleSpeed, EngineType engineType, DrivingMode drivingMode)
        {
            // Arrange
            var gearbox = new Mock<IGearBox>();
            var listener = new Mock<IGearboxComputerListener>();
            var communicator = new Mock<ICarComunication>();

            // Act
            gearbox.Setup(m => m.GearsCount).Returns(5);
            gearbox.Setup(m => m.GearsRatios).Returns(new List<float>()
            {
                30, 70, 120, 170, 200
            });

            listener.Setup(m => m.Receive(3, 4)).Verifiable();

            var gearboxComputerLogic = new GearboxComputerLogic(engineType, gearbox.Object, listener.Object, communicator.Object);
            gearboxComputerLogic.SetGear(3);
            var gearboxComputerData = new GearboxComputerData();
            gearboxComputerData.AccelerationLevel = acceleraionLevel;
            gearboxComputerData.DrivingMode = drivingMode;
            gearboxComputerData.RPM = rpm;
            gearboxComputerData.VehicleSpeed = vehicleSpeed;

            gearboxComputerLogic.Calculate(gearboxComputerData);

            // Assert
            listener.Verify(m => m.Receive(3, 4), Times.Exactly(1));
        }
    }
}
