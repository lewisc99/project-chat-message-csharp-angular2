using System.ComponentModel.DataAnnotations;

namespace TalkToApiStudyTest.V1.Models.dto
{
    public class UserDTO: BaseDTO
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public string Slogan { get; set; }

    }
}
