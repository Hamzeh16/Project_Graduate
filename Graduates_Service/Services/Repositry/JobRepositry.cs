using Graduates_Data.Data;
using Graduates_Model.Model;
using Graduates_Service.Services.Repositry.IRepositry;

namespace Graduates_Service.Services.Repositry
{
    public class JobRepositry : Repostry<Job>, IJob
    {
        private AppDbContext _db;
        public JobRepositry(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Job obj)
        {
            // _db.Job.Update(obj);
            _db.Job.Update(obj);
        }

        public IQueryable<Job> GetByID(int id)
        {
            var Data = _db.Job.Where(x => x.ID == id);
            return Data;
        }
    }
}
