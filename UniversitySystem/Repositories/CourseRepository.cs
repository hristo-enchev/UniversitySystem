namespace UniversitySystem.Repositories
{
    public class CourseRepository : BaseRepository<Entities.Course>
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