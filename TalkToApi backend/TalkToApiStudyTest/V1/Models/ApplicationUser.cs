﻿using Microsoft.AspNetCore.Identity;

namespace TalkToApiStudyTest.V1.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
        public string Slogan { get; set; }

    }
}
