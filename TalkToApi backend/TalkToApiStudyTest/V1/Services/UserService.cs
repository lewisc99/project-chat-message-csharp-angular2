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
           return await _userRepository.Get(email, password);
        }

        public async Task<ApplicationUser> Get(string userId)
        {
           return await _userRepository.Get(userId);

        }

        public void Register(ApplicationUser user, string password)
        {

            _userRepository.Register(user, password);
        }
    }
}
