using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UniversitySystem.Entities;

namespace UniversitySystem.ViewModel.Subjects
{
    public class SubjectEditSubjectVM
    {
        public int SubjectID { get; set; }

        [Required(ErrorMessage = "Please enter subject name")]
        public string SubjectName { get; set; }

    }
}