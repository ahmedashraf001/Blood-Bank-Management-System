using BloodTypess.Business.Interfaces;
using BloodTypess.Business.Services;
using BloodTypess.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BloodTypess.Web.Controllers
{
	public class DonorController : Controller
	{
		private readonly IDonorService _donorService;
		private readonly IBloodTypeStockService _bloodTypeStockService;
		private readonly IBloodTypeService _bloodTypeService;
		private readonly ILogger<DonorController> _logger;

		public DonorController(IDonorService donorService,
			IBloodTypeStockService bloodTypeStockService ,
			IBloodTypeService bloodTypeService,
			ILogger<DonorController> logger)
		{
			_donorService = donorService;
			_bloodTypeStockService = bloodTypeStockService;
			_bloodTypeService = bloodTypeService;
			_logger = logger;
		}

		 

		// GET: Donor
		[Authorize] // Anyone Logged in
		public async Task<IActionResult> Index()
		{
			var sw = Stopwatch.StartNew();
			var donorsDTO = await _donorService.GetAllDonorsAsync();
			sw.Stop();
 
			 _logger.LogInformation("GetAllDonorsAsync took {ElapsedMs} ms", sw.ElapsedMilliseconds);
 			return View("~/Views/BloodTypeSystem/view_donors.cshtml", donorsDTO);
		}


		// GET: Donor/Create
		[Authorize(Roles = "Admin,Employee")]
		public async Task<IActionResult> Create()
		{
			var TheFixedBloodTypes = await _bloodTypeService.GetAllBloodTypesAsync();
			ViewBag.BloodTypes = TheFixedBloodTypes;
			return View("~/Views/BloodTypeSystem/add_donor.cshtml");
		}

		// POST: Donor/Create
		[HttpPost]
		[ValidateAntiForgeryToken]

		[Authorize(Roles = "Admin,Employee")]
		public async Task<IActionResult> Create(DonorDto donorDto)
		{
			if (ModelState.IsValid)
			{
				await _donorService.AddDonorAsync(donorDto);
				return RedirectToAction(nameof(Index));
			}
			var TheFixedBloodTypes = await _bloodTypeService.GetAllBloodTypesAsync();
			ViewBag.BloodTypes = TheFixedBloodTypes;

			return View("~/Views/BloodTypeSystem/add_donor.cshtml", donorDto);
		}

		// GET: Donor/Edit/5
		[Authorize(Roles = "Admin,Employee")]
		public async Task<IActionResult> Edit(int id)
		{
			var donor = await _donorService.GetDonorByIdAsync(id);
			if (donor == null)
			{
				return NotFound();
			}
			var TheFixedBloodTypes = await _bloodTypeService.GetAllBloodTypesAsync();
			ViewBag.BloodTypes = TheFixedBloodTypes;
			return View("~/Views/BloodTypeSystem/add_donor.cshtml", donor);
		}

		// POST: Donor/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin,Employee")]
		public async Task<IActionResult> Edit(int id, DonorDto donorDto)
		{
			if (id != donorDto.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				await _donorService.UpdateDonorAsync(donorDto);
				return RedirectToAction(nameof(Index));
			}
			var TheFixedBloodTypes = await _bloodTypeService.GetAllBloodTypesAsync();
			ViewBag.BloodTypes = TheFixedBloodTypes; 
			return View("~/Views/BloodTypeSystem/add_donor.cshtml", donorDto);
		}

		[Authorize(Roles = "Admin,Employee")]
		// POST: Donor/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _donorService.DeleteDonorAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
