using System.Web.Mvc;
using UniversitySystemRegistration.Business_Logic;

namespace University_Registration_System_Current_.Controllers
{
    public class RequestInterfaceController : Controller
    {

        public readonly IUserServices userService;

        public RequestInterfaceController(IUserServices _userService)
        {
            userService = _userService;
        }
        // GET: RequestInterface
        public ActionResult Main()
        {
            return View();
        }



        [HttpGet]
        public JsonResult GetSessionRole()
        {
            string _role="";
            bool _flag = false;
            if (!(this.Session["CurrentRole"].ToString()=="")) {
                _role = this.Session["CurrentRole"].ToString();
                _flag = true;
            }
            
            return Json(new
            {
                flag = _flag,
                role = _role
            });
        }

        [HttpGet]
        public JsonResult GetSelectedStudents()
        {
            string _dataTableHTML=userService.ConvertDataTableToHTML(userService.GetSelectedStudents());

            return Json(new
            {
                dataTableHTML = _dataTableHTML
            });
        }

        [HttpGet]
        public JsonResult ReturnAllStudents()
        {
            string _dataTableHTML = userService.ConvertDataTableToHTML(userService.GetSelectedStudents());

            return Json(new
            {
                dataTableHTML = _dataTableHTML
            });
        }


    }
}