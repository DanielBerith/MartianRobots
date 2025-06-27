using MartianRobots.Application.DTOs;
using MartianRobots.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Infrastructure.Formatters
{
    public class OutputFormatter : IOutputFormatter
    {
        public string Format(IEnumerable<SimulationResult> results)
        {
            var formattedResults = results.Select(FormatSingleResult);
            return string.Join(Environment.NewLine, formattedResults);
        }

        private string FormatSingleResult(SimulationResult result)
        {
            var baseOutput = $"{result.FinalPosition.X} {result.FinalPosition.Y} {result.FinalOrientation}";

            return result.IsLost
                ? $"{baseOutput} LOST"
                : baseOutput;
        }
    }
}
