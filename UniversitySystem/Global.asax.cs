using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using UniversitySystem.Repositories;
using UniversitySystem.Entities;
using System.Configuration;
using UniversitySystem.Hasher;

namespace UniversitySystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            CreateAdministrator();
        }

        private void CreateAdministrator()
        {
            UserRepository<Administrator> administratorRepo = new UserRepository<Administrator>();
            
            string admin = ConfigurationManager.AppSettings["UniversityAdministrator"];

            if (administratorRepo.GetAll(filter: a => a.Username == admin).FirstOrDefault() == null)
            {
                Passphrase hash = PasswordHasher.Hash(ConfigurationManager.AppSettings["UniversityAdministratorPassword"]);
                Administrator administrator = new Administrator();  
               
                string ahash = hash.Hash;
                string asalt = hash.Salt;

                administrator.Username = ConfigurationManager.AppSettings["UniversityAdministrator"];
                administrator.LastName = ConfigurationManager.AppSettings["UniversityAdministrator"];
                administrator.FirstName = ConfigurationManager.AppSettings["UniversityAdministrator"];
                administrator.Hash = ahash;
                administrator.Salt = asalt;

                administratorRepo.Save(administrator);
            }
        }
    }
}
