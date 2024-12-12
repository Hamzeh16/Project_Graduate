using Graduates_Model.Model;
using Graduates_Service.Services.Dto;
using Graduates_Service.Services.Repositry;
using Graduates_Service.Services.Repositry.IRepositry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Graduates_API.Controllers
{
    //[Authorize(Roles = "Admin")]
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

            return Ok(jobs);
        }
        // Approve a post request
        //[HttpPost]
        //public IActionResult PendingRequests(JobDto JobDTO, TraningDto TRAINDTO, int ID)
        //{
        //    if (ID == null)
        //    {
        //        return NotFound();
        //    }

        //    if (JobDTO.formType == "Job")
        //    {
        //        Job job = _UnityofWork.JobRepositry.Get(c => c.ID == ID);

        //        if (JobDTO != null)
        //        {
        //            if (JobDTO.status == "Approved")
        //            {

        //                // Sent Approved Email *******
        //                //SentEmail(applicantUser.Email);

        //            }
        //            else if (JobDTO.status == "Rejected")
        //            {
        //                // Handle reject logic

        //            }
        //            if (JobDTO.title != null)
        //                job.Title = JobDTO.title;
        //            if (JobDTO.companyName != null)
        //                job.CompanyName = JobDTO.companyName;
        //            if (JobDTO.description != null)
        //                job.Description = JobDTO.description;
        //            if (JobDTO.location != null)
        //                job.Location = JobDTO.location;
        //            if (JobDTO.email != null)
        //                job.EmailJob = JobDTO.email;
        //            if (JobDTO.qualifications != null)
        //                job.qualifications = JobDTO.qualifications;
        //            if (JobDTO.responsibilities != null)
        //                job.Responsibilities = JobDTO.responsibilities;
        //            if (JobDTO.applicationDeadLine != null)
        //                job.ApplicationDeadLine = JobDTO.applicationDeadLine;

        //            _UnityofWork.JobRepositry.Update(job);
        //        }
        //    }

        //    if (TRAINDTO.formType == "Internship")
        //    {
        //        Traning traning = _UnityofWork.TrainingRepositry.Get(c => c.ID == ID);
        //        if (TRAINDTO != null)
        //        {
        //            if (TRAINDTO.status == "Approved")
        //            {
        //                // Handle approve logic

        //            }
        //            else if (TRAINDTO.status == "Rejected")
        //            {
        //                // Handle reject logic

        //            }
        //            traning.Title = TRAINDTO.title;
        //            traning.CompanyName = TRAINDTO.companyName;
        //            traning.Description = TRAINDTO.description;
        //            traning.Location = TRAINDTO.location;
        //            traning.internshipType = TRAINDTO.internshipType;
        //            traning.applicationDeadline = TRAINDTO.applicationDeadline;
        //            traning.duration = TRAINDTO.duration;
        //            traning.Responsibilities = TRAINDTO.responsibilities;
        //            traning.qualifications = TRAINDTO.qualifications;

        //            _UnityofWork.TrainingRepositry.Update(traning);
        //        }
        //    }

        //    _UnityofWork.Save();

        //    return Ok();
        //}

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

        [HttpPatch("api/compny/Approve/{id}")]
        public IActionResult Approvecompny(string id)
        {
            var job = _UnityofWork.ApplicantRepositry.Get(c => c.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            // Change status to Approved
            job.REQUIST = true;

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

        [HttpPatch("api/company/Reject/{id}")]
        public IActionResult Rejectcompany(string id)
        {
            var job = _UnityofWork.ApplicantRepositry.Get(c => c.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            // Change status to Rejected
            job.REQUIST = false;

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
        public IActionResult SentEmail(string email)
        {
            var message =
                new Messsage(new string[]
                { email }, "Confirm", "Account is Approved!");

            _emailService.SendEmail(message);

            return StatusCode(StatusCodes.Status200OK,
            new Respone() { Status = "Succes", Message = "Email Sent Succesfully!" });
        }
    }
}
