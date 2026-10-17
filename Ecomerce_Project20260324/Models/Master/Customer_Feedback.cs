using Ecomerce_Project20260324.Models.AuthModels;
using Ecomerce_Project20260324.Models.Product;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomerce_Project20260324.Models.Master
{
    public class Customer_Feedback
    {

        [Key]
        public int Customer_Feedback_Id { get; set; }
        public int Product_Id { get; set; }

        [ForeignKey("Product_Id")]
        public BaseProduct productBase { get; set; }

        public string User_Id { get; set; } // Here We Take User_Id OR Login Id
       
        [ForeignKey("User_Id")]
        public ApiUser User { get; set; }

        public int Status_Id { get; set; }

        [ForeignKey("Status_Id")]
        public Status status { get; set; }

        public string Message { get; set; }
        public string Images_Upload { get; set; }



          


    }
}
