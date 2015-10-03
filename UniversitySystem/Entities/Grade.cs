using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversitySystem.Entities
{
    public class Grade : BaseEntityWithID
    {
        public int SubjectID { get; set; }

        public int StudentID { get; set; }

        public int GradeValue { get; set; }

        public virtual Subject Subject { get; set; }

    }
}