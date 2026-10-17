using Ecomerce_Project20260324.DTOs.BaseProductDtos.CustomModel;

namespace Ecomerce_Project20260324.DTOs.BaseProductDtos.Response
{
    public class Response_BaseProductWithPaging
    {
        public List<Model_BaseProductCustom> baseProducts { get; set; }
      = new List<Model_BaseProductCustom>();

        public int TotalPages { get; set; }
    }
}
