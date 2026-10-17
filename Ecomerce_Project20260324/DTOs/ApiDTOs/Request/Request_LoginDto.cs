using System.ComponentModel.DataAnnotations;

namespace Ecomerce_Project20260324.DTOs.ApiDTOs.Request
{
    public class Request_LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
