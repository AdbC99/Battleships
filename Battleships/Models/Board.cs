using System;
using System.Collections.Generic;

namespace Battleships.Models
{
    public enum AttackResult
    {
        Miss = 0,
        Hit = 1,
        InvalidCoordinate = 2,
        InvalidState = 3
    }

    public enum CoordinateState
    {
        Empty = 0,
        Full = 1,
        Damaged = 2
    }

    /// <summary>
    /// An arbitrary sized board on which a battleships or other similar tile based game can be played
    /// </summary>
    public class Board
    {
        public List<Battleship> Ships { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        private CoordinateState[,] Squares {get; set;}

        public Board(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.Squares = new CoordinateState[width, height];
        }

        public bool CoordinateIsOnBoard(Coordinate coord)
        {
            if ((coord.X >= Width) ||
                (coord.Y >= Height) ||
                (coord.X < 0) ||
                (coord.Y < 0))
                return false;

            return true;
        }

        public CoordinateState GetCoordinateState(Coordinate coord)
        {
            return Squares[coord.X, coord.Y];
        }

        public bool SetCoordinateState(Coordinate coord, CoordinateState state)
        {
            if (!CoordinateIsOnBoard(coord))
                return false;

            Squares[coord.X, coord.Y] = state;
            return true;
        }

        public bool ValidateShipPlacement(Coordinate coord)
        {
            if (!CoordinateIsOnBoard(coord))
                return false;

            if (Squares[coord.X, coord.Y] != CoordinateState.Empty)
                return false;

            return true;
        }

        public bool PlaceShipSection(Coordinate coord)
        {
            if (!ValidateShipPlacement(coord))
                return false;

            Squares[coord.X, coord.Y] = CoordinateState.Full;

            return true;
        }

        public bool HasPlayerLostAllShips()
        {
            for (int i = 0; i < Squares.GetLength(0); i++)
            {
                for (int j = 0; j < Squares.GetLength(1); j++)
                {
                    if (Squares[i, j] == CoordinateState.Full)
                        return false;
                }
            }

            return true;
        }
    }
}
