using BloodTypess.Core.Models;
using BloodTypess.DataAccess.DataContext;
using BloodTypess.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.DataAccess.Repositories
{
	public class BloodTypeStockRepository : IBloodTypeStockRepository
	{
		private readonly ApplicationDbContext _context;

		public BloodTypeStockRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<BloodTypeStock>> GetAllAsync()
		{
			return await _context.BloodTypesStock.AsNoTracking().ToListAsync();
		}

		public async Task<BloodTypeStock> GetByIdAsync(int id)
		{
			return await _context.BloodTypesStock.FindAsync(id);
		}

		public async Task<BloodTypeStock> AddAsync(BloodTypeStock bloodType)
		{
			bloodType.LastUpdated = DateTime.Now;
			_context.BloodTypesStock.Add(bloodType);
			await _context.SaveChangesAsync();
			return bloodType;
		}

		public async Task<BloodTypeStock> UpdateAsync(BloodTypeStock bloodType)
		{
			bloodType.LastUpdated = DateTime.Now;
			_context.Entry(bloodType).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return bloodType;
		}

		public async Task DeleteAsync(int id)
		{
			var bloodType = await _context.BloodTypesStock.FindAsync(id);
			if (bloodType != null)
			{
				_context.BloodTypesStock.Remove(bloodType);
				await _context.SaveChangesAsync();
			}
		}
	}
}
