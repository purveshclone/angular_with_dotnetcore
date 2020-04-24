using System.ComponentModel.DataAnnotations;

namespace Testfirst.API.Dtos
{
    public class UserForLOginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}