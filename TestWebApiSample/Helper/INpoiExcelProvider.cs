using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApiSample.Helper
{
    public interface INpoiExcelProvider
    {
        void CreateExcel(IEnumerable<string> data, char dataSqiltor, string filePath = null);
    }
}
