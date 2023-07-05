using Practical13_Test2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Practical13_Test2.DAL
{
    public class DBContext : DbContext
    {
        public DBContext() : base("DBContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeModel>()
                .HasRequired<DesignationModel>(e =>  e.Designation)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DesignationId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<DesignationModel> Designation { get; set; }
    }
}