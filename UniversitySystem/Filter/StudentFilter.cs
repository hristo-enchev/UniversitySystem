using System.Web.Mvc;

namespace UniversitySystem.Filter
{
    public class StudentFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Models.AuthenticationManager.LoggedUser == null || (Models.AuthenticationManager.LoggedUser.GetType().Name).Split('_').GetValue(0).ToString() != "Student")
            {
                filterContext.Result = new RedirectResult("~/");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}