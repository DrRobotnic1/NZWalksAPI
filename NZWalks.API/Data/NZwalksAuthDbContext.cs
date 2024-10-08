using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZwalksAuthDbContext:IdentityDbContext
    {

        public NZwalksAuthDbContext(DbContextOptions<NZwalksAuthDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "a03beb34-30ac-476f-b631-3ebb21e52497";
            var writterId = "080371d1-c2ea-4ab2-94c8-a6e7c31e518c";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                   Id = readerRoleId,
                   ConcurrencyStamp = readerRoleId,
                   Name = "Reader",
                   NormalizedName = "Reader".ToUpper()

                },
                new IdentityRole
                {
                    Id = writterId,
                    ConcurrencyStamp = writterId,
                    Name = "Writer",
                    NormalizedName = "Writter".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
