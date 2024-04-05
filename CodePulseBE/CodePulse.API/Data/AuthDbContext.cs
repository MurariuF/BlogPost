using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;

namespace CodePulse.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "8882dbd6-ede0-467d-a7ea-b16d7ff5b576";
            var writerRoleId = "4d336bae-8595-438b-b3c9-36e04d23e746";

            //Create reader and writer role
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                }
            };

            //Seed the roles
            builder.Entity<IdentityRole>().HasData(roles);

            //Create an admin user
            var adminUserId = "85cb7f5a - 8cd9 - 4ce1 - 9db6 - 097449954aaf";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "Salosu@gmail.com",
                Email = "Salosu@gmail.com",
                NormalizedEmail = "Salosu@gmail.com".ToUpper(),
                NormalizedUserName = "Salosu@gmail.com".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "salosu@123");

            builder.Entity<IdentityUser>().HasData(admin);

            //Give roles to admin

            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = writerRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);

        }
    }
}
