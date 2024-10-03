using System.ComponentModel.DataAnnotations;

namespace ThreeLayer.API.Models.DTOS
{
    public class BrazilianPersonCreateDTO
    {
        [Required(ErrorMessage = "The first name is required to register a person")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The second name is required to register a person")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "The birthdate is required to register a person")]
        public DateOnly BirthDate { get; set; }
    }
}
