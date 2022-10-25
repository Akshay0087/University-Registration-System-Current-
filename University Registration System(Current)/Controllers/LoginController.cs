using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using University_Registration_System_Current_.Business_Logic;
using UniversitySystemRegistration.Business_Logic;
using UniversitySystemRegistration.Models.Entity;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Student Student)
        {
            
            return View();
        }

        
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
              url = Url.Action("Index", "RequestInterface"),
              
            });
        }
        

    }
}