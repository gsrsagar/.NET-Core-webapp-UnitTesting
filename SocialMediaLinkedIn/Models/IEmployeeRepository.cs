using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaLinkedIn.Models
{

    public class IEmployeeRepository : EmployeeRepository
    {
        private List<Employee> _employeelist;
        public IEmployeeRepository()
        {
            _employeelist = new List<Employee>()
            {
                new Employee() { Id = 1, Name = "Sagar",Email="sagar.guvvala@gmail.com" ,Department =Dept.Payroll,Photopath="sdfsvsdf-asfafsaf" },
           

        };
        }

        public IEnumerable<ActiveUsers> AddActiveUser(string email)
        {
            throw new NotImplementedException();
        }

        public Employee DeleteEmployee(int id)
        {
            Employee emp= _employeelist.FirstOrDefault(e => e.Id == id);
            if(emp!=null)
            {
                _employeelist.Remove(emp);
            }

            return emp;
        }

        public Employee EmpSave(Employee emp)
        {
             emp.Id=_employeelist.Max(e => e.Id ) +1;
            _employeelist.Add(emp);
            return emp;
        }

        public Employee getById(int Id)
        {
            return _employeelist.FirstOrDefault(e => e.Id == Id);
        }

        public Employee getEmpById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _employeelist;
        }

        public void RemoveActiveUsers(string email)
        {
            throw new NotImplementedException();
        }

        public Employee UpdateEmployee(Employee emp)
        {
            Employee employee = _employeelist.FirstOrDefault(e => e.Id == emp.Id);
            if (emp != null)
            {
                employee.Name = emp.Name;
                employee.Department = emp.Department;
                employee.Email = emp.Email;
            }

            return employee;
        }
    }
}

