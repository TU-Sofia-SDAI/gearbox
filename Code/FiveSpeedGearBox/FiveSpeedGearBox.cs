using GearboxContracts;
using System.Collections.Generic;

namespace FiveSpeedGearBox
{
    public class FiveSpeedGearBox : IGearBox
    {
        private List<float> gearRatios = new List<float>{
            30, 70, 120, 170, 200 
        };

        public int GearsCount
        {
            get
            {
                return gearRatios.Count;
            }
        }

        public List<float> GearsRatios
        {
            get
            {
                return gearRatios;
            }
        }
    }
}
