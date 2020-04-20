using System.ComponentModel.DataAnnotations;

namespace Testfirst.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(8, MinimumLength=4 ,ErrorMessage="Must be min 4 and max 8 charactor" )]
        public string Password { get; set; }
    }
}