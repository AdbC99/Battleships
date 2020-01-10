using System;
using System.Collections.Generic;

namespace Battleships.Models
{
    public enum BattleshipOrientation {
        Horizontal,
        Vertical
    }
    
    public class Battleship
    {
        public Coordinate Coords { get; set; }
        public int Length { get; set; }
        public List<bool> SectionHit { get; set; }
        public BattleshipOrientation Orientation { get; set; }

        public Battleship(int length, Coordinate coords, BattleshipOrientation orientation)
        {
            this.Coords = coords;
            this.Length = length;
            this.Orientation = orientation;
            this.SectionHit = new List<bool>(new bool[this.Length]);
        }

        public List<Coordinate> GetCoordinates()
        {
            var coords = new List<Coordinate>();

            for (int i = 0; i < Length; i++)
            {
                if (this.Orientation == BattleshipOrientation.Horizontal)
                    coords.Add(new Coordinate(Coords.X + i, Coords.Y));
                else if (this.Orientation == BattleshipOrientation.Vertical)
                    coords.Add(new Coordinate(Coords.X, Coords.Y + i));
            }

            return coords;
        }
    }
}
