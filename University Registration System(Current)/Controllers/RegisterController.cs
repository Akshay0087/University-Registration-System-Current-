using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversitySystemRegistration.Business_Logic;
using UniversitySystemRegistration.Models.Entity;

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
            userData.role = Entity.Role.Student;
            var flag = userService.insertUserData(userData);
            return Json(new
            {
                result = flag,
                url = Url.Action("Login", "Login")
            });
        }

    }
}
