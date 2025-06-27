using MartianRobots.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Infrastructure.Interfaces
{
    public interface IInputParser
    {
        SimulationInput Parse(string input);
    }
}
