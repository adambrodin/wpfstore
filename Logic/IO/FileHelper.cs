using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using CsvHelper.Configuration;
namespace Logic.IO
{
    public class FileHelper
    {

        protected CsvConfiguration csvConfig = new CsvConfiguration{ 
                    Delimiter = ";"}
        protected static string GetFilePath(string fileName)
        {
            string tempPath = Path.GetTempPath();
            return Path.Combine(tempPath, Path.GetFileName(fileName));
        }

        protected static string GetProjectFilePath(string fileName)
        {
            return Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, Path.GetFileName(fileName));
        }

        public static void DeleteFile(string fileName)
        {
            string file = GetProjectFilePath(fileName);
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }
    }
}