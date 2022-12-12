using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

          try
            {
                return await _repository.Get(id);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<List<Message>> GetMessages(string userOne, string userTwo)
        {

            try
            {
                return await _repository.GetMessages(userOne, userTwo);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public void Register(Message message)
        {

            try
            {
                  _repository.Register(message);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public void Update(Message message)
        {

            try
            {
                _repository.Update(message);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
