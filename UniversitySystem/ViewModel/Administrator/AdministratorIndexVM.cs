using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversitySystem.Entities;
namespace UniversitySystem.ViewModel.Administrator
{
    public class AdministratorIndexVM
    {
        public int CountTeachers { get; set; }
        public int CountStudents{ get; set; }
        public int CountAdministrators { get; set; }
    }
}