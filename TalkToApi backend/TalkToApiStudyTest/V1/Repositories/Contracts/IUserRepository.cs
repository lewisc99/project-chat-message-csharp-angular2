using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalkToApiStudyTest.V1.Models;

namespace TalkToApiStudyTest.V1.Repositories.Contracts
{
   public interface IUserRepository
    {

        void Register(ApplicationUser user, string password);

        Task<ApplicationUser> Get(string email, string password);

        Task<ApplicationUser> Get(string id);

    }
}
