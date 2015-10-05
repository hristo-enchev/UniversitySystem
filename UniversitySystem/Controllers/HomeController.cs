using System;
using System.Web.Mvc;
using UniversitySystem.ViewModel.Home;

namespace UniversitySystem.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            HomeIndexVM model = new HomeIndexVM();

            if (TempData["Error"] != null)
            {
                ModelState.AddModelError("Error", TempData["Error"] as string);
            }

            if (Models.AuthenticationManager.LoggedUser != null)
            {
                string[] type = (Models.AuthenticationManager.LoggedUser.GetType().Name).Split('_');
                return RedirectToAction("Index", type[0]);
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(HomeIndexVM model)
        {
            try
            {
                if (Models.AuthenticationManager.LoggedUser == null && model.username != null && model.password != null)
                {
                    Models.AuthenticationManager.Authenticate(model.username, model.password, model.usersEnum.ToString());
                    if (Models.AuthenticationManager.LoggedUser == null)
                    {
                        TempData["Error"] = "Invalid password or username";

                        return RedirectToAction("Index", "Home");
                    }
                }
                if (Models.AuthenticationManager.LoggedUser != null)
                {
                    string[] type = (Models.AuthenticationManager.LoggedUser.GetType().Name).Split('_');
                    return RedirectToAction("Index", type[0]);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }

        public ActionResult Logout()
        {
            Models.AuthenticationManager.Logout();

            return RedirectToAction("Index", "Home");
        }
    }
}