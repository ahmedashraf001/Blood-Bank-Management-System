using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.Core.ValidationAttributes
{
	public class AllowedAgeAttribute : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			if (value is DateTime dateOfBirth)
			{
				var age = DateTime.Now.Year - dateOfBirth.Year;
				if (dateOfBirth.Date > DateTime.Today.AddYears(-age)) age--;
				return age >= 18;
			}
			return false;
		}


	}
}
