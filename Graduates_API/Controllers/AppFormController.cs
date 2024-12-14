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

        [HttpPost("Add")]
        public async Task<IActionResult> AddItems([FromForm] ApplicationFormDto appFormDto)
        {
            try
            {
                // التحقق من أن الملف تم استلامه
                if (appFormDto.resume == null || appFormDto.resume.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                // تحديد المجلد الذي سيتم تخزين الملفات فيه
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

                // التأكد من أن المجلد موجود
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // تحديد المسار الذي سيتم تخزين السيرة الذاتية فيه
                var filePath = Path.Combine(uploadsFolder, appFormDto.resume.FileName);

                // تخزين الملف في المجلد
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await appFormDto.resume.CopyToAsync(stream);  // نسخ محتويات الملف إلى المجلد
                }

                // إنشاء الكائن الذي سيتم تخزينه في قاعدة البيانات
                var item = new ApplicationForm
                {
                    YourName = appFormDto.name,
                    YourEmail = appFormDto.email,
                    PhoneNumber = appFormDto.phone,
                    Address = appFormDto.address,
                    ImageUrl = filePath,  // تخزين المسار الفعلي للسيرة الذاتية
                };

                // إضافة السجل إلى قاعدة البيانات
                _UnityofWork.AppFormRepositry.Add(item);
                 _UnityofWork.Save();

                return Ok(new { message = "Application submitted successfully", item });
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ في السجلات
                //_logger.LogError($"An error occurred: {ex.Message}");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
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

            appform.YourName = appFormDto.name;
            appform.YourEmail = appFormDto.email;
            appform.PhoneNumber = appFormDto.phone;
            appform.Address = appFormDto.address;
            //appform.ImageUrl = appFormDto.resume;

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