using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversitySystem.ViewModel.CourseVM
{
    public class CourseAddSubjectToCourseVM
    {
        public List<SelectListItem> List { get; set; }
        public int CourseID { get; set; }

        public string Error { get; set; }
    }
}