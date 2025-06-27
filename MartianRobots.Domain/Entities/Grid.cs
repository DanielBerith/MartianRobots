using MartianRobots.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Domain.Entities
{
    public class Grid
    {
        public int MaxX { get; }
        public int MaxY { get; }
        private HashSet<Position> scentPositions = new();

        public bool HasScent(Position position) => scentPositions.Contains(position);
        public void AddScent(Position position) => scentPositions.Add(position);
    }
}
