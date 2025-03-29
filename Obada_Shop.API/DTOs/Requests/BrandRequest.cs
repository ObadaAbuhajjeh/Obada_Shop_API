using System.ComponentModel.DataAnnotations;

namespace Obada_Shop.API.DTOs.Requests
{
    public class BrandRequest
    {
        [Required(ErrorMessage = "name is required ... !")]
        [MinLength(2)]
        [MaxLength(5)]



        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
