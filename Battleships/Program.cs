using System;
using Battleships.Models;
using Battleships.ViewModels;
using Battleships.Controllers;

namespace Battleships
{
    /// <summary>
    /// An example of a state tracking system for a game of battleships
    /// </summary>
    class Battleships
    {
        public static void Main(string[] args)
        {
            // Sample game, see unit tests for more examples
            PlayerController playerController = new PlayerController(new Board(10, 10), 2);

            Battleship a = new Battleship(2, new Coordinate(5, 5), BattleshipOrientation.Horizontal);
            Battleship b = new Battleship(1, new Coordinate(6, 6), BattleshipOrientation.Vertical);

            Console.WriteLine("Game state: " + playerController.GetState());

            playerController.PlaceShip(a);
            playerController.PlaceShip(b);
            
            Console.WriteLine("Incoming attack result: " + playerController.IncomingAttack(new Coordinate(0, 0)));
            Console.WriteLine("Game state: " + playerController.GetState());
            Console.WriteLine("Incoming attack result: " + playerController.IncomingAttack(new Coordinate(5, 5)));
            Console.WriteLine("Game state: " + playerController.GetState());
            Console.WriteLine("Incoming attack result: " + playerController.IncomingAttack(new Coordinate(6, 6)));
            Console.WriteLine("Game state: " + playerController.GetState());
            Console.WriteLine("Incoming attack result: " + playerController.IncomingAttack(new Coordinate(6, 5)));
            Console.WriteLine("Game state: " + playerController.GetState());
        }
    }
}
