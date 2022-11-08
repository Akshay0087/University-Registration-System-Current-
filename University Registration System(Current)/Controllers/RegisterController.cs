using Newtonsoft.Json;
using System.Web.Mvc;
using UniversitySystemRegistration.Models;
using UniversitySystemRegistration.Services;

namespace UniversitySystemRegistration.Controllers
{
    [HandleError]
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
            userData.UserRole = UserRoles.Student;
            var userCheckResult=userService.UserCheck(userData);
            var dataCase = "insert";
         
            
            if (!userCheckResult.Item1)
            {
                dataCase = "uniquenessError";
                return Json(new{
                    data = dataCase
                },
                JsonRequestBehavior.AllowGet
                ) ;
            }
            else if(!userCheckResult.Item2)
            {
                dataCase = "validationError";
                return Json(new{ 
                    data = dataCase,
                    result = JsonConvert.SerializeObject(userCheckResult.Item3)}
                );
            }
            else if(userCheckResult.Item1&&userCheckResult.Item2){
                var flag = userService.insertUserData(userData);
                return Json(new {
                    data = dataCase, 
                    status = flag }
                ) ;
            }
            else {
                bool status = false; 
                return Json(new { 
                    status =status }
                ); 
            }
        }

    }
}
