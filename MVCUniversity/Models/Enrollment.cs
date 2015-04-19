using MVCUniversity.DAL;
using EntityFramework.Triggers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCUniversity.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public enum Points
    {
        A = 4,
        B = 3,
        C = 2,
        D = 1,
        F = 0
    }

    public class Enrollment : ITriggerable
    {
        //public int ID { get; set; }

        [Key, Column(Order = 0)]
        public int CourseID { get; set; }

        [Key, Column(Order = 1)]
        public int StudentID { get; set; }

        [Column(TypeName="Date"), DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public Grade? Grade { get; set; }

        public virtual Course Course { get; set; }

        public virtual Student Student { get; set; }

        public Enrollment()
        {
            this.Date = DateTime.Now;
            
            this.Triggers().Inserted += entry =>
                {
                    if (entry.Entity.Grade.HasValue)
                        UpdateStudentRecord(entry.Entity.StudentID);
                };

            this.Triggers().Updated += entry =>
                {
                    if (entry.Entity.Grade.HasValue)
                        UpdateStudentRecord(entry.Entity.StudentID);
                };

            this.Triggers().Deleted += entry =>
                {
                    if (entry.Entity.Grade.HasValue)
                        UpdateStudentRecord(entry.Entity.StudentID);
                };
        }

        public static void UpdateStudentRecord(int studentID)
        {
            using (UniversityContext db = new UniversityContext())
            {
                var student = db.Students
                    .Find(studentID);
                
                var enrollments = student.Enrollments
                    .Where(e => e.Grade.HasValue)
                    .ToList();

                student.Credits = enrollments
                    .Sum(e => e.Course.Credits);

                if (student.Credits > 0)
                {
                    student.GPA = enrollments
                        .Select(e => new { GradePoints = (int)Enum.Parse(typeof(Points), e.Grade.Value.ToString()) * e.Course.Credits })
                        .Sum(p => (decimal)p.GradePoints) / student.Credits;
                }                   
                else
                    student.GPA = 4.00M;

                db.SaveChanges();
            }
        }
    }
}