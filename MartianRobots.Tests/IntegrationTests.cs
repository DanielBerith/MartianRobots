using FluentAssertions;
using MartianRobots.Application.Services;
using MartianRobots.Infrastructure.Formatters;
using MartianRobots.Infrastructure.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Tests
{
    public class IntegrationTests
    {
        private readonly InputParser _inputParser;
        private readonly RobotSimulationService _simulationService;
        private readonly OutputFormatter _outputFormatter;

        public IntegrationTests()
        {
            _inputParser = new InputParser();
            _simulationService = new RobotSimulationService();
            _outputFormatter = new OutputFormatter();
        }

        [Fact]
        public void FullSimulation_WithSampleInput_ShouldProduceExpectedOutput()
        {
            // Arrange
            var input = @"5 3
                        1 1 E
                        RFRFRFRF
                        3 2 N
                        FRRFLLFFRRFLL
                        0 3 W
                        LLFFFLFLFL";

            var expectedOutput = "1 1 E\r\n3 3 N LOST\r\n2 3 S";

            // Act
            var simulationInput = _inputParser.Parse(input);
            var results = _simulationService.RunSimulation(simulationInput);
            var output = _outputFormatter.Format(results);

            // Assert
            output.Should().Be(expectedOutput);
        }

        [Fact]
        public void FullSimulation_WithRobotStartingAtEdgeGoingOff_ShouldBeLost()
        {
            // Arrange
            var input = @"2 2
                        2 2 N
                        F";

            var expectedOutput = "2 2 N LOST";

            // Act
            var simulationInput = _inputParser.Parse(input);
            var results = _simulationService.RunSimulation(simulationInput);
            var output = _outputFormatter.Format(results);

            // Assert
            output.Should().Be(expectedOutput);
        }

        [Fact]
        public void FullSimulation_WithScentPrevention_ShouldWork()
        {
            // Arrange
            var input = @"3 3
                        3 3 N
                        F
                        3 3 N
                        F";

            var expectedOutput = "3 3 N LOST\r\n3 3 N";

            // Act
            var simulationInput = _inputParser.Parse(input);
            var results = _simulationService.RunSimulation(simulationInput);
            var output = _outputFormatter.Format(results);

            // Assert
            output.Should().Be(expectedOutput);
        }

        [Theory]
        [InlineData("1 1\n0 0 N\nF", "0 1 N")]
        [InlineData("1 1\n0 0 E\nF", "1 0 E")]
        [InlineData("1 1\n1 1 S\nF", "1 0 S")]
        [InlineData("1 1\n1 1 W\nF", "0 1 W")]
        public void FullSimulation_WithSimpleMovements_ShouldWork(string input, string expectedOutput)
        {
            // Act
            var simulationInput = _inputParser.Parse(input);
            var results = _simulationService.RunSimulation(simulationInput);
            var output = _outputFormatter.Format(results);

            // Assert
            output.Should().Be(expectedOutput);
        }

        [Fact]
        public void FullSimulation_WithComplexPath_ShouldWork()
        {
            // Arrange
            var input = @"5 5
                        2 2 N
                        FFRFFRFFR";

            var expectedOutput = "2 2 N";

            // Act
            var simulationInput = _inputParser.Parse(input);
            var results = _simulationService.RunSimulation(simulationInput);
            var output = _outputFormatter.Format(results);

            // Assert
            output.Should().Be(expectedOutput);
        }
    }
}
