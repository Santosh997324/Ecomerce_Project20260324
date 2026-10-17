using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Ecomerce_Project20260324.Models.Master
{
    public class Banner
    {
        public int Id { get; set; }

        public string Heading { get; set; }

        // STORE IMAGE NAME/PATH IN DATABASE
        public string ImagePath { get; set; }

        public string Description { get; set; }

        // ONLY FOR FILE UPLOAD
        [NotMapped]
        [JsonIgnore]
        public IFormFile ImageFile { get; set; }
   
    }
}
