using BloodTypess.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.DataAccess.Interfaces
{
	public interface IBloodTypeApiService
	{
		Task<BloodTypeInfoDTO> GetBloodTypeInfoAsync(string bloodType, CancellationToken cancellationToken);
 	}
}
 