using System;

namespace BattleshipGame
{
    public class UserInteraction : IUserInteraction
    {
        public string Read()
        {
            return Console.ReadLine();
        }

        public void Write(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
