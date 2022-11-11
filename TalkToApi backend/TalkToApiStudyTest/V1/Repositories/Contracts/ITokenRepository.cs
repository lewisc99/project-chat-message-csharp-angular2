using System.Threading.Tasks;
using TalkToApiStudyTest.V1.Models;

namespace TalkToApiStudyTest.V1.Repositories.Contracts
{
    public interface ITokenRepository
    {
        void Register(Token token);
        Task<Token>  Get(string refreshToken);
        void Update(Token token);
    }
}
