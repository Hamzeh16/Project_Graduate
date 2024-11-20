using Graduates_Model.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Graduates_Data.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Applicant User Table
        /// </summary>
        public DbSet<ApplicantUser> ApplicantsUser { get; set; }

        /// <summary>
        /// Traning Table
        /// </summary>
        public DbSet<Traning> Traning { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder);
        }

        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
                (
            new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
            new IdentityRole() { Name = "Company", ConcurrencyStamp = "2", NormalizedName = "Company" },
            new IdentityRole() { Name = "Student", ConcurrencyStamp = "3", NormalizedName = "Student" }
                );
        }
    }
}
