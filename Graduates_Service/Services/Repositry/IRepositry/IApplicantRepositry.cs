using Graduates_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduates_Service.Services.Repositry.IRepositry
{
    public interface IApplicantRepositry : IRepositray<ApplicantUser>
    {
        /// <summary>
        /// Update Data For Table (Applicant User)
        /// </summary>
        /// <param name="ApplicantUserObj"></param>
        void Update(ApplicantUser ApplicantUserObj);
    }
}
