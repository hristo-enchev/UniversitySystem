using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversitySystem.Repositories
{
    public class CourseRepository : BaseRepository<UniversitySystem.Entities.Course>
    {
        public CourseRepository()
            : base()
        {

        }

        public CourseRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}