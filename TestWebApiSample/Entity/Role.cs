using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApiSample.Entity
{
    public class Role:IdentityRole<Guid>
    {
        public Role()
        {
            Id = Guid.NewGuid();
        }
    }
}
