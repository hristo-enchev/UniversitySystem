using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversitySystem.Entities;
using UniversitySystem.Services;

namespace UniversitySystem.Models
{
    public class AuthenticationManager
    {
        private static AuthenticationService AuthenticationServiceInstance
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Session[typeof(AuthenticationService).Name] == null)
                {
                    HttpContext.Current.Session[typeof(AuthenticationService).Name] = new AuthenticationService();
                }

                return (AuthenticationService)HttpContext.Current.Session[typeof(AuthenticationService).Name];
            }
        }

        public static User LoggedUser
        {
            get
            {
                return AuthenticationManager.AuthenticationServiceInstance.LoggedUser;
            }
        }
        public static void Authenticate(string username, string password, string type)
        {

            AuthenticationManager.AuthenticationServiceInstance.AuthenticateUser(username, password, type);

        }

        public static void Logout()
        {
            AuthenticationManager.AuthenticationServiceInstance.AuthenticateUser(null, null, "test");
            HttpContext.Current.Session[typeof(AuthenticationManager).Name] = null;

        }
    }
}