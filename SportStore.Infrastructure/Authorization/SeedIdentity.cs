using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace SportStore.Infrastructure.Authorization
{
    public class SeedIdentity
    {
        private const string UserName = "Admin"; 
        private const string Password = "Admin%125";

        public static async Task CreateSuperUser(UserManager<IdentityUser> userManager)
        {
            

            var user = await userManager.FindByIdAsync(UserName);
            if (user is null)
            {
                await userManager.CreateAsync(new IdentityUser(UserName), Password);
            }
        }

        public static async Task CreateSuperUser(IUserService userService)
        {                       
            await userService.CreateAsync(UserName, Password);
        }
    }
}

