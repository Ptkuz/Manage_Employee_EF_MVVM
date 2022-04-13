using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployeeDB.Model.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }

        public ApplicationContext() 
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlServer(@"Server=GURREX\SQLEXPRESS;Database=ManageEmployeeDB;Trusted_Connection=True;TrustServerCertificate=true;");
        
        }

    }
}
