using DataSaturdays.Core.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSaturdays.Core.Services
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateUser(IdentityUser identityUser)
        {
            await _userRepository.CreateUser(identityUser);
        }

        public async Task DeleteUser(IdentityUser identityUser)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUser(IdentityUser identityUser)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Login(string username, string password)
        {
            return await _userRepository.Login(username, password);
        }
    }
}
