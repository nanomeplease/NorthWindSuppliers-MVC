namespace SuppliersPL.Models
{
    using System.ComponentModel.DataAnnotations;
    public class UserPO
    {
        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 20 characters.")]
        public string Username { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Password must be between 5 and 20 characters.")]
        public string Password { get; set; }
    }
}