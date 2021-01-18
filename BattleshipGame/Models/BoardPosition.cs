
namespace BattleshipGame.Models
{
    public class BoardPosition
    {
        public Ship ShipPlaced { get; set; }
        public bool IsHit { get; internal set; }
    }
}
