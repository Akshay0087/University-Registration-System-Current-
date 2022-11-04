using ServiceLibrary.Services;
using System.Net.NetworkInformation;
using System.Web.Mvc;
using UniversitySystemRegistration;
using UniversitySystemRegistration.Models;

namespace University_Registration_System_Current_.Controllers
{
    public class AdminController : Controller 
    { 

        private readonly IAdminServices adminService;

        public AdminController(IAdminServices _adminService)
        {
            adminService = _adminService;
        }
        
        // GET: Admin
        public ActionResult AdminPanel()
        {
            var user = (User)Session["CurrentUser"];
            if (Session["CurrentUser"] != null && (UserRoles)Session["CurrentRole"] == UserRoles.Admin)
            {

                return View();
            }
            else
            {
                Session.Clear();
                return new RedirectResult("/Login/Login");
            }
        }
        [HttpPost]
        public JsonResult GetStudentStatusList()
        {
            var IsStatus = true;
            var result = adminService.GetListOfStudentStatus();
            return Json(new
            {
                status = IsStatus,
                approvedList = result.Item1,
                waitingList = result.Item2,
                rejectedList = result.Item3,
            },JsonRequestBehavior.AllowGet) ;
        }

        [HttpPost]
        public JsonResult ReloadStudentStatus()
        {
            var result=adminService.SetStudentRegistrationStatus();
            return Json(new
            {
                result = result,
               
            });
        }
    }
}