using Graduates_Data.Data;
using Graduates_Model.Model;
using Graduates_Service.Services.Repositry.IRepositry;

namespace Graduates_Service.Services.Repositry
{
    public class UnityofWork : IUnityofWork
    {
        private AppDbContext _db;
        public IApplicantRepositry ApplicantRepositry { get; set; }
        public ITraningRepositry TrainingRepositry { get; set; }
        public IJob JobRepositry { get; set; }

        public IApplicationForm AppFormRepositry { get; set; }

        public UnityofWork(AppDbContext db)
        {
            _db = db;
            ApplicantRepositry = new ApplicantRepositry(_db);
            TrainingRepositry = new Training(_db);
            JobRepositry = new JobRepositry(_db);
            AppFormRepositry = new ApplicationForms(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
