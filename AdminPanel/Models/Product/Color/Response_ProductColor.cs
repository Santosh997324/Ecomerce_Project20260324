namespace AdminPanel.Models.Product
{
    public class Response_ProductColor
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<ProductColorVM> ProductColors { get; set; } = new List<ProductColorVM>();

    }
}
