

using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TalkToApiStudyTest.Database;
using TalkToApiStudyTest.V1.Models;

namespace TalktoApiTest.TestProject.IntegrationTests.config
{
   public class DatabaseSeeder
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TalkToContext _context;

        public DatabaseSeeder(TalkToContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task Seed()
        {
            //// Add all the predefined profiles using the predefined password
            //foreach (var profile in PredefinedData.Profiles)
            //{
            //    await _userManager.CreateAsync(profile, PredefinedData.Password);
            //    // Set the AuthorId navigation property
            //    if (profile.Email == "author@test.com")
            //    {
            //        PredefinedData.Articles.ToList().ForEach(a => a.AuthorId = profile.Id);
            //    }
            //}

            //// Add all the predefined articles
            //_context.Article.AddRange(PredefinedData.Articles);
            //_context.SaveChanges();
        }


    }
}
