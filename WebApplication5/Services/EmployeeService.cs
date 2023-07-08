using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApplication5.Data;
using WebApplication5.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication5.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeDbContext _dbContext;

        public EmployeeService(EmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _dbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeById(Guid id)
        {
            return await _dbContext.Employees.FindAsync(id);
        }

        public async Task<int> AddEmployee(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();
            return 1;
        }

        public async Task<int> UpdateEmployee(Guid id, Employee employee)
        {
            var existingEmployee = await _dbContext.Employees.FindAsync(id);
            if (existingEmployee != null)
            {
                existingEmployee.EmployeeName = employee.EmployeeName;
                existingEmployee.Email = employee.Email;
                existingEmployee.Phone = employee.Phone;

                await _dbContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public async Task<int> DeleteEmployee(Guid id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);
            if (employee != null)
            {
                _dbContext.Remove(employee);
                await _dbContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }
    }

}
