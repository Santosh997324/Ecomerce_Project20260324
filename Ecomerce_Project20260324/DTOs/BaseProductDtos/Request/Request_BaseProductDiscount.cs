using System.ComponentModel.DataAnnotations;

namespace Ecomerce_Project20260324.DTOs.BaseProductDtos.Request
{
    public class Request_BaseProductDiscount
    {
        [Range(0, 99)]
        public int Discount { get; set; }

    }
}
