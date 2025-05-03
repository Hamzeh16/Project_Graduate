using Graduates_Model.Model;

namespace Graduates_Service.Services.Repositry.IRepositry
{
    public interface IFeedbackRepositry : IRepositray<Feedback>
    {
        /// <summary>
        /// Update Data For Table (Feedback User)
        /// </summary>
        /// <param name="FeedbackObj"></param>
        void Update(Feedback FeedbackObj);
    }
}