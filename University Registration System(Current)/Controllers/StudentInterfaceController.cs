using System.Web.Mvc;

namespace University_Registration_System_Current_.Controllers
{
    public class StudentInterfaceController : Controller
    {
        // GET: StudentInterface
        public ActionResult Main()
        {
            return View();
        }

        public ActionResult GetSubjectList()
        {


            return Json(new
            {
                
                url = Url.Action("Main", "RequestInterface"),

            });
        }
    }
}