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
        private static readonly Dictionary<Type, IRepository> FactoryDictionary;
        static AppRepositoryFactory()
        {
            FactoryDictionary = new Dictionary<Type, IRepository>()
            {
                { typeof(Item),new ItemRepository()}
            };
        }
        public IRepository CreateRepository(Type type)
        {
            try
            {
                return FactoryDictionary[type];
            }
            catch(Exception ex)
            {
                if (ex is KeyNotFoundException)
                    throw new ArgumentException($"在{nameof(FactoryDictionary)}不存在{type}的类型实例");
                else
                    throw new ArgumentException("创建仓储失败");
            }
        }
    }
}
