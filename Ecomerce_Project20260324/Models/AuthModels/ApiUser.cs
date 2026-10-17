using Microsoft.AspNetCore.Identity;

namespace Ecomerce_Project20260324.Models.AuthModels
{
    public class ApiUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
