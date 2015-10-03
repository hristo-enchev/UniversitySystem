using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversitySystem.ViewModel.TeacherVM
{
    public class TeacherEditTeacherVM
    {
        [Required(ErrorMessage = "Name required")]
        public string FirstName { get; set; }
         [Required(ErrorMessage = "Name required")]
        public string LastName { get; set; }
        public bool Active { get; set; }
        public int TeacherID { get; set; }

        public IEnumerable<SelectListItem> TitleList { get; set; }
        public string selectedValueID { get; set; }
    }
}