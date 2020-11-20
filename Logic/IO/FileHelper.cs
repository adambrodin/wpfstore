using System.IO;

namespace Logic.IO
{
    public class FileHelper
    {
        public static string GetTempFileLocation(string fileName)
        {
            string tempPath = Path.GetTempPath();
            return Path.Combine(tempPath, Path.GetFileName(fileName));
        }

        public static string GetProjectFilePath(string fileName)
        {
            return Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, fileName);
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