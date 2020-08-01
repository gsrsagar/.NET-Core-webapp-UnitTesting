using SocialMediaLinkedIn.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTestProject
{
    class Testing_Fake_Data
    {
        private readonly List<Employee> _employee;
        
        public Testing_Fake_Data()
        {
            _employee = new List<Employee>()
            {
                  new Employee(){Id=1,Name="Sagar",Email="sagar.guvvala@gmail.com",
                      Department=Dept.Payroll,Photopath="sdsgabasgsdbasbasdbadbsdb"}
            };
        }
        
       


    }
}
