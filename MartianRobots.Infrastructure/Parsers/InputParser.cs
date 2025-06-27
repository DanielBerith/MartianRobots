using MartianRobots.Application.DTOs;
using MartianRobots.Application.Interfaces;
using MartianRobots.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Infrastructure.Parsers
{
    public class InputParser : IInputParser
    {
        public SimulationInput Parse(string input)
        {
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                             .Select(line => line.Trim())
                             .ToArray();

            if (lines.Length < 3)
            {
                throw new InvalidOperationException("Input must contain at least grid dimensions and one robot");
            }

            var gridDimensions = ParseGridDimensions(lines[0]);
            var robots = ParseRobots(lines.Skip(1).ToArray());

            return new SimulationInput(
                gridDimensions.Width,
                gridDimensions.Height,
                robots
            );
        }

        private (int Width, int Height) ParseGridDimensions(string line)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
            {
                throw new InvalidOperationException($"Invalid grid dimensions: {line}");
            }

            if (!int.TryParse(parts[0], out var width) || !int.TryParse(parts[1], out var height))
            {
                throw new InvalidOperationException($"Grid dimensions must be integers: {line}");
            }

            if (width > 50 || height > 50 || width < 0 || height < 0)
            {
                throw new InvalidOperationException($"Grid dimensions must be between 0 and 50: {line}");
            }

            return (width, height);
        }

        private IEnumerable<RobotInput> ParseRobots(string[] lines)
        {
            var robots = new List<RobotInput>();

            for (int i = 0; i < lines.Length; i += 2)
            {
                if (i + 1 >= lines.Length)
                {
                    throw new InvalidOperationException("Each robot must have a position and instructions");
                }

                var position = ParseRobotPosition(lines[i]);
                var instructions = ParseInstructions(lines[i + 1]);

                robots.Add(new RobotInput(
                    position.Position,
                    position.Orientation,
                    instructions
                ));
            }

            return robots;
        }

        private (Position Position, Orientation Orientation) ParseRobotPosition(string line)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 3)
            {
                throw new InvalidOperationException($"Invalid robot position format: {line}");
            }

            if (!int.TryParse(parts[0], out var x) || !int.TryParse(parts[1], out var y))
            {
                throw new InvalidOperationException($"Robot coordinates must be integers: {line}");
            }

            if (!Enum.TryParse<Orientation>(parts[2], out var orientation))
            {
                throw new InvalidOperationException($"Invalid orientation: {parts[2]}. Must be N, S, E, or W");
            }

            return (new Position(x, y), orientation);
        }

        private string ParseInstructions(string line)
        {
            var instructions = line.Trim();

            if (string.IsNullOrEmpty(instructions))
            {
                throw new InvalidOperationException("Instructions cannot be empty");
            }

            if (instructions.Length >= 100)
            {
                throw new InvalidOperationException("Instructions must be less than 100 characters");
            }

            var validInstructions = new HashSet<char> { 'L', 'R', 'F' };
            foreach (char instruction in instructions)
            {
                if (!validInstructions.Contains(instruction))
                {
                    throw new InvalidOperationException($"Invalid instruction: {instruction}. Must be L, R, or F");
                }
            }

            return instructions;
        }
    }
}
