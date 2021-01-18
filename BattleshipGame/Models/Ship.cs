
namespace BattleshipGame.Models
{
    public class Ship
    {
        public Ship(int size, string name)
        {
            Size = size;
            Name = name;
        }

        public int Size { get; private set; }

        public string Name { get; private set; }
    }
}
