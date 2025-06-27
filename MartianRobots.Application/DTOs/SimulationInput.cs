using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Application.DTOs
{
    public record SimulationInput(
    int GridWidth,
    int GridHeight,
    IEnumerable<RobotInput> Robots
    );
}
