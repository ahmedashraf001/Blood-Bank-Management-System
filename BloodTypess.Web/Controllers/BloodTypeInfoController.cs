using BloodTypess.Business.Services;
using Microsoft.AspNetCore.Mvc;
using BloodTypess.Business.Interfaces;
using BloodTypess.DataAccess.Services;
using System.Threading;

namespace BloodTypess.Web.Controllers
{
	public class BloodTypeInfoController : Controller
	{
		private IBloodTypeService _service;
 		public BloodTypeInfoController(IBloodTypeService service)
		{
			_service = service;
		}
		[HttpGet]
		public async Task<IActionResult> BloodInfo( string BloodType, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(BloodType))
			{
				ViewData["ErrorMessage"] = "Blood type is required.";
				return View();
			}

			//validation for the input BloodType
			if (_service.IsValidBloodType(BloodType))
			{
				ViewData["ErrorMessage"] = "Invalid blood type. Please enter a valid blood type (e.g., A+, O-).";
				return View();
			}
			 
			var BloodTypeInfo = await _service.GetBloodTypeInfo(BloodType,cancellationToken);
			return View(BloodTypeInfo);
		}
	}
}
