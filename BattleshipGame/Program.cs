using BattleshipGame.Models;

namespace BattleshipGame
{
    class Program
    {
        const int BoardSize = 10;
        static void Main(string[] args)
        {
            var randomProvider = new Randomizer();
            var board = new Board(BoardSize, randomProvider);
            board.AddShipRandomly(new Ship(5, "Battleship"));
            board.AddShipRandomly(new Ship(4, "Destroyer"));
            board.AddShipRandomly(new Ship(4, "Destroyer"));

            var game = new Game(board, new UserInteraction());

            game.Play();
        }
    }
}
