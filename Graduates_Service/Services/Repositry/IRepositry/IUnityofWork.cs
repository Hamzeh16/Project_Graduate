using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduates_Service.Services.Repositry.IRepositry
{
    public interface IUnityofWork
    {
        #region Add Table To Action CRUD (Create , Update , Delete)
        public IApplicantRepositry ApplicantRepositry { get; set; }
        public ITraningRepositry TrainingRepositry { get; set; }

        #endregion


        /// <summary>
        /// Save Data For All Table
        /// </summary>
        void Save();
    }
}
