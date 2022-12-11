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
            Message message =  await Task.FromResult( _repository.Mensagem.FirstOrDefault(message => message.Id == id));
            return message;
          
        }

        public async Task<List<Message>> GetMessages(string userOne, string userTwo)
        {

            return  await  Task.FromResult( _repository.Mensagem.Where(a => (a.FromId == userOne || a.FromId == userTwo) &&
            (a.ToId == userOne || a.ToId == userTwo)).ToList());

        }

        public void Register(Message message)
        {

            message.Created = DateTime.Now;
            _repository.Mensagem.Add(message);
            _repository.SaveChanges();
        }

        public void Update(Message message)
        {
            _repository.Mensagem.Update(message);
            _repository.SaveChanges();
        }
    }
}
