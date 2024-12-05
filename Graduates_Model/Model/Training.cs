using System.ComponentModel.DataAnnotations;

namespace Graduates_Model.Model
{
    public class Traning
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
        public string? internshipType { get; set; }

        [StringLength(50)]
        public string? duration { get; set; }


        [StringLength(100)]
        public string[]? Responsibilities { get; set; }


        [StringLength(100)]
        public string[]? qualifications { get; set; }

        public DateTime? applicationDeadline { get; set; }

     
    }
}