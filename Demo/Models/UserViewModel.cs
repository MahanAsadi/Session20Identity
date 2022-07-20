using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class UserViewModel {

        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; }

    }


}
