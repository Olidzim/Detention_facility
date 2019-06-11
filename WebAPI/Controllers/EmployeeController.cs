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

        [Authorize(Roles ="Admin,Editor")] 
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

        [Authorize(Roles ="Admin,Editor")] 
        [HttpPut]
        public IHttpActionResult UpdateEmployee(int id, [FromBody] Employee Employee)
        {
            if (_employeeService.GetEmployeeByID(id) == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _employeeService.UpdateEmployee(id, Employee);
                return Ok(Employee);
            }
            return BadRequest(ModelState);
        }

        [Authorize(Roles ="Admin,Editor")] 
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

        [Authorize(Roles ="Admin,Editor")] 
        [HttpDelete]
        public IHttpActionResult DeleteEmployee(int id)
        {
            if (_employeeService.GetEmployeeByID(id) == null)
            {
                return NotFound();
            }
            _employeeService.DeleteEmployee(id);
            return Ok();
        }

        [Authorize(Roles ="Admin,Editor")]       
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
