using System;
using System.Collections.Concurrent;
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
            try
            {
                return new AppRepository();
            }
            catch(Exception ex)
            {
                    throw new ArgumentException("创建仓储失败");
            }
        }
    }
}
