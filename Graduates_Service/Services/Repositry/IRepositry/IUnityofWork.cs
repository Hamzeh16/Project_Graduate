namespace Graduates_Service.Services.Repositry.IRepositry
{
    public interface IUnityofWork
    {
        #region Add Table To Action CRUD (Create , Update , Delete)

        public IApplicantRepositry ApplicantRepositry { get; set; }
        public ITraningRepositry TrainingRepositry { get; set; }
        public IJob JobRepositry { get; set; }
        public IApplicationForm AppFormRepositry { get; set; }
        public IFeedbackRepositry FeedbackRepositry { get; set; }

        #endregion

        /// <summary>
        /// Save Data For All Table
        /// </summary>
        void Save();
    }
}
