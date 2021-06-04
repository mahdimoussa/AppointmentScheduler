using App.Library.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Library.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext dbContext;
        private readonly IConfiguration Config;
       // private readonly ILogger ILogger;
        public UserRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
           
        }

        public Task<Tuple<bool, bool>> DeleteUser(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Tuple<bool, List<User>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Tuple<bool>> SaveUser(User User)
        {
            throw new NotImplementedException();
        }
    }
}
