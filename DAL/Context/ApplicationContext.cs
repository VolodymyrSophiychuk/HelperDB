using Microsoft.EntityFrameworkCore;

using DAL.Models;

namespace DAL.Context
{
    public class ApplicationContext : DbContext
    { 
        public DbSet<Contact>? Contacts { get; set; }
        public DbSet<Account>? Accounts { get; set; }
        public DbSet<Incident>? Incidents { get; set; }

        public ApplicationContext() { }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    }
}
