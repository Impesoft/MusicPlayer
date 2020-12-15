using System;

namespace MusicPlayer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string dirPath = "P:\\Music";
            Player myPlayer = new Player();
            myPlayer.Load(dirPath);
            myPlayer.PlaySong();
            myPlayer.PublicPath = dirPath;
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