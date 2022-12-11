using System.Collections.Generic;
using System.Threading.Tasks;
using TalkToApiStudyTest.V1.Models;

namespace TalkToApiStudyTest.V1.Services.Contracts
{
   public interface IMessageService
    {
        Task<List<Message>> GetMessages(string userOne, string userTwo);
        void Register(Message message);
         Task<Message>  Get(int id);
        void Update(Message message);
    }
}
