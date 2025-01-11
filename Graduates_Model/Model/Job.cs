﻿using System.ComponentModel.DataAnnotations;

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

        [StringLength(50)]
        public string? JobType { get; set; }

        [StringLength(100)]
        public string[]? Responsibilities { get; set; }


        [StringLength(100)]
        public string[]? qualifications { get; set; }

        public DateTime? applicationDeadline { get; set; }

        [StringLength(50)]
        public string? internshipType { get; set; }

        [StringLength(50)]
        public string? duration { get; set; }

        public string? formType { get; set; }

        public string? status { get; set; }
        public string? EmailCompany { get; set; }

    }
}