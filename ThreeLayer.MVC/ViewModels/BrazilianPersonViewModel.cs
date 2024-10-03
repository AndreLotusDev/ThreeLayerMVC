using System.ComponentModel.DataAnnotations;

namespace ThreeLayer.MVC.ViewModels
{
    public class BrazilianPersonViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The person should have a first name filled")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The person should have a second name filled")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "The person should have a birth date")]
        public DateOnly BirthDate { get; set; }
    }
}
