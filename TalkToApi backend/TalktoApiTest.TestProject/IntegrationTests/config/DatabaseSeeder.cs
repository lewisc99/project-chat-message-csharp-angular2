

using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TalkToApiStudyTest.Database;
using TalkToApiStudyTest.V1.Models;

namespace TalktoApiTest.TestProject.IntegrationTests.config
{
   public static class DatabaseSeeder
    {


        public static async Task Seed(TalkToContext db)
        {
            ApplicationUser user = new ApplicationUser();
            user.Email = "lewiscarlos@gmail.com";
            user.Id = "e3169bc6-f433-4f97-9aed-93027cec38e0";
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword("vida376");
            user.UserName = "lewis";
            db.Users.Add(user);
            db.SaveChangesAsync();
        }

    }
}
