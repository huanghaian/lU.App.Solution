using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UI.Mobie.BasicCore.Services;

namespace UI.Mobie.AppCore.Services
{
    public class Repository<T> : IRepository<T>
    {
        private readonly IRepository _repository;
        public Repository(IRepositoryFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }
            _repository = factory.CreateRepository(typeof(T));
        }
        T[] IRepository.GetAll<T>()
        {
            return _repository.GetAll<T>();
        }

        async Task<T[]> IRepository.GetAllAsync<T>()
        {
            return await _repository.GetAllAsync<T>();
        }
    }
}
