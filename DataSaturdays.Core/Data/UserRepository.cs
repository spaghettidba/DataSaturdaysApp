using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSaturdays.Core.Data
{
    internal class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CreateUser(IdentityUser identityUser)
        {
            try
            {

                string sql = """"
                INSERT INTO AspNetUsers
                VALUES(
                	@Id,
                	@UserName,
                	@NormalizedUserName,
                	@Email,
                	@NormalizedEmail,
                	@EmailConfirmed,
                	@PasswordHash,
                	@SecurityStamp,
                	@ConcurrencyStamp,
                	@PhoneNumber,
                	@PhoneNumberConfirmed,
                	@TwoFactorEnabled,
                	@LockoutEnd,
                	@LockoutEnabled,
                	@AccessFailedCount
                )
                """";

                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(sql, identityUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
            bool result = false;
            try
            {
                string sql = """"
                SELECT *
                FROM AspNetUsers
                WHERE UserName = @Username AND PasswordHash = Password
                """";

                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(sql, username);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}
