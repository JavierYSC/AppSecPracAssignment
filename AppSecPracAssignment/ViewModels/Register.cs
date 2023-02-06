using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace AppSecAssignment.ViewModels
{
    public class Register
    {

        [Required, Display(Name = "Full Name"), DataType(DataType.Text), MinLength(2, ErrorMessage = "Enter at least 2 characters."), MaxLength(100)]

        public string Name { get; set; }
        [Required, Display(Name = "Credit Card Number"), DataType(DataType.Text), RegularExpression(@"[0-9 ]+", ErrorMessage = "Only numbers allowed"), MaxLength(19)]
        public string CreditCard { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required, Display(Name = "Mobile Number"), DataType(DataType.PhoneNumber), MinLength(8, ErrorMessage = "Minimum 8 digits"), MaxLength(15), RegularExpression(@"[0-9 ]+", ErrorMessage = "Only numbers allowed")]
        public string MobileNumber { get; set;}
        [Required, DataType(DataType.MultilineText), RegularExpression(@"[a-zA-Z]{2,}", ErrorMessage = "Invalid Country")]
        public string Country { get; set; }
        [Required]
        public string Street { get; set; }
        [Required, Display(Name = "Postal Code"), DataType(DataType.PostalCode), RegularExpression(@"[0-9]{6}", ErrorMessage = "Invalid Postal Code"), MaxLength(6)]
        public string PostalCode { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; } 
        [Required, DataType(DataType.Password),RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{12,}$", ErrorMessage = "Password requires at least 12 character, 1 upper-case character, 1 lower-case character, 1 number and 1 special character")]  
        public string Password { get; set; }
        [Required, Display(Name = "Compare Password"), DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]    
        public string ConfirmPassword { get; set; }
        [Required, Display(Name = "About Me"), DataType(DataType.MultilineText)]
        public string AboutMe { get; set; }
    }

}
