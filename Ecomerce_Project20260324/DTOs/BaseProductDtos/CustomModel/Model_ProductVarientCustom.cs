namespace Ecomerce_Project20260324.DTOs.BaseProductDtos.CustomModel
{
    public class Model_ProductVarientCustom
    {
        public int Id { get; set; }
        public int BaseProductId { get; set; }
        public Model_ProductColorCustom ProductColor { get; set; }

        public Model_ProductSizeCustom ProductSize { get; set; }
        public int Quantity { get; set; }




    }
}
