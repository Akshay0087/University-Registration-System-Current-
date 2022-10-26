using System.Web.Mvc;
using UniversitySystemRegistration.Business_Logic;
using UniversitySystemRegistration.Models;

namespace University_Registration_System_Current_.Controllers
{
    public class LoginController : Controller
    {

        public readonly IUserServices userService;

        public LoginController(IUserServices _userService)
        {
            userService= _userService;
        }
        
        public ActionResult Login()
        { 
            return View();
        }

        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Student Student)
        {
            
            return View();
        }*/

        
        [HttpPost]
        public JsonResult Authenticate(User userData)
        {
            var flag=userService.UserLogin(userData);

            if (flag)
            {
               User employeeInfo = userService.GetUserData(userData);
               this.Session["CurrentUser"] = employeeInfo;
               this.Session["CurrentRole"] = employeeInfo.role;
            }

            return Json(new 
            { 
              result = flag, 
              url = Url.Action("Main", "RequestInterface"),
              
            });
        }

        [HttpPost]
        public JsonResult logout()
        {
            this.Session.Clear();

            return Json(new
            {
                url = Url.Action("Login", "Login"),

            });
        }


    }
}