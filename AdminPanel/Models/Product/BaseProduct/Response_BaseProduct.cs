using System.Text.Json.Serialization;

namespace AdminPanel.Models.Product.BaseProduct
{
    public class Response_BaseProduct
    {
        public bool isSuccess { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("baseProduct")]
        public BaseProductVM_Dtos baseProductVM_Dtos { get; set; }

        //public List<BaseProductVM_Dtos> List_BaseProductVM_Dtos { get; set; }


    }
}
