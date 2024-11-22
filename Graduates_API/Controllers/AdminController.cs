using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Graduates_API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        [HttpGet("Employee")]
        public IEnumerable<string> Get()
        {
            return new List<string> { "Chips", "Ram", "ALU" };
        }
    }
}
