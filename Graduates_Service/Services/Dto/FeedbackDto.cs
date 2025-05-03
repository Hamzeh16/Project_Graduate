using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduates_Service.Services.Dto
{
    public class FeedbackDto
    {
        public string? Title { get; set; }
        public int? Rating { get; set; }
        public string? Message { get; set; }
        public string? CompanyId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
