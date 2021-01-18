using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    public class Randomizer : IRandomizer
    {
        private Random _random;

        public Randomizer()
        {
            _random = new Random();
        }
        public int Next(int maxValue)
        {
            return _random.Next(maxValue);
        }
    }
}
