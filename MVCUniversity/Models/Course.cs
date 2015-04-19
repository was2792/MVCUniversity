using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EntityFramework.Triggers;
using MVCUniversity.DAL;

namespace MVCUniversity.Models
{
    public class Course : ITriggerable
    {
        public int ID { get; set; }

        [Required]
        public int ProfessorID { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Range(1, 4)]
        public int Credits { get; set; }

        public virtual Professor Professor { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public Course()
        {
            this.Triggers().Updated += entry =>
                {
                    using (UniversityContext db = new UniversityContext())
                    {
                        db.Enrollments
                            .Where(e => e.CourseID == entry.Entity.ID && e.Grade.HasValue)
                            .Select(e => e.Student)
                            .ToList()
                            .ForEach(s => Enrollment.UpdateStudentRecord(s.ID)); 
                    }
                };

            this.Triggers().Deleting += entry =>
            {
                using (UniversityContext db = new UniversityContext())
                {
                    db.Enrollments
                        .Where(e => e.CourseID == entry.Entity.ID)
                        .ToList()
                        .ForEach(e =>
                        {
                            db.Enrollments.Remove(e);
                        });

                    db.SaveChanges();
                }
            };
        }
    }
}