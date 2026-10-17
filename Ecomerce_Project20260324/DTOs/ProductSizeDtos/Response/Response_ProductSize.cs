using Ecomerce_Project20260324.Models.Product;

namespace Ecomerce_Project20260324.DTOs.ProductSizeDtos.Response
{
    public class Response_ProductSize
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }
        public List<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();

    }
}
