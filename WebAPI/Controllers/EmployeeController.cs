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

        [Authorize(Roles = "Admin,Editor")]
        [HttpPost]
        public IHttpActionResult InsertEmployee([FromBody] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeService.InsertEmployee(employee);
                return Ok(employee);
            }
            else
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, CustomLogging.ModelStatusConverter(ModelState));
                return BadRequest(ModelState);
            }
        }

        [Authorize(Roles = "Admin,Editor")]
        [HttpPut]
        public IHttpActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeService.UpdateEmployee(id, employee);
                return Ok(employee);
            }
            if (_employeeService.GetEmployeeByID(id) == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Нет такого сотрудника");
                return NotFound();
            }
            CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, CustomLogging.ModelStatusConverter(ModelState));
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin,Editor")]
        [HttpGet]
        public IHttpActionResult GetEmployee(int id)
        {
            var Employee = _employeeService.GetEmployeeByID(id);
            if (Employee == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Нет такого сотрудника");
                return NotFound();
            }
            return Ok(Employee);
        }

        [Authorize(Roles = "Admin,Editor")]
        [HttpDelete]
        public IHttpActionResult DeleteEmployee(int id)
        {
            var employeeForDelete = _employeeService.GetEmployeeByID(id);
            if (employeeForDelete == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Нет такого сотрудника");
                return NotFound();
            }
            _employeeService.DeleteEmployee(id);
            return Ok(employeeForDelete);
        }

        [Authorize(Roles = "Admin,Editor")]
        [HttpGet]
        public IHttpActionResult GetEmployees()
        {
            var employeesList = _employeeService.GetEmployees();
            if (employeesList == null)
            {
                return NotFound();
            }
            return Ok(employeesList);
        }

       // [Route("Api/employee/name=/{term}")]
        [HttpGet]
        public IHttpActionResult GetEmploy(string term)
        {
            var employeesList = _employeeService.Employees(term);
            if (employeesList == null)
            {
                return NotFound();
            }
            return Ok(employeesList);
        }
    }
}
