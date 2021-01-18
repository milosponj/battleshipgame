
namespace BattleshipGame
{
    public static class Helper
    {
        public static string GetRowLetterByNumber(int number)
        {
            if (number >= 0 && number <= 26)
            {
                return "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Substring(number, 1);
            }
            else
            {
                return " ";
            }
        }

        public static int GetNumberOfLetter(char letter)
        {
            return char.ToUpperInvariant(letter) - 65;
        }
    }
}
