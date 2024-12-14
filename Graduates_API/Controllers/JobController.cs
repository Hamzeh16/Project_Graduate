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
        [Authorize]
        [HttpGet]
        public IActionResult GetJobPosts()
        {
            List<Job> objJobList = _UnityofWork.JobRepositry.GetAll().ToList();
            return Ok(objJobList);
        }

        [HttpGet("GetJobsAndInternships")]
        public async Task<IActionResult> GetJobsAndInternships()
        {
            // جلب الوظائف
            var jobs = _UnityofWork.JobRepositry.GetAll();

            // جلب الفرص التدريبية
            var internships = _UnityofWork.TrainingRepositry.GetAll();

            // دمج البيانات مع إضافة النوع (وظيفة أو تدريب)
            var combinedData = jobs.Select(job => new
            {
                job.Title,
                job.CompanyName,
                job.Description,
                job.Location,
            }).ToList();

            combinedData.AddRange(internships.Select(internship => new
            {
                internship.Title,
                internship.CompanyName,
                internship.Description,
                internship.Location,
            }));

            return Ok(combinedData);
        }

        /// <summary>
        /// Get By ID
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetByID")]
        public IActionResult GetJobPosts(int ID)
        {
            Job objJobList = _UnityofWork.JobRepositry.Get(c => c.ID == ID);
            return Ok(objJobList);
        }

        /// <summary>
        /// Save Data 
        /// </summary>
        /// <param name="ItemDto"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> AddItems([FromBody] JobDto ItemDto)
        {
            var item = new Job
            {
                Title = ItemDto.title,
                CompanyName = ItemDto.description,
                Description = ItemDto.companyName,
                Location = ItemDto.location ,
                EmailJob = ItemDto.email,
                qualifications = ItemDto.qualifications,
                ApplicationDeadLine = ItemDto.applicationDeadLine,
                Responsibilities= ItemDto.responsibilities,
                JobType= ItemDto.jobType,
                formType = ItemDto.formType,
                status = "Pending",
                internshipType= ItemDto.internshipType,
                duration= ItemDto.duration,
            };
            _UnityofWork.JobRepositry.Add(item);
            _UnityofWork.Save();
            return Ok(item);
        }

        /// <summary>
        /// Update Data
        /// </summary>
        /// <param name = "ID" ></ param >
        /// < param name="JobDTO"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateItems(int ID, [FromBody] JobDto JobDTO)
        {
            Job? job = _UnityofWork.JobRepositry.Get(c => c.ID == ID);
            if (job == null)
            {
                return NotFound($"Item id {ID} Not Exist");
            }

            if(JobDTO.title != null)
                job.Title = JobDTO.title;
            if (JobDTO.companyName != null)
                job.CompanyName = JobDTO.companyName;
            if (JobDTO.description != null)
                job.Description = JobDTO.description;
            if (JobDTO.location != null)
                job.Location = JobDTO.location;
            if (JobDTO.email != null)
                job.EmailJob = JobDTO.email;
            if (JobDTO.qualifications != null)
                job.qualifications = JobDTO.qualifications;
            if (JobDTO.responsibilities != null)
                job.Responsibilities = JobDTO.responsibilities;
            if (JobDTO.applicationDeadLine != null)
                job.ApplicationDeadLine = JobDTO.applicationDeadLine;
            if (JobDTO.internshipType != null)
                job.internshipType = JobDTO.internshipType;
            if (JobDTO.duration != null)
                job.duration = JobDTO.duration;

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