using System;
using Battleships.Models;
using Battleships.Controllers;
using Battleships.ViewModels;

namespace UnitTests
{
    class UnitTests
    {
        public bool TestShipPlacement()
        {
            Board board = new Board(10, 10);

            Battleship a = new Battleship(3, new Coordinate(5, 5), BattleshipOrientation.Horizontal);
            Battleship b = new Battleship(3, new Coordinate(6, 4), BattleshipOrientation.Vertical);
            Battleship c = new Battleship(3, new Coordinate(6, 6), BattleshipOrientation.Vertical);
            Battleship d = new Battleship(1, new Coordinate(5, 5), BattleshipOrientation.Vertical);
            Battleship e = new Battleship(1, new Coordinate(11, 11), BattleshipOrientation.Vertical);
            Battleship f = new Battleship(1, new Coordinate(-1, -1), BattleshipOrientation.Vertical);
            Battleship g = new Battleship(1, new Coordinate(9, 9), BattleshipOrientation.Vertical);

            PlayerController playerController = new PlayerController(board, 3);

            if (playerController.PlaceShip(a) != PlayerActionResult.MoreShipsRequired)
                return false;

            if (playerController.PlaceShip(b) != PlayerActionResult.InvalidMove)
                return false;

            if (playerController.PlaceShip(c) != PlayerActionResult.MoreShipsRequired)
                return false;

            if (playerController.PlaceShip(d) != PlayerActionResult.InvalidMove)
                return false;

            if (playerController.PlaceShip(e) != PlayerActionResult.InvalidMove)
                return false;

            if (playerController.PlaceShip(f) != PlayerActionResult.InvalidMove)
                return false;

            if (playerController.PlaceShip(g) != PlayerActionResult.AllShipsPlaced)
                return false;

            return true;
        }

        public bool TestHitAndMiss()
        {
            Board board = new Board(10, 10);

            Battleship a = new Battleship(2, new Coordinate(5, 5), BattleshipOrientation.Horizontal);
            Battleship b = new Battleship(1, new Coordinate(6, 6), BattleshipOrientation.Vertical);
            Battleship c = new Battleship(1, new Coordinate(5, 9), BattleshipOrientation.Vertical);

            PlayerController playerController = new PlayerController(board, 3);

            playerController.PlaceShip(a);
            playerController.PlaceShip(b);
            playerController.PlaceShip(c);

            if (playerController.IncomingAttack(new Coordinate(-1, -1)) != AttackResult.InvalidCoordinate)
                return false;

            if (playerController.IncomingAttack(new Coordinate(99, 99)) != AttackResult.InvalidCoordinate)
                return false;

            if (playerController.IncomingAttack(new Coordinate(2, 2)) != AttackResult.Miss)
                return false;

            if (playerController.IncomingAttack(new Coordinate(5, 5)) != AttackResult.Hit)
                return false;

            if (playerController.IncomingAttack(new Coordinate(5, 5)) != AttackResult.Hit)
                return false;

            return true;
        }

        public bool TestGameStateTracking()
        {
            Board board = new Board(10, 10);

            Battleship a = new Battleship(1, new Coordinate(5, 5), BattleshipOrientation.Horizontal);
            Battleship b = new Battleship(1, new Coordinate(6, 6), BattleshipOrientation.Vertical);

            PlayerController playerController = new PlayerController(board, 2);

            if (playerController.IncomingAttack(new Coordinate(5, 5)) != AttackResult.InvalidState)
                return false;

            playerController.PlaceShip(a);

            if (playerController.IncomingAttack(new Coordinate(5, 5)) != AttackResult.InvalidState)
                return false;

            if (playerController.PlaceShip(b) != PlayerActionResult.AllShipsPlaced)
                return false;

            if (playerController.GetState() != PlayerState.HasPlacedShips)
                return false;

            if (playerController.PlaceShip(b) != PlayerActionResult.InvalidState)
                return false;

            if (playerController.GetState() != PlayerState.HasPlacedShips)
                return false;

            if (playerController.IncomingAttack(new Coordinate(5, 5)) != AttackResult.Hit)
                return false;

            if (playerController.IncomingAttack(new Coordinate(5, 5)) != AttackResult.Hit)
                return false;

            if (playerController.GetState() == PlayerState.Lost)
                return false;

            if (playerController.IncomingAttack(new Coordinate(6, 6)) != AttackResult.Hit)
                return false;

            if (playerController.GetState() != PlayerState.Lost)
                return false;

            if (playerController.IncomingAttack(new Coordinate(6, 6)) != AttackResult.InvalidState)
                return false;

            return true;
        }

        private void doTest(string description, bool result)
        {
            Console.WriteLine(description + " " + (result?"[PASSED]":"[FAIL]"));
        }

        public static void Main(string[] args)
        {
            // Some lightweight unit tests
            UnitTests tests = new UnitTests();
            
            tests.doTest("Ship placement tests", tests.TestShipPlacement());

            tests.doTest("Ship being shot tests", tests.TestHitAndMiss());

            tests.doTest("Game state tracking tests", tests.TestGameStateTracking());
        }
    }
}
