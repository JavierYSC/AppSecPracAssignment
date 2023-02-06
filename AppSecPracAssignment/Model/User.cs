using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppSecAssignment.Model
{
	public class AppUser : IdentityUser
	{
		[Display(Name = "Full Name")]
		public string FName { get; set; } = string.Empty;
		[NotMapped]
		public string Password { get; set; } = string.Empty;
		[NotMapped]
		public string ConfirmPassword { get; set; } = string.Empty;
		[Display(Name = "Credit Card Number")]
		public string CreditCard { get; set; } = string.Empty;
		[MaxLength(1)]
		public string Gender { get; set; } = string.Empty;
		[Display(Name = "Mobile Number")]
		public string MobileNo { get; set; } = string.Empty;
		public string Country { get; set; } = string.Empty;
		public string Street { get; set; } = string.Empty;
		[Display(Name = "Postal Code")]
		public string PostalCode { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;

		[MaxLength(50)]
		public string? Photo { get; set; }
		public string AboutMe { get; set; } = string.Empty;
	}
}
