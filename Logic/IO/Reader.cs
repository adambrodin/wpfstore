using CsvHelper;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Logic.IO
{
    public class Reader : FileHelper
    {
        public IEnumerable<T> ReadDataFromCsv<T>(string fileName)
        {
            IEnumerable<T> result;
            var x = GetProjectFilePath(fileName);
            using (TextReader fileReader = File.OpenText(GetProjectFilePath(fileName)))
            {
                var csv = new CsvReader(fileReader, System.Globalization.CultureInfo.CurrentCulture);
                csv.Configuration.HasHeaderRecord = false;
                csv.Read();
                result = csv.GetRecords<T>().ToList();
                return result;
            }
        }
    }
}
