using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversitySystem.ViewModel.StudentVM
{
    public class StudentListStudentsBySubjectVM
    {
        public IEnumerable<UniversitySystem.Entities.Student> Students { get; set; }
        public string SubjectName { get; set; }
    }
}