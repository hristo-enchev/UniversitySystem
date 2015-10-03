using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversitySystem.Entities;
using UniversitySystem.Repositories;
using UniversitySystem.ViewModel.CourseVM;

namespace UniversitySystem.Controllers
{
    [UniversitySystem.Filter.AuthenticationFilter]
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult ListCourse(CourseListCourseVM model)
        {
            CourseRepository cRepo = new CourseRepository();

            model.Courses = cRepo.GetAll();

            return View(model);
        }

        [HttpGet]
        public ActionResult CreateCourse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCourse(CourseCreateCourseVM model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            CourseRepository cRepo = new CourseRepository();
            Course checkFKNumberIsItUniqie = cRepo.GetAll(filter: ch => ch.FKNumber == model.FKNumber).FirstOrDefault();
            string digitCount = model.FKNumber.ToString();
            if (digitCount.Count() != 6)
            {
                ModelState.AddModelError("Error", "Faculty number must be 6 digits" as string);
                return View(model);  
            }       
            else if(checkFKNumberIsItUniqie != null){
                ModelState.AddModelError("Error", "There is course with this FK" as string);
                return View(model);  
            }

            Course course = new Course();
            course.FKNumber = model.FKNumber;
            course.Name = model.Name;
            cRepo.Save(course);

            return RedirectToAction("ListCourse", "Course");
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            Course course = new Course();
            CourseRepository cRepo = new CourseRepository();
            CourseDeleteVM model = new CourseDeleteVM();

            course = cRepo.GetAll(filter: c => c.ID == id.Value).FirstOrDefault();

            model.CourseID = course.ID;
            model.Name = course.Name;

            if (TempData["Error"] != null)
            {
                ModelState.AddModelError("Error", TempData["Error"] as string);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(CourseDeleteVM model)
        {

            
            CourseSubjectRepository csRepo = new CourseSubjectRepository();
            CourseRepository cRepo = new CourseRepository();

            //var curSubj = csRepo.GetAll(filter: c => c.CourseID == model.Course.ID);
            var courseCount = cRepo.GetAll(filter: c => c.ID == model.CourseID && c.Student.Count() > 0 ).FirstOrDefault();

            if (courseCount != null)
            {
                TempData["Error"] = "Course contain students, teachers or subject";
                return RedirectToAction("Delete", "Course");
            }
            else
            {
                Course course = cRepo.GetByID(model.CourseID);
                cRepo.Delete(course);
            }


            return RedirectToAction("ListCourse", "Course");
        }

        [HttpGet]
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult EditCourse(int? id)
        {
            CourseEditCourseVM model = new CourseEditCourseVM();

            CourseRepository cRepo = new CourseRepository();

            Course course = new Course();

            course = cRepo.GetByID(id.Value);

            model.Name = course.Name;

            model.CourseID = course.ID;

            return View(model);
        }
        [HttpPost]
        public ActionResult EditCourse(CourseEditCourseVM model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            CourseRepository cRepo = new CourseRepository();
            Course course = cRepo.GetByID(model.CourseID);

            course.Name = model.Name;

            cRepo.Save(course);

            return RedirectToAction("ListCourse", "Course");
        }
        public List<SelectListItem> ListForSubjectToCourse(int subjectID)
        {
            SubjectRepository subjRepo = new SubjectRepository();
            List<SelectListItem> list = new List<SelectListItem>();
            CourseRepository courseRepo = new CourseRepository();

            var subjects = subjRepo.GetAll();
            var course = courseRepo.GetByID(subjectID);
            foreach (var item in subjects)
            {
                if (course.CourseSubject.Any(c => c.SubjectID == item.ID))
                {
                    list.Add(new SelectListItem() { Value = item.ID.ToString(), Selected = true, Text = item.Name });
                }
                else
                {
                    list.Add(new SelectListItem() { Value = item.ID.ToString(), Selected = false, Text = item.Name });
                }
            }

            return list;
            
        }

        [HttpGet]
        public ActionResult AddSubjectToCourse(int? id)
        {
            CourseAddSubjectToCourseVM model = new CourseAddSubjectToCourseVM();

         

            model.CourseID = id.Value;

            model.List = ListForSubjectToCourse(id.Value);
            if (TempData["Error"] != null)
            {
                ModelState.AddModelError("Error1", "Subject had teacher");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AddSubjectToCourse(int subjectID, bool isChecked, int courseID)
        {
            CourseAddSubjectToCourseVM model = new CourseAddSubjectToCourseVM();
            UnitOfWork unitOfWork = new UnitOfWork();
            SubjectRepository subjRepo = new SubjectRepository(unitOfWork);
            CourseRepository courseRepo = new CourseRepository(unitOfWork);
            CourseSubject cs = new CourseSubject();
            CourseSubjectRepository csRepo = new CourseSubjectRepository(unitOfWork);
            CourseSubject coursSubject = new CourseSubject();
            UserRepository<Teacher> tRepo = new UserRepository<Teacher>();

            try
            {
                if (isChecked == false)
                {
                    coursSubject = csRepo.GetAll(filter: c => c.SubjectID == subjectID && c.CourseID == courseID).FirstOrDefault();

                    Subject subject = subjRepo.GetByID(subjectID);

                    Course course = courseRepo.GetByID(courseID);

                    var teachers = tRepo.GetAll();

                    if (coursSubject.Teachers.Any())
                    {
                        TempData["Error"] = "Course contain students, teachers or subject";
                        //return RedirectToAction("AddSubjectToCourse", "Course", new { id = courseID });
                        model.List = ListForSubjectToCourse(subjectID);
                        model.Error = "Subject contain teacher";
                        return View(model);
                    }

                    csRepo.Delete(coursSubject);
                }
                else
                {
                    Course course = courseRepo.GetByID(courseID);

                    Subject sub = subjRepo.GetByID(subjectID);



                    coursSubject.Course = course;
                    coursSubject.Subject = sub;
                    csRepo.Save(coursSubject);
                }
                unitOfWork.Commit();
            }
            catch (Exception)
            {

                unitOfWork.RollBack();
            }

            return RedirectToAction("AddSubjectToCourse","Course");
        }
    }
}