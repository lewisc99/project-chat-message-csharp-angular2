using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TalkToApiStudyTest.Database;
using TalkToApiStudyTest.V1.Models;
using TalkToApiStudyTest.V1.Repositories.Contracts;
using TalkToApiStudyTest.V1.Services.Contracts;


#pragma warning disable 1591
namespace TalkToApiStudyTest.V1.Services
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepository _repository;
        public TokenService(ITokenRepository repository)
        {
            _repository = repository;
        }

        public async Task<Token> Get(string refreshToken)
        {
            try
            {
                return await _repository.Get(refreshToken);

            }
            catch(Exception e)
            {
                Debug.WriteLine(e.GetType());  // Displays the type of exception
                throw new Exception(e.Message);
            }



        }

        public void   Register(Token token)
        {

            try
            {
                _repository.Register(token);

            }
            catch(Exception e)
            {
                Debug.WriteLine(e.GetType());  // Displays the type of exception
                throw new Exception(e.Message);
            }
        }

        public void  Update(Token token)
        {
            try
            {
                _repository.Update(token);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.GetType());  // Displays the type of exception
                throw new Exception(e.Message);
            }

        }
    }
}
