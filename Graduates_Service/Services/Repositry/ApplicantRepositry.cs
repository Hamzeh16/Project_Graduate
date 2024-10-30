using Graduates_Data.Data;
using Graduates_Model.Model;
using Graduates_Service.Services.Repositry.IRepositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduates_Service.Services.Repositry
{
    public class ApplicantRepositry : Repostry<ApplicantUser>, IApplicantRepositry
    {
        private AppDbContext _db;
        public ApplicantRepositry(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ApplicantUser obj)
        {
            _db.ApplicantsUser.Update(obj);
        }
    }
}
