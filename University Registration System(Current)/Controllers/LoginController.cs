using System.Web.Mvc;
using UniversitySystemRegistration.Models;
using UniversitySystemRegistration.Services;

namespace UniversitySystemRegistration.Controllers
{
    public class LoginController : Controller
    {

        public readonly IUserServices userService;

        public LoginController(IUserServices _userService)
        {
            userService = _userService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Authenticate(User userData)
        {
            var flag = userService.UserLogin(userData);

            if (flag)
            {
                User authenticatedUser = userService.GetUserData(userData);

                this.Session["CurrentUser"] = authenticatedUser;
                this.Session["CurrentRole"] = authenticatedUser.UserRole;

            }

            return Json(new
            {
                result = flag,
                url = Url.Action("Main", "RequestInterface"),

            });
        }

        [HttpPost]
        public JsonResult Logout()
        {
            this.Session.Clear();

            return Json(new
            {
                result = true

            }) ;
        }

    }
}