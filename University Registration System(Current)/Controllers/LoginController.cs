using System.Web.Mvc;
using UniversitySystemRegistration.Models;
using UniversitySystemRegistration.Services;

namespace UniversitySystemRegistration.Controllers
{
    public class LoginController : Controller
    {

        public readonly IUserServices userService;
        public readonly IStudentServices studentService;

        public LoginController(IUserServices _userService,IStudentServices _studentService)
        {
            userService = _userService;
            studentService = _studentService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Authenticate(User userData)
        {
            var flag = userService.UserLogin(userData);
            var path = Url.Action("Main","StudentInterface");

            if (flag)
            {
                User authenticatedUser = userService.GetUserData(userData);
                this.Session["CurrentUser"] = authenticatedUser;
                this.Session["CurrentRole"] = authenticatedUser.UserRole;
                if (authenticatedUser.UserRole == UserRoles.Student) { 
                    var tuple=studentService.GetStudentDataFromDb(authenticatedUser);
                    this.Session["CurrentUser"] = tuple.Item2;
                 }else if(authenticatedUser.UserRole == UserRoles.Admin)
                {
                    path = Url.Action("AdminPanel","Admin");
                }

            }
            return Json(new{result = flag,url = path});
        }

        [HttpPost]
        [OutputCache(NoStore =true,Duration =0,VaryByParam ="None")]
        public JsonResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return Json(new
            {
                result = true

            }) ;
        }

    }
}