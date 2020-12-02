using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Logic.IO
{
    public class Reader : FileHelper
    {
        public IEnumerable<T> Read<T>(string path)
        {
            IEnumerable<T> result = Enumerable.Empty<T>();
            if (!File.Exists(path))
            {
                Console.WriteLine("File does not exist");
                return result;
            }

            using (TextReader fileReader = File.OpenText(path))
            {
                var csv = new CsvReader(fileReader, System.Globalization.CultureInfo.CurrentCulture);

                csv.Configuration.HasHeaderRecord = false;
                csv.Read();
                result = csv.GetRecords<T>().ToList();
                return result;
            }
        }

        public IEnumerable<T> ReadDataFromCsv<T>(string filename)
        {
            return Read<T>(GetProjectFilePath(filename));
        }

        public IEnumerable<T> ReadDataFromCsvTemp<T>(string filename)
        {
            return Read<T>(GetTempFileLocation(filename));
        }
    }
}
