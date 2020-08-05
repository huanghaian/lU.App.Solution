using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Mobie.BasicCore.Services
{
    public interface IRepository<out T>: IRepository
    {
    }
}
