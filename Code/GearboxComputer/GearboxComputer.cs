using GearboxContracts;
using System;

namespace GearboxComputer
{
    public class GearboxComputer
    {
        private int currentGear;
        private IGearBox gearbox;
        private EngineType engineType;
        private IGearboxComputerListener listener;

        // When gearbox is initialized, it should get the right config
        public GearboxComputer(EngineType engineType, IGearBox gearbox, IGearboxComputerListener listener)
        {
            // TODO: validate those
            this.gearbox = gearbox;
            this.engineType = engineType;
            this.listener = listener;
        }

        // When driver or something changes gear, gearbox computer must know
        public void SetGear(int gear)
        {
            this.currentGear = gear;
        }

        public void Calculate(GearboxComputerData data)
        {
            int predictedGear = this.currentGear;

            if (!IsDataReadingValid(data))
            {
                return; // TODO: log it somewhere and count to 1000 log to somewhere else
            }

            if (data.AccelerationLevel < 65)
            {
                int nextGearRPM = 2000;

                if (this.engineType == EngineType.Gasoline)
                {
                    nextGearRPM += 500;
                }

                if (data.DrivingMode == DrivingMode.Sport)
                {
                    nextGearRPM *= 2;
                }

                if (data.DrivingMode == DrivingMode.Normal)
                {
                    nextGearRPM += 500;
                }

                if (data.RPM >= nextGearRPM)
                {
                    predictedGear++;
                }

                int previousGearRPM = 1150;

                if (this.engineType == EngineType.Gasoline)
                {
                    previousGearRPM += 350;
                }

                if (data.DrivingMode == DrivingMode.Sport)
                {
                    previousGearRPM *= 2;
                }

                if (data.DrivingMode == DrivingMode.Economycal)
                {
                    previousGearRPM -= 200;
                }

                if (data.RPM <= previousGearRPM)
                {
                    predictedGear--;
                }
            }
            else
            {
                int speedInteger = Convert.ToInt32(data.VehicleSpeed); // use integer for speed to avoid slow calculations
                int correction = 15;

                if (data.DrivingMode == DrivingMode.Economycal)
                {
                    correction += 40;
                }

                if (data.DrivingMode == DrivingMode.Normal && data.AccelerationLevel < 85)
                {
                    correction += 25;
                }

                if (data.DrivingMode == DrivingMode.Normal && data.AccelerationLevel >= 85)
                {
                    correction += 15;
                }

                speedInteger += correction;

                for (int i = 1; i < this.gearbox.GearsCount; i++)
                {
                    if (this.gearbox.GearsRatios[i] > speedInteger)
                    {
                        predictedGear = i;
                    }
                }

            }

            if (predictedGear < 1)
            {
                predictedGear = 1;
            }

            if (predictedGear > this.gearbox.GearsCount)
            {
                predictedGear = this.gearbox.GearsCount;
            }

            if (predictedGear != currentGear)
            {
                this.listener.Receive(this.currentGear, predictedGear); // We have update for the gear
            }
        }

        public static bool IsDataReadingValid(GearboxComputerData data)
        {
            if (data.AccelerationLevel > 100 || data.AccelerationLevel < 0)
            {
                return false;
            }

            if (data.RPM < 0 || data.RPM > 9000) // For formula 1 bolids, use different check
            {
                return false;
            }

            if (data.VehicleSpeed < 0 || data.VehicleSpeed > 300) // Again, this is for normal cars
            {
                return false;
            }

            return true;
        }
    }
}
