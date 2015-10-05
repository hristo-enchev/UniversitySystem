using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using UniversitySystem.Entities;
using UniversitySystem.Repositories;
using UniversitySystem.ViewModel.Subjects;

namespace UniversitySystem.Controllers
{
    [UniversitySystem.Filter.AuthenticationFilter]
    public class SubjectController : Controller
    {
        // GET: Subject
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// List all subjects 
        /// </summary>
        /// <returns>List of subjects</returns>
        public ActionResult ListSubjects()
        {
            SubjectListSubjectsVM model = new SubjectListSubjectsVM();
            UserRepository<Student> stuRepo = new UserRepository<Student>();

            SubjectRepository subjRepo = new SubjectRepository();

            List<Student> student = stuRepo.GetAll();

            var subjects = subjRepo.GetAll();

            var subjectList = new Dictionary<Subject, int>();

            foreach (var subject in subjects)
            {
                int count = 0;
                if (student.Any(s => s.Course.CourseSubject.Any(x => x.SubjectID == subject.ID)))
                {
                    count = student.Count(x => x.Course.CourseSubject.Any(c => c.SubjectID == subject.ID));
                }
                subjectList.Add(subject, count);
            }

            model.SubjectList = subjectList;

            return View(model);
        }

        /// <summary>
        /// If id == null its create else it is edit 
        /// </summary>
        /// <param name="id">Id of the subject</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditSubject(int? id)
        {
            SubjectEditSubjectVM model = new SubjectEditSubjectVM();

            model.Action = "Create";

            if (id != null)
            {
                SubjectRepository subjectRepo = new SubjectRepository();
                Subject subject = subjectRepo.GetAll(filter: s => s.ID == id.Value).FirstOrDefault();

                model.Action = "Edit";
                model.SubjectID = subject.ID;
                model.SubjectName = subject.Name;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult EditSubject(SubjectEditSubjectVM model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            SubjectRepository subjectRepo = new SubjectRepository();
            Subject subject = new Subject();

            if (model.SubjectID != 0)
            {
                subject = subjectRepo.GetByID(model.SubjectID);
            }

            subject.Name = model.SubjectName;

            subjectRepo.Save(subject);

            return RedirectToAction("ListSubjects", "Subject");
        }

        [HttpGet]
        public ActionResult DeleteSubject(int id)
        {
            SubjectRepository subjectRepo = new SubjectRepository();
            SubjectDeleteSubjectVM model = new SubjectDeleteSubjectVM();

            Subject subject = subjectRepo.GetByID(id);

            model.SubjectID = subject.ID;
            model.SubjectName = subject.Name;


            return View(model);
        }
        [HttpPost]
        public ActionResult DeleteSubject(SubjectDeleteSubjectVM model)
        {
            SubjectRepository subjectRepo = new SubjectRepository();

            Subject subject = subjectRepo.GetByID(model.SubjectID);

            if (subject.CourseSubject.Count() > 0)
            {

                ModelState.AddModelError("Error", "Subject contains teacher or course");
                model.SubjectID = subject.ID;
                model.SubjectName = subject.Name;
                return View(model);
            }

            subjectRepo.Delete(subject);

            return RedirectToAction("ListSubjects", "Subject");
        }

        public void ExportSubjectListToExcel(int id)
        {
            GradeRepository gradeRepo = new GradeRepository();
            SubjectRepository subjRepo = new SubjectRepository();
            UserRepository<Student> studentRepo = new UserRepository<Student>();

            List<Student> students = studentRepo.GetAll(filter: s => s.Course.CourseSubject.Any(t => t.SubjectID == id));

            List<Grade> grades = gradeRepo.GetAll(filter: s => s.SubjectID == id);
            Subject subject = subjRepo.GetByID(id);

            var grid = new System.Web.UI.WebControls.GridView();
            var products = new System.Data.DataTable(subject.Name);

            products.Columns.Add("FacultiNumber", typeof(string));
            products.Columns.Add("FirstName", typeof(string));
            products.Columns.Add("LastName", typeof(string));
            products.Columns.Add("Grade", typeof(string));

            foreach (var student in students)
            {
                string grade = student.Grades.Select(x => x.GradeValue).FirstOrDefault().ToString();
                grade = grade == "0" ? "" : grade;
                products.Rows.Add(student.FacultiNumber, student.FirstName, student.LastName, grade);
            }

            grid.DataSource = products;
            grid.DataBind();

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + subject.Name + ".xls");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        public void ExportSubjectListToCSV(int id)
        {
            GradeRepository gradeRepo = new GradeRepository();
            SubjectRepository subjRepo = new SubjectRepository();
            UserRepository<Student> studentRepo = new UserRepository<Student>();

            List<Student> students = studentRepo.GetAll(filter: s => s.Course.CourseSubject.Any(t => t.SubjectID == id));
            if (students.Count() > 0)
            {
                List<Grade> grades = gradeRepo.GetAll(filter: s => s.SubjectID == id);
                Subject subject = subjRepo.GetByID(id);

                StringWriter sw = new StringWriter();

                sw.WriteLine("\"Faculty number\",\"First name\",\"Last name\",\"Grade\"");

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=" + subject.Name + ".csv");
                Response.ContentType = "text/csv";

                foreach (var student in students)
                {
                    string grade = student.Grades.Select(x => x.GradeValue).FirstOrDefault().ToString();
                    grade = grade == "0" ? "" : grade;
                    sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\"",
                                               student.FacultiNumber,
                                               student.FirstName,
                                               student.LastName,
                                               grade));
                }

                Response.Write(sw.ToString());
                Response.End();
            }
        }

    }
}