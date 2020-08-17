using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApiSample.Helper
{
    public class ExcelHandler : INpoiExcelProvider
    {
        private string FilePath = Path.GetDirectoryName(typeof(Startup).Assembly.Location);
        public void CreateExcel(IEnumerable<string> data,char dataSqiltor, string filePath = null)
        {
            if (filePath != null)
                FilePath = filePath;
            var sheetBook = new HSSFWorkbook();
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "UI.Test.com";
            sheetBook.DocumentSummaryInformation = dsi;
            var sheet = sheetBook.CreateSheet("周报");
            var firstRow = sheet.CreateRow(0);
            firstRow.CreateCell(0).SetCellValue("序号");
            firstRow.CreateCell(1).SetCellValue("工作内容");
            firstRow.CreateCell(2).SetCellValue("备注");

            for (var row = 1; row < data.Count() - 1; row++)
            {
                var excelRow = sheet.CreateRow(row);
                var _data = data.ToArray()[row].Split(dataSqiltor).ToArray();
                for (var col = 0; col < _data.Length - 1; col++)
                {
                    var cell = excelRow.CreateCell(col);
                    cell.SetCellValue(_data[col]);
                }
            };
            string root = FilePath + "\\Data_File\\";
            sheet.AutoSizeColumn(1);
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            var stream = new FileStream(root + Guid.NewGuid()+ ".xls", FileMode.OpenOrCreate);
            sheetBook.Write(stream);
            stream.Close();
        }
    }
}
