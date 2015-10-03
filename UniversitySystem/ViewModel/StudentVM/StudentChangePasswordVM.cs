using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversitySystem.ViewModel.StudentVM
{
    public class StudentChangePasswordVM
    {
        public int StudentID { get; set; }
         [Required(ErrorMessage = "Required")]
        public string OldPassword { get; set; }
         [Required(ErrorMessage = "Required")]
        public string NewPassword { get; set; }
         [Required(ErrorMessage = "Required")]
        public string NewPasswordRepeat { get; set; }
    }
}