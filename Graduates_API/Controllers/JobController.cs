using Graduates_Model.Model;
using Graduates_Service.Services.Repositry;
using Graduates_Service.Services.Repositry.IRepositry;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

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
        [HttpPost]
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
                Email = ItemDto.Email,
                Qalification =ItemDto.Qalification,
                JobDeadLine = ItemDto.JobDeadLine,
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
        [HttpPut("{ID}")]
        public async Task<IActionResult> UpdateItems(int ID, [FromForm] Job ITEMDTO)
        {
            var Item = _UnityofWork.JobRepositry.GetByID(ID);
            if (Item == null)
            {
                return NotFound($"Item id {ID} Not Exist");
            }
            //var IsExistCategoty = _db.Categories.AnyAsync(x => x.ID == ITEMDTO.CategoryID);
            //if (IsExistCategoty == null)
            //{
            //    return NotFound($"Category id {ID} Not Exist");
            //}
            //if (Item.Image != null)
            //{
            //    using var stream = new MemoryStream();
            //    await ITEMDTO.Image.CopyToAsync(stream);
            //    Item.Image = stream.ToArray();
            //}
                //Title = ItemDto.Title,
                //CompanyName = ItemDto.Description,
                //Description = ItemDto.CompanyName,
                //Location = ItemDto.Location,
                //Email = ItemDto.Email,
                //Qalification = ItemDto.Qalification,
                //JobDeadLine = ItemDto.JobDeadLine,
                
        _UnityofWork.Save();
            return Ok(Item);
        }

        /// <summary>
        /// Delete Data
        /// </summary>
        /// <param name = "ID" ></ param >
        /// < returns ></ returns >
        //[HttpDelete]
        //public async Task<IActionResult> DeleteItems(int ID)
        //{
        //    var Item =  _UnityofWork.JobRepositry.GetByID(ID);
        //    if (Item == null)
        //    {
        //        return NotFound($"Item id {ID} Not Exist");
        //    }
        //    _UnityofWork.JobRepositry.Remove(Item);
        //    _UnityofWork.Save();
        //    return Ok(Item);
        //}
    }

}

