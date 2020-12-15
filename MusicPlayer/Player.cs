using System;
using WMPLib;
using TagLib;
namespace MusicPlayer
{
    internal class Player
    {
           private int volume {
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
        {  get; set;
    
        }

        public WindowsMediaPlayer MyPlayer { get; set; }

        public string SongPath { get; set; }
        public string PublicPath { get; set; }
        public string SongTitle { get; set; }
        public string SongAlbum { get; set; }
        public string SongArtist { get; set; }

        public Player(string songPath)
        {
            WindowsMediaPlayer myPlayer = new WindowsMediaPlayer();
            MyPlayer = myPlayer;
            SongPath = songPath;
            Volume = myPlayer.settings.volume;
            PublicPath = "";
        }

        public void PlaySong()
        {
            
            MyPlayer.URL = SongPath;
            SongTitle = MyPlayer.currentMedia.name;
            Volume = MyPlayer.settings.volume;

            try
            {
                TagLib.File id3 = TagLib.File.Create(SongPath);
                SongAlbum = id3.Tag.Album;
                SongArtist = id3.Tag.Performers[0];
            }
            catch { 
                Console.WriteLine(  "oops, something went wrong");
                
            }
                    }

        public void ShowMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[p/P]");
            Console.ResetColor();
            Console.WriteLine(" Pauze/play");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[+/-]");
            Console.ResetColor();
            Console.WriteLine(" Volume wijzigen");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("[m/M]");//Volume dempen/dempen opheffen
            Console.ResetColor();
            Console.WriteLine(" Mute/unMute Volume");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[l/L]");//Liedje afspelen (om een ander liedje af te spelen dan het huidige liedje)
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
        }

        public void TogglePauze()
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
        public void ToggleMute()
        {
            if (MyPlayer.settings.mute){
                
                MyPlayer.settings.mute = false;
                // unMute
                }  else 
                {
                MyPlayer.settings.mute = true;
                //Mute
            }
        }
        public void Stop()
        {
            MyPlayer.controls.stop();
        }
        public void Load()
        {
            Console.WriteLine("Type full path to file, or drag and drop an mp3 on this console");
            
            GetSongs getSong = new GetSongs();
            string filename = getSong.ViewSongs(getSong.DirPath);

            //filename = filename.Replace("\"", "");
            //filename = filename.Replace("\\", "\\\\");
            Console.WriteLine(filename);

            this.SongPath =filename;
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
                    this.Load();
                    //this.SongPath = "p:\\Music\\Simple Minds - New Gold Dream -Maxi-  12-.mp3";
                    //this.PlaySong();
                    //string title = this.SongTitle;
                    
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
    }
}