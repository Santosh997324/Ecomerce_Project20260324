using Ecomerce_Project20260324.DTOs.BaseProductDtos.CustomModel;

namespace Ecomerce_Project20260324.DTOs.BaseProductDtos.Response
{
    public class Response_BaseProduct
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }
        public List<Model_BaseProductWithNoExtraInfo> baseProducts { get; set; } = new List<Model_BaseProductWithNoExtraInfo>();

    }
}
