using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace UniversitySystem.Models
{
    public class UniversitySystemContext : DbContext
    {
        public UniversitySystemContext()
            : base("UniversitySystemContext")
        {

        }
        public DbSet<UniversitySystem.Entities.Administrator> Administrators { get; set; }

        public DbSet<UniversitySystem.Entities.Course> Course { get; set; }

        public DbSet<UniversitySystem.Entities.Grade> Grade { get; set; }

        public DbSet<UniversitySystem.Entities.Student> Student { get; set; }

        public DbSet<UniversitySystem.Entities.Subject> Subject { get; set; }

        public DbSet<UniversitySystem.Entities.Teacher> Teacher { get; set; }

        public DbSet<UniversitySystem.Entities.Title> Title { get; set; }

       // public DbSet<UniversitySystem.Entities.User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}