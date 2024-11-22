using Graduates_Model.Model;

namespace Graduates_Service.Services.Repositry.IRepositry
{
    public interface IJob : IRepositray<Job>
    {
        /// <summary>
        /// Update Data For Table (Traning)
        /// </summary>
        /// <param name="jobObj"></param>
        void Update(Job jobObj);
    }
}
