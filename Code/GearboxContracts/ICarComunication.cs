using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GearboxContracts
{
    public interface ICarComunication
    {
        void Warn(string message);

        void Log(string message);
    }
}
