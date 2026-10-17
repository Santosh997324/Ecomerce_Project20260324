using System.ComponentModel.DataAnnotations;

namespace Ecomerce_Project20260324.DTOs.ApiDTOs.Request
{
    public class Request_ApiUserRegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
