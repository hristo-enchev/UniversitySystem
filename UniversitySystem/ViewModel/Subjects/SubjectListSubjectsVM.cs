using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversitySystem.ViewModel.Subjects
{
    public class SubjectListSubjectsVM
    {
        public List<UniversitySystem.Entities.Subject> Subjects { get; set; }

        public Dictionary<UniversitySystem.Entities.Subject, int> SubjectList { get; set; }

    }
}