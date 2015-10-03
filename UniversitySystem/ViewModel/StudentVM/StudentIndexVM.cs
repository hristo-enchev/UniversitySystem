using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversitySystem.ViewModel.StudentVM
{
    public class StudentIndexVM
    {

        public string FacultiNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string CourseName { get; set; }

        public List<UniversitySystem.Entities.Grade> Grades {get; set;}

    }
}