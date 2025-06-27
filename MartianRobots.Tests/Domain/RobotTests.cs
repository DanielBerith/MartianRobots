using FluentAssertions;
using MartianRobots.Domain.Entities;
using MartianRobots.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Tests.Domain
{
    public class RobotTests
    {
        [Fact]
        public void Robot_ShouldInitializeWithCorrectPositionAndOrientation()
        {
            // Arrange
            var position = new Position(1, 2);
            var orientation = Orientation.N;

            // Act
            var robot = new Robot(position, orientation);

            // Assert
            robot.Position.Should().Be(position);
            robot.Orientation.Should().Be(orientation);
            robot.IsLost.Should().BeFalse();
        }

        [Theory]
        [InlineData(Orientation.N, Orientation.W)]
        [InlineData(Orientation.W, Orientation.S)]
        [InlineData(Orientation.S, Orientation.E)]
        [InlineData(Orientation.E, Orientation.N)]
        public void TurnLeft_ShouldRotateCorrectly(Orientation initial, Orientation expected)
        {
            // Arrange
            var robot = new Robot(new Position(0, 0), initial);

            // Act
            robot.TurnLeft();

            // Assert
            robot.Orientation.Should().Be(expected);
        }

        [Theory]
        [InlineData(Orientation.N, Orientation.E)]
        [InlineData(Orientation.E, Orientation.S)]
        [InlineData(Orientation.S, Orientation.W)]
        [InlineData(Orientation.W, Orientation.N)]
        public void TurnRight_ShouldRotateCorrectly(Orientation initial, Orientation expected)
        {
            // Arrange
            var robot = new Robot(new Position(0, 0), initial);

            // Act
            robot.TurnRight();

            // Assert
            robot.Orientation.Should().Be(expected);
        }

        [Theory]
        [InlineData(1, 1, Orientation.N, 1, 2)]
        [InlineData(1, 1, Orientation.E, 2, 1)]
        [InlineData(1, 1, Orientation.S, 1, 0)]
        [InlineData(1, 1, Orientation.W, 0, 1)]
        public void CalculateNextPosition_ShouldReturnCorrectPosition(
            int startX, int startY, Orientation orientation, int expectedX, int expectedY)
        {
            // Arrange
            var robot = new Robot(new Position(startX, startY), orientation);

            // Act
            var nextPosition = robot.CalculateNextPosition();

            // Assert
            nextPosition.X.Should().Be(expectedX);
            nextPosition.Y.Should().Be(expectedY);
        }

        [Fact]
        public void MoveTo_ShouldUpdatePosition()
        {
            // Arrange
            var robot = new Robot(new Position(0, 0), Orientation.N);
            var newPosition = new Position(2, 3);

            // Act
            robot.MoveTo(newPosition);

            // Assert
            robot.Position.Should().Be(newPosition);
        }

        [Fact]
        public void MarkAsLost_ShouldSetIsLostToTrue()
        {
            // Arrange
            var robot = new Robot(new Position(0, 0), Orientation.N);

            // Act
            robot.MarkAsLost();

            // Assert
            robot.IsLost.Should().BeTrue();
        }
    }
}
