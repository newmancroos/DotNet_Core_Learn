using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp_Net_MVC.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employee;

        public MockEmployeeRepository()
        {
            _employee = new List<Employee>
            {
                new Employee{Id = 1, Name = "Mary", Department = Dept.HR, Email = "mary@test.com" },
                new Employee{Id = 2, Name = "John", Department = Dept.IT, Email = "john@test.com" },
                new Employee{Id = 3, Name = "Sam", Department = Dept.Payroll, Email = "sam@test.com" }
            };
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employee.ToList();
        }

        public Employee GetEmpoyee(int Id)
        {
            return _employee.FirstOrDefault(e => e.Id == Id);
        }
        public Employee Add(Employee employee)
        {
            employee.Id = _employee.Max(x => x.Id) + 1;
            _employee.Add(employee);
            return employee;
        }
    }
}
