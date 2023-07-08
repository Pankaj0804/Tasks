namespace WebApplication5.Services
{
    public interface IEmployeeService
    {

        Task<Employee> GetEmployeeById(Guid id);
        Task<int> AddEmployee(Employee employee);
        Task<int> UpdateEmployee(Guid id, Employee employee);
        Task<int> DeleteEmployee(Guid id);
        Task<List<Employee>> GetAllEmployees();

    }
}
