using AutoMapper;
using Ecomerce_Project20260324.Data;
using Ecomerce_Project20260324.DTOs.MainCategoryDtos.Request;
using Ecomerce_Project20260324.DTOs.MainCategoryDtos.Response;
using Microsoft.EntityFrameworkCore;

namespace Ecomerce_Project20260324.Repository.MainCategory
{
    public class MainCategoryRepository : IMainCategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public MainCategoryRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response_MainCategory> GetAllMainCategories()
        {
            var productMainCategories = await _context.MainCategories.ToListAsync();

            if (productMainCategories == null)
            {
                return new Response_MainCategory()
                {
                    isSuccess = false,
                    Message = "No main categories found"
                };
            }

            return new Response_MainCategory()
            {
                isSuccess = true,
                Message = "All main categories",
                mainCategories = productMainCategories
            };
            // throw new NotImplementedException();
        }

        public async Task<Response_MainCategory> GetMainCategoryById(int mainCategoryId)
        {
            var mainCategory = await _context.MainCategories.FirstOrDefaultAsync(mc => mc.Id == mainCategoryId);

            if (mainCategory == null)
            {
                return new Response_MainCategory()
                {
                    isSuccess = false,
                    Message = "No Main Category Found With Given Id"
                };
            }

            return new Response_MainCategory()
            {
                isSuccess = true,
                Message = "Main Category Found",
                mainCategories = new List<Ecomerce_Project20260324.Models.Product.MainCategory>()
                {
                    mainCategory
                }
            };
        }

        public async Task<Response_MainCategory> AddMainCategory(Request_MainCategory mainCategory)
        {
            var MainCategoryBaseModel = _mapper.Map<Ecomerce_Project20260324.Models.Product.MainCategory>(mainCategory);
            await _context.MainCategories.AddAsync(MainCategoryBaseModel);
            await _context.SaveChangesAsync();

            return new Response_MainCategory()
            {
                isSuccess = true,
                Message = "Main Category Added Successfully",
                mainCategories = new List<Ecomerce_Project20260324.Models.Product.MainCategory>()
                {
                    MainCategoryBaseModel
                }
            };
        }
        public async Task<Response_MainCategory> UpdateMainCategory(int mainCategoryId, Request_MainCategory mainCategory)
        {
            var existingMainCategory = await _context.MainCategories
                .FirstOrDefaultAsync(mc => mc.Id == mainCategoryId);

            if (existingMainCategory == null)
            {
                return new Response_MainCategory()
                {
                    isSuccess = false,
                    Message = "No Main Category Found With Given Id"
                };
            }

            existingMainCategory.Name = mainCategory.Name;
            await _context.SaveChangesAsync();

            return new Response_MainCategory()
            {
                isSuccess = true,
                Message = "Main Category Added Successfully",
                mainCategories = new List<Ecomerce_Project20260324.Models.Product.MainCategory>()
                {
                    existingMainCategory
                }
            };
        }
        public async Task<Response_MainCategory> DeleteMainCategory(int mainCategoryId)
        {
            var existingMainCategory = await _context.MainCategories
                .FirstOrDefaultAsync(mc => mc.Id == mainCategoryId);

            if (existingMainCategory == null)
            {
                return new Response_MainCategory()
                {
                    isSuccess = false,
                    Message = "No Main Category Found With Given Id"
                };
            }

            _context.MainCategories.Remove(existingMainCategory);
            await _context.SaveChangesAsync();

            return new Response_MainCategory()
            {
                isSuccess = true,
                Message = "Main Category Deleted Successfully",
                mainCategories = new List<Ecomerce_Project20260324.Models.Product.MainCategory>()
                {
                    existingMainCategory
                }
            };
        }
    }
}
