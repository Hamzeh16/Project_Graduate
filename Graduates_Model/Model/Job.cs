using System.ComponentModel.DataAnnotations;

namespace Graduates_Model.Model
{
    public class Job
    {
        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        public string? Title { get; set; }

        [StringLength(50)]
        public string? CompanyName { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(50)]
        public string? Location { get; set; }

        [StringLength(50)]
        public string? EmailJob { get; set; }

        [StringLength(100)]
        public string? Qalification { get; set; }

        public DateTime? JobDeadLine { get; set; }

    }
}