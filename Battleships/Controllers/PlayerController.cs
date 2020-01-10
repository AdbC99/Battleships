using System;
using System.Collections.Generic;
using Battleships.Models;
using Battleships.ViewModels;

namespace Battleships.Controllers
{
    public enum PlayerActionResult
    {
        MoreShipsRequired = 0,
        InvalidMove = 1,
        AllShipsPlaced = 2,
        InvalidState = 4
    }

    /// <summary>
    /// The player controller can be commanded to place ships and handle incoming attacks by acting upon
    /// the view model to alter the data in the model
    /// </summary>
    public class PlayerController
    {
        private PlayerStateTracker stateTracker;

        public PlayerController(Board board, int maxShips)
        {
            this.stateTracker = new PlayerStateTracker(board, maxShips);
        }

        /// <summary>
        /// Place a ship on a the board
        /// </summary>
        /// <param name="ship">A battleship</param>
        /// <returns></returns>
        public PlayerActionResult PlaceShip(Battleship ship)
        {
            if (this.stateTracker.State != PlayerState.Init)
                return PlayerActionResult.InvalidState;

            foreach (Coordinate section in ship.GetCoordinates())
            {
                if (this.stateTracker.TheBoard.ValidateShipPlacement(section) != true)
                    return PlayerActionResult.InvalidMove;
            }

            this.stateTracker.Ships.Add(ship);

            foreach (Coordinate section in ship.GetCoordinates())
            {
                this.stateTracker.TheBoard.PlaceShipSection(section);
            }

            if (this.stateTracker.State == PlayerState.HasPlacedShips)
                return PlayerActionResult.AllShipsPlaced;

            return PlayerActionResult.MoreShipsRequired;
        }

        /// <summary>
        /// Fetch the state of the game e.g. Init or Lost
        /// </summary>
        /// <returns> The state of the game </returns>
        public PlayerState GetState()
        {
            return this.stateTracker.State;
        }

        /// <summary>
        /// Command an incoming attack upon the board
        /// </summary>
        /// <param name="coordinate">the location of the attack</param>
        /// <returns>hit or miss</returns>
        public AttackResult IncomingAttack(Coordinate coordinate)
        {
            if (this.stateTracker.State != PlayerState.HasPlacedShips)
                return AttackResult.InvalidState;

            if ((coordinate.X >= this.stateTracker.TheBoard.Width) ||
                (coordinate.Y >= this.stateTracker.TheBoard.Height) ||
                (coordinate.X < 0) ||
                (coordinate.Y < 0))
                return AttackResult.InvalidCoordinate;

            if (this.stateTracker.TheBoard.GetCoordinateState(coordinate) == CoordinateState.Empty)
                return AttackResult.Miss;

            if (this.stateTracker.TheBoard.GetCoordinateState(coordinate) == CoordinateState.Full)
            {
                this.stateTracker.TheBoard.SetCoordinateState(coordinate, CoordinateState.Damaged);
                this.stateTracker.UpdateState();
            }

            return AttackResult.Hit;
        }
    }
}
