using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UI.Mobie.BasicCore.Services
{
    public interface IRepository
    {
        T[] GetAll<T>();
        Task<T[]> GetAllAsync<T>();
    }
}
