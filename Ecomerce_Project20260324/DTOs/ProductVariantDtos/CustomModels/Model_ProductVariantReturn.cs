using Ecomerce_Project20260324.Models.Product;

namespace Ecomerce_Project20260324.DTOs.ProductVariantDtos.CustomModels
{
    public class Model_ProductVariantReturn
    {
        public int Id { get; set; }
        public int BaseProductId { get; set; }
        public ProductColor productColor { get; set; }
        public ProductSize productSize { get; set; }
        public int Quantity { get; set; }

    }
}
