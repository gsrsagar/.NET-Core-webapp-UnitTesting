using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialMediaLinkedIn.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SocialMediaLinkedIn.Models
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ActiveUsers> ActiveUsers { get; set; } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
    
            
            modelBuilder.Entity<Employee>().HasData(  //This is to seed the data to the dataabse enigne by default at the time of the migration
                new Employee
                {
                    Id = 1,
                    Name = "Sagar Guvvala",
                    Department = Dept.Payroll,
                    Email = "sagar.guvvala@gmail.com",
                    Photopath="sacsafvsdfsa-SSSS"
                }

                );
        }
    }
}
    