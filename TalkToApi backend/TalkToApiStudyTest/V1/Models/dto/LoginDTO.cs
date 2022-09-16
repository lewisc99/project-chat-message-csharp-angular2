using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TalkToApiStudyTest.V1.Models.dto
{
    public class LoginDTO
    {

        [UIHint("email")]
        public string Name { get; set; }

        [UIHint("email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [UIHint("Senha")]
        public string Password { get; set; }


    }
}
