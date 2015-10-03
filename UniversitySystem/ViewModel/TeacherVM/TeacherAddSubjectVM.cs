using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversitySystem.ViewModel.TeacherVM
{
    public class TeacherAddSubjectVM
    {

        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TeacherID { get; set; }

        public IEnumerable<SelectListItem> CoursesList { get; set; }
        //public IEnumerable<Course> Courses { get; set; }
        public string selectedValueID { get; set; }
        public IEnumerable<SelectListItem> SubjectList { get; set; }
    }
}