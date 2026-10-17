using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminPanel.Models.CustomerFeedback
{
    public class CustomerFeedbackViewModel
    {
        public int Customer_Feedback_Id { get; set; }
        public int Product_Id { get; set; }
        public int Status_Id { get; set; }
        public string? Message { get; set; }
        public IFormFile? ImageFile { get; set; }
        public List<SelectListItem>? Products { get; set; }
        public List<SelectListItem>? Statuses { get; set; }

    }
}
