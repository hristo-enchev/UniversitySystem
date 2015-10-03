using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using UniversitySystem.Entities;
using UniversitySystem.Repositories;
using UniversitySystem.ViewModel.Grades;

namespace UniversitySystem.Controllers
{
    [UniversitySystem.Filter.AuthenticationFilter]
    public class GradeController : Controller
    {
        // GET: Grade
        public ActionResult StudentsGrades(int id)
        {
            GradeStudentsGradesVM model = new GradeStudentsGradesVM();
            GradeRepository gradeRepo = new GradeRepository();
            SubjectRepository subjRepo = new SubjectRepository();
            UserRepository<Student> studentRepo = new UserRepository<Student>();
            
            model.Students = studentRepo.GetAll(filter: s => s.Course.CourseSubject.Any(t => t.SubjectID == id));
            model.Grades = gradeRepo.GetAll(filter: s => s.SubjectID == id);
         

            Subject subject = subjRepo.GetByID(id);

            model.SubjectID = subject.ID;

            return View(model);
        }

        [HttpPost]
        public ActionResult StudentsGrades(string Grade)
        {

            return View();
        }
        [HttpGet]
        public ActionResult AddGrade(int studentID, int subjectID)
        {
           GradeAddGradeVM model = new GradeAddGradeVM();


            UserRepository<Student> sRepo = new UserRepository<Student>();
            SubjectRepository subjRepo = new SubjectRepository();
            GradeRepository gRepo = new GradeRepository();


            Student student = sRepo.GetAll(filter: s => s.ID == studentID && s.Course.CourseSubject.Any(sub => sub.SubjectID == subjectID)).FirstOrDefault();

            Subject subject = subjRepo.GetByID(subjectID);

            Grade grade = gRepo.GetAll(filter: g => g.StudentID == student.ID).FirstOrDefault();


            model.StudentID = student.ID;
            model.SubjectID = subject.ID;
 
            return View(model);
        }
        [HttpPost]
        public ActionResult AddGrade(GradeAddGradeVM model)
        {
            if (model.GradeValue < 2 || model.GradeValue > 6)
            {
                ModelState.AddModelError("Error", "Grade must be 2 - 6");
                return View(model);
            }  
           
            GradeRepository gRepo = new GradeRepository();
            Grade grade = new Grade();
            var test = gRepo.GetAll(filter: g => g.StudentID == model.StudentID && g.SubjectID == model.SubjectID).FirstOrDefault();
            if (test != null)
            {
                ModelState.AddModelError("Error", "Student already have grade");
                return View(model);
            }

            grade.StudentID = model.StudentID;
            grade.SubjectID = model.SubjectID;
            grade.GradeValue = model.GradeValue;

            gRepo.Save(grade);

            return RedirectToAction("StudentsGrades" + "/" + model.SubjectID, "Grade");
        }

        public void ExportSubjectListToExcel(int id)
        {

            GradeStudentsGradesVM model = new GradeStudentsGradesVM();
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
            foreach (var item in students)
            {
                string test = item.Grades.Select(x => x.GradeValue).FirstOrDefault().ToString();
                products.Rows.Add(item.FacultiNumber, item.FirstName, item.LastName, test);
            }

            grid.DataSource = products;
           // grid.DataSource = model.Students;

            grid.DataBind();

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename="+subject.Name+".xlsx");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Write(sw.ToString());

            Response.End();

        }
        public void ExportSubjectListToCSV(int id)
        {

            
            GradeStudentsGradesVM model = new GradeStudentsGradesVM();
            GradeRepository gradeRepo = new GradeRepository();
            SubjectRepository subjRepo = new SubjectRepository();
            UserRepository<Student> studentRepo = new UserRepository<Student>();

            List<Student> students = studentRepo.GetAll(filter: s => s.Course.CourseSubject.Any(t => t.SubjectID == id));
            List<Grade> grades = gradeRepo.GetAll(filter: s => s.SubjectID == id);
            Subject  subject = subjRepo.GetByID(id);

            StringWriter sw = new StringWriter();

            sw.WriteLine("\"Faculty number\",\"First name\",\"Last name\",\"Grade\"");

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename="+ subject.Name +".csv");
            Response.ContentType = "text/csv";   

            foreach (var item in students)
            {
                string test = item.Grades.Select(x => x.GradeValue).FirstOrDefault().ToString();
                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\"",
                                           item.FacultiNumber,
                                           item.FirstName,
                                           item.LastName,
                                           test));
            }

            Response.Write(sw.ToString());

            Response.End();

        }
    }
}