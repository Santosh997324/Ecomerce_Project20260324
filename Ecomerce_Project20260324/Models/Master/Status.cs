using System.ComponentModel.DataAnnotations;

namespace Ecomerce_Project20260324.Models.Master
{
    public class Status
    {
        [Key]    
        public int Status_Id { get; set; }
        public string Status_Name { get; set; }
    }
}
