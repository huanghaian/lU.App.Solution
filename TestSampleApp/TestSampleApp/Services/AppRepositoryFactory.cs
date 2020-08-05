using System;
using System.Collections.Generic;
using System.Text;
using TestSampleApp.Models;
using UI.Mobie.AppCore.Services;
using UI.Mobie.BasicCore.Services;

namespace TestSampleApp.Services
{
    public class AppRepositoryFactory : IRepositoryFactory
    {
        public IRepository CreateRepository(Type type)
        {
            if(type==typeof(Item))
                return new ItemRepository();
            return
                null;
        }
    }
}
