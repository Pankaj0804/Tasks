using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5;
using WebApplication5.Services;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> logger;

        // inject ContactsAPIDbContext into the controller
        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> _logger)
        {
            _employeeService = employeeService;
            logger = _logger;
        }

        // get method to get all the contacts
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            logger.LogInformation("Executing {Action}", nameof(GetAllEmployees));
            var res = await _employeeService.GetAllEmployees();
            if (res != null)
            {
                logger.LogInformation("Retrieve all employees");
                return Ok(res);
            }

            logger.LogError("No employees available");
            return BadRequest("Error while retrieving data");
        }

        // get method to get a specific employee using 'id' as a parameter in route
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            logger.LogInformation("Executing {Action} with parameters {id}", nameof(GetEmployee), id);
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee != null)
            {
                logger.LogInformation("Employee found : {name}", employee.EmployeeName);
                return Ok(employee);
            }

            logger.LogError("Employee not found");
            return NotFound();
        }

        // add a new employee to the database
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            logger.LogInformation("Executing {Action}", nameof(AddEmployee));
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid employee data");
            }

            var res = await _employeeService.AddEmployee(employee);
            if (res != 0)
            {
                logger.LogInformation("Employee added: {name}", employee.EmployeeName);
                return Ok(res);
            }

            logger.LogError("Error while inserting employee");
            return BadRequest("Error while inserting employee");
        }

        // update a particular contact in the database
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, [FromBody] Employee employee)
        {
            logger.LogInformation("Executing {Action}", nameof(UpdateEmployee));

            if (employee == null)
            {
                logger.LogInformation("Null input passed");
                return BadRequest("Null input passed");
            }
            if (!ModelState.IsValid)
            {
                logger.LogError("Object input format incorrect");
                return BadRequest("Object input format incorrect");
            }

            int res = await _employeeService.UpdateEmployee(id, employee);
            if (res != 0)
            {
                logger.LogInformation("Employee updated: {name}, updateemployee.name");
                return Ok();
            }

            logger.LogError("Contact not found");
            return NotFound("Employee not found");
        }

        // delete a contact from the database
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            logger.LogInformation("Executing {Action}", nameof(DeleteEmployee));
            int res = await _employeeService.DeleteEmployee(id);

            if (res == 1)
            {
                logger.LogInformation("Employee deleted");
                return Ok();
            }

            logger.LogError("Employee not found");
            return NotFound();
        }
    }
}
