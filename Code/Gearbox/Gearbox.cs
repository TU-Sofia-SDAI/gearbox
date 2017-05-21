using GearboxContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GearboxComputer
{
    public class Gearbox
    {
        static void Main(string[] args)
        {
            var gearbox = new FiveSpeedGearBox.FiveSpeedGearBox();
            var listener = new Listener();
            var gearboxComputer = new GearboxComputer(EngineType.Diesel, gearbox, listener);

            gearboxComputer.SetGear(2);

            gearboxComputer.Calculate(
                new GearboxComputerData {
                    AccelerationLevel = 100,
                    DrivingMode = DrivingMode.Normal,
                    RPM = 2000,
                    VehicleSpeed = 110
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
}
