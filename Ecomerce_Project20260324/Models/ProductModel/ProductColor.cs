using System.Text.Json.Serialization;

namespace Ecomerce_Project20260324.Models.Product
{
    public class ProductColor
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public IEnumerable<ProductVariant> productVariants { get; set; }
    }
}
