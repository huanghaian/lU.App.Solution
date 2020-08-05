using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSampleApp.Models;
using UI.Mobie.AppCore.Extensions;
using UI.Mobie.BasicCore.Services;

namespace TestSampleApp.Services
{
    public class AppRepository: IRepository
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });
        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;
        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Item).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Item)).ConfigureAwait(false);
                }
                initialized = true;
            }
        }

        public Item[] GetAll<Item>()
        {
            return new Item[10];
        }
      
        public Task<T[]> GetAllAsync<T>()
        {
            throw new NotImplementedException();
        }

        public AppRepository() 
        {
            InitializeAsync().SafeFireAndForget(false);
        }
    }
}
