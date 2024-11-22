using System.ComponentModel.DataAnnotations;

namespace Graduates_Service.Services.Dto
{
    public class JobDto
    {
        public string? Title { get; set; }

        public string? CompanyName { get; set; }

        public string? Description { get; set; }

        public string? Location { get; set; }

        public string? Email { get; set; }

        public string? Qalification { get; set; }

        public DateTime? JobDeadLine { get; set; }
    }
}
