using AutoMapper;
using Ecomerce_Project20260324.Data;
using Ecomerce_Project20260324.DTOs.ProductDtos.Request;
using Ecomerce_Project20260324.DTOs.ProductDtos.Response;
using Ecomerce_Project20260324.Models.Product;
using Microsoft.EntityFrameworkCore;

namespace Ecomerce_Project20260324.Repository.Material
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public MaterialRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<Response_ProductMaterial> GetAllProductMaterials()
        {
            var materials = await _db.Materials.ToListAsync();

            if (materials.Count <= 0)
            {
                return new Response_ProductMaterial()
                {
                    isSuccess = false,
                    Message = "No materials found."
                };
            }
            return new Response_ProductMaterial()
            {
                isSuccess = true,
                Message = "Materials retrieved successfully.",
                materials = materials
            };
            //throw new NotImplementedException();
        }


        public async Task<Response_ProductMaterial> GetProductMaterialById(int productMatetialId)
        {
            var material = await _db.Materials.FirstOrDefaultAsync(m => m.Id == productMatetialId);

            if (material == null)
            {
                return new Response_ProductMaterial()
                {
                    isSuccess = false,
                    Message = "Material not found."
                };
            }

            return new Response_ProductMaterial()
            {
                isSuccess = true,
                Message = "Material retrieved successfully.",
                materials = new List<Models.Product.Material> { material }
            };

            //throw new NotImplementedException();
        }


        public async Task<Response_ProductMaterial> UpdateProductMaterial(int productMatetialId, Request_ProductMaterial request)
        {
            var ExistingProductMaterial = await _db.Materials.FirstOrDefaultAsync(m => m.Id == productMatetialId);

            if (ExistingProductMaterial == null)
            {
                return new Response_ProductMaterial()
                {
                    isSuccess = false,
                    Message = "No product material found with given Id"
                };
            }
            ExistingProductMaterial.Name = request.Name;
            await _db.SaveChangesAsync();

            return new Response_ProductMaterial()
            {
                isSuccess = true,
                Message = "Product Material Updated Successfully",
                materials = new List<Ecomerce_Project20260324.Models.Product.Material>()
                {
                    ExistingProductMaterial
                }
            };
        }


        public async Task<Response_ProductMaterial> AddProductMaterial(Request_ProductMaterial productMaterialDto)
        {
            var productMaterialBase = _mapper.Map<Ecomerce_Project20260324.Models.Product.Material>(productMaterialDto);

            await _db.Materials.AddAsync(productMaterialBase);
            await _db.SaveChangesAsync();

            return new Response_ProductMaterial()
            {
                isSuccess = true,
                Message = "Product Material Added Successfully",
                materials = new List<Ecomerce_Project20260324.Models.Product.Material>()
                {
                    productMaterialBase
                }
            };

        }
        public async Task<Response_ProductMaterial> DeleteProductMaterial(int productMatetialId)
        {

            var existingProductMaterial = await _db.Materials
                .FirstOrDefaultAsync(m => m.Id == productMatetialId);
            if (existingProductMaterial == null)
            {
                return new Response_ProductMaterial()
                {
                    isSuccess = false,
                    Message = "No product material found with given Id"
                };
            }

            _db.Materials.Remove(existingProductMaterial);
            await _db.SaveChangesAsync();

            return new Response_ProductMaterial()
            {
                isSuccess = true,
                Message = "Product Material Removed Successfully",
                materials = new List<Ecomerce_Project20260324.Models.Product.Material>()
                {
                    existingProductMaterial
                }
            };
            //throw new NotImplementedException();
        }




    }
}
