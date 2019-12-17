using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp_Net_MVC.Models
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
            new Employee { Id = 1, Name = "Newman Croos", Department = Dept.IT, Email = "newmancroos@gmail.com" },
            new Employee { Id = 2, Name = "John", Department = Dept.HR, Email = "john@gmail.com" }
            );
        }
    }
}
