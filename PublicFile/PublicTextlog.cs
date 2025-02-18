using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDT_Reader
{
    internal static class PublicTextlog
    {
        private static string logPath = AppDomain.CurrentDomain.BaseDirectory + "Logs\\";
        public static void ShowLog(string data)
        {
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            DateTime dt = DateTime.Now;

            string logFile = string.Concat(logPath, dt.Day.ToString(), "-", dt.Month.ToString(), "-", dt.Year.ToString(), "_logfile.txt");
            File.AppendAllText(logFile, dt.ToString("dd/MM/yyyy HH:mm:ss.ffffff - ") + data + Environment.NewLine);
        }
    }
}
