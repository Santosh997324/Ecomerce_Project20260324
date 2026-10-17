using Ecomerce_Project20260324.DTOs.BannerDto.Request;
using Ecomerce_Project20260324.DTOs.BannerDto.Response;

namespace Ecomerce_Project20260324.Repository.Banner
{
    public interface IBannerRepository
    {
        Task<Response_Banner> GetAllBannerAsync();
        Task<Response_Banner> GetBannerByIdAsync(int bannerId);
        Task<Response_Banner> AddBanner(Request_Banner Banner);
        Task<Response_Banner> UpdateBanner(int bannerId, Request_Banner Banner);
        Task<Response_Banner> DeleteBanner(int bannerId);
    }
}
