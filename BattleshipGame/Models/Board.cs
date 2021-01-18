using BattleshipGame.Enums;
using System;
using System.Text;

namespace BattleshipGame.Models
{
    public class Board : IBoard
    {
        private IRandomizer _random { get; }
        private int _remainingShipPositionsCount { get; set; }
        private BoardPosition[,] Positions { get; set; }

        public bool AreAllShipsSinked => _remainingShipPositionsCount == 0;
        public int Size => Positions.GetLength(0);

        public Board(int size, IRandomizer randomProvider)
        {
            if (size > 26)
            {
                throw new Exception("Board too big!");
            }
            _random = randomProvider;
            Positions = new BoardPosition[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Positions[i, j] = new BoardPosition();
                }
            }
        }

        public void AddShipRandomly(Ship ship)
        {
            if (ship.Size > Size)
            {
                throw new Exception("The added ship is bigger than the board.");
            }

            Orientation orientation = _random.Next(2) == 1 ? Orientation.Vertical : Orientation.Horizontal;

            bool isShipAdded = false;
            //there is potential for infinite loop in case of adding so many ships that a new one doesn't have room
            while (!isShipAdded)
            {
                var X = orientation == Orientation.Vertical ? _random.Next(Size) : _random.Next(Size - ship.Size);
                var Y = orientation == Orientation.Vertical ? _random.Next(Size - ship.Size) : _random.Next(Size);

                if (CanShipBeAdded(ship, X, Y, orientation))
                {
                    AddShip(ship, X, Y, orientation);
                    isShipAdded = true;
                }
            }
        }

        public ShootResult ShootAt(Coordinate coordinate)
        {
            var result = new ShootResult();
            var boardPosition = Positions[coordinate.X, coordinate.Y];
            if (boardPosition.IsHit == true)
                result.ShootStatus = ShootStatus.AlreadyHit;
            else
            {
                boardPosition.IsHit = true;
                if (boardPosition.ShipPlaced != null)
                {
                    _remainingShipPositionsCount--;
                    result.ShootStatus = ShootStatus.ShipHit;
                    result.ShipHit = boardPosition.ShipPlaced;
                }
                else
                {
                    result.ShootStatus = ShootStatus.ShipMissed;
                }
            }

            return result;
        }

        private void AddShip(Ship ship, int x, int y, Orientation orientation)
        {
            _remainingShipPositionsCount += ship.Size;

            if (orientation == Orientation.Vertical)
            {
                for (int i = y; i < y + ship.Size; i++)
                {
                    Positions[x, i].ShipPlaced = ship;
                }
            }
            else
            {
                for (int i = x; i < x + ship.Size; i++)
                {
                    Positions[i, y].ShipPlaced = ship;
                }
            }
        }

        private bool CanShipBeAdded(Ship ship, int x, int y, Orientation orientation)
        {
            if (orientation == Orientation.Vertical)
            {
                for (int i = y; i < y + ship.Size; i++)
                {
                    if (Positions[x, i].ShipPlaced != null)
                    {
                        return false;
                    }
                }
            }
            else
            {
                for (int i = x; i < x + ship.Size; i++)
                {
                    if (Positions[i, y].ShipPlaced != null)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override string ToString()
        {
            StringBuilder output = GetColumnNumbers();
            for (var y = 0; y < Size; y++)
            {
                output.Append(Helper.GetRowLetterByNumber(y));
                output.Append("|");
                for (var x = 0; x < Size; x++)
                {
                    var position = Positions[x, y];
                    if (position.IsHit == true)
                    {
                        if (position.ShipPlaced == null)
                        {
                            output.Append("o|");
                            continue;
                        }
                        else
                        {
                            output.Append("X|");
                            continue;
                        }
                    }
                    else
                    {
                        output.Append(" |");
                        continue;
                    }
                }
                output.Append('\n');
            }

            return output.ToString();
        }

        private StringBuilder GetColumnNumbers()
        {
            ///1 2 3 4 5 6 7 8 9 10 \n
            var columnNames = new StringBuilder(" ");
            for (int i = 1; i <= Size; i++)
            {
                if (i <= 10)
                {
                    columnNames.Append($" {i}");
                }
                else
                {
                    columnNames.Append($"{i}");
                }
            }
            columnNames.Append("\n");
            return columnNames;
        }
    }
}
