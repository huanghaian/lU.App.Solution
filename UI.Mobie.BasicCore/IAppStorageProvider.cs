using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Mobie.BasicCore
{
    public interface IAppStorageProvider
    {
        IAppStorage GetStorage();
    }
}
