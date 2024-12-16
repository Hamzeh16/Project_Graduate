using Graduates_Model.Model;
using Graduates_Service.Services.Dto;
using Graduates_Service.Services.Repositry.IRepositry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Graduates_API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        public AdminController(IUnityofWork UnityofWork , IEmailService emailService)
        {
            _UnityofWork = UnityofWork;
            _emailService = emailService;
        }
        private readonly IUnityofWork _UnityofWork;
        private readonly IEmailService _emailService;

        /// <summary>
        /// Get All Data
        /// </summary>
        /// <returns></returns>
        [HttpGet("Accepted")]
        public IActionResult GetJobPostsAccepted()
        {
            List<Job> objJobList = _UnityofWork.JobRepositry.GetAll().ToList();
            //objJobList = objJobList.Where(x => x.CompanyName == CompanyName).ToList();

            // جلب الوظائف
            var jobs = _UnityofWork.JobRepositry.GetAll();
            jobs = jobs.Where(x => x.status == "Approved");

            // جلب الفرص التدريبية
            var internships = _UnityofWork.TrainingRepositry.GetAll();
            internships = internships.Where(x => x.status == "Approved");

            // Combine into a single list of type object
            var combinedList = new List<object>();

            // Add all jobs to the combined list
            combinedList.AddRange(jobs);

            // Add all internships to the combined list
            combinedList.AddRange(internships);

            return Ok(combinedList);
        }

        /// <summary>
        /// Get All Data
        /// </summary>
        /// <returns></returns>
        [HttpGet("Rejected")]
        public IActionResult GetJobPostsRejected()
        {
            List<Job> objJobList = _UnityofWork.JobRepositry.GetAll().ToList();
            //objJobList = objJobList.Where(x => x.CompanyName == CompanyName).ToList();

            // جلب الوظائف
            var jobs = _UnityofWork.JobRepositry.GetAll();
            jobs = jobs.Where(x => x.status == "Rejected");

            // جلب الفرص التدريبية
            var internships = _UnityofWork.TrainingRepositry.GetAll();
            internships = internships.Where(x => x.status == "Rejected");

            // Combine into a single list of type object
            var combinedList = new List<object>();

            // Add all jobs to the combined list
            combinedList.AddRange(jobs);

            // Add all internships to the combined list
            combinedList.AddRange(internships);

            return Ok(combinedList);
        }

        /// <summary>
        /// Get All Data
        /// </summary>
        /// <returns></returns>
        [HttpGet("Pending")]
        public IActionResult GetJobPostsPending()
        {
            // جلب الوظائف
            var jobs = _UnityofWork.JobRepositry.GetAll();
            jobs = jobs.Where(x => x.status == "Pending");

            return Ok(jobs);
        }

        // <summary>
        /// Get All Data
        /// </summary>
        /// <returns></returns>
        [HttpGet("Account/Pending")]
        public IActionResult GetAccountPostsPending()
        {
            // جلب الوظائف
            var jobs = _UnityofWork.ApplicantRepositry.GetAll();
            jobs = jobs.Where(x => x.REQUIST == null);

            // Create a collection of JobDto objects
            var jobDtos = jobs.Select(job => new JobDto
            {
                companyName = job.UserName,  // Assuming job.UserName contains the company name
                crFile = job.IMAGEURL,    // Example additional property
                email = job.Email
            }).ToList();

            return Ok(jobDtos);
        }

        [HttpPatch("Job/Approve/{id}")]
        public IActionResult ApproveJob(int id)
        {
            var job = _UnityofWork.JobRepositry.Get(c => c.ID == id);
            if (job == null)
            {
                return NotFound();
            }

            // Change status to Approved
            job.status = "Approved";

            _UnityofWork.JobRepositry.Update(job);

            try
            {
                _UnityofWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, "Internal server error");
            }

            return Ok();
        }

        [HttpPatch("Job/Reject/{id}")]
        public IActionResult RejectJob(int id)
        {
            var job = _UnityofWork.JobRepositry.Get(c => c.ID == id);
            if (job == null)
            {
                return NotFound();
            }

            // Change status to Rejected
            job.status = "Rejected";

            _UnityofWork.JobRepositry.Update(job);

            try
            {
                _UnityofWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, "Internal server error");
            }

            return Ok();
        }

        [HttpPatch("compny/Approve")]
        public IActionResult Approvecompny(string email)
        {
            var job = _UnityofWork.ApplicantRepositry.Get(c => c.Email == email);
            if (job == null)
            {
                return NotFound();
            }

            // Change status to Approved
            job.REQUIST = true;

            SentEmail(job);

            _UnityofWork.ApplicantRepositry.Update(job);

            try
            {
                _UnityofWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, "Internal server error");
            }

            return Ok();
        }

        [HttpPatch("company/Reject")]
        public IActionResult Rejectcompany(string email)
        {
            var job = _UnityofWork.ApplicantRepositry.Get(c => c.Email == email);
            if (job == null)
            {
                return NotFound();
            }

            // Change status to Rejected
            job.REQUIST = false;

            SentEmail(job);

            _UnityofWork.ApplicantRepositry.Update(job);

            try
            {
                _UnityofWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, "Internal server error");
            }

            return Ok();
        }



        [HttpGet("SentEmail")]
        public IActionResult SentEmail(ApplicantUser obj)
        {
            if (obj.REQUIST == true)
            {
                var message =
                new Messsage(new string[]
                { obj.Email }, "Your Company Registration is Approved",
                $"Dear [{obj.UserName}],\r\n\r\nWe are pleased to inform you that your company registration has been approved. You can now log in and start using our services." +
                $"\r\n\r\n[http://localhost:5173/login]\r\n\r\n" +
                $"Thank you for choosing us.\r\nBest regards,\r\n[{obj.UserName}]");

                _emailService.SendEmail(message);
            }
            else
            {
                var message =
                new Messsage(new string[]
                { obj.Email }, "Your Company Registration Request",
                $"Dear [{obj.UserName}],\r\n\r\nWe regret to inform you that your company registration request has been rejected. For more details, please contact our support team: [CareeerPathhub@gmail.com].\r\n\r\nBest regards,\r\n[{obj.UserName}]");

                _emailService.SendEmail(message);
            }
            
            return StatusCode(StatusCodes.Status200OK,
            new Respone() { Status = "Succes", Message = "Email Sent Succesfully!" });
        }
    }
}
