using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversitySystem.Entities
{
    public class CourseSubject : BaseEntityWithID
    {
        public int CourseID { get; set; }
        public int SubjectID { get; set; }

        public virtual Course Course { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual IList<Teacher> Teachers { get; set; }
    }
}