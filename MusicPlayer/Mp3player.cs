using System;

namespace MusicPlayer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string dirPath = "P:\\Music";
            //GetSongs getSong = new GetSongs();

            //string path = getSong.ViewSongs(dirPath);

            Player myPlayer = new Player();
            myPlayer.Load(dirPath);
            myPlayer.PlaySong();
            myPlayer.PublicPath= dirPath;
            string keyPress;
            do
            {
                myPlayer.ShowMenu();
                Console.WriteLine();
                Console.WriteLine($"now playing: {myPlayer.SongArtist} - { myPlayer.SongTitle}.");
      //          Console.WriteLine(myPlayer.SongTitle);
        //        Console.WriteLine(myPlayer.SongAlbum);
                Console.WriteLine($"Current Volume {myPlayer.Volume}%");
                keyPress = (Console.ReadKey().KeyChar).ToString().ToLower();
                myPlayer.Action(keyPress);
            } while (keyPress != "x");
        }
    }
}