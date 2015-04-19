using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EntityFramework.Triggers;
using MVCUniversity.DAL;

namespace MVCUniversity.Models
{
    public class Professor : ITriggerable
    {
        public int ID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public Professor()
        {
            this.Triggers().Deleting += entry =>
            {
                using (UniversityContext db = new UniversityContext())
                {
                    db.Courses
                        .Where(e => e.ProfessorID == entry.Entity.ID)
                        .ToList()
                        .ForEach(e =>
                        {
                            db.Courses.Remove(e);
                        });

                    db.SaveChanges();
                }
            };
        }
    }
}