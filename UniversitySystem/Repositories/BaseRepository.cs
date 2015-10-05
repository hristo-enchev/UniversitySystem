using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace UniversitySystem.Repositories
{
    public class BaseRepository<T> where T : UniversitySystem.Entities.BaseEntityWithID
    {
        public DbContext Context { get; set; }
        protected IDbSet<T> DbSet { get; set; }
        public UnitOfWork UnitOfWork { get; set; }

        public BaseRepository()
        {
            this.Context = new UniversitySystem.Models.UniversitySystemContext();
            this.DbSet = this.Context.Set<T>();
        }


        public BaseRepository(UnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentException("An instance of the unitOfWork is null", "unitOfWork");
            }

            this.Context = unitOfWork.Context;
            this.DbSet = this.Context.Set<T>();

            this.UnitOfWork = unitOfWork;
        }
        public virtual List<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }

        public virtual T GetByID(int id)
        {
            return DbSet.Find(id);
        }

        public virtual void Delete(T item)
        {
            DbSet.Remove(item);
            Context.SaveChanges();
        }

        public void Insert(T item)
        {
            this.DbSet.Add(item);
            Context.SaveChanges();
        }

        private void Update(T item)
        {
            Context.Entry(item).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public virtual void Save(T item)
        {
            if (item.ID <= 0)
            {
                Insert(item);
            }
            else
            {
                Update(item);
            }
        }

        public virtual void Dispose()
        {
            if (this.Context != null)
            {
                this.Context.Dispose();
            }
        }

  
    }
}