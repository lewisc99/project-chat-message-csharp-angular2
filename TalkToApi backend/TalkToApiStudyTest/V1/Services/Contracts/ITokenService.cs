using System.Threading.Tasks;
using TalkToApiStudyTest.V1.Models;


#pragma warning disable 1591
namespace TalkToApiStudyTest.V1.Services.Contracts
{
    public interface ITokenService
    {
        void Register(Token token);
        Task<Token>  Get(string refreshToken);
        void Update(Token token);
    }
}
