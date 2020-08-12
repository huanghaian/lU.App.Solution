using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using UI.Mobie.BasicCore;

namespace UI.Mobie.AppCore.Droid
{
    public class AppSettings : IAppSetting, IDisposable
    {
        private SQLiteConnection _Connection;
        public AppSettings()
        {
        }
        public Task ClearAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetAysnc(string key)
        {
            throw new NotImplementedException();
        }

        public Task SetAsync(string key, string value)
        {
            throw new NotImplementedException();
        }
    }
}