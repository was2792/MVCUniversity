using MVCUniversity.DAL;
using EntityFramework.Triggers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCUniversity.Models
{

    public enum Major
    {
        Arts, Biology, Business, Engineering, English, Humanities, Mathematics, Music, Undecided
    }

    public class Student : ITriggerable
    {
        public int ID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Range(0.00, 4.00)]
        public decimal GPA { get; set; }

        public int Credits { get; set; }

        public Major Major { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public Student()
        {
            this.GPA = 4.00M;
            this.Credits = 0;
        }
    }
}