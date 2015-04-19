using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MVCUniversity.Models;

namespace MVCUniversity.DAL
{
    public class UniversityInitializer : DropCreateDatabaseAlways<UniversityContext> //System.Data.Entity.DropCreateDatabaseIfModelChanges<UniversityContext>
    {
        protected override void Seed(UniversityContext context)
        {
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Student', RESEED, 1000000);");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Professor', RESEED, 1000000);");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Course', RESEED, 1000);");

            var students = new List<Student>
            {
                new Student{FirstName="Alex", LastName="Shelton", Major=Major.Business}
            };

            students.ForEach(a => context.Students.Add(a));
            context.SaveChanges();

            var professors = new List<Professor>
            {
                new Professor{FirstName="Wesley",LastName="Reisz"}
            };

            professors.ForEach(a => context.Professors.Add(a));
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course{ProfessorID=1000000, Title="Web Development", Credits=3}
            };

            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment{StudentID=1000000,CourseID=1000}
            };

            enrollments.ForEach(a => context.Enrollments.Add(a));
            context.SaveChanges();
        }
    }
}