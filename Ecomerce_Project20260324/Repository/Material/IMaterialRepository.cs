using Ecomerce_Project20260324.DTOs.ProductDtos.Request;
using Ecomerce_Project20260324.DTOs.ProductDtos.Response;

namespace Ecomerce_Project20260324.Repository.Material
{
    public interface IMaterialRepository
    {
        Task<Response_ProductMaterial> GetAllProductMaterials();
        Task<Response_ProductMaterial> GetProductMaterialById(int productMatetialId);
        Task<Response_ProductMaterial> AddProductMaterial(Request_ProductMaterial request);
        Task<Response_ProductMaterial> UpdateProductMaterial(int productMatetialId, Request_ProductMaterial request);
        Task<Response_ProductMaterial> DeleteProductMaterial(int productMatetialId);


    }
}
