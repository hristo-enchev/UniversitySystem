using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversitySystem.Entities;

namespace UniversitySystem.ViewModel.StudentVM
{
    public class StudentListStudentsVM
    {
        public IList<Student> Students { get; set; }
    }
}