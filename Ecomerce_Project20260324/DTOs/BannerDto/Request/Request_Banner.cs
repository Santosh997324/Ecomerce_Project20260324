//using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Ecomerce_Project20260324.DTOs.BannerDto.Request
{
    public class Request_Banner
    {
        public string Heading { get; set; }

        [JsonIgnore]
        public string? ImagePath { get; set; }

        public string Description { get; set; }

        // ONLY FOR FILE UPLOAD
        [NotMapped]
        public IFormFile ImageFile { get; set; }

    }
}
