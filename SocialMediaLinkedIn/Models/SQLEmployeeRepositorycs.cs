using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaLinkedIn.Models
{
    public class SQLEmployeeRepositorycs : EmployeeRepository
    {
        //To connect to sql server for all the operartions we need to use Dbcontext class
        private readonly AppDbContext context;
        public SQLEmployeeRepositorycs(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<ActiveUsers> AddActiveUser(string email)
        {
            ActiveUsers activeusers;
            activeusers = new ActiveUsers()
            {   Id=email,
                Email = email
            };
            context.ActiveUsers.Add(activeusers);
            context.SaveChanges();
            return context.ActiveUsers;
        }

        public void RemoveActiveUsers(string email)
        {
            ActiveUsers activeusers;
            activeusers = new ActiveUsers()
            {
                Id = email,
                Email = email
            };
            context.Remove(activeusers);
            context.SaveChanges();    
        }
        public Employee DeleteEmployee(int id)
        {
            Employee emp=context.Employees.Find(id);
            if (emp != null)
            {
                context.Employees.Remove(emp);
                context.SaveChanges();
            }
            return emp;
        }

        public Employee EmpSave(Employee emp)
        {
            context.Employees.Add(emp);   //TO perform action on the EMployee table 
            context.SaveChanges(); //To reflect changes in the sql server to add this SaveChanges() method.
            return emp;
        }

        public Employee getById(int id)
        {
            Employee employee = context.Employees.Find(id);
            return employee;
        }

        public Employee getEmpById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return context.Employees;
        }

        public Employee UpdateEmployee(Employee emp)
        {
            var employee=context.Employees.Attach(emp);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return emp;
        }

    }
}
