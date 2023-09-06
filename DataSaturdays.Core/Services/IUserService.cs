using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSaturdays.Core.Services
{
    public interface IUserService
    {
        public Task CreateUser(IdentityUser identityUser);

        public Task UpdateUser(IdentityUser identityUser);

        public Task DeleteUser(IdentityUser identityUser);

        public Task<bool> Login(string username, string password);
    }
}
