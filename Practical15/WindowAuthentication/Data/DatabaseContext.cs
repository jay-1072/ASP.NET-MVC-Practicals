using System.Data.Entity;
using WindowAuthentication.Models;

namespace WindowAuthentication.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("Name=Default") { }

        public DbSet<Student> Student { get; set; }
    }
}