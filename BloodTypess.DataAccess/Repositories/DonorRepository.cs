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
	public class DonorRepository : IDonorRepository
	{
		private readonly ApplicationDbContext _context;

		public DonorRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Donor>> GetAllAsync()
		{
			return await _context.Donors
				.Include(d => d.bloodType) // Eager load the related BloodType entity data(if we needed) 
				.ToListAsync();
		}

		public async Task<Donor> GetByIdAsync(int id)
		{
			return await _context.Donors
				.Include(d => d.bloodType)
				.FirstOrDefaultAsync(d => d.Id == id);
		}

		public async Task<Donor> AddAsync(Donor donor)
		{
			donor.CreatedDate = DateTime.Now;
			_context.Donors.Add(donor);
			await _context.SaveChangesAsync();
			return donor;
		}

		public async Task<Donor> UpdateAsync(Donor donor)
		{
			donor.UpdatedDate = DateTime.Now;
			_context.Entry(donor).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return donor;
		}

		public async Task DeleteAsync(int id)
		{
			var donor = await _context.Donors.FindAsync(id);
			if (donor != null)
			{
				_context.Donors.Remove(donor);
				await _context.SaveChangesAsync();
			}
		}
	}
}

