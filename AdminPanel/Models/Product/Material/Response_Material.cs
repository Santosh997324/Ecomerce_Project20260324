namespace AdminPanel.Models.Product
{
    public class Response_Material
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<MaterialVM> Materials { get; set; }

    }
}
