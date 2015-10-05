using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversitySystem.ViewModel.TeacherVM;
using UniversitySystem.Repositories;
using UniversitySystem.Entities;

namespace UniversitySystem.Controllers
{
    [UniversitySystem.Filter.AuthenticationFilter]
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult ListTeachers()
        {
            TeacherListTeachersVM model = new TeacherListTeachersVM();
            UserRepository<Teacher> teacherRepo = new UserRepository<Teacher>();

            model.Teachers = teacherRepo.GetAll();

            return View(model);
        }
        public List<SelectListItem> TeacherTitle()
        {
            TitleRepository titleRepo = new TitleRepository();
            List<SelectListItem> list = new List<SelectListItem>();

            var title = titleRepo.GetAll();

            foreach (var item in title)
            {
                list.Add(new SelectListItem() { Text = item.Name, Value = item.ID.ToString() });
            }
            return list;
        }

        [HttpGet]
        public ActionResult CreateTeacher()
        {
            TeacherCreateTeacherVM model = new TeacherCreateTeacherVM();

            model.TitleList = TeacherTitle();

            return View(model);
        }
        [HttpPost]
        public ActionResult CreateTeacher(TeacherCreateTeacherVM model)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            Teacher teacher = new Teacher();
            UserRepository<Teacher> teacherRepo = new UserRepository<Teacher>(unitOfWork);
            TitleRepository titleRepo = new TitleRepository(unitOfWork);

            if (!ModelState.IsValid)
            {
                model.TitleList = TeacherTitle();

                return View(model);
            }

            try
            {
                teacher.Username = model.FirstName;
                teacher.FirstName = model.FirstName;
                teacher.LastName = model.LastName;

                int tittleId = Convert.ToInt32(model.selectedValueID);

                Title title = titleRepo.GetByID(tittleId);

                teacher.Title = title;

                UniversitySystem.Hasher.Passphrase hash = UniversitySystem.Hasher.PasswordHasher.Hash("password");
                teacher.Hash = hash.Hash;
                teacher.Salt = hash.Salt;
                teacher.Active = true;

                teacherRepo.Save(teacher);

                unitOfWork.Commit();
            }

            catch (Exception)
            {
                unitOfWork.RollBack();
                throw;
            }

            return RedirectToAction("ListTeachers", "Teacher");
        }

        [HttpGet]
        public ActionResult AddSubject(int? id)
        {
            TeacherAddSubjectVM model = new TeacherAddSubjectVM();
            CourseRepository cRepo = new CourseRepository();
            UserRepository<Teacher> teacherRepo = new UserRepository<Teacher>();

            List<SelectListItem> list = new List<SelectListItem>();

            Teacher teacher = teacherRepo.GetByID(id.Value);

            model.Title = teacher.Title.Name;
            model.FirstName = teacher.FirstName;
            model.LastName = teacher.LastName;
            model.TeacherID = teacher.ID;

            var courses = cRepo.GetAll();

            foreach (var item in courses)
            {
                list.Add(new SelectListItem() { Text = item.Name, Value = item.ID.ToString() });
            }
            model.CoursesList = list;

            return View(model);
        }
        [HttpPost]
        public ActionResult AddSubject(int id, int[] subjectID, string Choose)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            CourseRepository courseRepo = new CourseRepository();
            CourseSubject cs = new CourseSubject();
            CourseSubjectRepository csRepo = new CourseSubjectRepository(unitOfWork);
            UserRepository<Teacher> teacherRepo = new UserRepository<Teacher>(unitOfWork);
            Teacher teacher = teacherRepo.GetAll(filter: t => t.ID == id).FirstOrDefault();

            int courseId = Convert.ToInt32(Choose);

            var curSubj = csRepo.GetAll(filter: x => x.CourseID == courseId && x.Teachers.Any(t => t.ID == id));

            try
            {
                foreach (var item in curSubj)
                {
                    teacher.CourseSubject.Remove(item);
                }
                if (subjectID != null)
                {
                    foreach (var item in subjectID)
                    {
                        cs = csRepo.GetAll(filter: c => c.SubjectID == item && c.CourseID == courseId).FirstOrDefault();
                        teacher.CourseSubject.Add(cs);
                    }
                }
                teacherRepo.Save(teacher);
                unitOfWork.Commit();
            }
            catch (Exception)
            {
                unitOfWork.RollBack();
            }

            return RedirectToAction("ListTeachers", "Teacher");
        }


        public JsonResult States(string Choose, int teacherID)
        {
            CourseRepository cRepo = new CourseRepository();
            CourseSubjectRepository csRepo = new CourseSubjectRepository();

            List<SelectListItem> StatesList = new List<SelectListItem>();

            int choose = Convert.ToInt32(Choose);

            var subjects = csRepo.GetAll(filter: s => s.CourseID == choose);

            foreach (var item in subjects)
            {
                if (item.Teachers.Any(x => x.ID == teacherID))
                {
                    StatesList.Add(new SelectListItem() { Text = item.Subject.Name, Value = item.SubjectID.ToString(), Selected = true });
                }
                else
                {
                    StatesList.Add(new SelectListItem() { Text = item.Subject.Name, Value = item.SubjectID.ToString(), Selected = false });
                }
            }

            return Json(StatesList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            TeacherIndexVM model = new TeacherIndexVM();
            CourseRepository courseRepo = new CourseRepository();

            SubjectRepository subjRepo = new SubjectRepository();

            CourseSubjectRepository csRepo = new CourseSubjectRepository();

            model.Subjects = subjRepo.GetAll(filter: s => s.CourseSubject.Any(c => c.Teachers.Any(t => t.ID == UniversitySystem.Models.AuthenticationManager.LoggedUser.ID)));

            model.Courses = courseRepo.GetAll(filter: c => c.CourseSubject.Any(s => s.Teachers.Any(t => t.ID == UniversitySystem.Models.AuthenticationManager.LoggedUser.ID)));

            return View(model);
        }
        [HttpGet]
        public ActionResult EditTeacher(int id)
        {
            TeacherEditTeacherVM model = new TeacherEditTeacherVM();
            TitleRepository titleRepo = new TitleRepository();
            List<SelectListItem> list = new List<SelectListItem>();
            UserRepository<Teacher> teacherRepo = new UserRepository<Teacher>();

            Teacher teacher = teacherRepo.GetByID(id);

            var teacherTitle = titleRepo.GetByID(teacher.Title.ID);

            list.Add(new SelectListItem() { Text = teacherTitle.Name, Value = teacherTitle.ID.ToString() });
            var title = titleRepo.GetAll();

            foreach (var item in title)
            {
                if (item.ID != teacher.Title.ID)
                {
                    list.Add(new SelectListItem() { Text = item.Name, Value = item.ID.ToString() });
                }

            }
            model.FirstName = teacher.FirstName;
            model.LastName = teacher.LastName;
            model.Active = teacher.Active;
            model.TeacherID = teacher.ID;
            model.TitleList = list;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditTeacher(TeacherEditTeacherVM model)
        {
            if (!ModelState.IsValid)
            {
                model.TitleList = TeacherTitle();
                return View(model);
            }
            UnitOfWork unitOfWork = new UnitOfWork();
            UserRepository<Teacher> teacherRepo = new UserRepository<Teacher>(unitOfWork);
            TitleRepository tiRepo = new TitleRepository(unitOfWork);

            Teacher teacher = teacherRepo.GetByID(model.TeacherID);

            try
            {
                int TitleId = Convert.ToInt32(model.selectedValueID);
                Title title = tiRepo.GetByID(TitleId);

                if (model.FirstName != teacher.FirstName || model.LastName != teacher.LastName || model.Active != teacher.Active || TitleId != teacher.Title.ID)
                {
                    teacher.FirstName = model.FirstName;
                    teacher.LastName = model.LastName;
                    teacher.Username = model.FirstName;
                    teacher.Active = model.Active;
                    teacher.Title = title;

                    teacherRepo.Save(teacher);
                    unitOfWork.Commit();
                }
            }
            catch (Exception)
            {
                unitOfWork.RollBack();
            }
            return RedirectToAction("ListTeachers", "Teacher");
        }
    }
}
