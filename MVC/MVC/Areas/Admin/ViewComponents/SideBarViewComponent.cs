using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using MVC.Areas.Admin.Extensions;
using MVC.Areas.Admin.Models;
using System.Text.Json;

namespace MVC.Areas.Admin.ViewComponents
{
    public class SideBarViewComponent : ViewComponent
    {
        public ViewViewComponentResult Invoke()
        {
            var adminUser = HttpContext.Session.GetObject<AdminUserItem>("ActiveAdminUser");

            return View(adminUser);
        }
    }
}
