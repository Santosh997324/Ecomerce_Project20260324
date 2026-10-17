using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Ecomerce_Project20260324.Models.Product
{
    public class ImageBase
    {
        [Key]
        [Column(TypeName = "bigint")]
        public long Id { get; set; }
        [Column(TypeName = "varchar(max)")]
        public string ImagePath { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AddedOn { get; set; }

        public int BaseProductId { get; set; }


        [JsonIgnore]
        public BaseProduct baseProduct { get; set; }
        public string staticPath { get; set; }

    }
}
