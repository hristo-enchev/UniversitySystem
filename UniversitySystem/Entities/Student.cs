using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversitySystem.Entities
{
  
    public class Student : User
    {
        public string FacultiNumber { get; set; }
        public int CourseID { get; set; }
        public bool Active { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
    }
}