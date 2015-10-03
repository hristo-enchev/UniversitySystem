using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversitySystem.ViewModel.Subjects
{
    public class SubjectCreateSubjectVM
    {
       // public UniversitySystem.Entities.Subject Subject { get; set; }
        [Required(ErrorMessage = "Please enter subject name")]
        public string SubjectName { get; set; }
    }
}