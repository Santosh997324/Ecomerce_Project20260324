using System.Text.Json.Serialization;

namespace Ecomerce_Project20260324.Models.Product
{
    public class ProductVariant
    {
        public int Id { get; set; }
        public int BaseProductId { get; set; }       
        
        [JsonIgnore]
        public BaseProduct baseProduct { get; set; }
        public int ProductColorId { get; set; }
        public ProductColor productColor { get; set; }
        public int ProductSizeId { get; set; }
        public ProductSize productSize { get; set; }
        public int Quantity { get; set; }

    }
}
