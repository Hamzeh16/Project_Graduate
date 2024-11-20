using System.ComponentModel.DataAnnotations;

namespace Graduates_Model.Model
{
        public class Traning
        {
            [Key]
            public int ID { get; set; }

            [StringLength(50)]
            public string? CompanyName { get; set; }

            [StringLength(500)]
            public string? Description { get; set; }

            [StringLength(50)]
            public string? Location { get; set; }

            [Range(0, float.MaxValue)]
            public float TrainCost { get; set; }

            [StringLength(50)]
            public string? TrainPeriod { get; set; }


        }
    }


