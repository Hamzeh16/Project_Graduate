using Graduates_Model.Model;
using Graduates_Service.Services.Dto;
using Graduates_Service.Services.Repositry.IRepositry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Graduates_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [Authorize]
        [HttpGet]
        //[Authorize(Roles = "Company")]
        public IActionResult GetAllItems()
        {
            var email = HttpContext.Session.GetString("Email");
            List<ApplicationForm> objList = _UnityofWork.AppFormRepositry.GetAll().ToList();
            objList = objList.Where(x => x.emailCompany == email).ToList();
            return Ok(objList);
        }


        [Authorize]
        [HttpPost("Add")]
        public async Task<IActionResult> AddItems([FromForm] ApplicationFormDto appFormDto)
        {
            var id = HttpContext.Request.Headers["id"].ToString();

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

                var baseUrl = $"{Request.Scheme}://{Request.Host}/uploads/";


                List<Job> applicationForms = _UnityofWork.JobRepositry.GetAll().ToList();
                var applicationForm = applicationForms.Where(a => a.ID.ToString() == id).FirstOrDefault();

                // إنشاء الكائن الذي سيتم تخزينه في قاعدة البيانات
                var item = new ApplicationForm
                {
                    name = appFormDto.name,
                    email = appFormDto.email,
                    phone = appFormDto.phone,
                    emailCompany = applicationForm.EmailCompany,
                    Address = appFormDto.address,
                    cv = baseUrl + appFormDto.resume.FileName,  // تخزين المسار الفعلي للسيرة الذاتية
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

            appform.name = appFormDto.name;
            appform.email = appFormDto.email;
            appform.phone = appFormDto.phone;
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