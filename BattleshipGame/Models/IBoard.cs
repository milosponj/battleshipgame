

namespace BattleshipGame.Models
{
    public interface IBoard
    {
        int Size { get; }
        bool AreAllShipsSinked { get; }
        void AddShipRandomly(Ship ship);
        ShootResult ShootAt(Coordinate coordinate);
    }
}
