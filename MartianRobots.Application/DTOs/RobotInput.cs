using MartianRobots.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Application.DTOs
{
    public record RobotInput(
    Position StartPosition,
    Orientation StartOrientation,
    string Instructions
    );
}
