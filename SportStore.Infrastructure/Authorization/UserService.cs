using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Infrastructure.Authorization
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<IdentityUser> GetByUsername(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<bool> CreateAsync(string userName, string password)
        {
            var user = await _userManager.FindByIdAsync(userName);
            if (user is null)
            {
               var result =  (await _userManager.CreateAsync(new IdentityUser(userName), password)).Succeeded;
               return result;
            }
            return false;
        }
    }
}
