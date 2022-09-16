using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalkToApiStudyTest.V1.Models.dto
{
    public class TokenDTO
    {


        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

        public string RefreshToken { get; set; }
        public DateTime ExpirationRefreshToken { get; set; }
    }
}
