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
using Mono.Data.Sqlite;
using UI.Mobie.BasicCore;

namespace UI.Mobie.AppCore.Droid
{
    public class AppSettings : IAppSetting, IDisposable
    {
        private SqliteConnection _Connection;
        public AppSettings(IAppStorageProvider provider)
        {
            var storage = provider.GetStorage();
            var file = storage.GetFileInfo("/AppSettings.db");
            if (!file.Exists)
            {
                SqliteConnection.CreateFile(file.FullName);
                _Connection = new SqliteConnection("Data Source="+file.FullName);
                _Connection.Open();
                using (var command = _Connection.CreateCommand())
                {
                    command.CommandText = "create table [settings] ([Key] ntext Not Null,[Value] ntext,PRIMARY KEY([Key]))";
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                _Connection = new SqliteConnection("Data Source=" + file.FullName);
                _Connection.Open();
            }
        }
        public async Task ClearAsync()
        {
            if (_Disposed)
                throw new ObjectDisposedException(nameof(AppSettings));
            if (_Connection.State == System.Data.ConnectionState.Closed)
                await _Connection.OpenAsync();
            using (var command = _Connection.CreateCommand())
            {
                command.CommandText = "DELETE [settings]";
                await command.ExecuteNonQueryAsync();
            }
        }


        private bool _Disposed;
        public void Dispose()
        {
            if (_Disposed)
                return;
            _Disposed = true;
            _Connection.Close();
            _Connection = null;

        }

        public async Task<string> GetAysnc(string key)
        {
            if (_Disposed)
                throw new ObjectDisposedException(nameof(AppSettings));
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if(_Connection.State == System.Data.ConnectionState.Closed)
                await _Connection.OpenAsync();
            using (var command  = _Connection.CreateCommand())
            {
                command.CommandText = "SELECT [Value] FROM [settings] WHERE [Key]=@KEY;";
                command.Parameters.AddWithValue("KEY",key);
                var result = await command.ExecuteScalarAsync();
                if (result == DBNull.Value)
                    return null;
                return (string)result;
            }
        }

        public async Task SetAsync(string key, string value)
        {
            if (_Disposed)
                throw new ObjectDisposedException(nameof(AppSettings));
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (_Connection.State == System.Data.ConnectionState.Closed)
                await _Connection.OpenAsync();
            using (var command = _Connection.CreateCommand())
            {
                command.CommandText = "REPLACE INTO [settings] VALUES(@KEY,@VALUE);";
                command.Parameters.AddWithValue("KEY",key);
                command.Parameters.AddWithValue("VALUE",value);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}