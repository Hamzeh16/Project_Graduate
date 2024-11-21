using System.ComponentModel.DataAnnotations;

namespace Graduates_Service.Services.Dto
{
    public class TraningDto
    {

        public string? Title { get; set; }

        public string? CompanyName { get; set; }

        public string? Description { get; set; }

        public string? Location { get; set; }

        public string? skillRequired { get; set; }

        public string? TrainPeriod { get; set; }

        public DateTime? applicDeadLine { get; set; }

        [Range(0, float.MaxValue)]
        public float TrainCost { get; set; }

        public string? Email { get; set; }

    }
}
