using Ecomerce_Project20260324.Models.Master;
using Ecomerce_Project20260324.Models.Product;

namespace Ecomerce_Project20260324.DTOs.StatusDto.Response
{
    public class Response_Status
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }

        public List<Status> status { get; set; } = new List<Status>();
    }
}
