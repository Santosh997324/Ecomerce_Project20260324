using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models.Product.BaseProduct
{
    public class Request_BaseProductDiscount
    {
        [Range(0, 99)]
        public int Discount { get; set; }
    }
}
