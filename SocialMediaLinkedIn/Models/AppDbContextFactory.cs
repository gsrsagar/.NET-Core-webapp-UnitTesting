using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaLinkedIn.Models
{
    public class AppDbContextFactory:IDesignTimeDbContextFactory<AppDbContext>
    { 
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;DataBase=MyFirstDataBase;Trusted_Connection=true");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
