using Detention_facility.Models;
using System.Collections.Generic;
using Detention_facility.Data;

namespace Detention_facility.Business
{
    public class EmployeesBusinessLayer : IEmployeeBusinesslayer
    {
        private IEmployeeDataAccess _employeesDataProvider;

        public EmployeesBusinessLayer(IEmployeeDataAccess employeesDataProvider)
        {
            _employeesDataProvider = employeesDataProvider;
        }

        public void InsertEmployee(Employee employee)
        {
            _employeesDataProvider.InsertEmployee(employee);
        }

        public void UpdateEmployee(int id, Employee employee)
        {
            _employeesDataProvider.UpdateEmployee(id, employee);
        }

        public void DeleteEmployee(int id)
        {
            _employeesDataProvider.DeleteEmployee(id);
        }

        public Employee GetEmployeeByID(int id)
        {
            return _employeesDataProvider.GetEmployeeByID(id);
            // return _employeesDataProvider.GetEmployeeByID(id);
        }

        public List<Employee> GetEmployees()
        {
            return _employeesDataProvider.GetEmployees();
        }
    }
}