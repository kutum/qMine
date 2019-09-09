using Microsoft.AspNet.Identity.EntityFramework;
using qMIne.Models;
using System.Data.Entity;

namespace qMIne.Context
{
    [DbConfigurationType(typeof(MySql.Data.EntityFramework.MySqlEFConfiguration))]
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<ServerCredentials> ServerCredentials { get; set; }
    }
}