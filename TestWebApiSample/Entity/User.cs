using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApiSample.Entity
{
    public class User:IdentityUser<Guid>
    {
        public User()
        {
            Id = Guid.NewGuid();
        }
    }
}
