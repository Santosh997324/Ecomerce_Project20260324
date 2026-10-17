using Ecomerce_Project20260324.DTOs.BaseProductDtos.CustomModel;

namespace Ecomerce_Project20260324.DTOs.BaseProductDtos.Response
{
    public class Response_BaseProductWithFullInfo
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }
        public Model_BaseProductCustom baseProduct { get; set; }


    }
}
