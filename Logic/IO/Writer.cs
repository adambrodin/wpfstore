using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Logic.IO
{
    public class Writer : FileHelper
    {

        public void Write(IEnumerable<object> data, string path)
        {
            using (var ms = new MemoryStream())
            {
                using (var writer = new StreamWriter(ms))
                {
                    using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csvWriter.WriteRecords(data);
                        writer.Flush();
                       
                        ms.Seek(0, SeekOrigin.Begin);

                        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                        {
                            ms.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                }
            }
        }
        public void WriteDataToCsv(IEnumerable<object> data, string filename)
        {
            Write(data, GetProjectFilePath(filename));
        }

        public void WriteDataToCsvTemp(IEnumerable<object> data, string filename)
        {
            Write(data, GetTempFileLocation(filename));
        }
    }
}
