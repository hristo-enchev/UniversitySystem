using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using UniversitySystem.Entities;
using UniversitySystem.Hasher;
using UniversitySystem.Repositories;
using UniversitySystem.ViewModel.StudentVM;

namespace UniversitySystem.Controllers
{
    [Filter.AuthenticationFilter]
    [Filter.AdministratorFilter]
    public class StudentController : Controller
    {
        public ActionResult ListStudents(int? id)
        {
            StudentListStudentsVM model = new StudentListStudentsVM();
            UserRepository<Student> sRepo = new UserRepository<Student>();
            if (id == null)
            {
                model.Students = sRepo.GetAll(filter: s => s.Active == true);
            }
            else
            {
                model.Students = sRepo.GetAll(filter: s => s.CourseID == id.Value);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult CreateStudent(int? id)
        {
            StudentCreateStudentVM model = new StudentCreateStudentVM();
            CourseRepository cRepo = new CourseRepository();
            List<SelectListItem> list = new List<SelectListItem>();

            if (id != null)
            {
                var courses = cRepo.GetAll(filter: c => c.ID == id.Value).FirstOrDefault();
                list.Add(new SelectListItem() { Text = courses.Name + " || " + courses.FKNumber, Value = courses.ID.ToString() });
            }
            else
            {
                var courses = cRepo.GetAll();

                foreach (var item in courses)
                {
                    list.Add(new SelectListItem() { Text = item.Name + " || " + item.FKNumber, Value = item.ID.ToString() });
                }
            }

            model.CoursesList = list;
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateStudent(StudentCreateStudentVM model, int? id)
        {
            Student student = new Student();
            Course course = new Course();
            UnitOfWork unitOfWork = new UnitOfWork();
            UserRepository<Student> sRepo = new UserRepository<Student>();
            CourseRepository courseRepo = new CourseRepository();

            if (!ModelState.IsValid)
            {
                List<SelectListItem> list = new List<SelectListItem>();
                if (id != null)
                {
                    var courses = courseRepo.GetAll(filter: c => c.ID == id.Value).FirstOrDefault();
                    list.Add(new SelectListItem() { Text = courses.Name + " || " + courses.FKNumber, Value = courses.ID.ToString() });
                }
                else
                {
                    var courses = courseRepo.GetAll();

                    foreach (var item in courses)
                    {
                        list.Add(new SelectListItem() { Text = item.Name + " || " + item.FKNumber, Value = item.ID.ToString() });
                    }
                }
                model.CoursesList = list;

                return View(model);
            }

            int courseID = Convert.ToInt32(model.selectedValueID);
            course = courseRepo.GetAll(filter: c => c.ID == courseID).FirstOrDefault();

            var fkNumberCount = (from t in sRepo.GetAll(filter: x => x.FacultiNumber.Contains(course.FKNumber)).OrderByDescending(s => s.FacultiNumber)
                                 select t).FirstOrDefault();

            student.FirstName = model.FirstName;
            student.LastName = model.LastName;

            if (fkNumberCount == null)
            {
                student.FacultiNumber = (Convert.ToInt32(course.FKNumber) * 1000 + 1).ToString();
            }
            else
            {
                student.FacultiNumber = (Convert.ToInt32(fkNumberCount.FacultiNumber) + 1).ToString();
            }

            student.CourseID = courseID;
            student.Active = true;
            Passphrase hash = PasswordHasher.Hash("password");
            student.Hash = hash.Hash;
            student.Salt = hash.Salt;

            student.Username = "stu" + student.FacultiNumber;

            sRepo.Save(student);
            if (id != null)
            {
                return RedirectToAction("CreateStudent", "Student", new { id = id });
            }
            return RedirectToAction("ListStudents", "Student");
        }

        [HttpGet]
        public ActionResult EditStudent(int? id)
        {
            StudentEditStudentVM model = new StudentEditStudentVM();
            UserRepository<Student> stuRepo = new UserRepository<Student>();
            CourseRepository cRepo = new CourseRepository();

            Student student = stuRepo.GetByID(id.Value);

            var courses = cRepo.GetAll();

            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = student.Course.Name + " || " + student.Course.FKNumber, Value = student.Course.ID.ToString() });
            foreach (var item in courses)
            {
                if (item.ID != student.Course.ID)
                {
                    list.Add(new SelectListItem() { Text = item.Name + " || " + item.FKNumber, Value = item.ID.ToString() });
                }
            }

            model.StudentID = student.ID;
            model.CourseID = student.CourseID;
            model.FirstName = student.FirstName;
            model.LastName = student.LastName;
            model.Active = student.Active;
            model.CoursesList = list;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditStudent(StudentEditStudentVM model)
        {
            CourseRepository courseRepo = new CourseRepository();

            if (!ModelState.IsValid)
            {
                List<SelectListItem> list = new List<SelectListItem>();
                var courses = courseRepo.GetAll();

                foreach (var item in courses)
                {
                    list.Add(new SelectListItem() { Text = item.Name + " || " + item.FKNumber, Value = item.ID.ToString() });
                }

                model.CoursesList = list;
                return View(model);
            }
            UserRepository<Student> stuRepo = new UserRepository<Student>();
            Student student = new Student();
            Course course = new Course();

            student = stuRepo.GetByID(model.StudentID);

            int courseID = Convert.ToInt32(model.selectedValueID);
            if (model.CourseID != courseID)
            {
                course = courseRepo.GetAll(filter: c => c.ID == courseID).FirstOrDefault();
                var fkNumberCount = (from t in stuRepo.GetAll(filter: x => x.FacultiNumber.Contains(course.FKNumber)).OrderByDescending(s => s.FacultiNumber)
                                     select t).FirstOrDefault();

                if (fkNumberCount == null)
                {
                    student.FacultiNumber = (Convert.ToInt32(course.FKNumber) * 1000 + 1).ToString();
                }
                else
                {
                    student.FacultiNumber = (Convert.ToInt32(fkNumberCount.FacultiNumber) + 1).ToString();
                }
                student.CourseID = courseID;
            }
            student.FirstName = model.FirstName;
            student.Username = "stu" + student.FacultiNumber;
            student.LastName = model.LastName;
            student.Active = model.Active;
            stuRepo.Save(student);

            return RedirectToAction("ListStudents", "Student");
        }

        [HttpGet]
        public ActionResult Deactivated()
        {
            StudentDeactivatedVM model = new StudentDeactivatedVM();
            UserRepository<Student> stuRepo = new UserRepository<Student>();
            model.Students = stuRepo.GetAll(filter: s => s.Active == false);

            return View(model);
        }

        [OverrideActionFilters]
        [Filter.StudentFilter]
        public ActionResult Index()
        {
            StudentIndexVM model = new StudentIndexVM();
            UserRepository<Student> stuRepo = new UserRepository<Student>();
            CourseRepository courseRepo = new CourseRepository();
            GradeRepository gradeRepo = new GradeRepository();
            UserRepository<Teacher> teacherRepo = new UserRepository<Teacher>();

            Student student = stuRepo.GetByID(Models.AuthenticationManager.LoggedUser.ID);

            model.FacultiNumber = student.FacultiNumber;
            model.FirstName = student.FirstName;
            model.LastName = student.LastName;

            Course course = courseRepo.GetAll(filter: x => x.CourseSubject.Any(c => c.Course.Student.Any(s => s.ID == Models.AuthenticationManager.LoggedUser.ID))).FirstOrDefault();

            model.CourseName = course.Name;

            model.Grades = gradeRepo.GetAll(filter: g => g.StudentID == Models.AuthenticationManager.LoggedUser.ID);

            return View(model);
        }

        [OverrideActionFilters]
        [Filter.StudentFilter]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            StudentChangePasswordVM model = new StudentChangePasswordVM();
            UserRepository<Student> stuRepo = new UserRepository<Student>();

            Student student = stuRepo.GetByID(Models.AuthenticationManager.LoggedUser.ID);

            model.StudentID = student.ID;

            return View(model);
        }

        [OverrideActionFilters]
        [Filter.StudentFilter]
        [HttpPost]
        public ActionResult ChangePassword(StudentChangePasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (model.NewPassword != model.NewPasswordRepeat)
            {
                ModelState.AddModelError("Error", "New password dont match");
                return View(model);
            }

            UserRepository<Student> stuRepo = new UserRepository<Student>();
            Student student = stuRepo.GetByID(model.StudentID);
            Passphrase hash = PasswordHasher.Hash(model.OldPassword);

            if (PasswordHasher.Equals(model.OldPassword, student.Salt, student.Hash))
            {
                Passphrase hash2 = PasswordHasher.Hash(model.NewPassword);

                student.Salt = hash2.Salt;
                student.Hash = hash2.Hash;

                stuRepo.Save(student);
            }
            else
            {
                ModelState.AddModelError("Error", "Old password dont match");
                return View(model);
            }

            return RedirectToAction("Index", "Student");
        }
        public void ExportSubjectListToExcel(int id)
        {
            StudentIndexVM model = new StudentIndexVM();
            GradeRepository gradeRepo = new GradeRepository();
            UserRepository<Student> studentRepo = new UserRepository<Student>();
            Student student = studentRepo.GetByID(id);

            model.Grades = gradeRepo.GetAll(filter: s => s.StudentID == id);

            var grid = new System.Web.UI.WebControls.GridView();

            var products = new System.Data.DataTable(student.FirstName);

            products.Columns.Add("Subject", typeof(string));
            products.Columns.Add("Grade", typeof(string));

            foreach (var item in model.Grades)
            {
                products.Rows.Add(item.Subject.Name, item.GradeValue);
            }

            grid.DataSource = products;
            grid.DataBind();

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + student.FacultiNumber + ".xls");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        public void ExportSubjectListToCSV(int id)
        {
            StudentIndexVM model = new StudentIndexVM();
            GradeRepository gradeRepo = new GradeRepository();
            UserRepository<Student> studentRepo = new UserRepository<Student>();

            model.Grades = gradeRepo.GetAll(filter: s => s.StudentID == id);
            Student student = studentRepo.GetByID(id);

            StringWriter sw = new StringWriter();

            sw.WriteLine("\"Subject\",\"Grade\"");

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=" + student.FacultiNumber + ".csv");
            Response.ContentType = "text/csv";

            foreach (var item in model.Grades)
            {
                sw.WriteLine(string.Format("\"{0}\",\"{1}\"",
                                           item.Subject.Name,
                                           item.GradeValue));
            }

            Response.Write(sw.ToString());
            Response.End();
        }

        public ActionResult ListStudentsBySubject(int id)
        {
            UserRepository<Student> stuRepo = new UserRepository<Student>();
            SubjectRepository subjectRepo = new SubjectRepository();
            StudentListStudentsBySubjectVM model = new StudentListStudentsBySubjectVM();
            Subject course = subjectRepo.GetByID(id);

            var students = stuRepo.GetAll(filter: s => s.Course.CourseSubject.Any(c => c.SubjectID == id));

            model.Students = students;
            model.SubjectName = course.Name;

            return View(model);
        }
    }
}