using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversitySystem.Entities
{
    public class Title : BaseEntityWithID
    {
        public string Name{ get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}