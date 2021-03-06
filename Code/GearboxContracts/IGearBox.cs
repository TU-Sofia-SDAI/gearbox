﻿using System.Collections.Generic;

namespace GearboxContracts
{
    public interface IGearBox
    {
        /// SysGB-COMP-3
        int GearsCount { get; }

        // As speed in km/h. Example:
        // 1. 30km/h
        // 2. 60 km/h
        // 3. 110 km/h
        // 4. 160 km/h
        // 5. 190 km/h
        // 6. 210 km/h
        /// SysGB-COMP-24
        List<float> GearsRatios { get; }
    }
}
