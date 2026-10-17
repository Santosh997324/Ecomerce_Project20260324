using Ecomerce_Project20260324.Models.AuthModels;

namespace Ecomerce_Project20260324.DTOs.ApiDTOs.Response
{
    public class Response_ApiUserRegisterDto
    {
        public bool IsSuccess { get; set; }
        public ApiUser apiUser { get; set; }
        public List<string> Message { get; set; } = new List<string>();
    }
}
