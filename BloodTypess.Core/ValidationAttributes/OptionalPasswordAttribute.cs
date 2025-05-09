using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.Core.ValidationAttributes
{
	public class OptionalPasswordAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var password = value as string;

			// Allow empty or null: skip validation
			if (string.IsNullOrWhiteSpace(password))
				return ValidationResult.Success;

			// If provided, enforce complexity rules
			var regex = new System.Text.RegularExpressions.Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d]).+$");
			if (!regex.IsMatch(password))
			{
				return new ValidationResult("Password must have at least one lowercase, one uppercase, one digit, and one special character.");
			}

			return ValidationResult.Success;
		}
	}
	
}
