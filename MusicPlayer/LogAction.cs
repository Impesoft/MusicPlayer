using System;
using System.IO;

namespace MusicPlayer
{
    internal class LogAction
    {
        public string LogFile { get; set; }

        public void Log(string path, string songName, string action)
        {
            string logFile = path + "\\logFile.txt";
            CreateLogFile(logFile); //if it doesn't exist already
            WriteDataToFile(action, songName, logFile);
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