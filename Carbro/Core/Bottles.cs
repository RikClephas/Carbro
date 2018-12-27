using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbro.Core
{
    class Bottle
    {
        public string Name { get; set; }
        public int BottleNumber { get; set; }
        public int ID { get; set; }
        public int Calibration { get; set; }
        public int BottlePin { get; set; }
    }

    class TwoFactor
    {
        public string Key { get; set; }
        public string Pass { get; set; }
    }
}
