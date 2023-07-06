using Microsoft.EntityFrameworkCore;
using WebApplication5;
namespace WebApplication5.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions options): base(options)
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
    }
    
}
