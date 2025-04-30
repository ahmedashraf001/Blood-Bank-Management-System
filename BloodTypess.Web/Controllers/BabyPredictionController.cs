using BloodTypess.Business.Interfaces;
using BloodTypess.Business.Services;
using BloodTypess.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BloodTypess.Web.Controllers
{
	public class BabyPredictionController : Controller
	{
		public async Task<IActionResult> PredictBabyBloodType()
		{
 			return View();
		}
	}
}
