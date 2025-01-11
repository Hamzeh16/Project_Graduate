using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Graduates_Service.Services.Dto
{
    public class JobDto
    {
        public string? title { get; set; }

        public string? companyName { get; set; }

        public string? description { get; set; }

        public string? location { get; set; }

        public string? email { get; set; }

        public string? jobType { get; set; }

        public string[]? responsibilities { get; set; }
        public string[]? qualifications { get; set; }

        public DateTime? applicationDeadline { get; set; }

        public string? internshipType { get; set; }

        public string? duration { get; set; }

        public string? formType { get; set; }

        public string? status { get; set; }

        [NotMapped]
        public string? crFile { get; set; }
        [NotMapped]
        public string? companyEmail { get; set; }

    }
}
