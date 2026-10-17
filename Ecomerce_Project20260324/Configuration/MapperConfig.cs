using AutoMapper;
using Ecomerce_Project20260324.DTOs.BannerDto.Request;
using Ecomerce_Project20260324.DTOs.CustomerFeedback.Request;
using Ecomerce_Project20260324.DTOs.MainCategoryDtos.Request;
using Ecomerce_Project20260324.DTOs.ProductColorDtos.Request;
using Ecomerce_Project20260324.DTOs.ProductDtos.Request;
using Ecomerce_Project20260324.DTOs.ProductSizeDtos.Request;
using Ecomerce_Project20260324.DTOs.ProductVariantDtos.CustomModels;
using Ecomerce_Project20260324.DTOs.StatusDto.Request;
using Ecomerce_Project20260324.Models.Master;
using Ecomerce_Project20260324.Models.Product;
//using Ecomerce_Project20260324.Models.ProductModel;

namespace Ecomerce_Project20260324.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Request_ProductMaterial, Material>().ReverseMap();
            CreateMap<Request_MainCategory, MainCategory>().ReverseMap();
            CreateMap<Request_ProductColor, ProductColor>().ReverseMap();
            CreateMap<Request_ProductSize, ProductSize>().ReverseMap();
            CreateMap<Request_Banner, Banner>().ReverseMap();
            CreateMap<Request_Status, Status>().ReverseMap();            
            CreateMap<Request_CustomerFeedback,Customer_Feedback>().ReverseMap();
            CreateMap<ProductVariant, Model_ProductVariantReturn>().ReverseMap();
            CreateMap<ProductVariant, Model_ProductVariantRequest>().ReverseMap();
        }
    }
}
