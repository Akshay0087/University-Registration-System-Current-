using System.Web.Mvc;
using UniversitySystemRegistration.Business_Logic;
using UniversitySystemRegistration.Models;

namespace University_Registration_System_Current_.Controllers
{
    public class RegisterController : Controller
    {

        public readonly IUserServices userService;

        public RegisterController(IUserServices _userService)
        {
            userService = _userService;
        }
        // GET: Register
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public JsonResult SaveUserData(User userData)
        {
            var _msg= true;
            var flag = false;
            userData.role = Role.Student;
            
            if (!userService.UserCheck(userData))
            {
                flag = userService.insertUserData(userData);
                _msg = false;
            }
            return Json(new
            {
                result = flag,
                msg = _msg
            });
        }

    }
}
