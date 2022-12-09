using Microsoft.AspNetCore.Identity;
using System;
using System.Text;
using System.Threading.Tasks;
using TalkToApiStudyTest.V1.Models;
using TalkToApiStudyTest.V1.Repositories.Contracts;

#pragma warning disable 
namespace TalkToApiStudyTest.V1.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser> Get(string email, string password)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);

           

            if(await _userManager.CheckPasswordAsync(user,password))
            {
                return user;
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        public async Task<ApplicationUser> Get(string userId)
        {
            return await _userManager.FindByIdAsync(userId);


        }

        public void Register(ApplicationUser user, string password)
        {
            var result = _userManager.CreateAsync(user, password).Result;

            if (!result.Succeeded)
            {
                StringBuilder stringBuider = new StringBuilder();

                foreach(var error in result.Errors)
                {
                    stringBuider.Append(error.Description);
                }

                throw new Exception("User Not Found. " + stringBuider.ToString());
            }
        }
    }
}
