using Graduates_Model.Model;
using Graduates_Service.Services.Dto;
using Graduates_Service.Services.Repositry.IRepositry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Graduates_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Company")]
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
        public async Task<IActionResult> AddItems([FromForm] JobDto ItemDto)
        {
            //using var stream = new MemoryStream();
            //await ItemDto.Image.CopyToAsync(stream);
            var item = new Job
            {
                Title = ItemDto.title,
                CompanyName = ItemDto.description,
                Description = ItemDto.CompanyName,
                Location = ItemDto.location ,
                EmailJob = ItemDto.email,
                qualifications = ItemDto.qualifications,
                ApplicationDeadLine = ItemDto.applicationDeadLine,
                Responsibilities= ItemDto.responsibilities,
                JobType= ItemDto.jobType,
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
        public async Task<IActionResult> UpdateItems(int ID, [FromForm] JobDto ITEMDTO)
        {
            Job? job = _UnityofWork.JobRepositry.Get(c => c.ID == ID);
            if (job == null)
            {
                return NotFound($"Item id {ID} Not Exist");
            }

            job.Title = ITEMDTO.title;
            job.CompanyName = ITEMDTO.description;
            job.Description = ITEMDTO.CompanyName;
            job.Location = ITEMDTO.location;
            job.EmailJob = ITEMDTO.email;
            job.qualifications = ITEMDTO.qualifications;
            job.ApplicationDeadLine = ITEMDTO.applicationDeadLine;

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