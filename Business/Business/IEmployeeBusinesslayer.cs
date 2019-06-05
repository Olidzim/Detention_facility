using Detention_facility.Models;
using System.Collections.Generic;

namespace Detention_facility.Business
{
    public interface IEmployeeBusinesslayer
    {
        void InsertEmployee(Employee employee);
        void UpdateEmployee(int id, Employee employee);
        void DeleteEmployee(int id);
        Employee GetEmployeeByID(int id);
        List<Employee> GetEmployees();
    }
}
