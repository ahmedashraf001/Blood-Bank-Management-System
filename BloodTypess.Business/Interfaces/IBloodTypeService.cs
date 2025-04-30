using BloodTypess.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.Business.Interfaces
{
	public interface IBloodTypeService 
	{
		public Task<BloodTypeInfoDTO> GetBloodTypeInfo(string bloodType);

		public bool IsValidBloodType(string bloodType);
	}
}
