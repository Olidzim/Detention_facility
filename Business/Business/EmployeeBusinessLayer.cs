using Detention_facility.Models;
using System.Collections.Generic;
using Detention_facility.Data;

namespace Detention_facility.Business
{
    public class EmployeesBusinessLayer : IEmployeeBusinesslayer
    {
        private IEmployeeDataAccess _employeeDataProvider;

        public EmployeesBusinessLayer(IEmployeeDataAccess employeesDataProvider)
        {
            _employeeDataProvider = employeesDataProvider;
        }

        public void InsertEmployee(Employee employee)
        {
            _employeeDataProvider.InsertEmployee(employee);
        }

        public void UpdateEmployee(int id, Employee employee)
        {
            _employeeDataProvider.UpdateEmployee(id, employee);
        }

        public void DeleteEmployee(int id)
        {
            _employeeDataProvider.DeleteEmployee(id);
        }

        public Employee GetEmployeeByID(int id)
        {
            return _employeeDataProvider.GetEmployeeByID(id);           
        }

        public List<Employee> GetEmployees()
        {
            return _employeeDataProvider.GetEmployees();
        }
    }
}