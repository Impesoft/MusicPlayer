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
            string[] songArray = songList.ToArray();
            return songArray;
        }

        public void GetMetaData(string file)
        {
           if( File.Exists(file)) { 
            TagLib.File tagFile = TagLib.File.Create(file);
          //  TagLib.Tag ourTag = tagFile.Tag;
            bool TagPresent = !(tagFile.Tag.IsEmpty);
            //Console.WriteLine($"{ourTag.Performers.ToString()} - {ourTag.Title}");
              //  Console.ReadLine();
                if (TagPresent)
                {
                    if (tagFile.Tag.Performers.Length < 1 || tagFile.Tag.Performers[0] == null)
                    {
                        tagFile.Tag.Performers = new[] { "unknown" };
                        //mp3tag.Save();
                    } 

                    string artist = tagFile.Tag.Performers[0];
                  //  string album = tagFile.Tag.Album;
                    string title = tagFile.Tag.Title;

                   // Console.Write($"{album}  |  ");
                    Console.WriteLine($"{artist} - {title}");
                }
                else
                {
                    Console.WriteLine(file);
                } 
            } else
            {
                throw new Exception();
            }
        }

        public string ViewSongs(string dirPath)
        {
            DirPath = dirPath;
            string[] songlist = FindMp3(dirPath);
            int selectIndex;
            Console.WriteLine();
            for (int i = 0; i < songlist.Length; i++)
            {
                if (i < 10)
                {
                    Console.Write("0");
                }
                selectIndex = i + 1;
                Console.Write($"{selectIndex}  ");
                GetMetaData(songlist[i]);
            }
            Console.Write("\nSelect number, type full path to file\nor drag and drop an mp3 on this console:");
            string invoer;
            do { invoer = Console.ReadLine(); } while (invoer == "");

            bool isInteger = Int32.TryParse(invoer, out int selection); //check of invoer een integer is
            string path;
            if (isInteger) //integer -> song selectie uit lijst
            {
                path = songlist[selection - 1]; // -1 want 0 based
            }
            else
            {
                invoer = invoer.Replace("\"", ""); // verwijder de mogelijke extra " " van drag n drop uit de string
                path = invoer;
            }
            return path;
        }
    }
}