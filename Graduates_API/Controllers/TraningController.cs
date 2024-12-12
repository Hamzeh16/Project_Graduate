using Graduates_Model.Model;
using Graduates_Service.Services.Dto;
using Graduates_Service.Services.Repositry.IRepositry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Graduates_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Company")]
    public class TraningController : Controller
    {
        public TraningController(IUnityofWork UnityofWork)
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
            List<Traning> objtraningList = _UnityofWork.TrainingRepositry.GetAll().ToList();
            return Ok(objtraningList);
        }

        /// <summary>
        /// Get By ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetByID")]
        public IActionResult GetTrainPosts(int ID)
        {
            Traning objJobList = _UnityofWork.TrainingRepositry.Get(c => c.ID == ID);
            return Ok(objJobList);
        }

        /// <summary>
        /// Save Data 
        /// </summary>
        /// <param name="ItemDto"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> AddItems([FromBody] TraningDto ItemDto)
        {
            var item = new Traning
            {
                Title = ItemDto.title,
                CompanyName = ItemDto.companyName,
                Description = ItemDto.description,
                Location = ItemDto.location,
                internshipType = ItemDto.internshipType,
                duration = ItemDto.duration,
                applicationDeadline = ItemDto.applicationDeadline,
                Responsibilities = ItemDto.responsibilities,
                qualifications = ItemDto.qualifications,
                formType = ItemDto.formType,
                status = "Pending"
            };
            _UnityofWork.TrainingRepositry.Add(item);
            _UnityofWork.Save();
            return Ok(item);
        }

        /// <summary>
        /// Update Data
        /// </summary>
        /// <param name = "ID" ></ param >
        /// < param name="TRAINDTO"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateItems(int ID, [FromForm] TraningDto TRAINDTO)
        {
            Traning? traning = _UnityofWork.TrainingRepositry.Get(c => c.ID == ID);
            if (traning == null)
            {
                return NotFound($"Item id {ID} Not Exist");
            }

            traning.Title = TRAINDTO.title;
            traning.CompanyName = TRAINDTO.companyName;
            traning.Description = TRAINDTO.description;
            traning.Location = TRAINDTO.location;
            traning.internshipType = TRAINDTO.internshipType;
            traning.applicationDeadline = TRAINDTO.applicationDeadline;
            traning.duration = TRAINDTO.duration;
            traning.Responsibilities = TRAINDTO.responsibilities;
            traning.qualifications = TRAINDTO.qualifications;
            _UnityofWork.Save();
            return Ok(traning);
        }

        /// <summary>
        /// Delete Data
        /// </summary>
        /// <param name = "ID" ></ param >
        /// < returns ></ returns >
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteItems(int ID)
        {
            Traning? traning = _UnityofWork.TrainingRepositry.Get(c => c.ID == ID);
            if (traning == null)
            {
                return NotFound($"Item id {ID} Not Exist");
            }
            _UnityofWork.TrainingRepositry.Remove(traning);
            _UnityofWork.Save();
            return Ok(traning);
        }
    }
}
