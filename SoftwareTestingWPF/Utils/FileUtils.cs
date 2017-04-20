using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CsvHelper;
using CsvHelper.Configuration;

namespace SoftwareTestingWPF.Utils
{
    static class FileUtils<T> where T : CsvClassMap
    {
        public static string ReadAll(string path)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                return streamReader.ReadToEnd();
            }
        }

        public static CsvWriter CreateCsvWriter(string path)
        {
            FileStream fs;
            try
            {
                fs = new FileStream(path, FileMode.Create);

            }
            catch (Exception e)
            {
                MessageBox.Show(Application.Current.MainWindow, "文件" + path + "被占用，请关闭后再试", "写入失败");
                return null;
            }

            return new CsvWriter(new StreamWriter(fs, Encoding.Default));
        }

        public static List<T> ReadDateList(string path, int colIndex = 0)
        {
            var fs = new FileStream(path, FileMode.Open);

            var csv = new CsvReader(new StreamReader(fs, Encoding.Default));
            var result = csv.GetRecords<T>().ToList();
            csv.Dispose();
            return result;
        }

        public static void WriteResult(List<T> resultList, string fileName)
        {
            var csvWriter = CreateCsvWriter(fileName);
            if (null != csvWriter)
            {
                csvWriter.WriteRecords(resultList);
                csvWriter.Dispose();
            }
        }
    }
}
