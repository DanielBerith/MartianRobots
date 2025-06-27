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
    public class GridTests
    {
        [Fact]
        public void Grid_ShouldInitializeWithCorrectDimensions()
        {
            // Arrange & Act
            var grid = new Grid(5, 3);

            // Assert
            grid.MaxX.Should().Be(5);
            grid.MaxY.Should().Be(3);
        }

        [Fact]
        public void HasScent_ShouldReturnFalseForNewGrid()
        {
            // Arrange
            var grid = new Grid(5, 3);
            var position = new Position(2, 2);

            // Act
            var hasScent = grid.HasScent(position);

            // Assert
            hasScent.Should().BeFalse();
        }

        [Fact]
        public void AddScent_ShouldAddScentAtPosition()
        {
            // Arrange
            var grid = new Grid(5, 3);
            var position = new Position(2, 2);

            // Act
            grid.AddScent(position);

            // Assert
            grid.HasScent(position).Should().BeTrue();
        }

        [Fact]
        public void AddScent_ShouldNotDuplicateScents()
        {
            // Arrange
            var grid = new Grid(5, 3);
            var position = new Position(2, 2);

            // Act
            grid.AddScent(position);
            grid.AddScent(position); // Add same position again

            // Assert
            grid.HasScent(position).Should().BeTrue();
        }

        [Fact]
        public void HasScent_ShouldReturnFalseForDifferentPosition()
        {
            // Arrange
            var grid = new Grid(5, 3);
            var scentPosition = new Position(2, 2);
            var checkPosition = new Position(3, 3);

            // Act
            grid.AddScent(scentPosition);

            // Assert
            grid.HasScent(checkPosition).Should().BeFalse();
        }
    }
}
