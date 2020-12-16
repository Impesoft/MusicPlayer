using System;
using WMPLib;

namespace MusicPlayer
{
    internal class Player
    {
        private int volume
        {
            get
            {
                return Volume;
            }
            set
            {
                if (value > 100)
                {
                    MyPlayer.settings.volume = 100;
                    Volume = 100;
                }
                else
                {
                    if (value < 0)
                    {
                        MyPlayer.settings.volume = 0;
                        Volume = 0;
                    }
                    else
                    {
                        Volume = value;
                        MyPlayer.settings.volume = Volume;
                    }
                }
            }
        }

        public int Volume
        {
            get; set;
        }

        public WindowsMediaPlayer MyPlayer { get; set; }

        public string SongPath { get; set; }
        public string PublicPath { get; set; }
        public string SongTitle { get; set; }
        public string SongAlbum { get; set; }
        public string SongArtist { get; set; }

        public Player()
        {
            WindowsMediaPlayer myPlayer = new WindowsMediaPlayer();
            MyPlayer = myPlayer;
            Volume = 100;
            PublicPath = "c:\\";
        }

        public void PlaySong()
        {
            MyPlayer.URL = SongPath;
            SongTitle = MyPlayer.currentMedia.name;
            Volume = 100;
        }

        public void ShowMenu()
        {
            Console.Clear();
            ShowTitle();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[p/P]");
            Console.ResetColor();
            Console.WriteLine(" Pauze/play");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[+/-]");
            Console.ResetColor();
            Console.WriteLine(" Volume wijzigen");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("[m/M]");//   Mute/unMute
            Console.ResetColor();
            Console.WriteLine(" Mute/unMute Volume");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[l/L]");//   Load track (om een ander liedje af te spelen dan het huidige liedje)
            Console.ResetColor();
            Console.WriteLine(" Laad (ander) Liedje");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[s/S]");//Stoppen (stopt het afspelen van het huidige liedje)
            Console.ResetColor();
            Console.WriteLine(" Stoppen (stopt het afspelen van het huidige liedje)");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write("[x/X]");
            Console.ResetColor();
            Console.WriteLine(" eXit (Afsluiten; sluit de volledige applicatie af)");
            Console.WriteLine();
            Console.WriteLine($"now playing: {SongArtist} - { SongTitle}.");
            Console.WriteLine($"Current Volume {Volume}%");
        }

        private void TogglePauze()
        {
            if (MyPlayer.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                MyPlayer.controls.pause();
                // pauze
            }
            else
            {
                MyPlayer.controls.play();
                //play
            }
        }

        private void ToggleMute()
        {
            if (MyPlayer.settings.mute)
            {
                MyPlayer.settings.mute = false;
                // unMute
            }
            else
            {
                MyPlayer.settings.mute = true;
                //Mute
            }
        }

        private void Stop()
        {
            MyPlayer.controls.stop();
        }

        public void Load(string path)
        {
            PublicPath = path;
            GetSongs getSong = new GetSongs();
            path = getSong.ViewSongs(PublicPath);
            string filename = path;
            Console.WriteLine(filename);
            this.SongPath = filename;
            this.PlaySong();
            string title = this.SongTitle;
        }

        public void Action(string action)
        {
            switch (action)
            {
                case "p":
                    this.TogglePauze();
                    break;

                case "l":
                    this.Load(PublicPath);
                    break;

                case "s":
                    this.Stop();
                    break;

                case "+":
                    volume++;
                    break;

                case "-":
                    volume--;
                    break;

                case "m":
                    this.ToggleMute();
                    break;

                default:
                    break;
            }
        }

        private static void ShowTitle()
        {
            string title = @"   *                           (    (
 (  `          (               )\ ) )\ )
 )\))(     (   )\ )  (      ) (()/((()/(     )  (       (   (
((_)()\   ))\ (()/(  )\  ( /(  /(_))/(_)) ( /(  )\ )   ))\  )(
(_()((_) /((_) ((_))((_) )(_))(_)) (_))   )(_))(()/(  /((_)(()\
|  \/  |(_))   _| |  (_)((_)_ | _ \| |   ((_)_  )(_))(_))   ((_)
| |\/| |/ -_)/ _` |  | |/ _` ||  _/| |__ / _` || || |/ -_) | '_|
|_|  |_|\___|\__,_|  |_|\__,_||_|  |____|\__,_| \_, |\___| |_|
                                                |__/             ";
            Console.WriteLine(title);
            Console.WriteLine();
        }
    }
}