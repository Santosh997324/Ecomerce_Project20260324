using Ecomerce_Project20260324.DTOs.CustomerFeedback.Request;
using Ecomerce_Project20260324.DTOs.CustomerFeedback.Response;

namespace Ecomerce_Project20260324.Repository.CustomerFeedback
{
    public interface ICustomerFeedbackRepository
    {
        Task<Response_CustomerFeedback> GetAllCustomerFeedback();
        Task<Response_CustomerFeedback> GetCustomerFeedbackById(int CustomerFeedback_Id);
        Task<Response_CustomerFeedback> AddCustomerFeedback(Request_CustomerFeedback customerFeedback);
        Task<Response_CustomerFeedback> UpdateCustomerFeedback(int CustomerFeedback_Id,Request_CustomerFeedback customerFeedback);
        Task<Response_CustomerFeedback> DeleteCustomerFeedback(int CustomerFeedback_Id);
    }
}
