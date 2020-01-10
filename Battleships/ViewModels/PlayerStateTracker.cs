using System;
using System.Collections.Generic;
using Battleships.Models;

namespace Battleships.ViewModels
{
    public enum PlayerState
    {
        Init = 0,
        HasPlacedShips = 1,
        Lost = 2,
    }

    /// <summary>
    /// Observes changes to the board and adjusts state appropriately
    /// </summary>
    public class PlayerStateTracker
    {
        public PlayerState State { get; set; }
        public int MaxShips { get; private set; }

        public Board TheBoard {
            get
            {
                // Update state whenever the board is accessed
                this.UpdateState();
                return _theBoard;
            }
            private set
            {
                _theBoard = value;
            }
        }

        public List<Battleship> Ships
        {
            get
            {
                // Update state whenever the ships are accessed
                this.UpdateState();
                return _ships;
            }
            private set
            {
                _ships = value;
            }
        }

        private Board _theBoard;
        private List<Battleship> _ships;

        public PlayerStateTracker(Board theBoard, int maxShips)
        {
            this.Ships = new List<Battleship>();
            this.MaxShips = maxShips;
            this._theBoard = theBoard;
            this.State = PlayerState.Init;
        }

        public void UpdateState()
        {
            if (State == PlayerState.Init)
            {
                if (_ships.Count >= MaxShips)
                {
                    this.State = PlayerState.HasPlacedShips;
                }
            }

            if (State == PlayerState.HasPlacedShips)
            {
                if (_theBoard.HasPlayerLostAllShips())
                    this.State = PlayerState.Lost;
            }
        }
    }
}
