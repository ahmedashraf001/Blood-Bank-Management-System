using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.Core.ValidationAttributes
{
	internal class NotInFutureDateAttribute : ValidationAttribute
	{
		public override bool IsValid(object value)
		{

			if (value is DateTime date)
			{
				return date <= DateTime.Today;
			}
			return true;
		}


	}
}