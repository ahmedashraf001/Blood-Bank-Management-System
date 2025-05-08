using BloodTypess.Core.Models;
using BloodTypess.DataAccess.DataContext;
using BloodTypess.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
		private readonly ILogger<DonorRepository> _logger;

		public DonorRepository(ApplicationDbContext context, ILogger<DonorRepository> Logger)
		{
			_context = context;
			_logger = Logger;
		}

		public async Task<IEnumerable<Donor>> GetAllAsync()
		{
			return await _context.Donors
				.AsNoTracking()
 				.ToListAsync();
		}

		public async Task<Donor> GetByIdAsync(int id)
		{
			return await _context.Donors
				.AsNoTracking()
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

		public async Task UpdateAgeAsync()
		{
			// using raw sql query to update the age of donors 
			// only Age Column affected , not the entire table
			// Efficeient , Scalable with  very large amount of data
			try
			{
				await _context.Database.ExecuteSqlRawAsync(@"
				UPDATE Donors
				SET Age = DATEDIFF(YEAR, DateOfBirth, GETDATE()) 
					      - CASE 
							WHEN DATEADD(YEAR, DATEDIFF(YEAR, DateOfBirth, GETDATE()), DateOfBirth) > GETDATE() 
							THEN 1 
							ELSE 0 
						   END
			     ");
			}
			catch (Exception ex)
			{
 				_logger.LogError(ex, "Failed to update donor ages.");
			}
		}

		public async Task UpdateIsEiligibleToDonateAsync()
		{
			try
			{
				await _context.Database.ExecuteSqlRawAsync(@"
						UPDATE Donors
						SET IsEligibleToDonate = 
							CASE 
								WHEN DATEDIFF(DAY, LastDonationDate, GETDATE()) >= 90 THEN 1 
								ELSE 0 
							END
			     ");
			}
			catch (Exception ex)
			{
 				_logger.LogError(ex, "Failed to update donor IsEligibleToDonate COLUMN");
			}
		}
	}
}

