using Microsoft.AspNet.Identity.EntityFramework;
using PenaltyCalculation.Base.Models;
using System.Data.Entity;

namespace PenaltyCalculation.Base
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Parameter> Parameter { get; set; }
        //public virtual DbSet<ApplicationUser> User { get; set; }
    }
}
