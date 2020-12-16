using System;

namespace MusicPlayer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string dirPath = "P:\\Music";
            Player myPlayer = new Player(dirPath);
            string keyPress;
            do
            {
                myPlayer.ShowMenu();
                keyPress = (Console.ReadKey().KeyChar).ToString().ToLower();
                myPlayer.Action(keyPress);
            } while (keyPress != "x");
        }
    }
}