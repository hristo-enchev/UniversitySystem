using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversitySystem.Entities
{
    public class Subject : BaseEntityWithID
    {
        public string Name { get; set; }

        public virtual ICollection<CourseSubject> CourseSubject { get; set; }
        //public virtual ICollection<Teacher> Teacher { get; set; }
    }
}