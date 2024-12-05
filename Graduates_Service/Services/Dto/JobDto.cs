using System.ComponentModel.DataAnnotations;

namespace Graduates_Service.Services.Dto
{
    public class JobDto
    {
        public string? title { get; set; }

        public string? CompanyName { get; set; }

        public string? description { get; set; }

        public string? location { get; set; }

        public string? email { get; set; }

        public string? jobType { get; set; }

        public string[]? responsibilities { get; set; }
        public string[]? qualifications { get; set; }

        public DateTime? applicationDeadLine { get; set; }
    }
}
