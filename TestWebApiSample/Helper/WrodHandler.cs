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
            if (files == null||!files.Any())
                throw new ArgumentException("文件夹下的word文件不存在！") ;
            var filePath = path;
            var stream = File.OpenRead(filePath);
            var doc = new XWPFDocument(stream);
            var list = new List<string>();
            if (doc.Tables.Count >= 1)
            {
                var table = doc.Tables[1];
                for(var index = 1; index <= table.Rows.Count - 1; index++)
                {
                    var stringBulder = new StringBuilder();

                    for (var cIndex = 0;cIndex <= table.Rows[index].GetTableCells().Count - 1; cIndex++)
                    {
                        stringBulder.Append(table.Rows[index].GetTableCells()[cIndex].GetText()+"\t");
                    }
                    list.Add(stringBulder.ToString());

                }
                return list;
            }
            stream.Close();
            return list;
        }

        public void ReadWord()
        {
            
        }
    }
}
