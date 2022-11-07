using Newtonsoft.Json;
using RepositoryLibrary.DataAccessLayer;
using System.Collections.Generic;
using System.Web.Mvc;
using UniversitySystemRegistration;
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
            var user = (User)Session["CurrentUser"];
            if (Session["CurrentUser"] != null && (UserRoles)Session["CurrentRole"] == UserRoles.Student)
            {
                var tuple = studentService.GetStudentDataFromDb(user);
                user = tuple.Item2;
                this.Session["CurrentUser"] = user;
                if (user.student == null)
                {
                    return View();
                }
                else
                {
                    return new RedirectResult("/StudentInterface/DetailsScreen");
                }
            }
            else
            {
                Session.Clear();
                return new RedirectResult("/Login/Login");
            }

        }

        public ActionResult DetailsScreen()
        {
            var user = (User)Session["CurrentUser"];
            if (Session["CurrentUser"] != null && (UserRoles)Session["CurrentRole"] == UserRoles.Student)
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
        public JsonResult GetSubjectList()
        {
            var list = studentService.GetListOfData(SqlQueries.getSubjectList);

            return Json(new
            {

                subjectList = JsonConvert.SerializeObject(list)
            });
        }
        [HttpPost]
        public JsonResult GetGradeList()
        {
            var list = studentService.GetListOfData(SqlQueries.getGradeList);

            return Json(new
            {
                subjectList = JsonConvert.SerializeObject(list)
            });
        }
        [HttpPost]
        public JsonResult GetStudentGuardianStatusInfo()
        {
            User user = (User)Session["CurrentUser"];
            var list = user.student.Subjects;

            return Json(new
            {
                subjectList = JsonConvert.SerializeObject(list)
            });
        }
        [HttpPost]
        public JsonResult GetStudentStudentSubjectList()
        {
            var result = true;
            User user = (User)Session["CurrentUser"];
            return Json(new
            {
                result = result,
                resultList = JsonConvert.SerializeObject(studentService.StudentInfoForDetailScreen(user))
            });
        }

        [HttpPost]
        public JsonResult SaveStudentSubjectGuardianInfo(List<Subject> subject, Guardian guardian)
        {
            var flag = true;
            var user = new User();
            user = (User)this.Session["CurrentUser"];
            Student stud = new Student();
            stud.StudentGuardianInfo = guardian;
            stud.StudentId = user.UserId;
            stud.Subjects = subject;
            user.student = stud;
            bool GuardianResponse = studentService.SaveStudentGuardian(user);
            bool SubjectResponse = studentService.SaveStudentSubject(user);
            var result = ((SubjectResponse || GuardianResponse) == false) ? flag = false : flag = true;
            return Json(new
            {
                result = flag,
            });
        }

    }
}