using UniversitySystem.Entities;


namespace UniversitySystem.Repositories
{
    public class CourseSubjectRepository : BaseRepository<CourseSubject>
    {
        public CourseSubjectRepository()
            : base()
        {

        }

        public CourseSubjectRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        //public void UpdateTable(Course course, IEnumerable<Subject> subjects)
        //{
        //    var all = DbSet.Where(x => x.CourseID == course.ID).ToList();

        //    DbSet.RemoveRange(all.Where(r => subjects.FirstOrDefault(x => x.ID == r.SubjectID) == null));

        //    var newEntries = subjects.Where(x => all.FirstOrDefault(y => y.SubjectID == x.ID) == null);
        //    DbSet.AddRange(newEntries.Select<Subject, CourseSubject>(x => new CourseSubject() { CourseID = course.ID, SubjectID = x.ID }));

        //    Context.SaveChanges();
            
        //}
        public void InsertSubject(CourseSubject item)
        {
            this.DbSet.Add(item);
            Context.SaveChanges();
        }

    }
}