using Ecomerce_Project20260324.Models.Product;

namespace Ecomerce_Project20260324.DTOs.MainCategoryDtos.Response
{
    public class Response_MainCategory
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }
        public List<MainCategory> mainCategories { get; set; } = new List<MainCategory>();
    }
}
