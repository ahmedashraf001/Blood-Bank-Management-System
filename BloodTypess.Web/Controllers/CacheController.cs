using BloodTypess.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodTypess.Web.Controllers
{
	[Authorize(Roles = "Admin")]  
	public class CacheController : Controller
	{
		private readonly IBloodTypeStockService _bloodTypeStockService;
		private readonly IDonorService _donorService;
		private readonly IReportService _reportService;

		public CacheController(
			IBloodTypeStockService bloodTypeStockService,
			IDonorService donorService,
			IReportService reportService)
		{
			_bloodTypeStockService = bloodTypeStockService;
			_donorService = donorService;
			_reportService = reportService;
		}

		public IActionResult Index()
		{
			return View("~/Views/shared/Cache.cshtml");
		}

		[HttpPost]
		public async Task<IActionResult> PurgeCache(string cacheType)
		{
			switch (cacheType)
			{
				case "all":
					_bloodTypeStockService.PurgeBloodTypeStockCache();
					await _donorService.PurgeDonorsCache();
					_reportService.PurgeReportCache();
					TempData["CacheMessage"] = "All caches purged successfully.";
					break;
				case "bloodTypeStock":
					_bloodTypeStockService.PurgeBloodTypeStockCache();
					TempData["CacheMessage"] = "Blood type stock cache purged successfully.";
					break;
				case "donors":
					await _donorService.PurgeDonorsCache();
					TempData["CacheMessage"] = "Donors cache purged successfully.";
					break;
				case "reports":
					_reportService.PurgeReportCache();
					TempData["CacheMessage"] = "Reports cache purged successfully.";
					break;
				default:
					TempData["CacheMessage"] = "Invalid cache type selected.";
					break;
			}

			return RedirectToAction("Index");
		}
	}
}
