using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversitySystem.Repositories
{
    public class GradeRepository : BaseRepository<UniversitySystem.Entities.Grade>
    {
        public GradeRepository()
            : base()
        {

        }

        public GradeRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}