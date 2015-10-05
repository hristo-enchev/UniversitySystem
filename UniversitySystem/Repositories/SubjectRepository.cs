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