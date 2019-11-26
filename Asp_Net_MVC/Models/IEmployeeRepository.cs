using System.Collections.Generic;

namespace Asp_Net_MVC.Models
{
    public interface IEmployeeRepository
    {
        Employee GetEmpoyee(int Id);
        IEnumerable<Employee> Get();
        Employee Add(Employee employee);
    }
}
