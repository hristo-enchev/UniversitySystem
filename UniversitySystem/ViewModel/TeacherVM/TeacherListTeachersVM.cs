using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversitySystem.ViewModel.TeacherVM
{
    public class TeacherListTeachersVM
    {
        public IList<UniversitySystem.Entities.Teacher> Teachers { get; set; }
    }
}