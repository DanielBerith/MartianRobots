using MartianRobots.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Domain.Entities
{
    public class Robot
    {
        public Position Position { get; private set; }
        public Orientation Orientation { get; private set; }
        public bool IsLost { get; private set; }

        public void TurnLeft()
        {
            Orientation = Orientation switch
            {
                Orientation.N => Orientation.W,
                Orientation.W => Orientation.S,
                Orientation.S => Orientation.E,
                Orientation.E => Orientation.N,
                _ => Orientation
            };
        }

        public void TurnRight()
        {
            Orientation = Orientation switch
            {
                Orientation.N => Orientation.E,
                Orientation.E => Orientation.S,
                Orientation.S => Orientation.W,
                Orientation.W => Orientation.N,
                _ => Orientation
            };
        }

        public Position CalculateNextPosition()
        {
            return Orientation switch
            {
                Orientation.N => new Position(Position.X, Position.Y + 1),
                Orientation.E => new Position(Position.X + 1, Position.Y),
                Orientation.S => new Position(Position.X, Position.Y - 1),
                Orientation.W => new Position(Position.X - 1, Position.Y),
                _ => Position
            };
        }

        public void MoveTo(Position newPosition)
        {
            Position = newPosition;
        }

        public void MarkAsLost()
        {
            IsLost = true;
        }
    }
}
