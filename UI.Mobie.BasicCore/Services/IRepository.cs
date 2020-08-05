using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UI.Mobie.BasicCore.Services
{
    public interface IRepository
    {
        Task<T[]> GetAllAsync<T>();
    }
}
