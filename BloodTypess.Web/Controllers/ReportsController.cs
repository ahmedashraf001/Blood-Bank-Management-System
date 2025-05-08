using BloodTypess.Business.Interfaces;
using BloodTypess.Core.DTOs.ReportDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodTypess.Web.Controllers
{
	[Authorize(Roles = "Admin,Employee")]
	public class ReportsController :Controller
	{
		private readonly IReportService _reportService;

		public ReportsController(IReportService reportService)
		{
			_reportService = reportService;
		}

		// GET: /BloodBankReports/
		public IActionResult Index()
		{
			return View();
		}

		// GET: /BloodBankReports/BloodStock
		public async Task<IActionResult> BloodStock()
		{
			var model = await _reportService.GetBloodStockReportAsync();
			return View(model);
		}

		// GET: /BloodBankReports/EligibleDonors
		public async Task<IActionResult> EligibleDonors()
		{
			var model = await _reportService.GetEligibleDonorsByTypeAsync();
			return View(model);
		}

		// GET: /BloodBankReports/AgeGroups
		public async Task<IActionResult> AgeGroups()
		{
			var model = await _reportService.GetDonorAgeGroupsAsync();
			return View(model);
		}

		// GET: /BloodBankReports/RiskyBloodTypes
		public async Task<IActionResult> RiskyBloodTypes()
		{
			var model = await _reportService.GetRiskyBloodTypesAsync();
			return View(model);
		}

		// GET: /BloodBankReports/ZeroStockTypes
		public async Task<IActionResult> ZeroStockTypes()
		{
			var model = await _reportService.GetZeroStockBloodTypesAsync();
			return View(model);
		}

		// GET: /BloodBankReports/Dashboard
		public async Task<IActionResult> Dashboard()
		{
			var viewModel = new DashboardViewDto
			{
				BloodStock = await _reportService.GetBloodStockReportAsync(),
				EligibleDonors = await _reportService.GetEligibleDonorsByTypeAsync(),
				AgeGroups = await _reportService.GetDonorAgeGroupsAsync(),
				RiskyBloodTypes = await _reportService.GetRiskyBloodTypesAsync(),
				ZeroStockTypes = await _reportService.GetZeroStockBloodTypesAsync()
			};

			return View(viewModel);
		}
	}
}
