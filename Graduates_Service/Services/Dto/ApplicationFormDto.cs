using Microsoft.AspNetCore.Http;

namespace Graduates_Service.Services.Dto
{
    public class ApplicationFormDto
    {
        public string? name { get; set; }

        public string? email { get; set; }

        public string? phone { get; set; }

        public string? address { get; set; }

        public IFormFile? resume { get; set; }
    }
}
