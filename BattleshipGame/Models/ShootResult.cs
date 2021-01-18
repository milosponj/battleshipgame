using BattleshipGame.Enums;

namespace BattleshipGame.Models
{
    public class ShootResult
    {
        public ShootStatus ShootStatus { get; set; } = ShootStatus.ShipMissed;
        public Ship ShipHit { get; set; }
    }
}
