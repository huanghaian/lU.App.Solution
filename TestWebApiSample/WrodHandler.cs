using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApiSample.Helper;

namespace TestWebApiSample.Helper
{
    public class WrodHandler: INpoiWordProvider
    {

        public IList<string> GetTextInTable(string path)
        {
            var file =Path.GetFileName(path);
            if (file.StartsWith("~$"))
            {
                return new List<string>();
            }
            var rootPath = $"C:\\WordTemp";
            var files = Directory.GetFiles(rootPath, "*.docx", SearchOption.AllDirectories);
            if (files == null)
                throw new ArgumentException("文件夹下的word文件不存在！") ;
            var filePath = path;
            var stream = File.OpenRead(filePath);
            var doc = new XWPFDocument(stream);
            var list = new List<string>();
            if (doc.Tables.Count >= 1)
            {
                var table = doc.Tables[1];
                foreach (var row in table.Rows)
                {
                    var stringBulder = new StringBuilder();
                    var count = 0;
                    foreach (var col in row.GetTableCells())
                    {
                        count++;
                        if (count == row.GetTableCells().Count - 1)
                        {
                            stringBulder.Append(col.GetText());
                        }
                        else
                            stringBulder.Append(col.GetText() + "\t");
                    }
                    Console.WriteLine(stringBulder);
                    Console.WriteLine("\n");
                    list.Add(stringBulder.ToString());
                }
                return list;
            }
            else
                return new List<string>();
        }

        public void ReadWord()
        {
            
        }
    }
}
