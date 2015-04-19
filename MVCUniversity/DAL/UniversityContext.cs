using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MVCUniversity.Models;
using EntityFramework.Triggers;
using System.Threading.Tasks;
using System.Threading;

namespace MVCUniversity.DAL
{
    public class UniversityContext : DbContext
    {

        public UniversityContext() : base("UniversityContext")
        {
        }

        public override Int32 SaveChanges()
        {
            return this.SaveChangesWithTriggers(base.SaveChanges);
        }
        public override Task<Int32> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return this.SaveChangesWithTriggersAsync(base.SaveChangesAsync, cancellationToken);
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Professor> Professors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }

        public System.Data.Entity.DbSet<MVCUniversity.Models.Cart> Carts { get; set; }        
    }
}