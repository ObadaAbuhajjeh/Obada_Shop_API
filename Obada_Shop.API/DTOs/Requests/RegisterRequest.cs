using System.ComponentModel.DataAnnotations;
using Obada_Shop.API.Model;
using Obada_Shop.API.Validations;

namespace Obada_Shop.API.DTOs.Requests
{
    public class RegisterRequest
    {
        [MinLength(3)]
        public string FirstName { get; set; }
        [MinLength(4)]
        public string LastName { get; set; }
        [MinLength(6)]
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        [Compare(nameof(Password),ErrorMessage = "Passwords is not matched !!")]
        public string ConfirmPassword { get; set; }
        public ApplicationUserGender Gender { get; set; }
        [Over18Years]
        public DateTime BirthOfDate { get; set; }
    }
}
