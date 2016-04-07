using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRollEntryApp
{
    public class Logger
    {
        public static FileInfo _FInfo;
        public static FileStream _sw;

        public static string fileName;

        public static void LogToFile(string message)
        {
            DirectoryInfo dInfo;
            var folderPath = System.Configuration.ConfigurationManager.AppSettings["LoggingPath"];
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            dInfo = new DirectoryInfo(folderPath);

            if (fileName == null)
            {
                var filePath = System.IO.Path.Combine(dInfo.FullName, string.Concat(Guid.NewGuid(), ".txt"));
                fileName = filePath;
                _FInfo = new FileInfo(filePath);
                _sw = _FInfo.Create();

                using (StreamWriter sw = new StreamWriter(_sw))
                {
                    sw.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(fileName))
                {
                    sw.WriteLine(message);
                }
            }

        }

    }
}
