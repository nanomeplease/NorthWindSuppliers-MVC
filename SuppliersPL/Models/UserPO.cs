namespace SuppliersPL.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserPO
    {
        //[Required]
        public int UserId { get; set; }
        //[Required]
        public string Username { get; set; }
        //[Required]
        public string Password { get; set; }  
        //[Required]
        public string FirstName { get; set; }
        //[Required]
        public string LastName { get; set; }
        

        public string Email { get; set; }
        public int UserRole { get; set; }
    }

}