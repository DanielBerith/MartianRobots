using MartianRobots.Application.DTOs;
using MartianRobots.Application.Interfaces;
using MartianRobots.Domain.Entities;
using MartianRobots.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Application.Services
{
    public class RobotSimulationService : IRobotSimulationService
    {
        public IEnumerable<SimulationResult> RunSimulation(SimulationInput input)
        {
            var grid = new Grid(input.GridWidth, input.GridHeight);
            var results = new List<SimulationResult>();

            foreach (var robotInput in input.Robots)
            {
                var result = SimulateRobot(grid, robotInput);
                results.Add(result);
            }

            return results;
        }

        private SimulationResult SimulateRobot(Grid grid, RobotInput input)
        {
            var robot = new Robot(input.StartPosition, input.StartOrientation);

            foreach (char instruction in input.Instructions)
            {
                if (robot.IsLost) break;

                ExecuteInstruction(robot, grid, instruction);
            }

            return new SimulationResult(
                robot.Position,
                robot.Orientation,
                robot.IsLost
            );
        }

        private void ExecuteInstruction(Robot robot, Grid grid, char instruction)
        {
            switch (instruction)
            {
                case 'L':
                    robot.TurnLeft();
                    break;
                case 'R':
                    robot.TurnRight();
                    break;
                case 'F':
                    MoveRobotForward(robot, grid);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown instruction: {instruction}");
            }
        }

        private void MoveRobotForward(Robot robot, Grid grid)
        {
            var nextPosition = robot.CalculateNextPosition();

            if (IsOutOfBounds(nextPosition, grid))
            {
                // Check if there's already a scent at current position
                if (!grid.HasScent(robot.Position))
                {
                    robot.MarkAsLost();
                    grid.AddScent(robot.Position);
                }
                // If there is a scent, ignore the move command
            }
            else
            {
                robot.MoveTo(nextPosition);
            }
        }

        private bool IsOutOfBounds(Position position, Grid grid)
        {
            return position.X < 0 || position.X > grid.MaxX ||
                   position.Y < 0 || position.Y > grid.MaxY;
        }
    }
}
