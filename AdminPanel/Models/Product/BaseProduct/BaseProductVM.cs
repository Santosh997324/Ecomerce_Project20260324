using Ecomerce_Project20260324.Models.Product;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminPanel.Models.Product.BaseProduct
{
    public class BaseProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MainCategoryId { get; set; }
        public int MaterialId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int Discount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

    }

}
