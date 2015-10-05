namespace UniversitySystem.Repositories
{
    public class InfoRepository : BaseRepository<Entities.Info>
    {
        public InfoRepository()
            : base()
        {

        }

        public InfoRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}