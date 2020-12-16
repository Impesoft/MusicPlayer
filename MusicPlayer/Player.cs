using System;
using WMPLib;

namespace MusicPlayer
{
    internal class Player
    {
        public string SongPath { get; set; }
        public string SongTitle { get; set; }
        public string SongAlbum { get; set; }
        public string SongArtist { get; set; }
        public string DirPath { get; set; }

        public Player(string dirPath)
        {
            WindowsMediaPlayer myPlayer = new WindowsMediaPlayer();
            MyPlayer = myPlayer;
            DirPath = dirPath;
            Volume = 100;
            Load();
            PlaySong();
        }

        private int volume { get; set; }

        public int Volume
        {
            get
            {
                return volume;
            }
            set
            {
                if (value > 100)
                {
                    MyPlayer.settings.volume = 100;
                    volume = 100;
                }
                else
                {
                    if (value < 0)
                    {
                        MyPlayer.settings.volume = 0;
                        volume = 0;
                    }
                    else
                    {
                        volume = value;
                        MyPlayer.settings.volume = volume;
                    }
                }
            }
        }

        public WindowsMediaPlayer MyPlayer { get; set; }

        private void PlaySong()
        {
            MyPlayer.URL = SongPath;
            SongTitle = MyPlayer.currentMedia.name;
        }

        public void ShowMenu()
        {
            Console.Clear();
            ShowTitle();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[p/P]"); // pause/unpause
            Console.ResetColor();
            Console.WriteLine(" Pauze/play");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[+/-]"); // use + or - to change volume
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
            Console.Write("[x/X]");//Exit the program completely
            Console.ResetColor();
            Console.WriteLine(" eXit (Afsluiten; sluit de volledige applicatie af)");

            Console.WriteLine();
            Console.WriteLine($"now playing: {SongArtist} - {SongTitle}.");
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

        private void Load()
        {
            string path = DirPath;
            GetSongs getSong = new GetSongs();
            path = getSong.ViewSongs(DirPath);
            string filename = path;
            Console.WriteLine(filename);
            this.SongPath = filename;
            this.PlaySong();
            string title = this.SongTitle;
        }

        public void Load(string DirPath)
        {
            //DirPath = DirPath;
            GetSongs getSong = new GetSongs();
            string filename = getSong.ViewSongs(DirPath); 
            Console.WriteLine(filename);
            this.SongPath = filename;
            this.PlaySong();
        }

        public void Action(Char action)
        {
            LogAction logAction = new LogAction();
            
            switch (action)
            {
                case 'p':
                    this.TogglePauze();
                    logAction.Log(DirPath, this.SongTitle, "Pauzed/Unpauzed");
                    break;

                case 'l':
                    this.Load(DirPath);
                    logAction.Log(DirPath, this.SongTitle, "Loaded");
                    break;

                case 's':
                    this.Stop();
                    logAction.Log(DirPath, this.SongTitle, "Stopped");
                    break;

                case '+':
                    Volume++;
                    break;

                case '-':
                    Volume--;
                    break;

                case 'm':
                    this.ToggleMute();
                    logAction.Log(DirPath, this.SongTitle, "Muted/Unmuted");
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