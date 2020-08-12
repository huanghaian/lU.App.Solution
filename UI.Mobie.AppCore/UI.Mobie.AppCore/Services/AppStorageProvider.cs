using System;
using System.Collections.Generic;
using System.Text;
using UI.Mobie.BasicCore;

namespace UI.Mobie.AppCore.Services
{
    public class AppStorageProvider : IAppStorageProvider
    {
        public IAppStorage GetStorage()
        {
            return new AppStroge(Xamarin.Essentials.FileSystem.AppDataDirectory+"/App_Data");
        }
    }
}
