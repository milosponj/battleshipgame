using System;

namespace BattleshipGame
{
    public interface IUserInteraction
    {
        void Write(string text, ConsoleColor color = ConsoleColor.White);
        string Read();
    }
}
