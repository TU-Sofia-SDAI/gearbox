using System.Collections.Generic;

namespace GearboxContracts
{
    public interface IGearBox
    {
        int GearsCount { get; }

        // As speed in km/h. Example:
        // 1. 30km/h
        // 2. 60 km/h
        // 3. 110 km/h
        // 4. 160 km/h
        // 5. 190 km/h
        // 6. 210 km/h
        List<float> GearsRatios { get; }
    }
}
