using DotNetCore_Concepts.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DotNetCore_Concepts.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
        }
        //public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}
