using FluentAssertions;
using MartianRobots.Application.DTOs;
using MartianRobots.Domain.Enums;
using MartianRobots.Infrastructure.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Tests.Infrastructure
{
    public class OutputFormatterTests
    {
        private readonly OutputFormatter _formatter;

        public OutputFormatterTests()
        {
            _formatter = new OutputFormatter();
        }

        [Fact]
        public void Format_WithSingleRobotNotLost_ShouldFormatCorrectly()
        {
            // Arrange
            var results = new[]
            {
                new SimulationResult(new Position(1, 1), Orientation.E, false)
            };

            // Act
            var output = _formatter.Format(results);

            // Assert
            output.Should().Be("1 1 E");
        }

        [Fact]
        public void Format_WithSingleRobotLost_ShouldIncludeLOST()
        {
            // Arrange
            var results = new[]
            {
                new SimulationResult(new Position(3, 3), Orientation.N, true)
            };

            // Act
            var output = _formatter.Format(results);

            // Assert
            output.Should().Be("3 3 N LOST");
        }

        [Fact]
        public void Format_WithMultipleRobots_ShouldFormatWithNewLines()
        {
            // Arrange
            var results = new[]
            {
                new SimulationResult(new Position(1, 1), Orientation.E, false),
                new SimulationResult(new Position(3, 3), Orientation.N, true),
                new SimulationResult(new Position(2, 3), Orientation.S, false)
            };

            // Act
            var output = _formatter.Format(results);

            // Assert
            var expectedOutput = "1 1 E\r\n3 3 N LOST\r\n2 3 S";
            output.Should().Be(expectedOutput);
        }

        [Fact]
        public void Format_WithEmptyResults_ShouldReturnEmptyString()
        {
            // Arrange
            var results = new SimulationResult[0];

            // Act
            var output = _formatter.Format(results);

            // Assert
            output.Should().BeEmpty();
        }

        [Fact]
        public void Format_WithAllOrientations_ShouldFormatCorrectly()
        {
            // Arrange
            var results = new[]
            {
                new SimulationResult(new Position(0, 0), Orientation.N, false),
                new SimulationResult(new Position(1, 1), Orientation.E, false),
                new SimulationResult(new Position(2, 2), Orientation.S, false),
                new SimulationResult(new Position(3, 3), Orientation.W, false)
            };

            // Act
            var output = _formatter.Format(results);

            // Assert
            var expectedOutput = "0 0 N\r\n1 1 E\r\n2 2 S\r\n3 3 W";
            output.Should().Be(expectedOutput);
        }
    }
}
