using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversitySystem.Entities;

namespace UniversitySystem.ViewModel.StudentVM
{
    public class StudentCreateStudentVM
    {

        [Required(ErrorMessage = "Name required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name required")]
        public string LastName { get; set; }
        

        public IEnumerable<SelectListItem> CoursesList { get; set; }
        public string selectedValueID { get; set; }
    }
}