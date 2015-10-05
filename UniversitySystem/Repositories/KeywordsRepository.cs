using UniversitySystem.Entities;

namespace UniversitySystem.Repositories
{
    public class KeywordsRepository : BaseRepository<Keyword>
    {
        public KeywordsRepository()
    : base()
        {

        }

        public KeywordsRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}