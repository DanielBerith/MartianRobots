using FluentAssertions;
using MartianRobots.Domain.Enums;
using MartianRobots.Infrastructure.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Tests.Infrastructure
{
    public class InputParserTests
    {
        private readonly InputParser _parser;

        public InputParserTests()
        {
            _parser = new InputParser();
        }

        [Fact]
        public void Parse_WithValidInput_ShouldReturnCorrectSimulationInput()
        {
            // Arrange
            var input = @"5 3
1 1 E
RFRFRFRF
3 2 N
FRRFLLFFRRFLL";

            // Act
            var result = _parser.Parse(input);

            // Assert
            result.GridWidth.Should().Be(5);
            result.GridHeight.Should().Be(3);
            result.Robots.Should().HaveCount(2);

            var robots = result.Robots.ToList();
            robots[0].StartPosition.Should().Be(new Position(1, 1));
            robots[0].StartOrientation.Should().Be(Orientation.E);
            robots[0].Instructions.Should().Be("RFRFRFRF");

            robots[1].StartPosition.Should().Be(new Position(3, 2));
            robots[1].StartOrientation.Should().Be(Orientation.N);
            robots[1].Instructions.Should().Be("FRRFLLFFRRFLL");
        }

        [Fact]
        public void Parse_WithInvalidGridDimensions_ShouldThrowException()
        {
            // Arrange
            var input = "invalid grid\n1 1 E\nF";

            // Act & Assert
            var act = () => _parser.Parse(input);
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("*Grid dimensions must be integers*");
        }

        [Fact]
        public void Parse_WithGridDimensionsExceedingLimit_ShouldThrowException()
        {
            // Arrange
            var input = "100 100\n1 1 E\nF";

            // Act & Assert
            var act = () => _parser.Parse(input);
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("*Grid dimensions must be between 0 and 50*");
        }

        [Fact]
        public void Parse_WithInvalidOrientation_ShouldThrowException()
        {
            // Arrange
            var input = "5 3\n1 1 X\nF";

            // Act & Assert
            var act = () => _parser.Parse(input);
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("*Invalid orientation: X*");
        }

        [Fact]
        public void Parse_WithInvalidInstruction_ShouldThrowException()
        {
            // Arrange
            var input = "5 3\n1 1 E\nFRX";

            // Act & Assert
            var act = () => _parser.Parse(input);
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("*Invalid instruction: X*");
        }

        [Fact]
        public void Parse_WithTooLongInstructions_ShouldThrowException()
        {
            // Arrange
            var longInstructions = new string('F', 100);
            var input = $"5 3\n1 1 E\n{longInstructions}";

            // Act & Assert
            var act = () => _parser.Parse(input);
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("*Instructions must be less than 100 characters*");
        }

        [Fact]
        public void Parse_WithEmptyInput_ShouldThrowException()
        {
            // Arrange
            var input = "";

            // Act & Assert
            var act = () => _parser.Parse(input);
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Parse_WithMissingRobotInstructions_ShouldThrowException()
        {
            // Arrange
            var input = "5 3\n1 1 E";

            // Act & Assert
            var act = () => _parser.Parse(input);
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("*Each robot must have a position and instructions*");
        }
    }
}
