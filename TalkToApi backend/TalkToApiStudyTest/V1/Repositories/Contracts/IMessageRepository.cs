using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalkToApiStudyTest.V1.Models;

namespace TalkToApiStudyTest.V1.Repositories.Contracts
{
   public interface IMessageRepository
    {
        Task<List<Message>> GetMessages(string userOne, string userTwo);

        void Register(Message message);
        Task<Message> Get(int id);

    
        void Update(Message message);
    }
}
