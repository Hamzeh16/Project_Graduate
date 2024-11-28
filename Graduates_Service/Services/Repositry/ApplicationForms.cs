using Graduates_Data.Data;
using Graduates_Model.Model;
using Graduates_Service.Services.Repositry.IRepositry;

namespace Graduates_Service.Services.Repositry
{
    public class ApplicationForms : Repostry<ApplicationForm>, IApplicationForm
    {
        private AppDbContext _db;
        public ApplicationForms(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ApplicationForm obj)
        {
            _db.ApplicationForms.Update(obj);
        }
    }
}
