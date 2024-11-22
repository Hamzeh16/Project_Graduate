using Graduates_Data.Data;
using Graduates_Model.Model;
using Graduates_Service.Services.Repositry.IRepositry;

namespace Graduates_Service.Services.Repositry
{
    public class Training : Repostry<Traning>, ITraningRepositry
    {
        private AppDbContext _db;
        public Training(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Traning obj)
        {
            _db.Traning.Update(obj);
        }
    }
}

