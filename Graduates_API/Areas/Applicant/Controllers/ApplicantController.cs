using Microsoft.AspNetCore.Mvc;

namespace Graduates_API.Areas.Applicant.Controllers
{
    [Area("Applicant")]
    public class ApplicantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
