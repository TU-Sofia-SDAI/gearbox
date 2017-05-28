using GearboxContracts;
using System;

namespace GearboxComputer
{
    public class GearboxComputerLogic
    {
        private int currentGear;
        private IGearBox gearbox;
        private EngineType engineType;
        private IGearboxComputerListener listener;
        private ICarComunication communication;
        private int warnMessages;

        // When gearbox is initialized, it should get the right config
        public GearboxComputerLogic(EngineType engineType, IGearBox gearbox, IGearboxComputerListener listener, ICarComunication communication)
        {
            /// SysGB-COMP-11
            if (!Validator.IsGearboxValid(gearbox))
            {
                communication.Warn("Gearbox is receiving wrong data!");
                throw new Exception("Gearbox is invalid!");
            }

            this.gearbox = gearbox;
            this.engineType = engineType;
            this.listener = listener;
            this.communication = communication;
            this.warnMessages = 0;
        }

        public int CurrentGear
        {
            get
            {
                return this.currentGear;
            }

            set
            {
                this.currentGear = value;
            }
        }

        private void SendWarning()
        {
            if (warnMessages >= 1000)
            {
                /// SysGB-COMP-20
                this.communication.Warn("Gearbox is receiving wrong data!");
                this.warnMessages = 0;
            }
            else
            {
                /// SysGB-COMP-19
                this.communication.Log("Gearbox computer data is invalid!");
                this.warnMessages++;
            }
        }

        // When driver or something changes gear, gearbox computer must know
        public void SetGear(int gear)
        {
            int maxGears = this.gearbox.GearsCount;
            /// SysGB-COMP-17
            if (Validator.IsGearValid(gear, maxGears))
            {
                this.CurrentGear = gear;
            }
        }

        public void Calculate(GearboxComputerData data)
        {
            int predictedGear = this.CurrentGear;

            /// SysGB-COMP-18
            if (!Validator.IsDataReadingValid(data))
            {
                this.SendWarning();
                return; // TODO: log it somewhere and count to 1000 log to somewhere else
            }

            /// SysGB-COMP-25
            if (data.AccelerationLevel < 65)
            {
                /// SysGB-COMP-27
                int nextGearRPM = 2500;

                /// SysGB-COMP-28
                if (this.engineType == EngineType.Gasoline)
                {
                    nextGearRPM += 500;
                }

                /// SysGB-COMP-29
                if (data.DrivingMode == DrivingMode.Sport)
                {
                    nextGearRPM *= 2;
                }

                /// SysGB-COMP-30
                if (data.DrivingMode == DrivingMode.Economycal)
                {
                    nextGearRPM -= 500;
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
                        break;
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
            if (predictedGear != this.CurrentGear)
            {
                /// SysGB-COMP-46, SysGB-COMP-47
                this.listener.Receive(this.CurrentGear, predictedGear); // We have update for the gear
            }
        }
    }
}
