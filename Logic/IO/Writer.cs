using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Logic.IO
{
    public class Writer
    {
        private string GetFilePath(string fileName)
        {
            string tempPath = Path.GetTempPath();
            return Path.Combine(tempPath, Path.GetFileName(fileName));
        }

        public void WriteDataToCsv(IEnumerable<Object> data, string fileName)
        {
            Console.WriteLine(GetFilePath(fileName));
            using (var ms = new MemoryStream())
            {
                using (var writer = new StreamWriter(ms))
                {
                    using (var csvWriter = new CsvWriter(writer, CultureInfo.CurrentCulture))
                    {
                        csvWriter.WriteRecords(data);
                        writer.Flush();
                        // Set position within current stream to beginning, before copying
                        ms.Seek(0, SeekOrigin.Begin);

                        using (FileStream fs = new FileStream(GetFilePath(fileName), FileMode.OpenOrCreate))
                        {
                            ms.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                }
            }
        }
    }
}
