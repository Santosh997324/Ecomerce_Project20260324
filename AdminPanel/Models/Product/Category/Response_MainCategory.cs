namespace AdminPanel.Models.Product
{
    public class Response_MainCategory
    {

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<MainCategoryVM> MainCategories { get; set; } = new List<MainCategoryVM>();

    }
}
