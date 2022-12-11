using System.Threading.Tasks;
using TalkToApiStudyTest.V1.Models;

namespace TalkToApiStudyTest.V1.Services.Contracts
{
   public interface IUserService
    {
        void Register(ApplicationUser user, string password);
        Task<ApplicationUser> Get(string email, string password);
        Task<ApplicationUser> Get(string id);
    }
}
