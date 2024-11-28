using Graduates_Model.Model;

namespace Graduates_Service.Services.Repositry.IRepositry
{
    public interface IApplicationForm : IRepositray<ApplicationForm>
    {
        /// <summary>
        /// Update Data For Table (ApplicationForm)
        /// </summary>
        /// <param name="AppFormObj"></param>
        void Update(ApplicationForm AppFormObj);
    }
}
