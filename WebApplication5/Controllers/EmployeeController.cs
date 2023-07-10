using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebApplication5.Services;

namespace WebApplication5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;


        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            _logger.LogInformation("Executing {Action}", nameof(GetAllEmployees));
            var res = await _employeeService.GetAllEmployees();
            if (res != null)
            {
                _logger.LogInformation("Retrieve all employees");
                return Ok(res);
            }

            _logger.LogError("No employees available");
            return BadRequest("Error while retrieving data");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            _logger.LogInformation("Executing {Action} with parameters {id}", nameof(GetEmployee), id);
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee != null)
            {
                _logger.LogInformation("Employee found: {name}", employee.EmployeeName);
                return Ok(employee);
            }

            _logger.LogError("Employee not found");
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            _logger.LogInformation("Executing {Action}", nameof(AddEmployee));
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid employee data");
            }

            var res = await _employeeService.AddEmployee(employee);
            if (res != 0)
            {
                _logger.LogInformation("Employee added: {name}", employee.EmployeeName);
                return Ok(res);
            }

            _logger.LogError("Error while inserting employee");
            return BadRequest("Error while inserting employee");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, [FromBody] Employee employee)
        {
            _logger.LogInformation("Executing {Action}", nameof(UpdateEmployee));

            if (employee == null)
            {
                _logger.LogInformation("Null input passed");
                return BadRequest("Null input passed");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Object input format incorrect");
                return BadRequest("Object input format incorrect");
            }

            int res = await _employeeService.UpdateEmployee(id, employee);
            if (res != 0)
            {
                _logger.LogInformation("Employee updated: {name}", employee.EmployeeName);
                return Ok();
            }

            _logger.LogError("Contact not found");
            return NotFound("Employee not found");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            _logger.LogInformation("Executing {Action}", nameof(DeleteEmployee));
            int res = await _employeeService.DeleteEmployee(id);

            if (res == 1)
            {
                _logger.LogInformation("Employee deleted");
                return Ok();
            }

            _logger.LogError("Employee not found");
            return NotFound();
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class MyController : ControllerBase
    {
        private readonly ILogger<MyController> _logger;

        public MyController(ILogger<MyController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("GET request received.");
            
            return Ok();
        }
    }
}
