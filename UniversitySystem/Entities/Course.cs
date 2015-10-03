using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversitySystem.Entities
{
    public class Course : BaseEntityWithID
    {
        public string Name { get; set; }
        public string FKNumber { get; set; }

        public virtual ICollection<Student> Student { get; set; }
       // public virtual ICollection<Teacher> Teacher { get; set; }
        public virtual ICollection<CourseSubject> CourseSubject { get; set; }
    }
}