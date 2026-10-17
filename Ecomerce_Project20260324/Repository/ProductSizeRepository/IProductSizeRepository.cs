using Ecomerce_Project20260324.DTOs.ProductSizeDtos.Request;
using Ecomerce_Project20260324.DTOs.ProductSizeDtos.Response;

namespace Ecomerce_Project20260324.Repository.ProductSizeRepository
{
    public interface IProductSizeRepository
    {
        Task<Response_ProductSize> GetAllProductSizes();
        Task<Response_ProductSize> GetProductSizeById(int productSizeId);
        Task<Response_ProductSize> AddProductSize(Request_ProductSize productSize);
        Task<Response_ProductSize> UpdateProductSize(int productSizeId,
            Request_ProductSize productSize);
        Task<Response_ProductSize> DeleteProductSize(int productSizeId);

    }
}
