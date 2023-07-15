using Microsoft.EntityFrameworkCore;
using PracticeWebApi.Data;
using PracticeWebApi.Model.Domain;

namespace PracticeWebApi.Repository
{
	public class WalkRepository : IWalkRepository
	{
		private readonly WalksDbContext _dbContext;
		public WalkRepository(WalksDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Walk> CreateAsync(Walk walk)
		{
			await _dbContext.Walks.AddAsync(walk);
			await _dbContext.SaveChangesAsync();

			return walk;
		}

		public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
			string? sortBy = null, bool isAscending = true,
			int pageNumber = 1, int pageSize = 1000)
		{
			var walks = _dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
			//filtering
			if(string.IsNullOrWhiteSpace(filterOn)== false && string.IsNullOrWhiteSpace(filterQuery) == false) 
			{
				if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
				{
					walks = walks.Where(x => x.Name.Contains(filterQuery));
				}
			}
			//Sorting
			if(string.IsNullOrWhiteSpace(sortBy) == false)
			{
				if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
				{
					walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
				}
				if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
				{
					walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
				}
			}
			// pagination
			var skipResult = (pageNumber - 1) * pageSize;

			//var walks = await _dbContext.Walks.Include("Difficulties").Include("Region").ToListAsync();
			return await walks.Skip(skipResult).Take(pageSize).ToListAsync();
		}

		public async Task<Walk?> GetByIdAsync(Guid id)
		{
			var walk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

			return walk;
		}

		public async Task<Walk> UpdateWalkAsync(Guid id, Walk walk)
		{
			var walkFromDb = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

			walkFromDb.Difficulty = walk.Difficulty;
			walkFromDb.DifficultyId = walk.DifficultyId;
			walkFromDb.WalkImageUrl = walk.WalkImageUrl;
			walkFromDb.LengthInKm = walk.LengthInKm;
			walkFromDb.RegionId = walk.RegionId;
			walkFromDb.Description =walk.Description;

			await _dbContext.SaveChangesAsync();

			return walkFromDb;

		}

		public async Task<Walk?> DeleteAsync(Guid id)
		{
			var walk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
			
			_dbContext.Walks.Remove(walk);
			await _dbContext.SaveChangesAsync();
			return walk;
		}


	}
}
