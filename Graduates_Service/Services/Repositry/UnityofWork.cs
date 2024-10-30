using Graduates_Data.Data;
using Graduates_Service.Services.Repositry.IRepositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduates_Service.Services.Repositry
{
    public class UnityofWork : IUnityofWork
    {
        private AppDbContext _db;
        public IApplicantRepositry ApplicantRepositry { get; set; }

        public UnityofWork(AppDbContext db)
        {
            _db = db;
            ApplicantRepositry = new ApplicantRepositry(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
