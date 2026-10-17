using Ecomerce_Project20260324.Models.Product;

namespace Ecomerce_Project20260324.DTOs.ProductDtos.Response
{
    public class Response_ProductMaterial
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }
        public List<Material> materials { get; set; } =new List<Material>();
    }
}
