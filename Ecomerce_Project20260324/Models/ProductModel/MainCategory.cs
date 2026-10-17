using System.Text.Json.Serialization;

namespace Ecomerce_Project20260324.Models.Product
{
    public class MainCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public IEnumerable<BaseProduct> baseProducts { get; set; }
    }
}
