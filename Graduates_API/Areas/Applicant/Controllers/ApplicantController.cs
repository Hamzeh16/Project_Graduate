using Microsoft.AspNetCore.Mvc;

namespace Graduates_API.Areas.Applicant.Controllers
{
    public class ApplicantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
