using Ecomerce_Project20260324.DTOs.MainCategoryDtos.Request;
using Ecomerce_Project20260324.DTOs.MainCategoryDtos.Response;

namespace Ecomerce_Project20260324.Repository.MainCategory
{
    public interface IMainCategoryRepository
    {
        Task<Response_MainCategory> GetAllMainCategories();
        Task<Response_MainCategory> GetMainCategoryById(int mainCategoryId);
        Task<Response_MainCategory> AddMainCategory(Request_MainCategory mainCategory);
        Task<Response_MainCategory> UpdateMainCategory(int mainCategoryId,
            Request_MainCategory mainCategory);
        Task<Response_MainCategory> DeleteMainCategory(int mainCategoryId);
    }
}
