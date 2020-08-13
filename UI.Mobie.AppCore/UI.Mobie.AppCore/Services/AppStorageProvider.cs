using System;
using System.Collections.Generic;
using System.Text;
using UI.Mobie.BasicCore;

namespace UI.Mobie.AppCore.Services
{
    public class AppStorageProvider : IAppStorageProvider
    {
        public IAppStorage GetStorage(string type=null)
        {
            if (type != null)
            {
                string root;
                switch (type)
                {
                    case "database":
                       root = Xamarin.Essentials.FileSystem.AppDataDirectory + "/Data";
                        break;
                    case "app":
                        root=Xamarin.Essentials.FileSystem.AppDataDirectory + "/App";
                        break; 
                    default:
                        root = Xamarin.Essentials.FileSystem.AppDataDirectory + "/File";
                        break;
                }
                return new AppStroge(root);

            }
            else
            {
                return new AppStroge(Xamarin.Essentials.FileSystem.AppDataDirectory + "/App_Data");

            }
        }
    }
}
