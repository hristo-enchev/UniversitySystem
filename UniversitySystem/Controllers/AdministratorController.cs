using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversitySystem.ViewModel.Administrator;
using UniversitySystem.Repositories;
using UniversitySystem.Entities;
using UniversitySystem.Hasher;

namespace UniversitySystem.Controllers
{
    [Filter.AuthenticationFilter]
    [Filter.AdministratorFilter]
    public class AdministratorController : Controller
    {
        public ActionResult Index()
        {
            AdministratorIndexVM model = new AdministratorIndexVM();
            UserRepository<Teacher> tRepo = new UserRepository<Teacher>();
            UserRepository<Student> sRepo = new UserRepository<Student>();
            UserRepository<Administrator> aRepo = new UserRepository<Administrator>();

            model.CountTeachers = tRepo.GetAll().Count();
            model.CountStudents = sRepo.GetAll().Count();
            model.CountAdministrators = aRepo.GetAll().Count();

            return View(model);
        }

        [HttpGet]
        public ActionResult CreateAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAdmin(AdministratorCreateAdminVM model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            UserRepository<Administrator> aRepo = new UserRepository<Administrator>();
            Administrator admin = new Administrator();

            Passphrase hash = PasswordHasher.Hash(model.Password);

            admin.FirstName = model.FirstName;
            admin.LastName = model.LastName;
            admin.Username = model.Username;
            admin.Hash = hash.Hash;
            admin.Salt = hash.Salt;

            aRepo.Save(admin);
            return RedirectToAction("ListAdmins", "Administrator");
        }

        public ActionResult ListAdmins()
        {
            AdminListAdminsVM model = new AdminListAdminsVM();
            UserRepository<Administrator> adminRepo = new UserRepository<Administrator>();

            model.Administrators = adminRepo.GetAll();

            return View(model);
        }
    }
}