namespace AdminPanel.Models.Banner
{
    public class Response_Banner
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }
        public List<BannerVM> Banners { get; set; } = new List<BannerVM>();
    }
}
