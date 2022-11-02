using Newtonsoft.Json;
using RepositoryLibrary.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using UniversitySystemRegistration.Models;
using UniversitySystemRegistration.Services;

namespace University_Registration_System_Current_.Controllers
{
    public class StudentInterfaceController : Controller
    {
        public readonly IStudentServices studentService;

        public StudentInterfaceController(IStudentServices studentService)
        {
            this.studentService = studentService;
        }

        // GET: StudentInterface
        public ActionResult Main()
        {
            if (Session["CurrentUser"] == null)
            {
                return new RedirectResult("/Login/Login");
            }
            else { return View(); }

        }

        public ActionResult DetailScreen()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetSubjectList()
        {
            List<string> list = new List<string>();
            list = studentService.GetSubjectAndGradeList(SqlQueries.getSubjectList);

            return Json(new
            {

                subjectList = JsonConvert.SerializeObject(list)
            }); 
        }
        [HttpPost]
        public ActionResult GetGradeList()
        {
            List<string> list = new List<string>();
            list = studentService.GetSubjectAndGradeList(SqlQueries.getGradeList);

            return Json(new{subjectList = JsonConvert.SerializeObject(list)}); 
        }
        [HttpPost]
        public ActionResult SaveStudentSubjectGuardianInfo(List<Subject> subject,Guardian guardian)
        {
            var flag = true;
       

            return Json(new
            {
                result = flag,
                

            });
        }

       
    }
}