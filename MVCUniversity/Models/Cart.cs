using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCUniversity.Models
{
    public class Cart
    {
        [Key, Column(Order = 0)]
        public int StudentID { get; set; }

        [Key, Column(Order = 1)]
        public int CourseID { get; set; }

        public virtual Student Student { get; set; }

        public virtual Course Course { get; set; }
    }
}