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
        /// <param name="ID"></param>
        /// <param name="ITEMDTO"></param>
        /// <returns></returns>
        //[HttpPut("{ID}")]
        //public async Task<IActionResult> UpdateItems(int ID, [FromForm] Traning ITEMDTO)
        //{
        //var Item = await _db.Items.FindAsync(ID);
        //if (Item == null)
        //{
        //    return NotFound($"Item id {ID} Not Exist");
        //}
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
        //       Title = ItemDto.Title,
        //        CompanyName = ItemDto.Description,
        //        Description = ItemDto.CompanyName,
        //        Location = ItemDto.Location,
        //        skillRequired = ItemDto.skillRequired,
        //        TrainPeriod = ItemDto.TrainPeriod,
        //        applicDeadLine = ItemDto.applicDeadLine,
        //        TrainCost = ItemDto.TrainCost,
                //CategoryID = ItemDto.CategoryID,
                //Image = stream.ToArray()

        //_UnityofWork.Save();
        //return Ok(Item);
        //}

        /// <summary>
        /// Delete Data
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        //[HttpDelete]
        //public async Task<IActionResult> DeleteItems(int ID)
        //{
        //    var Item = await _UnityofWork.TrainingRepositry.Get(ID);
        //    if (Item == null)
        //    {
        //        return NotFound($"Item id {ID} Not Exist");
        //    }
        //    _UnityofWork.TrainingRepositry.Remove(Item);
        //    _UnityofWork.Save();
        //    return Ok(Item);
        //}
    }
}
