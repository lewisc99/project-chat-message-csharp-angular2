using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalkToApiStudyTest.Database;
using TalkToApiStudyTest.V1.Models;
using TalkToApiStudyTest.V1.Repositories.Contracts;



#pragma warning disable 1591
namespace TalkToApiStudyTest.V1.Repositories
{

  
    public class MessageRepository : IMessageRepository
    {
        private readonly TalkToContext _database;
        public MessageRepository(TalkToContext database)
        {
            _database = database;
        }

       public async Task<Message> Get(int id)
        {
            Message message =  await Task.FromResult( _database.Mensagem.FirstOrDefault(message => message.Id == id));
            return message;
        }

        public async Task<List<Message>> GetMessages(string userOne, string userTwo)
        {

            return  await  Task.FromResult( _database.Mensagem.Where(a => (a.FromId == userOne || a.FromId == userTwo) &&
            (a.ToId == userOne || a.ToId == userTwo)).ToList());

        }

        public void Register(Message message)
        {

            message.Created = DateTime.Now;
            _database.Mensagem.Add(message);
            _database.SaveChanges();
        }

        public void Update(Message message)
        {
            _database.Mensagem.Update(message);
            _database.SaveChanges();
        }
    }
}
