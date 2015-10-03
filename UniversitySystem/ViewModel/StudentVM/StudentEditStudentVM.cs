using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversitySystem.ViewModel.StudentVM
{
    public class StudentEditStudentVM
    {
        // public UniversitySystem.Entities.Student Student { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string LastName { get; set; }

        public int StudentID { get; set; }

        public int CourseID { get; set; }

        public bool Active { get; set; }

        public IEnumerable<SelectListItem> CoursesList { get; set; }
        public string selectedValueID { get; set; }
    }
}