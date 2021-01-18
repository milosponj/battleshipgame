using BattleshipGame.Enums;
using BattleshipGame.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace BattleshipGame
{
    public class Game
    {
        private readonly IUserInteraction _userInteraction;
        private readonly IBoard _board;

        public Game(IBoard board, IUserInteraction userInteraction)
        {
            _board = board;
            _userInteraction = userInteraction;
        }

        public void Play()
        {
            PrintTutorial();

            while (!IsFinished())
            {
                _userInteraction.Write("Type in the coordinate to shoot at: ");
                var coordinateString = _userInteraction.Read();
                if (TryExtractCoordinate(coordinateString, out Coordinate coordinate))
                {
                    HandleUserShot(coordinate);
                    _userInteraction.Write(_board.ToString());
                }
                else
                {
                    _userInteraction.Write("Invalid coordinate, please try again.");
                }
            }

            PrintGameEnd();
        }

        private void PrintGameEnd()
        {
            _userInteraction.Write("You've sinked every ship!", ConsoleColor.Cyan);
            _userInteraction.Write("Thank you for playing.", ConsoleColor.Green);
            _userInteraction.Write("Bye bye.\n", ConsoleColor.Yellow);
        }

        private void PrintTutorial()
        {
            _userInteraction.Write("Try to sink every ship on the map. \n" +
                "Use the board markers to choose a coordinate where you want to shoot at.\n" +
                "For example 'B3' for shooting at third column of second row.");
            _userInteraction.Write("Good Luck!\n", ConsoleColor.Yellow);
        }

        private void HandleUserShot(Coordinate coordinate)
        {
            var shootResult = _board.ShootAt(coordinate);
            if(shootResult.ShootStatus == ShootStatus.ShipMissed)
            {
                _userInteraction.Write("Miss.", ConsoleColor.Red);
            }
            else if(shootResult.ShootStatus == ShootStatus.ShipHit)
            {
                _userInteraction.Write($"Ship hit. {shootResult.ShipHit.Name}.", ConsoleColor.Green);
            }
            else
            {
                _userInteraction.Write("You have already hit that position.");
            }
        }

        private bool TryExtractCoordinate(string coordinateString, out Coordinate coordinate)
        {
            Regex regex = new Regex(@"^[A-J][1-9][0-9]*$");
            if (!regex.IsMatch(coordinateString))
            {
                coordinate = default;
                return false;
            }

            int yCoordinate = Helper.GetNumberOfLetter(coordinateString.First());
            int xCoordinate = int.Parse(coordinateString.Substring(1)) - 1;

            coordinate = new Coordinate(xCoordinate, yCoordinate);
            if (xCoordinate >= _board.Size || yCoordinate >= _board.Size)
                return false;

            return true;
        }


        public bool IsFinished()
        {
            return _board.AreAllShipsSinked;
        }
    }
}
