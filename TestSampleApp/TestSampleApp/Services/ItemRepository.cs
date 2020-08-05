using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UI.Mobie.BasicCore.Services;

namespace TestSampleApp.Services
{
    public class ItemRepository : IRepository<TestSampleApp.Models.Item>
    {
        public ItemRepository() { }
        public Item[] GetAll<Item>()
        {
            return new Item[10];
        }

        public Task<Item[]> GetAllAsync<Item>()
        {
            throw new NotImplementedException();
        }
    }
}
