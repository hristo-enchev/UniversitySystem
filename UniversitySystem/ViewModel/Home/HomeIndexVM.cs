using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversitySystem.ViewModel.Home
{
    public class HomeIndexVM
    {
        [Required(ErrorMessage = "no username")]
        public string username { get; set; }
        [Required(ErrorMessage = "no password")]
        public string password { get; set; }

        public UsersEnum usersEnum { get; set; }
        public enum UsersEnum
        {
            Admin,
            Teacher,
            Student
        }
    }
}