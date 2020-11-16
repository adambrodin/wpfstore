using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Logic.IO
{
    public class Reader : FileHelper
    {
        public IEnumerable<T> ReadDataFromCsv<T>(string fileName)
        {
            IEnumerable<T> result;
            using (TextReader fileReader = File.OpenText(GetProjectFilePath(fileName)))
            {
                var csv = new CsvReader(fileReader, CultureInfo.CurrentCulture);
                csv.Configuration.HasHeaderRecord = false;
                csv.Read();
                result = csv.GetRecords<T>().ToList();
                return result;
            }
        }
    }
}
