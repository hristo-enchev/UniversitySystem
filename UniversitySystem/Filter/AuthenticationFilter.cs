using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversitySystem.Services;

namespace UniversitySystem.Filter
{
    public class AuthenticationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (UniversitySystem.Models.AuthenticationManager.LoggedUser == null)
            {
                // filterContext.HttpContext.Response.Redirect("~/?RedirectUrl=" + filterContext.HttpContext.Request.Url);
                //  filterContext.Result = new EmptyResult();

                filterContext.Result = new RedirectResult("~/");
                return;
            }

            base.OnActionExecuting(filterContext);

        }
    }
}