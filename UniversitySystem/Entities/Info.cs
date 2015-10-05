using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversitySystem.Entities
{
    [Table("Info")]
    public class Info : BaseEntityWithID
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public virtual ICollection<Keyword> Keywords { get; set; }

        public string FileName { get; set; }

        public virtual ICollection<InfoImage> Images { get; set; }

        public override string ToString()
        {
            return typeof(Info).Name;
        }
    }
}