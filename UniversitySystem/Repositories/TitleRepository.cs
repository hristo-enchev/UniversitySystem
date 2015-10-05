namespace UniversitySystem.Repositories
{
    public class TitleRepository : BaseRepository<UniversitySystem.Entities.Title>
    {
        public TitleRepository()
            : base()
        {

        }

        public TitleRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}