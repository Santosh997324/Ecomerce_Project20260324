using Ecomerce_Project20260324.Models.Master;

namespace Ecomerce_Project20260324.DTOs.CustomerFeedback.Response
{
    public class Response_CustomerFeedback
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }
        public List<Customer_Feedback> customer_Feedbacks { get; set; } = new List<Customer_Feedback>();

    }
}
