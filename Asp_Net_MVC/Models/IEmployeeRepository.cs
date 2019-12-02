using System.Collections.Generic;

namespace Asp_Net_MVC.Models
{
    public interface IEmployeeRepository
    {
        Employee GetEmpoyee(int id);
        IEnumerable<Employee> GetAllEmployee();
        Employee Add(Employee employee);
        Employee Delete(int id);
        Employee Update(Employee employee);
    }
}
