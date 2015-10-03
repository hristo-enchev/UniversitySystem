using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UniversitySystem.Entities;

namespace UniversitySystem.ViewModel.CourseVM
{
    public class CourseCreateCourseVM
    {
       
        //public Course Course { get; set; }
         [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }
         [Required(ErrorMessage = "Field is required")]
         [Range(minimum: 100000, maximum: 999999, ErrorMessage = "Fk number must be 6 digits")]
        public string FKNumber { get; set; }
    }
}