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
	public class BloodTypeRepository : IBloodTypeRepository
	{
		private ApplicationDbContext _dbContext;

		public BloodTypeRepository(ApplicationDbContext dbContext )
		{
			_dbContext = dbContext;
		}
		public async Task<IEnumerable<BloodType>> GetAllAsync()
		{
			return await _dbContext.BloodTypes.ToListAsync();
		}
		public async Task<BloodType> GetByIdAsync(int id)
		{
			return await _dbContext.BloodTypes.FindAsync(id);
		}
	}
}
