using UniversitySystem.Entities;

namespace UniversitySystem.Repositories
{
    public class InfoImageRepository : BaseRepository<InfoImage>
    {
        public InfoImageRepository()
            : base()
        {

        }

        public InfoImageRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}