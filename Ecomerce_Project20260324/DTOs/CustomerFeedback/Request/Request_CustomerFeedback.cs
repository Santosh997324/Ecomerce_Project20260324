namespace Ecomerce_Project20260324.DTOs.CustomerFeedback.Request
{
    public class Request_CustomerFeedback
    {
        public int Product_Id { get; set; }
        public int User_Id { get; set; } // Here We Take User_Id OR Login Id
        public int Status_Id { get; set; }
        public string Message { get; set; }
        public string Images_Upload { get; set; }


    }
}
