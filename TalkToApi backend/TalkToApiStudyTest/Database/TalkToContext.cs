using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TalkToApiStudyTest.V1.Models;

namespace TalkToApiStudyTest.Database
{
    public class TalkToContext: IdentityDbContext<ApplicationUser>
    {


        public TalkToContext(DbContextOptions<TalkToContext> options):base(options)
        { }

        public DbSet<Message> Mensagem { get; set; }

        public DbSet<Token> tokens { get; set; }
    }
}
