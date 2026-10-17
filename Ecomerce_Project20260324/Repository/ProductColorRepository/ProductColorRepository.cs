using AutoMapper;
using Ecomerce_Project20260324.Data;
using Ecomerce_Project20260324.DTOs.ProductColorDtos.Request;
using Ecomerce_Project20260324.DTOs.ProductColorDtos.Response;
using Ecomerce_Project20260324.Models.Product;
using Microsoft.EntityFrameworkCore;

namespace Ecomerce_Project20260324.Repository.ProductColorRepository
{
    public class ProductColorRepository : IProductColorRepository
    {

        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ProductColorRepository(ApplicationDbContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public async Task<Response_ProductColor> GetAllProductColors()
        {
            var productColors = await _db.ProductColors.ToListAsync();

            if (productColors == null)
            {
                return new Response_ProductColor()
                {
                    isSuccess = false,
                    Message = "No Product Colors in Db"
                };
            }

            return new Response_ProductColor()
            {
                isSuccess = true,
                Message = "Product Colors Retrieved",
                productColors = productColors
            };
        }

        public async Task<Response_ProductColor> GetProductColorById(int productColorId)
        {
            var productColor = await _db.ProductColors
                .FirstOrDefaultAsync(pc => pc.Id == productColorId);

            if (productColor == null)
            {
                return new Response_ProductColor()
                {
                    isSuccess = false,
                    Message = "No Product Color Found With Given Id"
                };
            }
            return new Response_ProductColor()
            {
                isSuccess = true,
                Message = "Product Color Retrieved",
                productColors = new List<ProductColor>()
                {
                    productColor
                }
            };
        }
        public async Task<Response_ProductColor> AddProductColor(Request_ProductColor productColor)
        {
            var productColorBase = _mapper.Map<ProductColor>(productColor);

            await _db.ProductColors.AddAsync(productColorBase);
            await _db.SaveChangesAsync();

            return new Response_ProductColor()
            {
                isSuccess = true,
                Message = "Product Color Added Successfully",
                productColors = new List<ProductColor>()
                {
                    productColorBase
                }
            };
        }

        public async Task<Response_ProductColor> UpdateProductColor(int productColorId, Request_ProductColor productColor)
        {
            var existingProductColor = await _db.ProductColors
                .FirstOrDefaultAsync(pc => pc.Id == productColorId);
            if (existingProductColor == null)
            {
                return new Response_ProductColor()
                {
                    isSuccess = false,
                    Message = "No Product Color Found With Given Id"
                };
            }
            existingProductColor.Name = productColor.Name;
            await _db.SaveChangesAsync();

            return new Response_ProductColor()
            {
                isSuccess = true,
                Message = "Product Color Updated Successfully",
                productColors = new List<ProductColor>()
                {
                    existingProductColor
                }
            };
        }
        public async Task<Response_ProductColor> DeleteProductColor(int productColorId)
        {
            var existingProductColor = await _db.ProductColors
                .FirstOrDefaultAsync(pc => pc.Id == productColorId);

            if (existingProductColor == null)
            {
                return new Response_ProductColor()
                {
                    isSuccess = false,
                    Message = "No Product Color Found With Given Id"
                };
            }

            _db.ProductColors.Remove(existingProductColor);
            await _db.SaveChangesAsync();

            return new Response_ProductColor()
            {
                isSuccess = true,
                Message = "Product Color Deleted Successfully",
                productColors = new List<ProductColor>()
                {
                    existingProductColor
                }
            };
        }
    }
}
