using Graduates_Model.Model;

namespace Graduates_Service.Services.Repositry.IRepositry
{
    public interface ITraningRepositry : IRepositray<Traning>
    {
        /// <summary>
        /// Update Data For Table (Traning)
        /// </summary>
        /// <param name="TraningObj"></param>
        void Update(Traning TraningObj);

        IQueryable<Traning> GetByID(int id);
    }
}