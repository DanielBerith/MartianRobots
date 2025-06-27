using MartianRobots.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Domain.Entities
{
    public class Robot
    {
        public Position Position { get; private set; }
        public Orientation Orientation { get; private set; }
        public bool IsLost { get; private set; }

    }
}
