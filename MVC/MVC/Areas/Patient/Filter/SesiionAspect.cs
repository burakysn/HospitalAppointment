using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Areas.Patient.Filter
{
    public class SessionAspect : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var sessionJson = context.HttpContext.Session.GetString("ActivePatient");

            if (string.IsNullOrEmpty(sessionJson))
            {
                context.Result = new RedirectResult("/Patient/Authentication/LogIn");
            }
        }
    }
}
