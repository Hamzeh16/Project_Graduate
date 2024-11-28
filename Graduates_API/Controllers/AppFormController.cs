using Graduates_Model.Model;
using Graduates_Service.Services.Dto;
using Graduates_Service.Services.Repositry.IRepositry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Graduates_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppFormController : Controller
    {
        public AppFormController(IUnityofWork UnityofWork)
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
            List<ApplicationForm> objList = _UnityofWork.AppFormRepositry.GetAll().ToList();
            return Ok(objList);
        }

        /// <summary>
        /// Save Data 
        /// </summary>
        /// <param name="ItemDto"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> AddItems([FromForm] ApplicationFormDto appFormDto)
        {
            var item = new ApplicationForm
            {
                YourName = appFormDto.YourName,
                YourEmail = appFormDto.YourEmail,
                PhoneNumber = appFormDto.PhoneNumber,
                Address = appFormDto.Address,
                ImageUrl = appFormDto.ImageUrl,
            };
            _UnityofWork.AppFormRepositry.Add(item);
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
        public async Task<IActionResult> UpdateItems(int ID, [FromForm] ApplicationFormDto appFormDto)
        {
            ApplicationForm? appform = _UnityofWork.AppFormRepositry.Get(c => c.ID == ID);
            if (appform == null)
            {
                return NotFound($"Item id {ID} Not Exist");
            }

            appform.YourName = appFormDto.YourName;
            appform.YourEmail = appFormDto.YourEmail;
            appform.PhoneNumber = appFormDto.PhoneNumber;
            appform.Address = appFormDto.Address;
            appform.ImageUrl = appFormDto.ImageUrl;

            _UnityofWork.Save();
            return Ok(appform);
        }

        /// <summary>
        /// Delete Data
        /// </summary>
        /// <param name = "ID" ></ param >
        /// < returns ></ returns >
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteItems(int ID)
        {
            ApplicationForm? appform = _UnityofWork.AppFormRepositry.Get(c => c.ID == ID);
            if (appform == null)
            {
                return NotFound($"Item id {ID} Not Exist");
            }
            _UnityofWork.AppFormRepositry.Remove(appform);
            _UnityofWork.Save();
            return Ok(appform);
        }
    }
}