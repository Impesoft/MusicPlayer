using System;
using System.IO;

namespace MusicPlayer
{
    internal class LogAction
    {
        public void Log(string path, string songName, string action)
        {
            CreateDFolder(path + "\\log");
            string logFile = path + "\\log\\logFile.txt";
            CreateLogFile(logFile); //if it doesn't exist already
            WriteDataToFile(action, songName, logFile);
        }

        public void CreateDFolder(string dirPath)
        {
            Directory.CreateDirectory(dirPath);
        }
        private void CreateLogFile(string logFile)
        {
            if (!File.Exists(logFile))
            {
                FileStream fileStream = File.Create(logFile);
                fileStream.Close();
            }
        }

        private void WriteDataToFile(string action, string songName, string path)
        {
            using StreamWriter writer = new StreamWriter(path, true);
            string timeStamp = Convert.ToString(DateTime.Now);
            string logMessage = $"Log:{timeStamp}---{songName} was {action}";
            writer.WriteLine(logMessage);
        }
    }
}