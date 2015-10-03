using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UniversitySystem.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private UniversitySystem.Models.UniversitySystemContext context = new UniversitySystem.Models.UniversitySystemContext();

        private DbContextTransaction trans = null;

        public DbContext Context { get; private set; }

        //Represetns the main constructor of the class. In this constructor the transaction begins
        public UnitOfWork()
        {
            this.trans = context.Database.BeginTransaction();
            this.Context = context;
        }
        //Represents a method that commits all changes made in  the database
        public void Commit()
        {
            if (this.trans != null)
            {
                this.trans.Commit();
                this.trans = null;
            }
        }
        //Represents a method that rolls back the transaction and cancels all changes
        public void RollBack()
        {
            if (this.trans != null)
            {
                this.trans.Rollback();
                this.trans = null;
            }
        }
        //Represents a method that disposes the context of the database
        public void Dispose()
        {
            Commit();
            context.Dispose();
        }
    }
}