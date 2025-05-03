using Graduates_Data.Data;
using Graduates_Model.Model;
using Graduates_Service.Services.Repositry.IRepositry;

namespace Graduates_Service.Services.Repositry
{
    public class FeedbackRepositry : Repostry<Feedback>, IFeedbackRepositry
    {
        private AppDbContext _db;
        public FeedbackRepositry(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Feedback obj)
        {
            _db.Feedbacks.Update(obj);
        }
    }
}
