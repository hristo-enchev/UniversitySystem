using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversitySystem.ViewModel.Grades
{
    public class GradeAddGradeVM
    {
        //public UniversitySystem.Entities.Student Student { get; set; }

        //public UniversitySystem.Entities.Subject Subject { get; set; }

        //public UniversitySystem.Entities.Grade Grade { get; set; }

        public int StudentID { get; set; }
        public int SubjectID { get; set; }
        [Required]
        public int GradeValue { get; set; }
    }
}