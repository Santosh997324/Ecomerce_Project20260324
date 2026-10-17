using Ecomerce_Project20260324.Models.Master;

namespace Ecomerce_Project20260324.DTOs.BannerDto.Response
{
    public class Response_Banner
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }
        public List<Banner> Banners { get; set; } = new List<Banner>();
    }
}
