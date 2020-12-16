using System;

namespace MusicPlayer
{
    internal class Program
    {
        private static void Main()
        {
            string dirPath = "P:\\Music";
            Player myPlayer = new Player(dirPath);
            char keyPress;
            do
            {
                myPlayer.ShowMenu();
                keyPress = char.ToLower(Console.ReadKey().KeyChar);
                myPlayer.Action(keyPress);
            } while (keyPress != 'x');
        }
    }
}