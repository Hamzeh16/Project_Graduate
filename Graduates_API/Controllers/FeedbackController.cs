using System.Collections.Generic;
using Graduates_Model.Model;
using Graduates_Service.Services.Dto;
using Graduates_Service.Services.Repositry.IRepositry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Graduates_API.Controllers
{
    public class FeedbackController : Controller
    {
        public FeedbackController(IUnityofWork UnityofWork)
        {
            _UnityofWork = UnityofWork;
        }
        private readonly IUnityofWork _UnityofWork;

        /// <summary>
        /// Get All Feedback
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllFeedback")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllFeedback()
        {
            List<Feedback> objList = _UnityofWork.FeedbackRepositry.GetAll().ToList();
            return Ok(objList);
        }


        /// <summary>
        /// Get All Company
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetALlCompany")]
        //[Authorize]
        public IActionResult GetALlCompany()
        {
            List<ApplicantUser> objList = _UnityofWork.ApplicantRepositry.GetAll().ToList().Where(x => x.APPLICANTTYPE == "Company").ToList();
            return Ok(objList);
        }

        /// <summary>
        /// Get Feedback By ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetFeedbackByID")]
        [Authorize(Roles = "Company")]
        public IActionResult GetFeedbackByID(string ID)
        {
            List<Feedback> objList = _UnityofWork.FeedbackRepositry.GetAll().Where(c => c.CompanyId == ID).ToList();
            return Ok(objList);
        }

        [HttpPost("AddFeedback")]
        //[Authorize]
        public async Task<IActionResult> AddFeedback([FromBody] FeedbackDto feedbackDto)
        {
            try
            {
                var Feedback = new Feedback
                {
                    Title = feedbackDto.Title,
                    Rating = feedbackDto.Rating,
                    Message = feedbackDto.Message,
                    CompanyId = feedbackDto.CompanyId,
                    CreatedDate = DateTime.UtcNow,
                };

                _UnityofWork.FeedbackRepositry.Add(Feedback);
                _UnityofWork.Save();

                return Ok(new { message = "Feedback submitted successfully", Feedback });
            }
            catch (Exception ex)
            {
                //_logger.LogError($"An error occurred: {ex.Message}");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        /// <summary>
        /// Delete Data
        /// </summary>
        /// <param name = "ID" ></ param >
        /// < returns ></ returns >
        [HttpDelete("DeleteFeedback")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteFeedback(int ID)
        {
            Feedback? appform = _UnityofWork.FeedbackRepositry.Get(c => c.Id == ID);
            if (appform == null)
            {
                return NotFound($"Item id {ID} Not Exist");
            }
            _UnityofWork.FeedbackRepositry.Remove(appform);
            _UnityofWork.Save();
            return Ok(appform);
        }
    }
}
