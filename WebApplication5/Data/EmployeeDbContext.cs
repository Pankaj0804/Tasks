using Microsoft.EntityFrameworkCore;
using WebApplication5;

namespace WebApplication5.Data
{
    public class EmployeeDbContext : DbContext
        // EmployeeDbcontext inherits from class DbContext
    {
        public EmployeeDbContext(DbContextOptions options): base(options) //constructor
        {
            
        }
        public DbSet<Employee> Employees { get; set; } //property Employees
    }
    
}
