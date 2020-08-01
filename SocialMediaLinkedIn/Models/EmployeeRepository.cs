using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaLinkedIn.Models
{
    public interface EmployeeRepository
    {
         public Employee getEmpById(int id);
         public Employee EmpSave(Employee emp);
         public Employee getById(int id);
         public IEnumerable<Employee> GetEmployees();
         public Employee UpdateEmployee(Employee emp);
         public Employee DeleteEmployee(int id);
         public  void RemoveActiveUsers(string email);
         public IEnumerable<ActiveUsers> AddActiveUser(string email);
    }

}
