using System.ComponentModel.DataAnnotations;

namespace Graduates_Service.Services.Dto
{
    public class TraningDto
    {

        public string? title { get; set; }

        public string? companyName { get; set; }

        public string? description { get; set; }

        public string? location { get; set; }

        public string? internshipType { get; set; }

        public string? duration { get; set; }

        public string[]? responsibilities { get; set; }

        public string[]? qualifications { get; set; }

        public DateTime? applicationDeadline { get; set; }

    }
}
