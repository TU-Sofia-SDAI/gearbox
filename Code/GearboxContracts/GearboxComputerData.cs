namespace GearboxContracts
{
    public class GearboxComputerData
    {
        /// SysGB-COMP-5
        public int RPM { get; set; }

        /// SysGB-COMP-6
        public int VehicleSpeed { get; set; }

        /// SysGB-COMP-7
        public DrivingMode DrivingMode { get; set; }

        /// SysGB-COMP-8
        public int AccelerationLevel { get; set; }
    }
}
