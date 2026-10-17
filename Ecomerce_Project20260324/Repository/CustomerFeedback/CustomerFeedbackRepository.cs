using AutoMapper;
using Ecomerce_Project20260324.Data;
using Ecomerce_Project20260324.DTOs.CustomerFeedback.Request;
using Ecomerce_Project20260324.DTOs.CustomerFeedback.Response;
using Ecomerce_Project20260324.Models.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Ecomerce_Project20260324.Repository.CustomerFeedback
{
    public class CustomerFeedbackRepository : ICustomerFeedbackRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public CustomerFeedbackRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Response_CustomerFeedback> AddCustomerFeedback(Request_CustomerFeedback customerFeedback)
        {
            var customerFeedbackExisting = _mapper.Map<Ecomerce_Project20260324.Models.Master.Customer_Feedback>(customerFeedback);

            await _db.CustomerFeedbacks.AddAsync(customerFeedbackExisting);
            await _db.SaveChangesAsync();

            return new Response_CustomerFeedback()
            {
                isSuccess = true,
                Message = "Customer feedback added successfully.",
                customer_Feedbacks = new List<Customer_Feedback>()
                {
                  customerFeedbackExisting
                }
            };
        }

        public async Task<Response_CustomerFeedback> DeleteCustomerFeedback(int CustomerFeedback_Id)
        {
            var CustomerFeedback = await _db.CustomerFeedbacks.FirstOrDefaultAsync(m => m.Customer_Feedback_Id == CustomerFeedback_Id);
            if (CustomerFeedback == null)
            {
                return new Response_CustomerFeedback()
                {
                    isSuccess = false,
                    Message = "Not Found Customer Feedback!",
                };
            }

            _db.CustomerFeedbacks.Remove(CustomerFeedback);
            await _db.SaveChangesAsync();

            return new Response_CustomerFeedback()
            {
                isSuccess = true,
                Message = "Delelet Successfully!",
                customer_Feedbacks = new List<Customer_Feedback>()
                {
                   CustomerFeedback
                }
            };
        }

        public async Task<Response_CustomerFeedback> GetAllCustomerFeedback()
        {
            var customerFeedbacks = await _db.CustomerFeedbacks
                .Include(x => x.User)
                .ToListAsync();

            if (customerFeedbacks == null || !customerFeedbacks.Any())
            {
                return new Response_CustomerFeedback()
                {
                    isSuccess = false,
                    Message = "No Customer Feedback Found!"
                };
            }

            return new Response_CustomerFeedback()
            {
                isSuccess = true,
                Message = "Customer Feedback List",
                customer_Feedbacks = customerFeedbacks
            };
        }

        public async Task<Response_CustomerFeedback> GetCustomerFeedbackById(int CustomerFeedback_Id)
        {
            var customerFeedback = await _db.CustomerFeedbacks
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Customer_Feedback_Id == CustomerFeedback_Id);

            if (customerFeedback == null)
            {
                return new Response_CustomerFeedback()
                {
                    isSuccess = false,
                    Message = "Customer Feedback Not Found!"
                };
            }

            return new Response_CustomerFeedback()
            {
                isSuccess = true,
                Message = "Customer Feedback Found",
                customer_Feedbacks = new List<Customer_Feedback>()
        {
            customerFeedback
        }
            };
        }

        public async Task<Response_CustomerFeedback> UpdateCustomerFeedback(
     int CustomerFeedback_Id,
     Request_CustomerFeedback customerFeedback)
        {
            var customerFeedbackExisting = await _db.CustomerFeedbacks
                .FirstOrDefaultAsync(x => x.Customer_Feedback_Id == CustomerFeedback_Id);

            if (customerFeedbackExisting == null)
            {
                return new Response_CustomerFeedback()
                {
                    isSuccess = false,
                    Message = "Customer Feedback Not Found!"
                };
            }

            customerFeedbackExisting.Product_Id =
                customerFeedback.Product_Id != 0
                ? customerFeedback.Product_Id
                : customerFeedbackExisting.Product_Id;

            customerFeedbackExisting.User_Id = customerFeedbackExisting.User_Id; // Used here as Hidden!

            customerFeedbackExisting.Status_Id =
                customerFeedback.Status_Id != 0
                ? customerFeedback.Status_Id
                : customerFeedbackExisting.Status_Id;

            customerFeedbackExisting.Message =
                customerFeedback.Message ?? customerFeedbackExisting.Message;

            customerFeedbackExisting.Images_Upload =
                customerFeedback.Images_Upload ?? customerFeedbackExisting.Images_Upload;

            _db.CustomerFeedbacks.Update(customerFeedbackExisting);

            await _db.SaveChangesAsync();

            return new Response_CustomerFeedback()
            {
                isSuccess = true,
                Message = "Customer Feedback Updated Successfully!",
                customer_Feedbacks = new List<Customer_Feedback>()
        {
            customerFeedbackExisting
        }
            };
        }
    }
}
