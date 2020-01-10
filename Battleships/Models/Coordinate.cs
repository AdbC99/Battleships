using System;
namespace Battleships.Models
{
    /// <summary>
    /// A simple X,Y point
    /// </summary>
    public class Coordinate
    {
        public readonly int X;
        public readonly int Y;

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Coordinate coord = (Coordinate)obj;
                return (X == coord.X) && (Y == coord.Y);
            }
        }

        public override int GetHashCode()
        {
            return (X << 2) ^ Y;
        }

        public override string ToString()
        {
            return String.Format("[Coordinate] ({0}, {1})", X, Y);
        }
    }
}
