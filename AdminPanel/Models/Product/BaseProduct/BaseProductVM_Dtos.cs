using System.Text.Json.Serialization;

namespace AdminPanel.Models.Product.BaseProduct
{
    public class BaseProductVM_Dtos
    {
        //"{\"isSuccess\":true,\"message\":\"Base product retrieved\",\"baseProduct\":{\"id\":1,\"name\":\"Running Shoes Classic\",\"description\":\"Puma launch on 2026 \",\"mainCategory\":{\"id\":1,\"name\":\"Shoes\"},\"material\":{\"id\":1,\"name\":\"Leater pure\"},\"productVariants\":[],\"imagesBases\":[],\"price\":10000.00,\"discount\":10,\"totalPrice\":9000.00}}"
        
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("mainCategory")]
        public MainCategoryDto MainCategory { get; set; }

        [JsonPropertyName("material")]
        public MaterialDto Material { get; set; }
        //public List<ProductVariantDto> ProductVariants { get; set; }
        //public List<ImageBaseDto> ImagesBases { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("discount")]
        public decimal Discount { get; set; }

        [JsonPropertyName("totalPrice")]
        public decimal TotalPrice { get; set; }
    }
    public class MainCategoryDto
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class MaterialDto
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
