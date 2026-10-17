using Ecomerce_Project20260324.Models.Product;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomerce_Project20260324.DTOs.BaseProductDtos.CustomModel
{
    public class Model_BaseProductCustom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public MainCategory mainCategory { get; set; }
        public Material material { get; set; }
        public ICollection<Model_ProductVarientCustom> productVariants { get; set; }
        public ICollection<Model_BaseImagesCustom> ImagesBases { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int Discount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

    }
}
