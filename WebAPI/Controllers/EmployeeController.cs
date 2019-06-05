using Detention_facility.Business;
using Detention_facility.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace Detention_facility.Controllers
{
    public class EmployeeController : ApiController
    {
        private IEmployeeBusinesslayer _employeeService;

        public EmployeeController(IEmployeeBusinesslayer employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public IHttpActionResult InsertEmployee([FromBody] Employee Employee)
        {
            if (ModelState.IsValid)
            {
                _employeeService.InsertEmployee(Employee);
                return Ok(Employee);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateEmployee(int id, [FromBody] Employee Employee)
        {
            if (ModelState.IsValid)
            {
                _employeeService.UpdateEmployee(id, Employee);
                return Ok(Employee);
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public IHttpActionResult GetEmployee(int id)
        {
            var Employee = _employeeService.GetEmployeeByID(id);
            if (Employee == null)
            {
                return NotFound();
            }
            return Ok(Employee);
        }

        [HttpDelete]
        public void DeleteEmployee(int id)
        {
            _employeeService.DeleteEmployee(id);
        }

        [HttpGet]
        public IHttpActionResult GetEmployees()
        {
            List<Employee> Employees_list = _employeeService.GetEmployees();
            if (Employees_list == null)
            {
                return NotFound();
            }
            return Ok(Employees_list);
        }
    }
}
