using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Logic.IO
{
    public class FileHelper
    {
        protected static string GetFilePath(string fileName)
        {
            string tempPath = Path.GetTempPath();
            return Path.Combine(tempPath, Path.GetFileName(fileName));
        }

        public static void DeleteFile(string fileName)
        {
            string file = GetFilePath(fileName);
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }
    }
}