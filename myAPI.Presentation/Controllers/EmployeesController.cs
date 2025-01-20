using Microsoft.AspNetCore.Mvc;
using myAPI.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myAPI.Presentation.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public EmployeesController(IServiceManager service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult GetEmployees(Guid companyId)
        {
            var employees = _service.EmployeeService.GetEmployees(companyId,false);
            return Ok(employees);
        }
        [HttpGet("{employeeId:guid}")]

        public IActionResult GetEmployee(Guid employeeId)
        {
            var employee = _service.EmployeeService.GetEmployee(employeeId,false);
            return Ok(employee);
        }
    }
}
