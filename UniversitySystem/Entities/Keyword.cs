using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversitySystem.Entities
{
    public class Keyword : BaseEntityWithID
    {
        public string Value { get; set; }

        public virtual ICollection<Info> Info { get; set; }
    }
}