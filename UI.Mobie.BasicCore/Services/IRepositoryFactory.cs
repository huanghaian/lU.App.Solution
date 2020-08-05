using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Mobie.BasicCore.Services
{
    public interface IRepositoryFactory
    {
        IRepository CreateRepository(Type type);
    }
}
