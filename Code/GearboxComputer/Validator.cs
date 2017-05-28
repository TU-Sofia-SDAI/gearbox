using GearboxContracts;

namespace GearboxComputer
{
    public static class Validator
    {
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

        public static bool IsGearValid(int gear, int maxGears)
        {
            if (gear < -1)
            {
                return false;
            }

            if (gear > maxGears)
            {
                return false;
            }

            return true;
        }

        public static bool IsGearboxValid(IGearBox gb)
        {
            if (gb.GearsCount <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
