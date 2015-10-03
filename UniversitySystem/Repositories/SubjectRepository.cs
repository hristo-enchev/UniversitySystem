using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversitySystem.Repositories
{
    public class SubjectRepository : BaseRepository<UniversitySystem.Entities.Subject>
    {
        public SubjectRepository()
            : base()
        {

        }

        public SubjectRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}