using Microsoft.EntityFrameworkCore;
using PracticeTesting.Models;

namespace PracticeTesting.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> dbContextOptions):base(dbContextOptions)
        {
                
        }

        public DbSet<Employee> employees { get; set; }
    }
}
