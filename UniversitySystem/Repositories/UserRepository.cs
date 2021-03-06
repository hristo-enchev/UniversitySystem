﻿namespace UniversitySystem.Repositories
{
    public class UserRepository<T> : BaseRepository<T> where T : UniversitySystem.Entities.BaseEntityWithID
    {
           public UserRepository()
            : base()
        {

        }

           public UserRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}