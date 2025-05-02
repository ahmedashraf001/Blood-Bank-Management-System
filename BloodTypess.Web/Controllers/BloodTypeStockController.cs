using BloodTypess.Business.Interfaces;
using BloodTypess.Business.Services;
using BloodTypess.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BloodTypess.Web.Controllers
{
	public class BloodTypeStockController : Controller
	{
		private IBloodTypeStockService _bloodTypeStockService;
		private IBloodTypeService _bloodTypeService;

		public BloodTypeStockController(IBloodTypeStockService bloodTypeStockService,
			IBloodTypeService bloodTypeService)
		{
			_bloodTypeStockService = bloodTypeStockService;
			_bloodTypeService = bloodTypeService;
		}

		// GET: BloodTypeStock
		public async Task<IActionResult> Index()
		{
			var bloodTypes = await _bloodTypeStockService.GetAllBloodTypesAsync();
			return View("~/Views/BloodTypeSystem/view_bloodtype_stock.cshtml", bloodTypes);
		}

		// GET: BloodTypeStock/Create
		public async Task<IActionResult> Create()
		{
			var TheFixedBloodTypes = await _bloodTypeService.GetAllBloodTypesAsync();
			ViewBag.BloodTypes = TheFixedBloodTypes;
			return View("~/Views/BloodTypeSystem/add_bloodtype_stock.cshtml");
		}

		// POST: BloodTypeStock/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(BloodTypeStockDto bloodTypeStockDto)
		{
			// Check if the blood type already exists
			if(!_bloodTypeStockService.IsBloodTypeExist(bloodTypeStockDto))
			{
				ModelState.AddModelError("BloodTypeId", "This blood type already exists in the stock, edit the stock to Add the new Units ");
			}
			if (ModelState.IsValid)
			{
				bloodTypeStockDto.LastUpdated = DateTime.Now;
				await _bloodTypeStockService.AddBloodTypeAsync(bloodTypeStockDto);
				return RedirectToAction(nameof(Index));
			}
			var TheFixedBloodTypes = await _bloodTypeService.GetAllBloodTypesAsync();
			ViewBag.BloodTypes = TheFixedBloodTypes;

			return View("~/Views/BloodTypeSystem/add_bloodtype_stock.cshtml", bloodTypeStockDto);
		}

		// GET: BloodTypeStock/Edit/5
		public async Task<IActionResult> Edit(int id)
		{
			var bloodType = await _bloodTypeStockService.GetBloodTypeByIdAsync(id);
			if (bloodType == null)
			{
				return NotFound();
			}

			var TheFixedBloodTypes = await _bloodTypeService.GetAllBloodTypesAsync();
			ViewBag.BloodTypes = TheFixedBloodTypes;

			return View("~/Views/BloodTypeSystem/add_bloodtype_stock.cshtml", bloodType);
		}

		// POST: BloodTypeStock/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, BloodTypeStockDto bloodTypeDto)
		{
			if (id != bloodTypeDto.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				bloodTypeDto.LastUpdated = DateTime.Now;
				await _bloodTypeStockService.UpdateBloodTypeAsync(bloodTypeDto);
				return RedirectToAction(nameof(Index));
			}

			var TheFixedBloodTypes = await _bloodTypeService.GetAllBloodTypesAsync();
			ViewBag.BloodTypes = TheFixedBloodTypes;

			return View("~/Views/BloodTypeSystem/add_bloodtype_stock.cshtml", bloodTypeDto);
		}

		// POST: BloodTypeStock/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _bloodTypeStockService.DeleteBloodTypeAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
