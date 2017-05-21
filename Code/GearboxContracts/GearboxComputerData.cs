﻿namespace GearboxContracts
{
    public class GearboxComputerData
    {
        public float VehicleSpeed { get; set; }

        public int RPM { get; set; }

        public int AccelerationLevel { get; set; }

        public DrivingMode DrivingMode { get; set; } 
    }
}