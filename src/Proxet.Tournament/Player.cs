using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxet.Tournament
{
    class Player
    {
        public string Name { get; set; }
        public int WaitTime { get; set; }
        public int VehicleType { get; set; }
        public bool inGame { get; set; } = false;
    }
}
