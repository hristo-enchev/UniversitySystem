using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversitySystem.Entities;
using UniversitySystem.Repositories;

namespace UniversitySystem.Controllers
{
    [UniversitySystem.Filter.AuthenticationFilter]
    public class TitleController : Controller
    {

        [HttpGet]
        public ActionResult AddTitle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTitle(UniversitySystem.ViewModel.Titles.AddTitle model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            TitleRepository titleRepo = new TitleRepository();
            Title title = new Title();

            title.Name = model.TitlteName;

            titleRepo.Save(title);

            return RedirectToAction("ListTeachers", "Teacher");
        }
    }
}