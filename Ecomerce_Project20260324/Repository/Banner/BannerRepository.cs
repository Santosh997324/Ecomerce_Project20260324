using AutoMapper;
using Ecomerce_Project20260324.Data;
using Ecomerce_Project20260324.DTOs.BannerDto.Request;
using Ecomerce_Project20260324.DTOs.BannerDto.Response;
using Ecomerce_Project20260324.Models.Master;
using Ecomerce_Project20260324.Repository.Banner;
using iText.IO.Font.Otf;
using Microsoft.EntityFrameworkCore;

namespace Ecomerce_Project20260324.Repository
{
    public class BannerRepository : IBannerRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public BannerRepository(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _db = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<Response_Banner> AddBanner(Request_Banner banner)
        {
            var BannerData = _mapper.Map<Ecomerce_Project20260324.Models.Master.Banner>(banner);

            await _db.Banners.AddAsync(BannerData);
            await _db.SaveChangesAsync();

            return new Response_Banner()
            {
                isSuccess = true,
                Message = "Banner added successfully.",
                Banners = new List<Models.Master.Banner> { BannerData }
            };

            //throw new NotImplementedException();
        }

        public async Task<Response_Banner> DeleteBanner(int bannerId)
        {
            var BannerExting = await _db.Banners.FirstOrDefaultAsync(m => m.Id == bannerId);

            if (BannerExting == null)
            {

                return new Response_Banner() { isSuccess = false, Message = "Banner Not Found!" };
            }
            _db.Banners.Remove(BannerExting);
            await _db.SaveChangesAsync();
            return new Response_Banner()
            {
                isSuccess = true,
                Message = "Deleted Succefuly!",
                Banners = new List<Models.Master.Banner> {
                        BannerExting
                    }
            };

        }
        // throw new NotImplementedException();


        public async Task<Response_Banner> GetAllBannerAsync()
        {
            var Banners = await _db.Banners.ToListAsync();

            if (Banners.Count <= 0)
            {
                return new Response_Banner()
                {
                    isSuccess = false,
                    Message = "No Banners found."
                };
            }


            return new Response_Banner()
            {
                isSuccess = true,
                Message = "Banners retrieved successfully.",
                Banners = Banners
            };

            // throw new NotImplementedException();
        }

        public async Task<Response_Banner> GetBannerByIdAsync(int bannerId)
        {
            var banner = await _db.Banners.FirstOrDefaultAsync(b => b.Id == bannerId);
            if (banner == null)
            {
                return new Response_Banner()
                {
                    isSuccess = false,
                    Message = "Banner not found."
                };
            }
            return new Response_Banner()
            {
                Banners = new List<Models.Master.Banner> { banner },
                isSuccess = true,
                Message = "Banner retrieved successfully."
            };

            //throw new NotImplementedException();
        }

        public async Task<Response_Banner> UpdateBanner(int bannerId, Request_Banner Banner)
        {
            var BannerEsistingdata = await _db.Banners.FirstOrDefaultAsync(b => b.Id == bannerId);

            if (BannerEsistingdata == null)
            {
                return new Response_Banner()
                {
                    isSuccess = false,
                    Message = "Banner not found."
                };
            }

            BannerEsistingdata.Heading = Banner.Heading ?? BannerEsistingdata.Heading;
            BannerEsistingdata.Description = Banner.Description ?? BannerEsistingdata.Description;
            BannerEsistingdata.ImagePath = Banner.ImagePath ?? BannerEsistingdata.ImagePath;
            //_db.Banners.Update(BannerEsistingdata);
            await _db.SaveChangesAsync();

            return new Response_Banner()
            {
                isSuccess = true,
                Message = "Banner updated successfully.",
                Banners = new List<Models.Master.Banner> { BannerEsistingdata }
            };

            //throw new NotImplementedException();
        }
    }
}
