using System.Web.Mvc;
using UniversitySystemRegistration;
using UniversitySystemRegistration.Models;
using UniversitySystemRegistration.Services;

namespace UniversitySystemRegistration.Controllers
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
            userData.UserRole = UserRoles.Student;
            
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
