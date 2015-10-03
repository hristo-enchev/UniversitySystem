using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversitySystem.ViewModel.TeacherVM
{
    public class TeacherIndexVM
    {
        public List<UniversitySystem.Entities.Course> Courses { get; set; }
        public List<UniversitySystem.Entities.Subject> Subjects { get; set; }
        public List<UniversitySystem.Entities.CourseSubject> CourseSubjects { get; set; }
        public IEnumerable<UniversitySystem.Entities.CourseSubject> CourseSubjects2 { get; set; }

    }
}