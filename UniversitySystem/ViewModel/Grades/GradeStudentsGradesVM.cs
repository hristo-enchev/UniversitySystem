using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversitySystem.ViewModel.Grades
{
    public class GradeStudentsGradesVM
    {
       public int SubjectID { get; set; }
       public List<UniversitySystem.Entities.Student> Students { get; set; }
       public List<UniversitySystem.Entities.Grade> Grades { get; set; }
    }
}