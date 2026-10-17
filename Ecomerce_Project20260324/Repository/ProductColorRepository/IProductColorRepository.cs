using Ecomerce_Project20260324.DTOs.ProductColorDtos.Request;
using Ecomerce_Project20260324.DTOs.ProductColorDtos.Response;

namespace Ecomerce_Project20260324.Repository.ProductColorRepository
{
    public interface IProductColorRepository
    {
        Task<Response_ProductColor> GetAllProductColors();
        Task<Response_ProductColor> GetProductColorById(int productColorId);
        Task<Response_ProductColor> AddProductColor(Request_ProductColor productColor);
        Task<Response_ProductColor> UpdateProductColor(int productColorId,Request_ProductColor productColor);
        Task<Response_ProductColor> DeleteProductColor(int productColorId);


    }
}
