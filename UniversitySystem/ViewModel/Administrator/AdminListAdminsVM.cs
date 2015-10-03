using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversitySystem.ViewModel.Administrator
{
    public class AdminListAdminsVM
    {
        public IList<UniversitySystem.Entities.Administrator> Administrators { get; set; }
    }
}