using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversitySystem.Entities
{
    
    public class Teacher : User 
    {
        public virtual ICollection<CourseSubject> CourseSubject { get; set; }
        //public virtual ICollection<Course> Course { get; set; }
        public virtual Title Title  { get; set; }

        public bool Active { get; set; }
    }
}