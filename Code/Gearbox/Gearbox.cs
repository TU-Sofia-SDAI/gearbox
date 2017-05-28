using GearboxContracts;
using System;

namespace GearboxComputer
{
    public class Gearbox
    {
        static void Main(string[] args)
        {
            var gearbox = new FiveSpeedGearBox.FiveSpeedGearBox();
            var listener = new Listener();
            var communicator = new Communicator();
            var gearboxComputer = new GearboxComputerLogic(EngineType.Diesel, gearbox, listener, communicator);

            gearboxComputer.SetGear(2);

            gearboxComputer.Calculate(
                new GearboxComputerData {
                    AccelerationLevel = 30,
                    DrivingMode = DrivingMode.Normal,
                    RPM = 2000,
                    VehicleSpeed = 100
                });

        }
    }

    public class Listener : IGearboxComputerListener
    {
        public void Receive(int currentGear, int nextGear)
        {
            Console.WriteLine("Current gear is {0}, and gearbox computer suggests {1}", currentGear, nextGear);
        }
    }

    public class Communicator : ICarComunication
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void Warn(string message)
        {
            Console.WriteLine(message);
        }
    }
}
