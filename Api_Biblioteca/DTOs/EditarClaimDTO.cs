using System.ComponentModel.DataAnnotations;

namespace Api_Biblioteca.DTOs
{
    public class EditarClaimDTO
    {
        [EmailAddress]
        [Required]
        public required string Email { get; set; }
    }
}
