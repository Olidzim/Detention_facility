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
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, CustomLogging.ModelStatusConverter(ModelState));
                return BadRequest(ModelState);
            }
        }

        [Authorize(Roles ="Admin,Editor")] 
        [HttpPut]
        public IHttpActionResult UpdateEmployee(int id, [FromBody] Employee Employee)
        {
            if (_employeeService.GetEmployeeByID(id) == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Нет такого сотрудника");
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _employeeService.UpdateEmployee(id, Employee);
                return Ok(Employee);
            }
            CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, CustomLogging.ModelStatusConverter(ModelState));
            return BadRequest(ModelState);
        }

        [Authorize(Roles ="Admin,Editor")] 
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

        [Authorize(Roles ="Admin,Editor")] 
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
