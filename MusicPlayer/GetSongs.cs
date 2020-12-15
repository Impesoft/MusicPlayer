using System;
using System.Collections.Generic;
using System.IO;

namespace MusicPlayer
{
    internal class GetSongs
    {
        public string DirPath { get; set; }
        public string[] FindMp3(string dir)   
        {
            List<string> songList = new List<string>();

            foreach (string file in Directory.EnumerateFiles(dir, "*.mp3"))
            {
                songList.Add(file);
            }
            string[] songArray = new string[songList.Count];

            for (int i = 0; i < songArray.Length; i++)
            {
                songArray[i] = songList[i];
            }
            return songArray;
        }

        public void GetMetaData(string file)
        {
            
            TagLib.File tagFile = TagLib.File.Create(file);
            bool booltje = tagFile.Tag.IsEmpty;
            if (!booltje) { 
            string artist = tagFile.Tag.Performers[0];
            string album = tagFile.Tag.Album;
            string title = tagFile.Tag.Title;
           
            Console.Write($"{album}  |  ");
            Console.Write($"{artist}  |  ");
            Console.WriteLine(title);
            } else
            {
                Console.WriteLine(file);
            }
        }

        public string ViewSongs(string dirPath)
        {
            DirPath = dirPath;
            string[] songlist = FindMp3(dirPath);
            int selectIndex;
            for (int i = 0; i < songlist.Length; i++)
            {
                if (i < 10)
                {
                    Console.Write("0");
                }
                Console.Write($"{i}  ");
                GetMetaData(songlist[i]);
                selectIndex = i + 1;
            }
            string invoer = Console.ReadLine();
            bool success = Int32.TryParse(invoer, out int selection);
            string path;
            if (success)
            {
                path = songlist[selection];
            }
            else
            {
                invoer = invoer.Replace("\"", "");
                path = invoer;
            }
            return path;
        }
    }
}