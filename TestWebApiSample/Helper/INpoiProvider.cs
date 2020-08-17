using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApiSample.Helper
{
    public interface INpoiWordProvider
    {
        IList<string> GetTextInTable(string path);

    }
}
