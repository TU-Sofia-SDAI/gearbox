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

            /// SysGB-COMP-18
            if (!IsDataReadingValid(data))
            {
                return; // TODO: log it somewhere and count to 1000 log to somewhere else
            }

            /// SysGB-COMP-25
            if (data.AccelerationLevel < 65)
            {
                int nextGearRPM = 2000;

                if (this.engineType == EngineType.Gasoline)
                {
                    nextGearRPM += 500;
                }

                /// SysGB-COMP-29
                if (data.DrivingMode == DrivingMode.Sport)
                {
                    nextGearRPM *= 2;
                }

                if (data.DrivingMode == DrivingMode.Normal)
                {
                    nextGearRPM += 500;
                }

                /// SysGB-COMP-31
                if (data.RPM >= nextGearRPM)
                {
                    predictedGear++;
                }

                /// SysGB-COMP-32
                int previousGearRPM = 1150;

                /// SysGB-COMP-33
                if (this.engineType == EngineType.Gasoline)
                {
                    previousGearRPM += 350;
                }

                /// SysGB-COMP-34
                if (data.DrivingMode == DrivingMode.Sport)
                {
                    previousGearRPM *= 2;
                }

                /// SysGB-COMP-35
                if (data.DrivingMode == DrivingMode.Economycal)
                {
                    previousGearRPM -= 200;
                }

                /// SysGB-COMP-36
                if (data.RPM <= previousGearRPM)
                {
                    predictedGear--;
                }
            }
            else
            {
                int speedInteger = data.VehicleSpeed;
                /// SysGB-COMP-38
                int correction = 15;

                /// SysGB-COMP-39
                if (data.DrivingMode == DrivingMode.Economycal)
                {
                    correction += 40;
                }

                /// SysGB-COMP-40
                if (data.DrivingMode == DrivingMode.Normal && data.AccelerationLevel < 85)
                {
                    correction += 25;
                }

                /// SysGB-COMP-41
                if (data.DrivingMode == DrivingMode.Normal && data.AccelerationLevel >= 85)
                {
                    correction += 15;
                }

                /// SysGB-COMP-42
                speedInteger += correction;

                /// SysGB-COMP-43
                for (int i = 0; i < this.gearbox.GearsCount; i++)
                {
                    if (this.gearbox.GearsRatios[i] > speedInteger)
                    {
                        predictedGear = (i + 1);
                    }
                }

            }

            /// SysGB-COMP-22
            if (predictedGear < 1)
            {
                predictedGear = 1;
            }

            /// SysGB-COMP-23
            if (predictedGear > this.gearbox.GearsCount)
            {
                predictedGear = this.gearbox.GearsCount;
            }

            /// SysGB-COMP-48, SysGB-COMP-49, SysGB-COMP-50
            if (predictedGear != currentGear)
            {
                /// SysGB-COMP-46, SysGB-COMP-47
                this.listener.Receive(this.currentGear, predictedGear); // We have update for the gear
            }
        }

        public static bool IsDataReadingValid(GearboxComputerData data)
        {
            /// SysGB-COMP-16
            if (data.AccelerationLevel > 100 || data.AccelerationLevel < 0)
            {
                return false;
            }

            /// SysGB-COMP-13
            if (data.RPM < 0 || data.RPM > 9000) // For formula 1 bolids, use different check
            {
                return false;
            }

            /// SysGB-COMP-14
            if (data.VehicleSpeed < 0 || data.VehicleSpeed > 300) // Again, this is for normal cars
            {
                return false;
            }

            return true;
        }
    }
}
