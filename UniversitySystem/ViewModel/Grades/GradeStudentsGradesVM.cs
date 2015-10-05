using System.Collections.Generic;

namespace UniversitySystem.ViewModel.Grades
{
    public class GradeStudentsGradesVM
    {
       public int SubjectID { get; set; }

       public List<Entities.Student> Students { get; set; }
    }
}