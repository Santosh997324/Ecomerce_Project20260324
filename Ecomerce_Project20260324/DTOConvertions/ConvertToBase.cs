using Ecomerce_Project20260324.DTOs.BaseProductDtos.Request;
using Ecomerce_Project20260324.Models.Product;

namespace Ecomerce_Project20260324.DTOConvertions
{
    public static class ConvertToBase
    {
        public static BaseProduct ConvertToBaseProduct(this Request_BaseProduct baseProduct)
        {

            decimal totalPrice;
            decimal decimalTotalPrice;
            if (baseProduct.Discount > 0)
            {
                totalPrice = baseProduct.Price - (baseProduct.Price * baseProduct.Discount / 100);
                decimalTotalPrice=decimal.Round(totalPrice,2);
            }
            else
            {
                totalPrice=baseProduct.Price;
                decimalTotalPrice = decimal.Round(totalPrice, 2);
            }

            var baseProductReturn = new BaseProduct()
            {
                Name = baseProduct.Name,
                Description = baseProduct.Description,
                MainCategoryId = baseProduct.MainCategoryId,
                MaterialId = baseProduct.MaterialId,
                Price = baseProduct.Price,
                Discount = baseProduct.Discount,
                TotalPrice = decimalTotalPrice
            };
            return baseProductReturn;
        }
    }
}
