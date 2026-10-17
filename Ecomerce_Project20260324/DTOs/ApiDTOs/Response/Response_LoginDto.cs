namespace Ecomerce_Project20260324.DTOs.ApiDTOs.Response
{
    public class Response_LoginDto
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public bool Result { get; set; }

        public List<string> error { get; set; }=new List<string>();

    }
}
