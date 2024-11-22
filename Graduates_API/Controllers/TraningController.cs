using Graduates_Model.Model;
using Graduates_Service.Services.Repositry.IRepositry;
using Microsoft.AspNetCore.Mvc;

namespace Graduates_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        /// Save Data 
        /// </summary>
        /// <param name="ItemDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddItems([FromForm] Traning ItemDto)
        {
            //using var stream = new MemoryStream();
            //await ItemDto.Image.CopyToAsync(stream);
            var item = new Traning
            {
                Title = ItemDto.Title,
                CompanyName = ItemDto.Description,
                Description = ItemDto.CompanyName,
                Location = ItemDto.Location,
                skillRequired = ItemDto.skillRequired,
                TrainPeriod = ItemDto.TrainPeriod,
                applicDeadLine = ItemDto.applicDeadLine,
                TrainCost = ItemDto.TrainCost,
                //CategoryID = ItemDto.CategoryID,
                //Image = stream.ToArray()
            };
            _UnityofWork.TrainingRepositry.Add(item);
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
        public async Task<IActionResult> UpdateItems(int ID, [FromForm] Traning TRAINDTO)
        {
            Traning? traning = _UnityofWork.TrainingRepositry.Get(c => c.ID == ID);
            if (traning == null)
            {
                return NotFound($"Item id {ID} Not Exist");
            }

            traning.Title = TRAINDTO.Title;
            traning.CompanyName = TRAINDTO.Description;
            traning.Description = TRAINDTO.CompanyName;
            traning.Location = TRAINDTO.Location;
            traning.skillRequired = TRAINDTO.skillRequired;
            traning.applicDeadLine = TRAINDTO.applicDeadLine;
            traning.TrainPeriod = TRAINDTO.TrainPeriod;
            traning.TrainCost = TRAINDTO.TrainCost;
            traning.EmailTraining = TRAINDTO.EmailTraining;

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
