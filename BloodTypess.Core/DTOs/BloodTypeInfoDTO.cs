using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.Core.DTOs
{
	public class BloodTypeInfoDTO
	{
		 
		public string BloodType { get; set; } = default!;
		public List<string> CanDonateTo { get; set; } = default!;
		public List<string> CanReceiveFrom { get; set; } = default!;
		public string PrevalenceGlobal { get; set; } = default!;
		public string Id { get; set; } = default!;
	}
}
