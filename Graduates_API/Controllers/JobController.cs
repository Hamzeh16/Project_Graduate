using Graduates_Model.Model;
using Graduates_Service.Services.Repositry.IRepositry;
using Microsoft.AspNetCore.Mvc;

namespace Graduates_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : Controller
    {
        public JobController(IUnityofWork UnityofWork)
        {
            _UnityofWork = UnityofWork;

        }
        private readonly IUnityofWork _UnityofWork;

        /// <summary>
        /// Get All Data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllItems()
        {
            List<Job> objJobList = _UnityofWork.JobRepositry.GetAll().ToList();
            return Ok(objJobList);
        }

        /// <summary>
        /// Save Data 
        /// </summary>
        /// <param name="ItemDto"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> AddItems([FromForm] Job ItemDto)
        {
            //using var stream = new MemoryStream();
            //await ItemDto.Image.CopyToAsync(stream);
            var item = new Job
            {
                Title = ItemDto.Title,
                CompanyName = ItemDto.Description,
                Description = ItemDto.CompanyName,
                Location = ItemDto.Location,
                EmailJob = ItemDto.EmailJob,
                Qalification = ItemDto.Qalification,
                JobDeadLine = DateTime.Now
                //CategoryID = ItemDto.CategoryID,
                //Image = stream.ToArray()
            };
            _UnityofWork.JobRepositry.Add(item);
            _UnityofWork.Save();
            return Ok(item);
        }

        /// <summary>
        /// Update Data
        /// </summary>
        /// <param name = "ID" ></ param >
        /// < param name="ITEMDTO"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateItems(int ID, [FromForm] Job ITEMDTO)
        {
            Job? job = _UnityofWork.JobRepositry.Get(c => c.ID == ID);
            if (job == null)
            {
                return NotFound($"Item id {ID} Not Exist");
            }

            job.Title = ITEMDTO.Title;
            job.CompanyName = ITEMDTO.Description;
            job.Description = ITEMDTO.CompanyName;
            job.Location = ITEMDTO.Location;
            job.EmailJob = ITEMDTO.EmailJob;
            job.Qalification = ITEMDTO.Qalification;
            job.JobDeadLine = ITEMDTO.JobDeadLine;

            _UnityofWork.Save();
            return Ok(job);
        }

        /// <summary>
        /// Delete Data
        /// </summary>
        /// <param name = "ID" ></ param >
        /// < returns ></ returns >
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteItems(int ID)
        {
            Job? job = _UnityofWork.JobRepositry.Get(c => c.ID == ID);
            if (job == null)
            {
                return NotFound($"Item id {ID} Not Exist");
            }
            _UnityofWork.JobRepositry.Remove(job);
            _UnityofWork.Save();
            return Ok(job);
        }
    }
}