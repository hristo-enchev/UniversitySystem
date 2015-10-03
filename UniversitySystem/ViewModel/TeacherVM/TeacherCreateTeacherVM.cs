using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversitySystem.ViewModel.TeacherVM
{
    public class TeacherCreateTeacherVM
    {
        [Required(ErrorMessage = "Fist name required")]
        public string FirstName { get; set; }

         [Required(ErrorMessage = "Last name required")]
        public string LastName { get; set; }  
        public IEnumerable<SelectListItem> TitleList{ get; set; }

        [Required(ErrorMessage = "required")]
        public string selectedValueID { get; set; }
    }
}