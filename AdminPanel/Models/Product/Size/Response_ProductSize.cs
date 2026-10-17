namespace AdminPanel.Models.Product
{
    public class Response_ProductSize
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<ProductSizeVM> ProductSizes { get; set; } = new List<ProductSizeVM>();
    }
}
