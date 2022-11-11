using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalkToApiStudyTest.V1.Models
{
    public class Token
    {

        [Key]
        public int Id { get; set; }
        public string RefreshToken { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public bool Utilized { get; set; }
        public DateTime ExpirationToken { get; set; }
        public DateTime ExpirationRefreshToken { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public Token(string refreshToken, ApplicationUser user, bool utlized, DateTime expirationToken,
            DateTime expirationRefreshToken, DateTime created)
        {
            RefreshToken = refreshToken;
            User = user;
            Utilized = utlized;
            ExpirationToken = expirationToken;
            ExpirationRefreshToken = expirationRefreshToken;
            Created = created;
        }
        public Token() { }
    }
}
