using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversitySystem.ViewModel.CourseVM
{
    public class CourseEditCourseVM
    {
        //public UniversitySystem.Entities.Course Course { get; set; }
         [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }

        public int CourseID { get; set; }
    }
}