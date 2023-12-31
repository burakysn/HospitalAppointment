﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MVC.Areas.Admin.Filters
{
    public class SessionAspect : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var sessionJson = context.HttpContext.Session.GetString("ActiveAdminUser");

            if (string.IsNullOrEmpty(sessionJson))
            {
                context.Result = new RedirectResult("/Admin/Authentication/LogIn");
            }
        }
    }
}
