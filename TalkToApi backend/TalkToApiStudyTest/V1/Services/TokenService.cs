using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TalkToApiStudyTest.Database;
using TalkToApiStudyTest.V1.Models;
using TalkToApiStudyTest.V1.Repositories.Contracts;


#pragma warning disable 1591
namespace TalkToApiStudyTest.V1.Services
{
    public class TokenService : ITokenService
    {
        private readonly TalkToContext _banco;
        public TokenService(TalkToContext banco)
        {
            _banco = banco;
        }

        public async Task<Token> Get(string refreshToken)
        {


            return await _banco.tokens.FirstOrDefaultAsync(a => a.RefreshToken == refreshToken
             && a.Utilized == false);
         
        }

        public void Register(Token token)
        {
            _banco.tokens.Add(token);
            _banco.SaveChanges();
        }

        public void Update(Token token)
        {

            _banco.tokens.Update(token);
            _banco.SaveChanges();
        }
    }
}
