using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalkToApiStudyTest.Database;
using TalkToApiStudyTest.V1.Models;
using TalkToApiStudyTest.V1.Repositories.Contracts;
using TalkToApiStudyTest.V1.Services.Contracts;



#pragma warning disable 1591
namespace TalkToApiStudyTest.V1.Services
{

  
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _repository;
        public MessageService(IMessageRepository repository)
        {
            _repository = repository;
        }

        
       public async Task<Message> Get(int id)
        {

          return await _repository.Get(id);
          
        }

        public async Task<List<Message>> GetMessages(string userOne, string userTwo)
        {

            return await   _repository.GetMessages(userOne, userTwo);

        }

        public void Register(Message message)
        {

            _repository.Register(message);
        }

        public void Update(Message message)
        {
            _repository.Update(message);
        }
    }
}
