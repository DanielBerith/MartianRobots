using FluentAssertions;
using MartianRobots.Application.DTOs;
using MartianRobots.Application.Services;
using MartianRobots.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Tests.Application
{
    public class RobotSimulationServiceTests
    {
        private readonly RobotSimulationService _service;

        public RobotSimulationServiceTests()
        {
            _service = new RobotSimulationService();
        }

        [Fact]
        public void RunSimulation_WithSingleRobot_ShouldReturnCorrectResult()
        {
            // Arrange
            var input = new SimulationInput(
                5, 3,
                new[]
                {
                    new RobotInput(new Position(1, 1), Orientation.E, "RFRFRFRF")
                }
            );

            // Act
            var results = _service.RunSimulation(input).ToList();

            // Assert
            results.Should().HaveCount(1);
            results[0].FinalPosition.Should().Be(new Position(1, 1));
            results[0].FinalOrientation.Should().Be(Orientation.E);
            results[0].IsLost.Should().BeFalse();
        }

        [Fact]
        public void RunSimulation_WithRobotGoingOffGrid_ShouldMarkAsLost()
        {
            // Arrange
            var input = new SimulationInput(
                5, 3,
                new[]
                {
                    new RobotInput(new Position(3, 2), Orientation.N, "FRRFLLFFRRFLL")
                }
            );

            // Act
            var results = _service.RunSimulation(input).ToList();

            // Assert
            results.Should().HaveCount(1);
            results[0].FinalPosition.Should().Be(new Position(3, 3));
            results[0].FinalOrientation.Should().Be(Orientation.N);
            results[0].IsLost.Should().BeTrue();
        }

        [Fact]
        public void RunSimulation_WithScent_ShouldPreventSecondRobotFromFallingOff()
        {
            // Arrange
            var input = new SimulationInput(
                2, 2,
                new[]
                {
                    new RobotInput(new Position(2, 2), Orientation.N, "F"), // Falls off
                    new RobotInput(new Position(2, 2), Orientation.N, "F")  // Should ignore due to scent
                }
            );

            // Act
            var results = _service.RunSimulation(input).ToList();

            // Assert
            results.Should().HaveCount(2);

            // First robot should be lost
            results[0].FinalPosition.Should().Be(new Position(2, 2));
            results[0].IsLost.Should().BeTrue();

            // Second robot should not be lost
            results[1].FinalPosition.Should().Be(new Position(2, 2));
            results[1].IsLost.Should().BeFalse();
        }

        [Fact]
        public void RunSimulation_WithCompleteExample_ShouldMatchExpectedOutput()
        {
            // Arrange - using the exact example from the PDF
            var input = new SimulationInput(
                5, 3,
                new[]
                {
                    new RobotInput(new Position(1, 1), Orientation.E, "RFRFRFRF"),
                    new RobotInput(new Position(3, 2), Orientation.N, "FRRFLLFFRRFLL"),
                    new RobotInput(new Position(0, 3), Orientation.W, "LLFFFLFLFL")
                }
            );

            // Act
            var results = _service.RunSimulation(input).ToList();

            // Assert
            results.Should().HaveCount(3);

            // First robot: 1 1 E
            results[0].FinalPosition.Should().Be(new Position(1, 1));
            results[0].FinalOrientation.Should().Be(Orientation.E);
            results[0].IsLost.Should().BeFalse();

            // Second robot: 3 3 N LOST
            results[1].FinalPosition.Should().Be(new Position(3, 3));
            results[1].FinalOrientation.Should().Be(Orientation.N);
            results[1].IsLost.Should().BeTrue();

            // Third robot: 2 3 S
            results[2].FinalPosition.Should().Be(new Position(2, 3));
            results[2].FinalOrientation.Should().Be(Orientation.S);
            results[2].IsLost.Should().BeFalse();
        }

        [Fact]
        public void RunSimulation_WithEmptyInstructions_ShouldNotMoveRobot()
        {
            // Arrange
            var input = new SimulationInput(
                5, 3,
                new[]
                {
                    new RobotInput(new Position(2, 2), Orientation.N, "")
                }
            );

            // Act
            var results = _service.RunSimulation(input).ToList();

            // Assert
            results[0].FinalPosition.Should().Be(new Position(2, 2));
            results[0].FinalOrientation.Should().Be(Orientation.N);
            results[0].IsLost.Should().BeFalse();
        }
    }
}
