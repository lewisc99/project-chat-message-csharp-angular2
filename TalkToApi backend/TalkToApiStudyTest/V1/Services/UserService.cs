using System;
using System.Threading.Tasks;
using TalkToApiStudyTest.V1.Models;
using TalkToApiStudyTest.V1.Repositories.Contracts;
using TalkToApiStudyTest.V1.Services.Contracts;

#pragma warning disable
namespace TalkToApiStudyTest.V1.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
     

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

     
        public async Task<ApplicationUser> Get(string email, string password)
        {
            try
            {
                return await _userRepository.Get(email, password);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ApplicationUser> Get(string userId)
        {
            try
            {
                return await _userRepository.Get(userId);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        public void Register(ApplicationUser user, string password)
        {

            try
            {
                _userRepository.Register(user, password);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
