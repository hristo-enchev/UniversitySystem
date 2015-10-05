using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversitySystem.Entities
{
    public class InfoImage : BaseEntityWithID
    {
        public string Name { get; set; }

        public int InfoID { get; set; }

        public virtual Info Info { get; set; }

        public override string ToString()
        {
            return typeof(InfoImage).Name;
        }
    }
}